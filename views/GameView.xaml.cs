using System.Windows.Controls;
using TwoEleven.viewmodels;

namespace TwoEleven.views;

public partial class GameView : UserControl {
    public GameView() {
        InitializeComponent();
        Focusable = true;
        Loaded += (_, _) => Focus();
    }
}