using System;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;


namespace getscreenshot
{
    class ScreenCapture
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, out Rect rect);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);


        private static int SW_RESTORE = 9;
        private static int SW_SHOWNA = 8;

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public static void BringWindowToFront(IntPtr hWnd)
        {
            // Restore window if it is minimized
            ShowWindow(hWnd, SW_RESTORE);
        

            // Bring the window to the front
            SetForegroundWindow(hWnd);
        }
        public static Image CaptureDesktop()
        {
            return CaptureWindow(GetDesktopWindow());
        }

        public static Bitmap CaptureActiveWindow()
        {
            return CaptureWindow(GetForegroundWindow());
        }

        public static Bitmap CaptureWindow(IntPtr handle)
        {
            BringWindowToFront(handle);

            Rect rect = new Rect();
            GetWindowRect(handle, out rect);
            


            Bitmap bmp = new Bitmap(rect.Right-rect.Left, rect.Bottom-rect.Top);

            Graphics g = Graphics.FromImage(bmp);

            g.CopyFromScreen(rect.Left, rect.Top, 0, 0, bmp.Size);


            return bmp;
        }

    }
}
