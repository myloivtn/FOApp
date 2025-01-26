using CQApp.Data;
using CQApp.Models;
using System.Collections.ObjectModel;
using System.IO;
using static CQApp.Data.FiberItemDatabase;

namespace CQApp.Views;

[QueryProperty("Item", "Item")]
public partial class FiberItemPage : ContentPage
{
    //readonly FiberItem item;
    //readonly FiberItemDatabase database;
    public ObservableCollection<FiberItem> Items { get; set; } = [];
    public ObservableCollection<ImageItem> ImageItems { get; set; } = [];
    public FiberItem Item
    {
        get => BindingContext as FiberItem;
        set => BindingContext = value;
    }

    readonly FiberItemDatabase database;
    public FiberItemPage(FiberItemDatabase fiberItemDatabase)
    {
        InitializeComponent();
        database = fiberItemDatabase;
        BindingContext = this;
    }
    async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Item.Diemdacbiet))
        {
            await DisplayAlert("Dữ liệu bắt buộc", "Vui lòng không để trống Điểm đặc biệt.", "OK");
            return;
        }

        await database.SaveItemAsync(Item);
        await Shell.Current.GoToAsync("..");
    }

    async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (Item.Id == 0)
            return;
        await database.DeleteItemAsync(Item);
        await Shell.Current.GoToAsync("..");
    }

    async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}