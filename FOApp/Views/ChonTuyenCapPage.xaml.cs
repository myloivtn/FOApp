using FOApp.Models;
using static FOApp.Services.CachedService;
using FOApp.Views;
using Maui.GoogleMaps;
//using Android.Telecom;

namespace FOApp.Views;

public partial class ChonTuyenCapPage : ContentPage
{
    private List<SegmentItem> tuyenCapList;
    private readonly ListView listView;    
    private CheckBox chkAll;

    public ChonTuyenCapPage()
    {
        InitializeComponent();

        // Ẩn thanh Navigation Bar
        //NavigationPage.SetHasNavigationBar(this, false);
        //Slider titleView = new Slider { HeightRequest = 20, WidthRequest = 300 };
        //NavigationPage.SetTitleView(this, titleView);
        // Tạo ô tìm kiếm
        var searchBar = new SearchBar
        {
            Placeholder = "Từ khóa tìm kiếm",
            //HorizontalOptions = LayoutOptions.FillAndExpand // Giãn đều
        };

        listView = new ListView
        {
            ItemTemplate = new DataTemplate(() =>
            {
                //// Tạo Grid với 2 cột
                var grid = new Grid
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = GridLength.Star }, // Cột dành cho Label
                        new ColumnDefinition { Width = GridLength.Auto }  // Cột dành cho CheckBox
                    }
                };
               

                // Tạo Label và CheckBox
                var idLabel = new Label();
                var checkBox = new CheckBox();

                // Ràng buộc dữ liệu cho Label và CheckBox
                idLabel.SetBinding(Label.TextProperty, "DisplayText");
                checkBox.SetBinding(CheckBox.IsCheckedProperty, "IsSelected");

                // Thêm Label vào Grid
                grid.Children.Add(idLabel); // Thêm vào Grid trước
                Grid.SetColumn(idLabel, 0); // Đặt Label vào cột 0 (Hàng 0 mặc định)

                // Thêm CheckBox vào Grid
                grid.Children.Add(checkBox); // Thêm vào Grid trước
                Grid.SetColumn(checkBox, 1); // Đặt CheckBox vào cột 1 (Hàng 0 mặc định)

                // Trả về ViewCell với Grid
                return new ViewCell { View = grid };
            })
        };

        // Tạo Button Map
        var btnMap = new Button
        {
            Text = "Point",
            HorizontalOptions = LayoutOptions.End // Canh về phía cuối
        };
        btnMap.Clicked += OnMapClicked;

        var btnMapClustering = new Button
        {
            Text = "Group",
            HorizontalOptions = LayoutOptions.End // Canh về phía cuối
        };
        btnMapClustering.Clicked += OnMapClusteringClicked;

        chkAll = new CheckBox { IsChecked = false };
        var lblAll = new Label { Text = "Tất cả" };

        // Thêm sự kiện CheckedChanged
        chkAll.CheckedChanged += ChkAll_CheckedChanged;

        // Grid để chứa SearchBar, Map và Detail trên cùng 1 hàng
        var searchGrid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitionCollection
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }, // Tỷ lệ 1 cho SearchBar
                new ColumnDefinition { Width = GridLength.Auto }, // Tự động co với btnMap
                new ColumnDefinition { Width = GridLength.Auto },  // Tự động co với btnDetail
                 new ColumnDefinition { Width = GridLength.Auto }  // Tự động co với chkAll
            }
        };

        // Thêm SearchBar vào cột 0
        searchGrid.Children.Add(searchBar);
        Grid.SetColumn(searchBar, 0);

        // Thêm btnMap vào cột 1
        searchGrid.Children.Add(btnMap);
        Grid.SetColumn(btnMap, 1);

        // Thêm btnMapClustering vào cột 2
        searchGrid.Children.Add(btnMapClustering);
        Grid.SetColumn(btnMapClustering, 2);

        // Thêm chbAll vào cột 3
        searchGrid.Children.Add(chkAll);
        Grid.SetColumn(chkAll, 3);
        //searchGrid.Children.Add(lblAll);
        //Grid.SetColumn(lblAll, 3);
        // Xử lý sự kiện tìm kiếm
        searchBar.TextChanged += (sender, e) =>
        {
            if (tuyenCapList == null) return;

            var searchText = searchBar.Text.ToLower();
            listView.ItemsSource = tuyenCapList.Where(item =>
                item.Tuyen.ToLower().Contains(searchText) ||
                item.LoaicapFO.ToString().Contains(searchText) ||
                item.IdSegment.ToString().Contains(searchText)).ToList();
        };

        // StackLayout chứa toàn bộ nội dung
        Content = new StackLayout
        {
            Padding = new Thickness(10),
            Children =
            {
                searchGrid, // Đặt Grid chứa SearchBar, btnMap, btnDetail ở trên cùng
                listView    // Đặt ListView dưới Grid
            }
        };
    }

    private void OnMapClicked(object sender, EventArgs e)
    {
        //NavigateToMainPage();
        NavigateToFiberPage();
    }
    private void OnMapClusteringClicked(object sender, EventArgs e)
    {        
        NavigateToFiberMapClusteredPage();
    }

    // Điều hướng tới MainPage
    private async void NavigateToFiberPage()
    {
        List<int> selectedSegments = tuyenCapList
            .Where(item => item.IsSelected)
            .Select(item => item.IdSegment)
            .ToList(); // Lấy danh sách các segment đã chọn
        List<string?> selectedTuyen = tuyenCapList
            .Where(item => item.IsSelected)
            .Select(item => item.Tuyen)
            .ToList(); // Lấy danh sách các segment đã chọn

        string title = string.Join(", ", selectedTuyen);
        var fiberPage = new FiberMapPage(title);
        fiberPage.SetSelectedSegments(selectedSegments); // Gọi phương thức để set giá trị
        await Navigation.PushAsync(fiberPage);
    }
    private async void NavigateToFiberMapClusteredPage()
    {
        List<int> selectedSegments = tuyenCapList
            .Where(item => item.IsSelected)
            .Select(item => item.IdSegment)
            .ToList(); // Lấy danh sách các segment đã chọn
        List<string?> selectedTuyen = tuyenCapList
            .Where(item => item.IsSelected)
            .Select(item => item.Tuyen)
            .ToList(); // Lấy danh sách các segment đã chọn

        string title = string.Join(", ", selectedTuyen);
        var fiberPage = new FiberMapClusteringPage(title);
        fiberPage.SetSelectedSegments(selectedSegments); // Gọi phương thức để set giá trị
        await Navigation.PushAsync(fiberPage);

    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Nếu userLocation chưa có dữ liệu, lấy từ Preferences
        double cachedLat = Preferences.Get("UserLat", 0.0);
        double cachedLon = Preferences.Get("UserLon", 0.0);
        Position   userLocation = new Position(cachedLat, cachedLon);
        
        //tuyenCapList = await SegmentItemCache.LoadSegmentItemsAsync(listView);
        tuyenCapList = await SegmentItemCache.LoadSegmentItemsAsync(listView, userLocation, Constants.radiusInKm);
        listView.ItemsSource = tuyenCapList;
    }
    private async void ChkAll_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {

        // Kiểm tra trạng thái của chkAll
        if (chkAll.IsChecked)
        {
            // Nếu chkAll được chọn, gọi lại LoadSegmentItemsAsync không có bộ lọc
            tuyenCapList = await SegmentItemCache.LoadSegmentItemsAsync(listView);
            listView.ItemsSource = tuyenCapList;  // Hoặc có thể gọi lại với tham số lọc nếu cần.
        }
        else
        {
            double cachedLat = Preferences.Get("UserLat", 0.0);
            double cachedLon = Preferences.Get("UserLon", 0.0);
            Position userLocation = new(cachedLat, cachedLon);

            tuyenCapList = await SegmentItemCache.LoadSegmentItemsAsync(listView, userLocation, Constants.radiusInKm);
            listView.ItemsSource = tuyenCapList;
        }
    }
}

