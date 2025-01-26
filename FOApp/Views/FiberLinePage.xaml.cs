namespace CQApp.Views;
using CQApp.Data;
using SQLite;
using CQApp.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;
using static CQApp.Data.FiberItemDatabase;
using Microsoft.Maui.Graphics;
using System.Reflection;
using SkiaSharp;
using SkiaSharp.Views.Maui.Controls.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CQApp.Services.CachedService;

public partial class FiberLinePage : ContentPage
{
    private bool isListViewVisible = true;
    private ListView segmentListView;
    private Map map;
    private Grid mainGrid;
    
    private Polyline sementLine;
    private Picker segmentPicker;

    private int idsegment = 3;
    public class CustomPin : Pin
    {
        public string ImageSource { get; set; }
    }

    [Obsolete]
    public FiberLinePage()
    {

        InitializeComponent();
        //Title = "Xem tuyến cáp";
        // Tạo StackLayout và các thành phần con của nó
        var stackLayout = new StackLayout();

        // Tạo HorizontalStackLayout cho các nút
        var buttonLayout = new HorizontalStackLayout
        {
            Spacing = 10,
            HorizontalOptions = LayoutOptions.Center
        };
        // Tạo nút để ẩn/hiện ListView
        var toggleButton = new Button
        {
            Text = "Chọn tuyến"
        };
        toggleButton.Clicked += ToggleListViewVisibility;

        // Thêm nút vào StackLayout
        //stackLayout.Children.Add(toggleButton);

        // Tạo ListView
        segmentListView = new ListView
        {
            ItemTemplate = new DataTemplate(() =>
            {
                var grid = new Grid();
                var idLabel = new Label();
                var checkBox = new CheckBox();

                idLabel.SetBinding(Label.TextProperty, "DisplayText");

                checkBox.SetBinding(CheckBox.IsCheckedProperty, "IsSelected");

                grid.Children.Add(idLabel);
                grid.Children.Add(checkBox);

                return new ViewCell { View = grid };
            }),
            HeightRequest = 200, // Thiết lập chiều cao ban đầu của ListView
            IsVisible = false // Ẩn ListView mặc định
        };

        // Thêm ListView vào StackLayout
        stackLayout.Children.Add(segmentListView);

        // Tạo bản đồ
        map = new Map
        {
            MapType = MapType.Street,
            VerticalOptions = LayoutOptions.FillAndExpand
        };
        
        // Thêm bản đồ vào StackLayout
        stackLayout.Children.Add(map);

        // Tạo nút để thêm polyline
        var polylineButton = new Button
        {
            Text = "Xem tuyến cáp"
        };
        polylineButton.Clicked += AddPolylineClicked;
        //stackLayout.Children.Add(polylineButton);

        // Tạo nút để xóa các pin
        var clearButton = new Button
        {
            Text = "Xóa"
        };
        clearButton.Clicked += ClearClicked;
        //stackLayout.Children.Add(clearButton);        
        stackLayout.Children.Add(buttonLayout);
        // Đặt StackLayout làm nội dung của trang
        Content = stackLayout;
        // Thêm các nút vào HorizontalStackLayout
        buttonLayout.Add(toggleButton);
        buttonLayout.Add(clearButton);
        buttonLayout.Add(polylineButton);

        // Tải dữ liệu vào ListView
        LoadSegmentItemsAsync();

        map.MoveToRegion(
            MapSpan.FromCenterAndRadius(
                new Location(15.965524, 108.285633), Distance.FromMiles(1)));

    }
    
