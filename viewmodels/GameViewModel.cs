using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TwoEleven.viewmodels;

public enum Direction {
    Up,
    Right,
    Down,
    Left
}

public partial class GameViewModel : ObservableObject {
    [ObservableProperty]
    private int _score;
    
    public ICommand UpCommand => new RelayCommand(() => ProcessMove(Direction.Up));
    public ICommand RightCommand => new RelayCommand(() => ProcessMove(Direction.Right));
    public ICommand DownCommand => new RelayCommand(() => ProcessMove(Direction.Down));
    public ICommand LeftCommand => new RelayCommand(() => ProcessMove(Direction.Left));

    private int ProcessMove(Direction dir, bool dryRun = false) {
        var mergeCount = 0;
        return mergeCount;
    }
}