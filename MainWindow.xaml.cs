using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace TwoEleven;

public partial class MainWindow {
    public MainWindow() {
        Shared.NavigationService = NavigationService;

        InitializeComponent();
        Loaded += (_, _) => SetImmersiveDarkMode(true);
    }

    [DllImport("dwmapi.dll", PreserveSig = true)]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr,
        ref int attrValue, int attrSize);

    private void SetImmersiveDarkMode(bool enable) {
        var value = enable ? 1 : 0;
        var windowInterop = new WindowInteropHelper(this);
        var hwnd = windowInterop.Handle;
        var result = DwmSetWindowAttribute(
            hwnd, 20 /* DWMWA_USE_IMMERSIVE_DARK_MODE */, ref value,
            sizeof(int));
        if (result != 0) {
            Console.WriteLine($"Immersive dark mode result: {result}");
        }

        Width += 1;
        Width -= 1;
    }
}