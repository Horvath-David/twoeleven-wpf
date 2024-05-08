using System.Windows.Controls;

namespace TwoEleven.views;

public partial class HomeView : UserControl {
    public HomeView() {
        InitializeComponent();
        Focusable = true;
        Loaded += (_, _) => Focus();
    }
}