    private async Task LoadSegmentItemsAsync()
    {
        var segmentItems = await SegmentItemCache.GetSegmentFromSQLiteAsync();
        segmentListView.ItemsSource = segmentItems;
        int firstsegment = segmentItems.FirstOrDefault().IdSegment;
        sementLine = await FiberItemDatabase.SetupPolylineAsync(firstsegment);
        if (!map.MapElements.Contains(sementLine))
        {
            map.MapElements.Add(sementLine);
        }
        if (sementLine != null && sementLine.Geopath.Count > 0)
        {
            double totalLatitude = 0;
            double totalLongitude = 0;
            foreach (var location in sementLine.Geopath)
            {
                totalLatitude += location.Latitude;
                totalLongitude += location.Longitude;
            }

            double centerLatitude = totalLatitude / sementLine.Geopath.Count;
            double centerLongitude = totalLongitude / sementLine.Geopath.Count;
            var centerLocation = new Location(centerLatitude, centerLongitude);

            map.MoveToRegion(
                MapSpan.FromCenterAndRadius(centerLocation, Distance.FromMiles(1)));

            if (!map.MapElements.Contains(sementLine))
            {
                map.MapElements.Add(sementLine);
            }
        }
    }
    private void ToggleListViewVisibility(object sender, EventArgs e)
    {
        if (isListViewVisible)
        {
            segmentListView.IsVisible = false;
        }
        else
        {
            segmentListView.IsVisible = true;
        }
        isListViewVisible = !isListViewVisible;
        segmentListView.IsVisible = isListViewVisible;
    }
    private async Task InitializePolylineAsync(int segmentId)
    {
        sementLine = await FiberItemDatabase.SetupPolylineAsync(segmentId);
    }

    private async void AddPolylineClicked(object sender, EventArgs e)
    {
        var selectedSegments = ((List<SegmentItem>)segmentListView.ItemsSource)
            .Where(item => item.IsSelected)
            .Select(item => item.IdSegment)
            .ToList();

        if (selectedSegments.Count > 0)
        {
            var segmentLocations = await FiberItemDatabase.GetLocationsFromSQLiteAsync(selectedSegments);
            var allLocations = new List<Location>();

            foreach (var segment in segmentLocations)
            {
                var sementLine = new Polyline
                {
                    StrokeColor = Colors.Blue,
                    StrokeWidth = 12,
                };
                foreach (var locationWithLabel in segment.Value)
                {
                    sementLine.Geopath.Add(locationWithLabel.Location);
                    allLocations.Add(locationWithLabel.Location);
                    var pin = new Pin
                    {
                        Type = PinType.Place,
                        Location = new Location(locationWithLabel.Location.Latitude, locationWithLabel.Location.Longitude),
                        Label = locationWithLabel.Label,
                        Address = locationWithLabel.Address
                        //Icon = BitmapDescriptorFactory.FromView(icon)
                    };
                    map.Pins.Add(pin);
                }
                if (!map.MapElements.Contains(sementLine))
                {
                    map.MapElements.Add(sementLine);
                }
            }

            // Center map to the average location of all selected segments
            if (allLocations.Any())
            {
                var averageLatitude = allLocations.Average(loc => loc.Latitude);
                var averageLongitude = allLocations.Average(loc => loc.Longitude);
                var centerLocation = new Location(averageLatitude, averageLongitude);

                map.MoveToRegion(
                    MapSpan.FromCenterAndRadius(centerLocation, Distance.FromMiles(15)));
            }
        }
        // Thu gọn ListView khi thêm polyline
        if (isListViewVisible)
        {
            ToggleListViewVisibility(sender, e);
        }
    }

    private void ClearClicked(object sender, EventArgs e)
    {
        map.Pins.Clear(); // Xóa tất cả các Pin
        map.MapElements.Clear();
    }
    private async void SegmentPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedSegmentId = (int)picker.SelectedItem;
        await InitializePolylineAsync(selectedSegmentId);

        if (sementLine != null && sementLine.Geopath.Count > 0)
        {
            double totalLatitude = 0;
            double totalLongitude = 0;
            foreach (var location in sementLine.Geopath)
            {
                totalLatitude += location.Latitude;
                totalLongitude += location.Longitude;
            }

            double centerLatitude = totalLatitude / sementLine.Geopath.Count;
            double centerLongitude = totalLongitude / sementLine.Geopath.Count;
            var centerLocation = new Location(centerLatitude, centerLongitude);

            map.MoveToRegion(
                MapSpan.FromCenterAndRadius(centerLocation, Distance.FromMiles(1)));

            if (!map.MapElements.Contains(sementLine))
            {
                map.MapElements.Add(sementLine);
            }
        }
    }
}