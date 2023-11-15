using System;
using System.Runtime.InteropServices;

namespace crunch.NET
{
    [StructLayout(LayoutKind.Sequential)]
    public class crn_comp_params
    {
        [MarshalAs(UnmanagedType.U4)]
        public uint size_of_obj;

        /// <summary>
        /// // Output file type: FileTypeCRN or FileTypeDDS.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public crn_file_type file_type;

        /// <summary>
        /// // 1 (2D map) or 6 (cubemap)
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public uint faces;
        /// <summary>
        /// // [1,cCRNMaxLevelResolution], non-power of 2 OK, non-square OK
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public uint width;
        /// <summary>
        /// // [1,cCRNMaxLevelResolution], non-power of 2 OK, non-square OK
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public uint height;
        /// <summary>
        /// // [1,cCRNMaxLevelResolution], non-power of 2 OK, non-square OK
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public uint levels;

        /// <summary>
        /// // Output pixel format.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public crn_format format;

        [MarshalAs(UnmanagedType.U4)]
        public crn_comp_flags flags;

        /// <summary>
        /// // Array of pointers to 32bpp input images.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.MAX_FACES * Constants.MAX_LEVELS)]
        public IntPtr[,] images;

        /// <summary>
        /// Target bitrate - if non-zero, the compressor will use an interpolative search to find the
        /// highest quality level that is <= the target bitrate. If it fails to find a bitrate high enough, it'll
        /// try disabling adaptive block sizes (cCRNCompFlagHierarchical flag) and redo the search. This process can be pretty slow.
        /// </summary>
        [MarshalAs(UnmanagedType.R4)]
        public float target_bitrate;

        /// <summary>
        /// Desired quality level.
        /// Currently, CRN and DDS quality levels are not compatible with eachother from an image quality standpoint.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public uint quality_level;

        [MarshalAs(UnmanagedType.U4)]
        public uint dxt1_alpha_threshold;

        [MarshalAs(UnmanagedType.U4)]
        public crn_dxt_quality dxt_quality;

        [MarshalAs(UnmanagedType.U4)]
        public crn_dxt_compressor_type dxt_compressor_type;

        /// <summary>
        /// Alpha channel's component. Defaults to 3.
        /// </summary>
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

        /// <summary>
        /// Number of helper threads to create during compression. 0=no threading.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public uint num_helper_threads;

        /// <summary>
        /// CRN userdata0 and userdata1 members, which are written directly to the header of the output file.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public uint userdata0;
        [MarshalAs(UnmanagedType.U4)]
        public uint userdata1;

        /// <summary>
        /// User provided progress callback.
        /// </summary>
        public IntPtr progress_func;
        public IntPtr progress_func_data;

        public void Clear()
        {
            size_of_obj = (uint)Marshal.SizeOf<crn_comp_params>();

            file_type = crn_file_type.FileTypeCRN;
            faces = 1;
            width = 0;
            height = 0;
            levels = 1;
            format = crn_format.DXT1;
            flags = crn_comp_flags.Perceptual | crn_comp_flags.UseBothBlockTypes;

            images = new IntPtr[Constants.MAX_FACES, Constants.MAX_LEVELS];

            target_bitrate = 0f;
            quality_level = Constants.MAX_QUALITY_LEVEL;
            dxt1_alpha_threshold = 128;
            dxt_quality = crn_dxt_quality.QualityUber;
            dxt_compressor_type = crn_dxt_compressor_type.CompressorCRN;
            alpha_component = 3;

            crn_adaptive_tile_color_psnr_derating = 2;
            crn_adaptive_tile_alpha_psnr_derating = 2;
            crn_color_endpoint_palette_size = 0;
            crn_color_selector_palette_size = 0;
            crn_alpha_endpoint_palette_size = 0;
            crn_alpha_selector_palette_size = 0;

            num_helper_threads = 0;
            userdata0 = 0;
            userdata1 = 0;

            progress_func = IntPtr.Zero;
            progress_func_data = IntPtr.Zero;
        }

        public crn_comp_params()
        {
            Clear();
        }
    }
}
