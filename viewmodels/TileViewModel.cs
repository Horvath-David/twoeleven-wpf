using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TwoEleven.models;

namespace TwoEleven.viewmodels;

public partial class TileViewModel(Tile tile) : ObservableObject {
    [ObservableProperty]
    private Tile _tile = tile;

    public int X {
        get => Tile.X;
        set => SetProperty(Tile.X, value, Tile, (t, v) => t.X = v);
    }

    public int Y {
        get => Tile.Y;
        set => SetProperty(Tile.Y, value, Tile, (t, v) => t.Y = v);
    }

    public int Value {
        get => Tile.Value;
        set => SetProperty(Tile.Value, value, Tile, (t, v) => t.Value = v);
    }

    [ObservableProperty]
    private double _physicalX;
    [ObservableProperty]
    private double _physicalY;
    [ObservableProperty]
    private double _prevPhysicalX;
    [ObservableProperty]
    private double _prevPhysicalY;
    [ObservableProperty]
    private bool _isSpawned;
    [ObservableProperty]
    private bool _isMoving;
    [ObservableProperty]
    private bool _isMerged;
    [ObservableProperty]
    private bool _isDeleted;

    public ICommand RefreshInitials => new RelayCommand(() => {
        var oldSpawned = IsSpawned;
        IsSpawned = false;
        IsSpawned = oldSpawned;
    });
    
    public ICommand TestCommand => new RelayCommand(() => {
        Console.WriteLine(IsSpawned);
    });

    public void UpdatePhysicalPosition() { 
        PhysicalX = 14 + X * 90;
        PhysicalY = 14 + Y * 90;
        
        if (PrevPhysicalX != 0) return;
        PrevPhysicalX = PhysicalX;
        PrevPhysicalY = PhysicalY;
    }
}