using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Buffers;

namespace crunch.NET
{
    public static class Crunch
    {
        public static unsafe byte[] Compress(int width, int height, List<List<Memory<byte>>> data, 
            crn_format format,
            crn_mipmap_params mipmaps = null, int maxThreads = -1)
        {
            var comp_params = new crn_comp_params();

            comp_params.width = (uint)width;
            comp_params.height = (uint)height;
            comp_params.format = format;

            return Compress(data, comp_params, mipmaps);
        }

        public static unsafe byte[] Compress(List<List<Memory<byte>>> data, crn_comp_params comp_params, crn_mipmap_params mipmaps = null, int maxThreads = -1)
        {
            if (data.Count != 1 && data.Count != 6)
                throw new ArgumentOutOfRangeException("Invalid number of faces");

            var levels = data[0].Count;

            foreach (var face in data)
                if (face.Count != levels)
                    throw new ArgumentOutOfRangeException("Inconsistent number of levels between faces");

            if (comp_params.height <= 0 || comp_params.width <= 0)
                throw new ArgumentOutOfRangeException("Texture size");

            comp_params.faces = (uint)data.Count;
            comp_params.levels = (uint)levels;

            int threads = Environment.ProcessorCount - 1;

            if (maxThreads >= 0)
                threads = maxThreads;

            comp_params.num_helper_threads = (uint)Math.Min(Constants.MAX_HELPER_THREADS, threads);

            List<MemoryHandle> handles = new List<MemoryHandle>();

            try
            {
                for(int f = 0; f < data.Count; f++)
                    for(int m = 0; m < levels; m++)
                    {
                        var handle = data[f][m].Pin();
                        handles.Add(handle);

                        comp_params.images[f, m] = new IntPtr(handle.Pointer);
                    }

                IntPtr compressedPtr;
                uint compressed_size;
                uint actual_quality_level;
                float actual_bitrate;

                if (mipmaps != null)
                    compressedPtr = NativeMethods.crn_compress_mip(comp_params, mipmaps, out compressed_size, out actual_quality_level, out actual_bitrate);
                else
                    compressedPtr = NativeMethods.crn_compress(comp_params, out compressed_size, out actual_quality_level, out actual_bitrate);

                byte[] compressedData = null;

                if (compressedPtr != IntPtr.Zero)
                {
                    compressedData = new byte[compressed_size];

                    Marshal.Copy(compressedPtr, compressedData, 0, (int)compressed_size);

                    NativeMethods.crn_free_block(compressedPtr);
                }

                return compressedData;
            }
            finally
            {
                foreach (var handle in handles)
                    handle.Dispose();
            }
        }

        public static unsafe bool Decompress(byte[] rawData, List<List<Memory<byte>>> data) => Decompress(rawData, data, out _);

        public static unsafe bool Decompress(byte[] rawData, List<List<Memory<byte>>> data, out crn_texture_info info,
            Action<crn_texture_info> onInfoDecoded = null)
        {
            fixed(byte* pData = rawData)
            {
                info = new crn_texture_info();

                if (!NativeMethods.crnd_get_texture_info(new IntPtr(pData), (uint)rawData.Length, info))
                    return false;

                onInfoDecoded?.Invoke(info);

                bool allocateData = data.Count == 0;

                if (!allocateData)
                {
                    if (data.Count != info.faces)
                        return false;

                    foreach (var face in data)
                        if (face.Count != info.levels)
                            return false;
                }
                else
                {
                    for (int f = 0; f < info.faces; f++)
                        data.Add(new List<Memory<byte>>());
                }

                var pointers = new IntPtr[Constants.MAX_FACES];
                var handles = new List<MemoryHandle>();

                IntPtr context = IntPtr.Zero;
                var dataHandle = rawData.AsMemory().Pin();

                try
                {
                    context = NativeMethods.crnd_unpack_begin(new IntPtr(dataHandle.Pointer), (uint)rawData.Length); 

                    for (int m = 0; m < info.levels; m++)
                    {
                        uint width = Math.Max(1, info.width >> m);
                        uint height = Math.Max(1, info.height >> m);

                        uint blocks_x = Math.Max(1, (width + 3) >> 2);
                        uint blocks_y = Math.Max(1, (height + 3) >> 2);

                        uint row_pitch = blocks_x * info.bytes_per_block;
                        uint face_size = row_pitch * blocks_y;

                        try
                        {
                            for (int f = 0; f < info.faces; f++)
                            {
                                if(allocateData)
                                {
                                    var face_data = new byte[face_size];
                                    data[f].Add(face_data.AsMemory());
                                }

                                var handle = data[f][m].Pin();
                                handles.Add(handle);

                                pointers[f] = new IntPtr(handle.Pointer);
                            }

                            fixed (void* p = pointers)
                            {
                                var unpacked = NativeMethods.crnd_unpack_level(context, new IntPtr(p), face_size, row_pitch, (uint)m);

                                if (!unpacked)
                                    return false;
                            }
                        }
                        finally
                        {
                            foreach (var h in handles)
                                h.Dispose();

                            handles.Clear();
                        }
                    }
                }
                finally
                {
                    if (context != IntPtr.Zero)
                        NativeMethods.crnd_unpack_end(context);

                    dataHandle.Dispose();
                }

                return true;
            }
        }
    }
}
