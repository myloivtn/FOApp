using FOApp.Services;
using Maui.GoogleMaps;
using Maui.GoogleMaps.Clustering;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Layouts;
namespace FOApp.Views;

public partial class FiberMapClusteringPage : ContentPage
{
    Timer locationUpdateTimer;
    Pin userLocationPin;

    public List<int> SelectedSegments { get; set; } = new List<int>();

    private readonly ClusteredMap _map; // Luôn khởi tạo _map
    public const double MyFontSize = 20;
    public Position userLocation;// = Constants.defaultLocation;

    // Add the resultLabel
    private readonly Label resultLabel;
    private readonly Entry pinDisplay;
    public FiberMapClusteringPage(string title)
    {
		InitializeComponent();
        this.Title = title;
      
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
        
        _map = new ClusteredMap
        {
            MapType = MapType.Street,
            IsEnabled = true,
            MyLocationEnabled = true
        };
        
        var logPageButton = new ImageButton
        {
            Source = "log_icon.png", // Đường dẫn đến biểu tượng nhỏ của bạn
            WidthRequest = 30,  // Thiết lập kích thước cho icon
            HeightRequest = 30,
            Margin = new Thickness(4),
            BackgroundColor = Colors.Transparent  // Không có nền
        };
        logPageButton.Clicked += OnLogPageButtonClicked;

        // Tạo search bar

        var searchBarBorder = new Border
        {
            BackgroundColor = Colors.White,
            Stroke = Colors.LightGray,                 // Màu viền của Border
            StrokeThickness = 0.2,                         // Độ dày của viền
            StrokeShape = new RoundRectangle() { CornerRadius = 10 }, // Bo tròn góc cho Border
            Padding = new Thickness(0)                 // Giảm Padding để có thêm không gian cho Entry
        };

        var searchBar = new Entry
        {
            Placeholder = "Từ khóa tìm kiếm",
            FontSize = MyFontSize - 4,
            BackgroundColor = Colors.Transparent,      // Giữ Entry trong suốt để chỉ hiển thị màu nền của Border
            TextColor = Colors.Black,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center,
            Margin = new Thickness(1, 0)               // Tạo khoảng cách bên trong cho Entry
        };

        // Đặt searchBar bên trong Border
        searchBarBorder.Content = searchBar;

        searchBar.Completed += OnSearchBarCompleted;

        // Tạo Label với màu chữ đỏ đậm
        resultLabel = new Label
        {
            Text = "0", // Nội dung sẽ được gán sau
            FontSize = 16,
            FontAttributes = FontAttributes.Bold, // Làm chữ in đậm
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            TextColor = Colors.Red, // Màu chữ đỏ đậm
            BackgroundColor = Colors.Transparent,
            Margin = new Thickness(2),
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center
        };

        //// Bọc Label bên trong Frame để tạo khung bo tròn với nền xanh
        var resultLabelBorder = new Border
        {
            BackgroundColor = Colors.White,
            Stroke = Colors.LightGray,                 // Màu viền của Border
            StrokeThickness = 0.2,                       // Độ dày của viền
            StrokeShape = new RoundRectangle() { CornerRadius = 10 }, // Bo tròn góc cho Border
            Padding = new Thickness(0),                 // Giảm Padding để có thêm không gian cho Entry
            HeightRequest = 35, // Chiều cao của khung (tùy chỉnh theo nhu cầu)
            WidthRequest = 50, // Chiều rộng của khung
        };
        resultLabelBorder.Content = resultLabel;

        var filterButton = new ImageButton
        {
            Source = "filter_icon.png", // Đường dẫn biểu tượng lọc
            WidthRequest = 30,
            HeightRequest = 30,
            Margin = new Thickness(4),
            BackgroundColor = Colors.Transparent
        };

        // Xử lý sự kiện click để mở danh sách Label
        filterButton.Clicked += OnFilterButtonClicked;

          
        // Sử dụng AbsoluteLayout để quản lý các thành phần chồng lên nhau
        var absoluteLayout = new AbsoluteLayout();

        // Thêm bản đồ vào AbsoluteLayout, nó sẽ chiếm toàn bộ màn hình
        AbsoluteLayout.SetLayoutBounds(_map, new Rect(0, 0, 1, 1));
        AbsoluteLayout.SetLayoutFlags(_map, AbsoluteLayoutFlags.All);
        absoluteLayout.Children.Add(_map);

        // Thêm các nút vào AbsoluteLayout ở phía trên cùng màn hình           
        // Thêm filterButton vào giao diện
        AbsoluteLayout.SetLayoutBounds(filterButton, new Rect(0.05, 0.02, 30, 30)); // Tùy chỉnh vị trí
        AbsoluteLayout.SetLayoutFlags(filterButton, AbsoluteLayoutFlags.PositionProportional);
        absoluteLayout.Children.Add(filterButton);

        AbsoluteLayout.SetLayoutBounds(resultLabelBorder, new Rect(0.85, 0.02, 80, 40)); // Tùy chỉnh vị trí
        AbsoluteLayout.SetLayoutFlags(resultLabelBorder, AbsoluteLayoutFlags.PositionProportional);
        absoluteLayout.Children.Add(resultLabelBorder);

        AbsoluteLayout.SetLayoutBounds(searchBarBorder, new Rect(0.2, 0.02, 0.6, 40)); // Căn giữa và chiếm 80% chiều rộng
        AbsoluteLayout.SetLayoutFlags(searchBarBorder, AbsoluteLayoutFlags.PositionProportional | AbsoluteLayoutFlags.WidthProportional);
        absoluteLayout.Children.Add(searchBarBorder);

        AbsoluteLayout.SetLayoutBounds(logPageButton, new Rect(0.95, 0.02, 30, 30)); // Tùy chỉnh vị trí
        AbsoluteLayout.SetLayoutFlags(logPageButton, AbsoluteLayoutFlags.PositionProportional);
        absoluteLayout.Children.Add(logPageButton);

        Content = absoluteLayout;

    }

