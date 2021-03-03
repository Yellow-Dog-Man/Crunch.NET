using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crunch.NET
{
    public enum crn_file_type : int
    {
        FileTypeCRN = 0,
        FileTypeDDS,
    }

    public enum crn_format : int
    {
        Invalid = -1,

        DXT1 = 0,
        FirstValid = DXT1,

        DXT3,
        DXT5,

        DXT5_CCxY, // Luma-chroma
        DXT5_xGxR, // Swizzled 2-component
        DXT5_xGBR, // Swizzled 3-component
        DXT5_AGBR, // Swizzled 4-component

        // ATI 3DC and X360 DXN
        DXN_XY,
        DXN_YX,

        // DXT5 alpha blocks only
        DXT5A,

        ETC1,
        ETC2,
        ETC2A,
        ETC1S,
        ETC2AS,
    }

    [Flags]
    public enum crn_comp_flags : int
    {
        // Enables perceptual colorspace distance metrics if set.
        // Important: Be sure to disable this when compressing non-sRGB colorspace images, like normal maps!
        // Default: Set
        Perceptual = 1,

        // Enables (up to) 8x8 macroblock usage if set. If disabled, only 4x4 blocks are allowed.
        // Compression ratio will be lower when disabled, but may cut down on blocky artifacts because the process used to determine
        // where large macroblocks can be used without artifacts isn't perfect.
        // Default: Set.
        Hierarchical = 2,

        // cCRNCompFlagQuick disables several output file optimizations - intended for things like quicker previews.
        // Default: Not set.
        FlagQuick = 4,

        // DXT1: OK to use DXT1 alpha blocks for better quality or DXT1A transparency.
        // DXT5: OK to use both DXT5 block types.
        // Currently only used when writing to .DDS files, as .CRN uses only a subset of the possible DXTn block types.
        // Default: Set.
        UseBothBlockTypes = 8,

        // OK to use DXT1A transparent indices to encode black (assumes pixel shader ignores fetched alpha).
        // Currently only used when writing to .DDS files, .CRN never uses alpha blocks.
        // Default: Not set.
        UseTransparentIndicesForBlack = 16,

        // Disables endpoint caching, for more deterministic output.
        // Currently only used when writing to .DDS files.
        // Default: Not set.
        DisableEndpointCaching = 32,

        // If enabled, use the cCRNColorEndpointPaletteSize, etc. params to control the CRN palette sizes. Only useful when writing to .CRN files.
        // Default: Not set.
        ManualPaletteSizes = 64,

        // If enabled, DXT1A alpha blocks are used to encode single bit transparency.
        // Default: Not set.
        DXT1AForTransparency = 128,

        // If enabled, the DXT1 compressor's color distance metric assumes the pixel shader will be converting the fetched RGB results to luma (Y part of YCbCr).
        // This increases quality when compressing grayscale images, because the compressor can spread the luma error amoung all three channels (i.e. it can generate blocks
        // with some chroma present if doing so will ultimately lead to lower luma error).
        // Only enable on grayscale source images.
        // Default: Not set.
        GrayscaleSampling = 256,

        // If enabled, debug information will be output during compression.
        // Default: Not set.
        Debugging = unchecked((int)0x80000000),
    }

    public enum crn_dxt_quality : int
    {
        QualitySuperFast,
        QualityFast,
        QualityNormal,
        QualityBetter,
        QualityUber
    };

    public enum crn_dxt_compressor_type : int
    {
        CompressorCRN,   // Use crnlib's ETC1 or DXTc block compressor (default, highest quality, comparable or better than ati_compress or squish, and crnlib's ETC1 is a lot fasterw with similiar quality to Erricson's)
        CompressorCRNF,  // Use crnlib's "fast" DXTc block compressor
        CompressorRYG,   // Use RYG's DXTc block compressor (low quality, but very fast)

        CompressorATI,

        CompressorSquish
    };

    public enum crn_mip_mode : int
    {
        UseSourceOrGenerateMips,  // Use source texture's mipmaps if it has any, otherwise generate new mipmaps
        UseSourceMips,            // Use source texture's mipmaps if it has any, otherwise the output has no mipmaps
        GenerateMips,             // Always generate new mipmaps
        NoMips,
    }

    public enum crn_mip_filter : int
    {
        Box,
        Tent,
        Lanczos4,
        Mitchell,
        Kaiser,  // Kaiser=default mipmap filter
    }

    public enum crn_scale_mode : int
    {
        Disabled,
        Absolute,
        Relative,
        LowerPow2,
        NearestPow2,
        NextPow2,
    };
}
