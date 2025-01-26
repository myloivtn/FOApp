using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CQApp.ViewModel
{

    public partial class MainViewModel : ObservableObject
    {
        readonly IConnectivity connectivity;
        public MainViewModel(IConnectivity connectivity)
        {
            Items = [];
            this.connectivity = connectivity;
        }

        [ObservableProperty]
        ObservableCollection<string> items;
        
        [ObservableProperty]
        string text;

        [RelayCommand]
        
        async Task Add()
        {
            if (string.IsNullOrWhiteSpace(Text))
                return;

            if(connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
                return;
                }
                

#pragma warning disable MVVMTK0034 // Direct field reference to [ObservableProperty] backing field
            Items.Add(text);
#pragma warning restore MVVMTK0034 // Direct field reference to [ObservableProperty] backing field
                                  //add our item
            Text = string.Empty;
        }


        [RelayCommand]
        void Delete(string s)
        {
            if (!Items.Contains(s))
            {
                return;
            }
            Items.Remove(s);
        }
        [RelayCommand]
        async Task Tap(string s)
        {
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Text={s}");
        }
    }
}
