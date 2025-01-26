using CQApp.Models;
using Microsoft.Data.SqlClient;
namespace CQApp.Views;

public partial class TuyencapByUserPage : ContentPage
{
    private ListView listView;    
    private string iduser;
    private List<FiberItem> fiberItems;
    public TuyencapByUserPage()
    {
        Title = "Danh sách tuyến cáp giao quản lý";        

        listView = new ListView
        {
            ItemTemplate = new DataTemplate(() =>
            {
                var cell = new TextCell();
                cell.SetBinding(TextCell.TextProperty, "Tuyen");
                return cell;
            })
        };
        listView.ItemTapped += OnItemTapped;

        Content = new StackLayout
        {
            Children = { listView }
        };
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await GetTuyenCapListFromDatabaseAsync();
    }
    private async Task<List<string>> GetTuyenCapListFromDatabaseAsync()
    {
        var list = new List<string>();

        try
        {
            using (var connection = new SqlConnection(Constants.sqlConnectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("sp_HSKT_CAPQUANG_HSHC_TUYEN", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            //string name =  reader.GetString(0) + " - " + reader.GetString(1) + " - " + reader.GetString(2);
                            //list.Add(name);
                            list.Add(reader.GetString(1));
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Lỗi", $"Không thể lấy dữ liệu: {ex.Message}", "OK");
        }

        return list;
    }
    private async void OnItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item != null && e.Item is FiberItem fiberItem)
        {
            await Navigation.PushAsync(new FiberPage(fiberItem));
        }
    }
}


        