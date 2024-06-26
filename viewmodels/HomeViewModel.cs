﻿using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TwoEleven.views;

namespace TwoEleven.viewmodels;

public partial class HomeViewModel : ObservableObject {
    public static ICommand NavigateToGameCommand => new RelayCommand(() => {
        Shared.NavigationService.Navigate(new GameView());
    });
}