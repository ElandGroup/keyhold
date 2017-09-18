using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KeyHold
{
    class Program
    {
        private static int WM_LBUTTONDOWN = 0xF5;

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        public delegate bool CallBack(IntPtr hwnd, int lParam);

        [DllImport("user32.dll", EntryPoint = "EnumChildWindows", SetLastError = true)]
        public static extern int EnumChildWindows(IntPtr hWndParent, CallBack lpfn, int lParam);


        [DllImport("user32.dll", EntryPoint = "GetClassName", SetLastError = true)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder IpClassName, int nMaxCount);


        [DllImport("user32.dll", EntryPoint = "GetWindowText", SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);


        [DllImport("user32.dll", EntryPoint = "PostMessage", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        public static extern bool SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        static void Main(string[] args)
        {
            IntPtr winPtr = FindWindow(null, "沉默是金");
            Console.WriteLine("主窗体句柄：" + winPtr);
            if (winPtr != IntPtr.Zero)
            {
                EnumChildWindows(winPtr, (hWnd, lParam) =>
                {
                    StringBuilder IpClassName = new StringBuilder();
                    StringBuilder lpText = new StringBuilder();
                    int hClassName = GetClassName(hWnd, IpClassName, 255);
                    if (hClassName != 0)
                    {
                        int hText = GetWindowText(hWnd, lpText, 255);
                        Console.WriteLine("类名：" + IpClassName);
                        Console.WriteLine("文本：" + lpText);
                        if (lpText.ToString().Equals("按钮1"))
                        {
                            SendMessage(hWnd, WM_LBUTTONDOWN, 0, 0);
                            Console.WriteLine("触发事件");
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }, 0);
            }

        }
    }
}
