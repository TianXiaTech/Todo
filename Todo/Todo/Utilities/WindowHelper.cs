using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace Todo.Utilities
{
    public class WindowHelper
    {
        public static  void DisableMaxmizebox(Window window,bool isDisable)
        {
            int GWL_STYLE = -16;
            int WS_MAXIMIZEBOX = 0x00010000;
            int SWP_NOSIZE = 0x0001;
            int SWP_NOMOVE = 0x0002;
            int SWP_FRAMECHANGED = 0x0020;

            IntPtr handle = new WindowInteropHelper(window).Handle;

            int nStyle = WinAPI.GetWindowLong(handle, GWL_STYLE);
            if (isDisable)
            {
                nStyle &= ~(WS_MAXIMIZEBOX);
            }
            else
            {
                nStyle |= WS_MAXIMIZEBOX;
            }

            WinAPI.SetWindowLong(handle, GWL_STYLE, nStyle);
            WinAPI.SetWindowPos(handle, IntPtr.Zero, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_FRAMECHANGED);
        }
    }
}
