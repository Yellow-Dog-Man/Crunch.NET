using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace crunch.NET
{
    public static class NativeMethods
    {
        const string LIBRARY_NAME = "crnlib";

        [DllImport(LIBRARY_NAME)]
        public static extern void disable_console_output();

        [DllImport(LIBRARY_NAME)]
        public static extern void enable_console_output();

        [DllImport(LIBRARY_NAME)]
        public static extern IntPtr crn_compress(crn_comp_params comp_params,
            out uint compressed_size, out uint actual_quality_level, out float actual_bitrate);

        [DllImport(LIBRARY_NAME)]
        public static extern IntPtr crn_compress_mip(crn_comp_params comp_params, crn_mipmap_params mip_params,
            out uint compressed_size, out uint actual_quality_level, out float actual_bitrate);

        [DllImport(LIBRARY_NAME)]
        public static extern IntPtr crn_free_block(IntPtr block);

        [DllImport(LIBRARY_NAME)]
        public static extern bool crnd_get_texture_info(IntPtr data, uint data_size, [In, Out] crn_texture_info info);

        [DllImport(LIBRARY_NAME)]
        public static extern IntPtr crnd_unpack_begin(IntPtr data, uint data_size);

        [DllImport(LIBRARY_NAME)]
        public static extern bool crnd_unpack_level(IntPtr context,
            IntPtr destPointers, uint dest_disze_in_bytes, uint row_pitch_in_bytes,
            uint level_index);

        [DllImport(LIBRARY_NAME)]
        public static extern bool crnd_unpack_end(IntPtr context);
    }
}
