using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TwoEleven.models;

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

    [ObservableProperty]
    private ObservableCollection<TileViewModel> _tiles = [];

    public ICommand UpCommand => new RelayCommand(() => ProcessMove(Direction.Up));
    public ICommand RightCommand => new RelayCommand(() => ProcessMove(Direction.Right));
    public ICommand DownCommand => new RelayCommand(() => ProcessMove(Direction.Down));
    public ICommand LeftCommand => new RelayCommand(() => ProcessMove(Direction.Left));
    public ICommand SpawnCommand => new RelayCommand(SpawnNew);

    private int ProcessMove(Direction dir, bool dryRun = false) {
        var mergeCount = 0;

        SpawnNew();
        return mergeCount;
    }

    private void SpawnNew() {
        if (Tiles.Count(t => !t.IsDeleted) >= 16) {
            Console.WriteLine("out of space");
            return;
        }

        int x;
        int y;

        do {
            x = Random.Shared.Next(0, 4);
            y = Random.Shared.Next(0, 4);
        } while (Tiles.Any(t => t.X == x && t.Y == y));

        var value = Random.Shared.Next(0, 20) switch {
            0 => 4,
            _ => 2
        };

        var newTile = new TileViewModel(new Tile(x, y, value)) {
            IsSpawned = true
        };
        newTile.UpdatePhysicalPosition();
        Tiles.Add(newTile);
        
        Console.WriteLine(Tiles.Last().PhysicalX);
        Console.WriteLine(Tiles.Last().PhysicalY);

        Console.WriteLine(Tiles.Count);
    }
}