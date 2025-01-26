using System.Collections.ObjectModel;
using System.Net.NetworkInformation;
using FOApp.Data;
using FOApp.Models;
using Microsoft.Data.SqlClient;

namespace FOApp.Views;

public partial class FiberListPage : ContentPage
{    
    int totalItemsOnSQLite, totalItemsOnServer, totalLinesOnSQLite, totalLinesOnServer;
    public FiberListPage()
    {
        InitializeComponent();        
        BindingContext = this;
    }

    private CancellationTokenSource? _cancellationTokenSource;

    private async Task StartPeriodicConnectionCheckAsync(TimeSpan interval)
    {       
        _cancellationTokenSource?.Dispose(); // Dispose of the old one if it exists
        _cancellationTokenSource = new CancellationTokenSource();
        var token = _cancellationTokenSource.Token;

        try
        {
            while (!token.IsCancellationRequested)
            {
                await RetryConnectionButton_Clicked(null, EventArgs.Empty);

                // Wait for smaller chunks of time to avoid long delays
                var remainingInterval = interval;
                while (remainingInterval > TimeSpan.Zero && !token.IsCancellationRequested)
                {
                    var delayChunk = TimeSpan.FromMilliseconds(100); // Small delay chunk
                    await Task.Delay(delayChunk, token); // Respect the cancellation token
                    remainingInterval -= delayChunk;
                }
            }
        }
        catch (TaskCanceledException)
        {
            // Handle the cancellation gracefully
        }
        finally
        {
            // Dispose only if not already disposed
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null; // Ensure it's null after disposing
        }
    }

    private void StopPeriodicConnectionCheck()
    {
        if (_cancellationTokenSource != null && !_cancellationTokenSource.IsCancellationRequested)
        {
            _cancellationTokenSource.Cancel();
        }

        // Avoid disposing if the task is still running, so we don't get disposed errors
    }

    private async Task RetryConnectionButton_Clicked(object? sender, EventArgs e)
    {
        // Kiểm tra lại trạng thái kết nối
        bool isConnected = await FiberItemDatabase.CheckServerConnectionAsync();

        if (isConnected)
        {
            // Nếu kết nối thành công, cập nhật biểu tượng và trạng thái
            ServerStatusIcon.Source = "connect_icon.png";
            //serverStatusLabel.Text += Environment.NewLine + "Kết nối SQL Server thành công!";
            serverStatusLabel.TextColor = Colors.Green;
            SyncButton.IsEnabled = true;  // Enable nút "Get Data"
            SyncPinButton.IsEnabled = true;  // Enable nút "Get Pin Data"
            SyncLineButton.IsEnabled = true;  // Enable nút "Get Line Data"

            // Dừng kiểm tra kết nối định kỳ
            StopPeriodicConnectionCheck();
        }
        else
        {
            // Nếu chưa kết nối được, cập nhật biểu tượng và hiển thị thông báo
            ServerStatusIcon.Source = "alert_icon.png";
            serverStatusLabel.Text += Environment.NewLine + "Không thể kết nối đến SQL Server, đang thử lại...";
            serverStatusLabel.TextColor = Colors.Red;
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Tự động kiểm tra kết nối khi trang xuất hiện
        await RetryConnectionButton_Clicked(null, EventArgs.Empty);

        if (ServerStatusIcon.Source is FileImageSource fileImageSource && fileImageSource.File == "alert_icon.png")
        {
            await StartPeriodicConnectionCheckAsync(TimeSpan.FromMilliseconds(100));
        }
        else
        {
            var result = await FiberItemDatabase.CheckRecordStatisticsAsync(serverStatusLabel);
          
            // Lấy các giá trị từ tuple trả về
            bool dataChanged = result.dataChanged;            
            totalItemsOnServer = result.totalItemsOnServer;
            totalItemsOnSQLite = result.totalItemsOnSQLite;
            totalLinesOnServer = result.totalLinesOnServer;
            totalLinesOnSQLite = result.totalLinesOnSQLite;
           
            // Sử dụng các giá trị này theo nhu cầu
            if (dataChanged)
            {
                serverStatusLabel.Text += Environment.NewLine + $"Dữ liệu điểm cáp đã thay đổi: " + Environment.NewLine + $" + Tổng số điểm cáp trên SQLite: {totalItemsOnSQLite}" + Environment.NewLine + $" + Tổng số điểm cáp trên SQL Server: {totalItemsOnServer}";
                serverStatusLabel.Text += Environment.NewLine + $"Dữ liệu tuyến cáp đã thay đổi: " + Environment.NewLine + $" + Số điểm vẽ tuyến cáp SQLite: {totalLinesOnSQLite}" + Environment.NewLine + $" + Số điểm vẽ tuyến cáp trên SQL Server: {totalLinesOnServer}";
            }
            else
            {
                serverStatusLabel.Text += Environment.NewLine + "Dữ liệu không thay đổi.";
            }
            SyncButton.IsEnabled = dataChanged;
        }
    }

    protected override void OnDisappearing()
    {
        // Dừng kiểm tra kết nối khi trang biến mất
        ProgressDetailLabel.Text = "";
        serverStatusLabel.Text = "";
        StopPeriodicConnectionCheck();
        
        base.OnDisappearing();
    }

    private async void SyncButton_Clicked(object sender, EventArgs e)
    {   
        string result = await FiberItemDatabase.SyncDataAsync(DownloadProgressBar, ProgressDetailLabel, serverStatusLabel,totalItemsOnServer);
        await DisplayAlert("Kết quả", result, "OK");
        SyncButton.IsEnabled = false;
    }
    private async void SyncLineButton_Clicked(object sender, EventArgs e)
    {
        string result = await FiberItemDatabase.SyncLineDataAsync(DownloadProgressBar, ProgressDetailLabel, serverStatusLabel, totalLinesOnServer);
        await DisplayAlert("Kết quả", result, "OK");
        SyncButton.IsEnabled = false;
    }
    private async void SyncPinButton_Clicked(object sender, EventArgs e)
    {
        string result = await FiberItemDatabase.SyncPinDataAsync(DownloadProgressBar, ProgressDetailLabel, serverStatusLabel, totalItemsOnServer);
        await DisplayAlert("Kết quả", result, "OK");
        SyncButton.IsEnabled = false;
    }
}
