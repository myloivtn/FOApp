using Maui.GoogleMaps;
namespace FOApp.Views;
using FOApp.Services;
using Microsoft.Maui.Layouts;

public partial class PolylinePage : ContentPage
{
    private readonly Maui.GoogleMaps.Map _map; // Luôn khởi tạo _map
    public const double MyFontSize = 20;
    public Position userLocation;// = Constants.defaultLocation;

    // Add the ResultLabel
    private readonly Label resultLabel;
    private readonly Entry pinDisplay;
    public PolylinePage(List<int> selectedSegments)
    {
        // Tạo pinDisplay label
        pinDisplay = new Entry
        {
            Margin = new Thickness(0, 10, 0, 0),
            TextColor = Colors.Black,
            FontSize = 8,
            HorizontalTextAlignment = TextAlignment.Center,
            BackgroundColor = Colors.AntiqueWhite
        };
        pinDisplay.SetBinding(Label.TextProperty, "Display");

        // Khởi tạo _map
        _map = new Maui.GoogleMaps.Map
        {
            MapType = MapType.Street
        };
               
        // Tạo ResultLabel
        resultLabel = new Label
        {
            Text = "",
            FontSize = MyFontSize,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(2)
        };

        // Sử dụng AbsoluteLayout để quản lý các thành phần chồng lên nhau
        var absoluteLayout = new AbsoluteLayout();

        // Thêm bản đồ vào AbsoluteLayout, nó sẽ chiếm toàn bộ màn hình
        AbsoluteLayout.SetLayoutBounds(_map, new Rect(0, 0, 1, 1));
        AbsoluteLayout.SetLayoutFlags(_map, AbsoluteLayoutFlags.All);
        absoluteLayout.Children.Add(_map);


        // Đặt nội dung trang là AbsoluteLayout
        Content = absoluteLayout;

   
        DisplayPolyline(selectedSegments);
    }  
    private void ClearClicked(object sender, EventArgs e)
    {
        _map.Pins.Clear(); // Xóa tất cả các Pin
        _map.Polylines.Clear();
    }
    private async void DisplayPolyline(List<int> selectedSegments)
    {
        var topSegments = selectedSegments.Take(Constants.topSegments).ToList();
        //await Mapservice.DisplayNearbyPolylineAndPinsAsync(_map, userLocation, Constants.radiusInKm, "", resultLabel, topSegments);
        LoggingService.Log($"{resultLabel}");
        //LoggingService.Log($"Kết quả tìm điểm cáp quang theo từ khóa: {topSegments}");
    }
}