using System.Windows.Input;
using System.Windows.Media;
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

    public void UpdatePhysicalPosition() { 
        PhysicalX = 14 + X * 90;
        PhysicalY = 14 + Y * 90;
        
        if (PrevPhysicalX != 0) return;
        PrevPhysicalX = PhysicalX;
        PrevPhysicalY = PhysicalY;
    }

    public Color BackgroundColor => Value switch {
        2 => Color.FromRgb(238, 228, 218),
        4 => Color.FromRgb(237, 224, 200),
        8 => Color.FromRgb(242, 177, 121),
        16 => Color.FromRgb(245, 149, 99),
        32 => Color.FromRgb(246, 124, 95),
        64 => Color.FromRgb(246, 94, 59),
        128 => Color.FromRgb(237, 207, 114),
        256 => Color.FromRgb(237, 204, 97),
        512 => Color.FromRgb(237, 200, 80),
        1024 => Color.FromRgb(237, 197, 63),
        2048 => Color.FromRgb(237, 194, 46),
        _ => Color.FromRgb(62, 57, 51)
    };
    
    public Color ForegroundColor => Value switch {
        <= 4 => Color.FromRgb(119, 110, 101),
        _ => Color.FromRgb(249, 246, 242)
    };

    public double FontSize => Value switch {
        >= 1024 => 24,
        _ => 32
    };
}