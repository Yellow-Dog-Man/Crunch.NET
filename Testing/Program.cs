using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using crunch.NET;
using System.IO;


namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new byte[512 * 512 * 4];

            var r = new Random();
            r.NextBytes(data);

            var memoryData = data.AsMemory();

            var mips = new crn_mipmap_params();

            var list = new List<List<Memory<byte>>>();
            list.Add(new List<Memory<byte>>());

            list[0].Add(data.AsMemory());

            var result = Crunch.Compress(512, 512, list, crn_format.DXT1, mips);

            var decompressedData = new List<List<Memory<byte>>>();

            Crunch.Decompress(result, decompressedData);

            var memoryStream = new MemoryStream();

            foreach(var d in decompressedData[0])
            {
                var a = d.ToArray();
                memoryStream.Write(a, 0, a.Length);
            }

            File.WriteAllBytes("Result.bin", memoryStream.ToArray());

            //var data = File.ReadAllBytes("test_wood.crn");

            //unsafe
            //{
            //    fixed(byte* p = data)
            //    {
            //        var info = new crn_texture_info();
            //        var result = NativeMethods.crnd_get_texture_info(new IntPtr(p), (uint)data.Length, info);

            //        var pointers = new IntPtr[Constants.MAX_FACES];
            //        var texData = new byte[(info.width * info.height) / 2];

            //        fixed(byte* tp = texData)
            //        {
            //            pointers[0] = new IntPtr(tp);

            //            var context = NativeMethods.crnd_unpack_begin(new IntPtr(p), (uint)data.Length);

            //            fixed (void* pp = pointers)
            //            {
            //                var unpacked = NativeMethods.crnd_unpack_level(context, new IntPtr(pp), (uint)texData.Length, info.width * 2, 0);
            //                Console.WriteLine("Unpacked: " + unpacked);
            //            }

            //            NativeMethods.crnd_unpack_end(new IntPtr(p));
            //        }

            //        File.WriteAllBytes("Tex.dxt1", texData);

            //        Console.WriteLine(result);
            //    }
            //}

            //var p = new crn_comp_params();

            //var imageData = new byte[16 * 16 * 4];

            //var r = new Random();

            //for(int i = 0; i < imageData.Length; i+= 4)
            //{
            //    imageData[i + 0] = (byte)r.Next();
            //    imageData[i + 1] = (byte)r.Next();
            //    imageData[i + 2] = (byte)r.Next();
            //    imageData[i + 3] = 0;
            //}

            //unsafe
            //{
            //    fixed (byte* data = imageData)
            //    {
            //        p.width = 16;
            //        p.height = 16;
            //        p.images[0, 0] = new IntPtr(data);

            //        float actual_bitrate = -1f;
            //        uint compressed_size = uint.MaxValue;
            //        uint actual_quality_level = uint.MaxValue;

            //        var result = NativeMethods.crn_compress(p, out compressed_size, out actual_quality_level, out actual_bitrate);

            //        var resultData = new byte[compressed_size];
            //        Marshal.Copy(result, resultData, 0, (int)compressed_size);

            //        File.WriteAllBytes("test.crn", resultData);

            //        Console.Read();
            //    }
            //}
        }
    }
}
