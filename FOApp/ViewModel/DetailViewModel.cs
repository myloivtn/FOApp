﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQApp.ViewModel;
[QueryProperty("Text", "Text")]

public partial class DetailViewModel : ObservableObject
{
    [ObservableProperty]
    string? text;

    [RelayCommand]
    static async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}
