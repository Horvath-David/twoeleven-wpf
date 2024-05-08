using System.Windows.Controls;
using TwoEleven.viewmodels;

namespace TwoEleven.views;

public partial class GameView : UserControl {
    public GameView() {
        InitializeComponent();
        Focusable = true;
        Loaded += (_, _) => Focus();
    }

    private void Timeline_OnCompleted(object? sender, EventArgs e) {
        
        Console.WriteLine("timeline completed event");
        var vm = DataContext as GameViewModel;
        vm?.StopMovingCommand.Execute(null);
    }
}