using Maui.GoogleMaps;
namespace CQApp.Views;
using CQApp.Services;
using Microsoft.Maui.Layouts;

public partial class FiberDetailPage : ContentPage
{

    public List<int> SelectedSegments { get; set; }
    private readonly Label resultLabel;
    public const double MyFontSize = 20;
    public FiberDetailPage()
    {
        var logPageButton = new ImageButton
        {
            Source = "note_icon.png", // Đường dẫn đến biểu tượng nhỏ của bạn
            WidthRequest = 30,  // Thiết lập kích thước cho icon
            HeightRequest = 30,
            Margin = new Thickness(4),
            BackgroundColor = Colors.Transparent  // Không có nền
        };
        logPageButton.Clicked += OnLogPageButtonClicked;

        var searchBar = new Entry
        {
            Placeholder = "Từ khóa tìm kiếm",
            FontSize = MyFontSize,
            BackgroundColor = Colors.WhiteSmoke,  // Đặt trong suốt vì Frame sẽ có màu nền
            TextColor = Colors.Black,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center,
            HeightRequest = 40,
            Visual = VisualMarker.Default
        };

        searchBar.Completed += OnSearchBarCompleted;

        // Tạo Label với màu chữ đỏ đậm
        resultLabel = new Label
        {
            Text = "0", // Nội dung sẽ được gán sau
            FontSize = MyFontSize,
            FontAttributes = FontAttributes.Bold, // Làm chữ in đậm
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            TextColor = Colors.Red, // Màu chữ đỏ đậm
            BackgroundColor = Colors.LightBlue,
            Margin = new Thickness(2),
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center
        };

        //// Bọc Label bên trong Frame để tạo khung bo tròn với nền xanh
        var resultFrame = new Frame
        {
            //Content = resultLabel,
            CornerRadius = 100, // Đặt giá trị lớn để tạo hiệu ứng hình tròn
            BackgroundColor = Colors.LightBlue, // Nền màu xanh blue
            Padding = new Thickness(20), // Đặt khoảng cách giữa nội dung và biên ngoài
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            HasShadow = false, // Tắt đổ bóng (nếu muốn)
            HeightRequest = 35, // Chiều cao của khung (tùy chỉnh theo nhu cầu)
            WidthRequest = 35, // Chiều rộng của khung
        };

        // Sử dụng AbsoluteLayout để quản lý các thành phần chồng lên nhau
        var absoluteLayout = new AbsoluteLayout();


        // Thêm các nút vào AbsoluteLayout ở phía trên cùng màn hình           
        //AbsoluteLayout.SetLayoutBounds(resultFrame, new Rect(0.80, 0.02, 30, 30)); // Tùy chỉnh vị trí
        //AbsoluteLayout.SetLayoutFlags(resultFrame, AbsoluteLayoutFlags.PositionProportional);
        //absoluteLayout.Children.Add(resultFrame);

        AbsoluteLayout.SetLayoutBounds(resultLabel, new Rect(0.80, 0.02, 30, 30)); // Tùy chỉnh vị trí
        AbsoluteLayout.SetLayoutFlags(resultLabel, AbsoluteLayoutFlags.PositionProportional);
        absoluteLayout.Children.Add(resultLabel);

        //// Thêm nút searchBar vào AbsoluteLayout ở phía trên cùng màn hình
        AbsoluteLayout.SetLayoutBounds(searchBar, new Rect(0.2, 0.02, 0.6, 40)); // Căn giữa và chiếm 80% chiều rộng
        AbsoluteLayout.SetLayoutFlags(searchBar, AbsoluteLayoutFlags.PositionProportional | AbsoluteLayoutFlags.WidthProportional);
        absoluteLayout.Children.Add(searchBar);

        AbsoluteLayout.SetLayoutBounds(logPageButton, new Rect(0.95, 0.02, 30, 30)); // Tùy chỉnh vị trí
        AbsoluteLayout.SetLayoutFlags(logPageButton, AbsoluteLayoutFlags.PositionProportional);
        absoluteLayout.Children.Add(logPageButton);
        //absoluteLayout.Children.Add(resultFrame);
        // Đặt nội dung trang là AbsoluteLayout
        Content = absoluteLayout;        
    }
    public void SetSelectedSegments(List<int> selectedSegments)
    {
        SelectedSegments = selectedSegments;
    }
    // Điều hướng tới LogPage
    private async void OnLogPageButtonClicked(object sender, EventArgs e)
    {
        // Use relative routing to navigate to LogPage
        //await Shell.Current.GoToAsync("LogPage");
        await Navigation.PushAsync(new LogPage());
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Tải dữ liệu vào ListView
        //LoggingService.Log("Tải dữ liệu tuyến cáp.");
        //_ = LoadSegmentItemsAsync();
        LoggingService.Log("Trang chính xuất hiện.");
        //resultLabel.Text = $"NOK";
       
        if (SelectedSegments == null || !SelectedSegments.Any())
        {
            // Nếu chưa có segment nào được chọn, hiển thị bản đồ mặc định với vị trí người dùng
            //await Mapservice.DisplayPolylineAndPinsAsync(_map, userLocation, resultLabel);
        }
        else
        {
            // Nếu đã chọn segment, hiển thị các segment đã chọn
            var firstThreeSegments = SelectedSegments.Take(3).ToList();
            //await Mapservice.DisplayPolylineAsync(_map, firstThreeSegments, resultLabel);
        }
    }

    private async void OnSearchBarCompleted(object sender, EventArgs e)
    {
        var searchBar = (Entry)sender;
        string searchText = searchBar.Text;

        LoggingService.Log($"Bắt đầu tìm điểm cáp quang theo từ khóa: {searchText}");

        // Pass the resultLabel to display search results
        //await Mapservice.DisplayPolylineAndPinsAsync(_map, userLocation, searchText, resultLabel);
        LoggingService.Log($"{resultLabel.Text}");
        LoggingService.Log($"Kết quả tìm điểm cáp quang theo từ khóa: {searchText}");
    }
}

