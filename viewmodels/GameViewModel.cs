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

    private bool ProcessMove(Direction dir, bool dryRun = false) {
        var didMerge = false;
        var didMove = false;

        // Removing unneeded tiles, flags
        foreach (var tile in Tiles.ToList().Where(tile => tile.IsDeleted)) {
            Tiles.Remove(tile);
        }
        foreach (var tile in Tiles.ToList().Where(tile => tile.IsMerged)) {
            tile.IsMerged = false;
        }

        var deltaX = dir switch {
            Direction.Left => -1,
            Direction.Right => 1,
            _ => 0
        };
        var deltaY = dir switch {
            Direction.Up => -1,
            Direction.Down => 1,
            _ => 0
        };

        var sorted = (dir switch {
            Direction.Left => Tiles.OrderBy(t => t.X),
            Direction.Right => Tiles.OrderBy(t => -t.X),
            Direction.Up => Tiles.OrderBy(t => t.Y),
            Direction.Down => Tiles.OrderBy(t => -t.Y),
            _ => new List<TileViewModel>().Order()
        }).ToList();

        // Preprocessing for animations
        foreach (var tile in sorted) {
            tile.IsMoving = false;
            tile.PrevPhysicalX = tile.PhysicalX;
            tile.PrevPhysicalY = tile.PhysicalY;
        }
        
        // Pre-pushing
        foreach (var tile in sorted) {
            // Console.WriteLine($"Starting: {tile.X}, {tile.Y}");
            // Console.WriteLine($"Deltas: {deltaX}, {deltaY}");
            while (Tiles.FirstOrDefault(t => tile.X + deltaX == t.X && tile.Y + deltaY == t.Y && !t.IsDeleted) == null
                   && tile.X + deltaX < 4 && tile.X + deltaX >= 0 && tile.Y + deltaY < 4 && tile.Y + deltaY >= 0) {
                didMove = true;
                if (dryRun) break;
                tile.X += deltaX;
                tile.Y += deltaY;
            }
            // Console.WriteLine($"After: {tile.X}, {tile.Y}");
        }
        // Console.WriteLine("---------");

        // Merging
        foreach (var tile in sorted) {
            var adjacent = Tiles.FirstOrDefault(t => tile.X - deltaX == t.X && tile.Y - deltaY == t.Y);
            if (adjacent == null || adjacent.Value != tile.Value) continue;
            // Console.WriteLine($"Deltas: {deltaX}, {deltaY}");
            // Console.WriteLine($"Starting: {adjacent.X}, {adjacent.Y}");
            didMerge = true;
            if (dryRun) break;
            tile.IsDeleted = true;
            adjacent.IsDeleted = true;
            adjacent.X -= deltaX;
            adjacent.Y -= deltaY;
            var newTile = new TileViewModel(new Tile(tile.X, tile.Y, tile.Value * 2)) {
                IsMerged = true
            };
            newTile.UpdatePhysicalPosition();
            Score += newTile.Value;
            Tiles.Add(newTile);
            // Console.WriteLine($"After: {adjacent.X}, {adjacent.Y}");
            // Console.WriteLine($"AfterTile: {tile.X}, {tile.Y}");
            // Console.WriteLine($"AfterNew: {newTile.X}, {newTile.Y}");
        }
        // Console.WriteLine("---------");
        
        sorted = (dir switch {
            Direction.Left => Tiles.OrderBy(t => t.X),
            Direction.Right => Tiles.OrderBy(t => -t.X),
            Direction.Up => Tiles.OrderBy(t => t.Y),
            Direction.Down => Tiles.OrderBy(t => -t.Y),
            _ => new List<TileViewModel>().Order()
        }).ToList();

        // Pushing
        foreach (var tile in sorted) {
            // Console.WriteLine($"Starting: {tile.X}, {tile.Y}");
            // Console.WriteLine($"Deltas: {deltaX}, {deltaY}");
            while (Tiles.FirstOrDefault(t => tile.X + deltaX == t.X && tile.Y + deltaY == t.Y && !t.IsDeleted) == null
                   && tile.X + deltaX < 4 && tile.X + deltaX >= 0 && tile.Y + deltaY < 4 && tile.Y + deltaY >= 0) {
                didMove = true;
                if (dryRun) break;
                tile.X += deltaX;
                tile.Y += deltaY;
            }
            // Console.WriteLine($"After: {tile.X}, {tile.Y}");
        }
        // Console.WriteLine("---------");

        // Postprocessing for animations
        foreach (var tile in sorted) {
            tile.UpdatePhysicalPosition();
            tile.IsMoving = true;
        }

        var didChange = didMerge || didMove;
        if (didChange && !dryRun) SpawnNew();
        return didChange;
    }

    private void SpawnNew() {
        // Console.WriteLine($"Before: {Tiles.Count(t => !t.IsDeleted)}");
        if (Tiles.Count(t => !t.IsDeleted) >= 16) {
            Console.WriteLine("out of space");
            return;
        }

        int x;
        int y;

        do {
            x = Random.Shared.Next(0, 4);
            y = Random.Shared.Next(0, 4);
        } while (Tiles.Any(t => t.X == x && t.Y == y && !t.IsDeleted));

        var value = Random.Shared.Next(0, 20) switch {
            0 => 4,
            _ => 2
        };

        var newTile = new TileViewModel(new Tile(x, y, value)) {
            IsSpawned = true
        };
        newTile.UpdatePhysicalPosition();
        Tiles.Add(newTile);
        // Console.WriteLine($"After: {Tiles.Count(t => !t.IsDeleted)}");
    }
}