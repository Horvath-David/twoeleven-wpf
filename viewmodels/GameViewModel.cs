using CommunityToolkit.Mvvm.ComponentModel;

namespace TwoEleven.viewmodels;

public partial class GameViewModel : ObservableObject {
    [ObservableProperty]
    private string something = "asd";
}