    public void SetSelectedSegments(List<int> selectedSegments)
    {
        SelectedSegments = selectedSegments;
    }


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
        LoggingService.Log($"Xem tuyến {SelectedSegments}");
        //resultLabel.Text = $"NOK";
        _ = RequestLocationPermissionAsync();
        try
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            var position = await Geolocation.GetLocationAsync(request);

            // Sử dụng defaultLocation nếu userLocation là null
            userLocation = position != null
                ? new Position(position.Latitude, position.Longitude)
                : new Position(Constants.defaultLocation.Latitude, Constants.defaultLocation.Longitude);
            LoggingService.Log($"Tọa độ người dùng: {userLocation.Latitude}, {userLocation.Longitude}");
        }
        catch (Exception ex)
        {
            resultLabel.Text = $"NOK";
            LoggingService.Log($"Lỗi lấy tọa độ GPS: {ex.Message}");
        }

        if (SelectedSegments.Count != 0)
        {
            //StartTrackingUserLocation(_map);
            // Nếu đã chọn segment, hiển thị các segment đã chọn
            var topSegments = SelectedSegments.Take(Constants.topSegments).ToList();
            await Mapservice.DisplayPolylineClusteringAsync(_map, topSegments);
            await Mapservice.DisplayPinsClusteringAsync(_map, topSegments, resultLabel);
        }
        //StartTrackingUserLocation(_map); // Gọi StartTrackingUserLocation từ thư viện MapService
        //LoggingService.Log($"{resultLabel.Text}");
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        StopTrackingUserLocation(); // Dừng theo dõi khi rời khỏi trang (nếu cần)
    }
    private async void OnFilterButtonClicked(object sender, EventArgs e)
    {
        // Hiển thị danh sách Label để người dùng chọn
        var labels = GetAvailableLabels(); // Hàm trả danh sách Label từ dữ liệu của bạn
        string selectedLabel = await DisplayActionSheet("Chọn Label", "Hủy", null, labels.ToArray());

        if (!string.IsNullOrEmpty(selectedLabel) && selectedLabel != "Hủy")
        {
            FilterPinsByLabel(selectedLabel);
        }
    }
    private void FilterPinsByLabel(string label)
    {
        // Lọc các Pin theo Label
        var filteredPins = _map.Pins.Where(pin => pin.Label == label).ToList();

        // Xóa tất cả Pin khỏi bản đồ
        _map.Pins.Clear();

        // Thêm lại các Pin được lọc
        foreach (var pin in filteredPins)
        {
            _map.Pins.Add(pin);
        }

        // Cập nhật thông báo số lượng Pin
        resultLabel.Text = filteredPins.Count.ToString();
    }
    private List<string> GetAvailableLabels()
    {
        // Lấy danh sách các Label duy nhất từ danh sách Pin
        return _map.Pins.Select(pin => pin.Label).Distinct().ToList();
    }

    private async void OnSearchBarCompleted(object sender, EventArgs e)
    {
        var searchBar = (Entry)sender;
        string searchText = searchBar.Text;

        LoggingService.Log($"Bắt đầu tìm điểm cáp quang theo từ khóa: {searchText}");

        if (SelectedSegments.Any())  //Tìm trên các tuyến đã chọn
        {
            // Nếu đã chọn segment, hiển thị các segment đã chọn
            var topSegments = SelectedSegments.Take(Constants.topSegments).ToList();
            await Mapservice.DisplayPinsClusteringAsync(_map, topSegments, searchText, resultLabel);
        }
        //// Pass the resultLabel to display search results
        //await Mapservice.DisplayPinsAsync(_map, userLocation, searchText, resultLabel);

        LoggingService.Log($"{resultLabel.Text}");
        LoggingService.Log($"Kết quả tìm điểm cáp quang theo từ khóa: {searchText}");
    }

    public void StartTrackingUserLocation(Maui.GoogleMaps.Map map)
    {
        // Tạo một Pin để hiển thị vị trí người dùng
        userLocationPin = new Pin
        {
            Label = "Vị trí của bạn",
            Position = new Position(0, 0), // Khởi tạo vị trí ban đầu, sẽ cập nhật sau
            Icon = BitmapDescriptorFactory.FromBundle("userlocation3"),
            //Icon = BitmapDescriptorFactory.FromView(() => new BindingPinView($"📍", $"marker03.png")),
        };
        map.Pins.Add(userLocationPin);

        var locationUpdateTimer = new System.Threading.Timer(async (state) =>
        {
            var location = await Geolocation.GetLastKnownLocationAsync() ?? await Geolocation.GetLocationAsync();
            if (location != null)
            {
                Microsoft.Maui.ApplicationModel.MainThread.BeginInvokeOnMainThread(() =>
                {
                    userLocationPin.Position = new Position(location.Latitude, location.Longitude);
                    map.MoveToRegion(MapSpan.FromCenterAndRadius(userLocationPin.Position, Distance.FromKilometers(Constants.defaultDistancekm)));
                });
            }
        }, null, 0, 5000); // Cập nhật mỗi 5 giây
    }
    public void StopTrackingUserLocation()
    {
        locationUpdateTimer?.Dispose();
        locationUpdateTimer = null;
    }

    async Task RequestLocationPermissionAsync()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
        if (status != PermissionStatus.Granted)
        {
            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
        }

        if (status == PermissionStatus.Granted)
        {
            // Quyền đã được cấp, tiếp tục truy cập vị trí người dùng
        }
        else
        {
            // Hiển thị thông báo nếu người dùng không cấp quyền
            await Application.Current.MainPage.DisplayAlert("Quyền vị trí", "Ứng dụng cần quyền truy cập vị trí để hoạt động.", "OK");
        }
    }
}