using CQApp.Data;
using CQApp.Models;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;


namespace CQApp.Views;

public partial class FiberDataPage : ContentPage
{
    private readonly ILogger _logger;
    
    readonly FiberItemDatabase database;
    private int _selectedSegmentId;
    public ObservableCollection<FiberItem> FiberItems { get; set; } = [];
    //public ObservableCollection<FiberItem> FiberItems { get; } = new ObservableCollection<FiberItem>();
    public ObservableCollection<FiberSegment> FiberSegments { get; set; } = [];

    //public FiberDataPage(FiberItemDatabase fiberItemDatabase, ILogger<FiberDataPage> logger)
    public FiberDataPage(FiberItemDatabase fiberItemDatabase)
    {
        InitializeComponent();
        database = fiberItemDatabase;
        BindingContext = this;
        //BindingContext = _viewModel;
        
    }
    public int SelectedSegmentId
    {
        get => _selectedSegmentId;
        set
        {
            if (_selectedSegmentId != value)
            {
                _selectedSegmentId = value;
                OnPropertyChanged(nameof(SelectedSegmentId));
                _ = LoadFiberItems(_selectedSegmentId); // Load lại danh sách FiberItem khi có sự thay đổi trong IdSegment                
            }
        }
    }
    // Hàm này được gọi khi IdSegment thay đổi
    private async Task LoadFiberItems(int selectedSegmentId)
    {
        var items = await database.GetItemsAsync(selectedSegmentId);

        FiberItems.Clear();
        foreach (var item in items)
            FiberItems.Add(item);

    }


    // Xử lý sự kiện khi một FiberSegment được chọn
    private void OnSegmentSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count > 0)
        {
            if (e.CurrentSelection[0] is FiberSegment selectedSegment)
            {
                SelectedSegmentId = selectedSegment.IdSegment;
            }
            else
            {
                // Xử lý trường hợp không phải FiberSegment
            }
        }
        else if (FiberSegments != null && FiberSegments.Count > 0)
        {
            SelectedSegmentId = FiberSegments[0].IdSegment;
        }
        else
        {
            _logger.LogInformation("Không có tuyến cáp nào được liệt kê.");
        }
    }
    //private void OnSegmentSelected(object sender, SelectionChangedEventArgs e)
    //{
    //    var selectedSegment = e.CurrentSelection.FirstOrDefault() as FiberSegment;

    //    if (selectedSegment != null)
    //    {
    //        SelectedSegmentId = selectedSegment.IdSegment;
    //    }
    //    else
    //    {
    //        if (FiberSegments != null && FiberSegments.Any())
    //        {
    //            var firstSegment = FiberSegments.FirstOrDefault();
    //            SelectedSegmentId = (int)(firstSegment?.IdSegment);
    //        }
    //        else
    //        {
    //            _logger.LogInformation("Không có tuyến cáp nào được liệt kê.");
    //        }
    //    }
    //}

    protected override async void OnAppearing()
{
    base.OnAppearing();

    if (SelectedSegmentId == 0)
    {
        SelectedSegmentId = 4; //tạm thời chỉ định đoạn cáp

        //if (FiberSegments != null && FiberSegments.Any())
        //{
        //    var firstSegment = FiberSegments.FirstOrDefault();
        //    SelectedSegmentId = (short)firstSegment?.IdSegment;
        //    await Shell.Current.GoToAsync(nameof(FiberItemPage), true, new Dictionary<string, object>
        //        {
        //            ["Item"] = firstSegment
        //        });
        //    }
        //else
        //{
        //    _logger.LogInformation("OnAppearing: The FiberSegments list is empty.");
        //}
    }  
}

//// Xử lý sự kiện khi một FiberSegment được chọn
//private async void OnSegmentSelected(object sender, SelectionChangedEventArgs e)
//{
//    var selectedSegment = e.CurrentSelection.FirstOrDefault() as FiberSegment;

//    if (selectedSegment != null)
//    {
//        SelectedSegmentId = selectedSegment.IdSegment;
//    }
//}
//protected override async void OnAppearing()
//{
//    base.OnAppearing();

//}
protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        //var items = await database.GetItemsAsync();
        var items = await database.GetItemsAsync(_selectedSegmentId);

        var segments = await database.GetSegmentsAsync();

        MainThread.BeginInvokeOnMainThread(() =>
        {
            FiberItems.Clear();
            foreach (var item in items)
                FiberItems.Add(item);

            FiberSegments.Clear();
            foreach (var item in segments)
                FiberSegments.Add(item);
        });
    }
    async void OnItemAdded(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(FiberItemPage), true, new Dictionary<string, object>
        {
            ["Item"] = new FiberItem()
        });
    }
    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count > 0)
        {
            if (e.CurrentSelection[0] is FiberItem item)
            {
                await Shell.Current.GoToAsync(nameof(FiberItemPage), true, new Dictionary<string, object>
                {
                    ["Item"] = item
                });
            }
        }
        //if (e.CurrentSelection.FirstOrDefault() is not FiberItem item)
        //    return;

        //await Shell.Current.GoToAsync(nameof(FiberItemPage), true, new Dictionary<string, object>
        //{
        //    ["Item"] = item
        //});

    }    
}