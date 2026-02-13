using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Layoutfixer.Services;

public static class InputSimulator
{
    public static void SendCtrlC() => SendChord(VK_CONTROL, (ushort)'C');
    public static void SendCtrlV() => SendChord(VK_CONTROL, (ushort)'V');
    public static void SendCtrlShiftLeft() => SendChord(VK_CONTROL, VK_SHIFT, VK_LEFT);

    public static void SendChord(ushort modifier, ushort key)
    {
        KeyDown(modifier); KeyDown(key); KeyUp(key); KeyUp(modifier);
    }

    public static void SendChord(ushort mod1, ushort mod2, ushort key)
    {
        KeyDown(mod1); KeyDown(mod2); KeyDown(key);
        KeyUp(key); KeyUp(mod2); KeyUp(mod1);
    }

    private static void KeyDown(ushort key) => SendKey(key, 0);
    private static void KeyUp(ushort key) => SendKey(key, KEYEVENTF_KEYUP);

    private static void SendKey(ushort key, uint flags)
    {
        var input = new INPUT
        {
            type = INPUT_KEYBOARD,
            u = new InputUnion
            {
                ki = new KEYBDINPUT { wVk = key, dwFlags = flags }
            }
        };
        SendInput(1, new[] { input }, Marshal.SizeOf<INPUT>());
        Thread.Sleep(20);
    }

    private const int INPUT_KEYBOARD = 1;
    private const uint KEYEVENTF_KEYUP = 0x0002;

    private const ushort VK_CONTROL = 0x11;
    private const ushort VK_SHIFT = 0x10;
    private const ushort VK_LEFT = 0x25;

    [StructLayout(LayoutKind.Sequential)]
    private struct INPUT
    {
        public uint type;
        public InputUnion u;
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct InputUnion
    {
        [FieldOffset(0)] public KEYBDINPUT ki;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct KEYBDINPUT
    {
        public ushort wVk;
        public ushort wScan;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);
}
