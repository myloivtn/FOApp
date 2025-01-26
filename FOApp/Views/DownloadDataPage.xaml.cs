using CQApp.Models;
using static CQApp.Services.CachedService;
using System.Net.NetworkInformation;
using CQApp.Data;
namespace CQApp.Views;

public partial class DownloadDataPage : ContentPage
{
    private List<SegmentItem> tuyenCapList;
    private readonly ListView listView;

    public DownloadDataPage()
    {
        //// Tạo ô tìm kiếm
        //var searchBar = new SearchBar
        //{
        //    Placeholder = "Tìm tuyến cáp"
        //};
        //listView = new ListView
        //{
        //    ItemTemplate = new DataTemplate(() =>
        //    {
        //        var grid = new Grid();
        //        var idLabel = new Label();
        //        var checkBox = new CheckBox();

        //        idLabel.SetBinding(Label.TextProperty, "DisplayText");

        //        checkBox.SetBinding(CheckBox.IsCheckedProperty, "IsSelected");

        //        grid.Children.Add(idLabel);
        //        grid.Children.Add(checkBox);

        //        return new ViewCell { View = grid };
        //    })
        //};
        //searchBar.TextChanged += (sender, e) =>
        //{
        //    if (tuyenCapList == null) return;
            
        //    var searchText = searchBar.Text.ToLower();
        //    listView.ItemsSource = tuyenCapList.Where(item =>
        //        //item.Tuyen.ToLower().Contains(searchText)
        //        (item?.Tuyen ?? "").Contains(searchText, StringComparison.CurrentCultureIgnoreCase) ||
        //        (item?.LoaicapFO ?? "").Contains(searchText, StringComparison.CurrentCultureIgnoreCase) ||
        //        (item?.IdSegment.ToString() ?? "").Contains(searchText)).ToList();
        //};
        var buttonLayout = new HorizontalStackLayout
        {
            Spacing = 10,
            HorizontalOptions = LayoutOptions.Center
        };        
        var btnDownload = new Button
        {
            Text = "Download"
        };
        btnDownload.Clicked += OnDownloadClicked;
        var btnChangepass = new Button
        {
            Text = "Đổi mật khẩu"
        };     
        btnChangepass.Clicked += OnChangepassClicked;
        var stackLayout = new StackLayout();
        //stackLayout.Children.Add(searchBar);
        //stackLayout.Children.Add(listView);
        stackLayout.Children.Add(buttonLayout);
        buttonLayout.Add(btnDownload);
        buttonLayout.Add(btnChangepass);
        
        Content = stackLayout;
        //{
        //    Padding = new Thickness(10),
        //    Children =
        //        {
        //            searchBar,
        //            listView,
        //            buttonLayout        
        //        }
        //};
    }   

    private async void OnDownloadClicked(object sender, EventArgs e)
    {
        // Lấy danh sách các tuyến đã chọn từ ListView
        var selectedSegments = tuyenCapList
            .Where(item => item.IsSelected)
            .Select(item => item.IdSegment)
            .ToList();

        // Chuyển đổi danh sách các tuyến thành một chuỗi
        var selectedSegmentsString = string.Join(",", selectedSegments);
        //Download dữ liệu
        bool serverIsDown = CheckServerStatus(); // kiểm tra trạng thái của server

        if (serverIsDown)
        {
            try
            {
                // Gọi phương thức đồng bộ dữ liệu từ SQL Server sang SQLite
                _ = new FiberItemDatabase();

                string idUser = Preferences.Get("IdUser", null) ?? string.Empty;

                _ = FiberItemDatabase.DownloadAndSaveFiberOpticDataAsync(idUser);                
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ khi có lỗi xảy ra trong quá trình đồng bộ hóa
                Console.WriteLine($"Error during synchronization: {ex.Message}");
                // Có thể hiển thị thông báo lỗi cho người dùng tại đây nếu cần
            }
        }
    }
    private async void OnChangepassClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ChangePasswordPage());
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        int iduser = int.TryParse(Preferences.Get("IdUser", null), out int result) ? result : default;
        if (iduser == 0)
        {
            Application.Current.MainPage = new NavigationPage(new Views.LoginPage());
            return;
        }
        else
        {
            //Nếu chuyển từ Đức -> Sơn thì chỉ còn 1 tuyến trên SQL
            //Cần bổ sung thủ tục kiểm tra số liệu SQLite và SQL trước khi hiển thị, nếu không đúng phải download.
            tuyenCapList = await SegmentItemCache.GetSegmentFromSQLiteAsync();            
            listView.ItemsSource = tuyenCapList;
        }
    }   
    private static bool CheckServerStatus()
    {
        try
        {
            Ping ping = new();
            PingReply reply = ping.Send(Constants.serverIPAddress);

            if (reply.Status == IPStatus.Success)
            {
                Console.WriteLine($"Ping tới Server thành công.");
                return true;
            }
            else
            {
                // Kết nối tới máy chủ không thành công
                Console.WriteLine($"Ping tới Server KHÔNG thành công.");
                return false;
            }
        }
        catch (PingException)
        {
            // Xử lý ngoại lệ nếu có lỗi xảy ra trong quá trình ping
            return false;
        }
    }    

}