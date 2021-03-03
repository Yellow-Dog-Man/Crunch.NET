using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace crunch.NET
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class crn_comp_params
    {
        [MarshalAs(UnmanagedType.U4)]
        public uint size_of_obj;

        [MarshalAs(UnmanagedType.U4)]
        public crn_file_type file_type;

        [MarshalAs(UnmanagedType.U4)]
        public uint faces;
        [MarshalAs(UnmanagedType.U4)]
        public uint width;
        [MarshalAs(UnmanagedType.U4)]
        public uint height;
        [MarshalAs(UnmanagedType.U4)]
        public uint levels;

        [MarshalAs(UnmanagedType.U4)]
        public crn_format format;

        [MarshalAs(UnmanagedType.U4)]
        public crn_comp_flags flags;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.MAX_FACES * Constants.MAX_LEVELS)]
        public IntPtr[,] images;

        [MarshalAs(UnmanagedType.R4)]
        public float target_bitrate;

        [MarshalAs(UnmanagedType.U4)]
        public uint quality_level;

        [MarshalAs(UnmanagedType.U4)]
        public uint dxt1_alpha_threshold;

        [MarshalAs(UnmanagedType.U4)]
        public crn_dxt_quality dxt_quality;

        [MarshalAs(UnmanagedType.U4)]
        public crn_dxt_compressor_type dxt_compressor_type;

        [MarshalAs(UnmanagedType.U4)]
        public uint alpha_component;

        [MarshalAs(UnmanagedType.R4)]
        public float crn_adaptive_tile_color_psnr_derating;
        [MarshalAs(UnmanagedType.R4)]
        public float crn_adaptive_tile_alpha_psnr_derating;

        [MarshalAs(UnmanagedType.U4)]
        public uint crn_color_endpoint_palette_size;
        [MarshalAs(UnmanagedType.U4)]
        public uint crn_color_selector_palette_size;

        [MarshalAs(UnmanagedType.U4)]
        public uint crn_alpha_endpoint_palette_size;
        [MarshalAs(UnmanagedType.U4)]
        public uint crn_alpha_selector_palette_size;

        [MarshalAs(UnmanagedType.U4)]
        public uint num_helper_threads;

        [MarshalAs(UnmanagedType.U4)]
        public uint userdata0;
        [MarshalAs(UnmanagedType.U4)]
        public uint userdata1;

        public IntPtr progress_func;
        public IntPtr progress_func_data;
    }
}
