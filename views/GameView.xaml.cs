using System.Windows.Controls;

namespace TwoEleven.views;

public partial class GameView : UserControl {
    public GameView() {
        InitializeComponent();
        Focusable = true;
        Loaded += (_, _) => Focus();
    }
}