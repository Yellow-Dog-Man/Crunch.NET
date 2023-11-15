using System.Runtime.InteropServices;

namespace crunch.NET
{
    [StructLayout(LayoutKind.Sequential)]
    public class crn_texture_info
    {
        [MarshalAs(UnmanagedType.U4)]
        public uint struct_size;

        [MarshalAs(UnmanagedType.U4)]
        public uint width;
        [MarshalAs(UnmanagedType.U4)]
        public uint height;
        [MarshalAs(UnmanagedType.U4)]
        public uint levels;
        [MarshalAs(UnmanagedType.U4)]
        public uint faces;

        [MarshalAs(UnmanagedType.U4)]
        public uint bytes_per_block;

        [MarshalAs(UnmanagedType.U4)]
        public uint userdata0;
        [MarshalAs(UnmanagedType.U4)]
        public uint userdata1;

        [MarshalAs(UnmanagedType.U4)]
        public crn_format format;

        public crn_texture_info()
        {
            struct_size = (uint)Marshal.SizeOf<crn_texture_info>();
        }
    }
}
