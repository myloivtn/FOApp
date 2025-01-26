using FOApp.Views;
using FOApp.Services;
using Maui.GoogleMaps;
using Microsoft.Maui.Layouts;
using Microsoft.Maui.Controls.Shapes;

namespace FOApp
{
    public partial class MainPage : ContentPage
    {
        Timer? locationUpdateTimer;
        Pin userLocationPin;
       
        private readonly Maui.GoogleMaps.Map _map; 
        public const double MyFontSize = 20;
        public Position userLocation;

        private readonly Label resultLabel;
        private readonly Entry pinDisplay;
        
        public MainPage()
        {
            InitializeComponent();
        
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
                FontSize = MyFontSize-4,
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

            // Sử dụng AbsoluteLayout để quản lý các thành phần chồng lên nhau
            var absoluteLayout = new AbsoluteLayout();
            
            // Thêm bản đồ vào AbsoluteLayout, nó sẽ chiếm toàn bộ màn hình
            AbsoluteLayout.SetLayoutBounds(_map, new Rect(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(_map, AbsoluteLayoutFlags.All);
            absoluteLayout.Children.Add(_map);

            // Thêm các nút vào AbsoluteLayout ở phía trên cùng màn hình           
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
        private async void OnLogPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LogPage());
        }
      
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //LoggingService.Log("Trang chính xuất hiện.");
            _ = RequestLocationPermissionAsync();
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                var position = await Geolocation.GetLocationAsync(request);

                userLocation = position != null
                    ? new Position(position.Latitude, position.Longitude)
                    : new Position(Constants.defaultLocation.Latitude, Constants.defaultLocation.Longitude);
                //LoggingService.Log($"Tọa độ người dùng: {userLocation.Latitude}, {userLocation.Longitude}");
            }
            catch (Exception ex)
            {
                resultLabel.Text = $"NOK";
                LoggingService.Log($"Lỗi lấy tọa độ GPS: {ex.Message}");
            }
            
            await Mapservice.DisplayPolylineAsync(_map, userLocation);
            await Mapservice.DisplayPinsAsync(_map, userLocation, resultLabel);
            StartTrackingUserLocation(_map); 
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            StopTrackingUserLocation(); 
        }
        private async void OnSearchBarCompleted(object sender, EventArgs e) 
        {
            var searchBar = (Entry)sender;
            string searchText = searchBar.Text;

            //LoggingService.Log($"Bắt đầu tìm điểm cáp quang theo từ khóa: {searchText}");
            await Mapservice.DisplayPinsAsync(_map, userLocation, searchText, resultLabel);
            //LoggingService.Log($"{resultLabel.Text}");
            //LoggingService.Log($"Kết quả tìm điểm cáp quang theo từ khóa: {searchText}");            
        }
        public void StartTrackingUserLocation(Maui.GoogleMaps.Map map)
        {
            // Lấy vị trí đã lưu từ Preferences (nếu có)
            double cachedLat = Preferences.Get("UserLat", 0.0);
            double cachedLon = Preferences.Get("UserLon", 0.0);
            var initialPosition = new Position(cachedLat, cachedLon);

            // Tạo pin cho vị trí người dùng
            userLocationPin = new Pin
            {
                Label = "Vị trí của bạn",
                Position = (cachedLat != 0.0 && cachedLon != 0.0) ? initialPosition : new Position(0, 0),
                Icon = BitmapDescriptorFactory.FromBundle("userlocation3"),
            };
            map.Pins.Add(userLocationPin);

            // Nếu có dữ liệu cache, di chuyển bản đồ đến vị trí đó
            if (cachedLat != 0.0 && cachedLon != 0.0)
            {
                map.MoveToRegion(MapSpan.FromCenterAndRadius(userLocationPin.Position, Distance.FromKilometers(Constants.defaultDistancekm)));
            }

            // Bắt đầu cập nhật vị trí theo thời gian thực
            var locationUpdateTimer = new System.Threading.Timer(async (state) =>
            {
                var location = await Geolocation.GetLastKnownLocationAsync() ?? await Geolocation.GetLocationAsync();
                if (location != null)
                {
                    Microsoft.Maui.ApplicationModel.MainThread.BeginInvokeOnMainThread(() =>
                    {
                        userLocationPin.Position = new Position(location.Latitude, location.Longitude);
                        map.MoveToRegion(MapSpan.FromCenterAndRadius(userLocationPin.Position, Distance.FromKilometers(Constants.defaultDistancekm)));

                        // Lưu vị trí vào Preferences
                        Preferences.Set("UserLat", location.Latitude);
                        Preferences.Set("UserLon", location.Longitude);
                    });
                }
            }, null, 0, 5000);
        }

        //public void StartTrackingUserLocation(Maui.GoogleMaps.Map map)
        //{
        //    userLocationPin = new Pin
        //    {
        //        Label = "Vị trí của bạn",
        //        Position = new Position(0, 0),
        //        Icon = BitmapDescriptorFactory.FromBundle("userlocation3"),
        //    };
        //    map.Pins.Add(userLocationPin);

        //    var locationUpdateTimer = new System.Threading.Timer(async (state) =>
        //    {
        //        var location = await Geolocation.GetLastKnownLocationAsync() ?? await Geolocation.GetLocationAsync();
        //        if (location != null)
        //        {
        //            Microsoft.Maui.ApplicationModel.MainThread.BeginInvokeOnMainThread(() =>
        //            {
        //                userLocationPin.Position = new Position(location.Latitude, location.Longitude);
        //                map.MoveToRegion(MapSpan.FromCenterAndRadius(userLocationPin.Position, Distance.FromKilometers(Constants.defaultDistancekm)));
        //            });
        //        }
        //    }, null, 0, 5000); 
        //}
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

            if (status != PermissionStatus.Granted)            
            {                
                await Application.Current.MainPage.DisplayAlert("Quyền vị trí", "Ứng dụng cần quyền truy cập vị trí để hoạt động.", "OK");
            }
        }
    }
}
