using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace crunch.NET
{
    [StructLayout(LayoutKind.Sequential)]
    public class crn_mipmap_params
    {
        [MarshalAs(UnmanagedType.U4)]
        public uint size_of_obj;

        [MarshalAs(UnmanagedType.U4)]
        public crn_mip_mode mode;
        [MarshalAs(UnmanagedType.U4)]
        public crn_mip_filter filter;

        [MarshalAs(UnmanagedType.Bool)]
        public bool gamma_filtering;
        [MarshalAs(UnmanagedType.R4)]
        public float gamma;

        [MarshalAs(UnmanagedType.R4)]
        public float blurriness;

        [MarshalAs(UnmanagedType.U4)]
        public uint max_levels;
        [MarshalAs(UnmanagedType.U4)]
        public uint min_mip_size;

        [MarshalAs(UnmanagedType.Bool)]
        public bool renormalize;
        [MarshalAs(UnmanagedType.Bool)]
        public bool tiled;

        [MarshalAs(UnmanagedType.U4)]
        public crn_scale_mode scale_mode;
        [MarshalAs(UnmanagedType.R4)]
        public float scale_x;
        [MarshalAs(UnmanagedType.R4)]
        public float scale_y;

        [MarshalAs(UnmanagedType.U4)]
        public uint window_left;
        [MarshalAs(UnmanagedType.U4)]
        public uint window_top;
        [MarshalAs(UnmanagedType.U4)]
        public uint window_right;
        [MarshalAs(UnmanagedType.U4)]
        public uint window_bottom;

        [MarshalAs(UnmanagedType.Bool)]
        public bool clamp_scale;
        [MarshalAs(UnmanagedType.U4)]
        public uint clamp_width;
        [MarshalAs(UnmanagedType.U4)]
        public uint clamp_height;

        public void Clear()
        {
            size_of_obj = (uint)Marshal.SizeOf<crn_mipmap_params>();

            mode = crn_mip_mode.UseSourceOrGenerateMips;
            filter = crn_mip_filter.Kaiser;

            gamma_filtering = true;
            gamma = 2.2f;

            blurriness = 0.9f;
            renormalize = false;
            tiled = false;
            max_levels = Constants.MAX_LEVELS;
            min_mip_size = 1;

            scale_mode = crn_scale_mode.Disabled;
            scale_x = 1f;
            scale_y = 1f;

            window_left = 0;
            window_top = 0;
            window_right = 0;
            window_bottom = 0;

            clamp_scale = false;
            clamp_width = 0;
            clamp_height = 0;
        }

        public crn_mipmap_params()
        {
            Clear();
        }
    }
}
