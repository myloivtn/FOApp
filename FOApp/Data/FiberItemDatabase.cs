using SQLite;
using FOApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System.Collections;
using System.Reflection.PortableExecutable;
//using Microsoft.Maui.Controls.Maps;
using System.Globalization;
using Microsoft.Maui.ApplicationModel;
//using Microsoft.Maui.Maps;
//using SkiaSharp;
//using SkiaSharp.Views.Maui.Controls.Hosting;
using System.IO;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using static FOApp.Models.CablePinData;

namespace FOApp.Data;
public class FiberItemDatabase
{
    //20/10/2024 Station
//    List<Station> stations = new List<Station>
//{
//    new Station { Name = "NVL", Latitude = 16.060951326168, Longitude = 108.2164001 },
//    new Station { Name = "AĐN2", Latitude = 16.079679426168, Longitude = 108.2363251 },
//    new Station { Name = "HHI", Latitude = 15.970147326168, Longitude = 108.2833568 },
//    new Station { Name = "HIN", Latitude = 15.926845, Longitude = 107.647258 },
//    new Station { Name = "PSN", Latitude = 15.4394355, Longitude = 107.7849724 },
//    new Station { Name = "QNI", Latitude = 15.132098526168, Longitude = 108.8011889 },
//    new Station { Name = "TKY", Latitude = 15.577916226168, Longitude = 108.4761718 },
//    new Station { Name = "AĐN1", Latitude = 16.075297126168, Longitude = 108.2339402 },
//    new Station { Name = "2T9", Latitude = 16.042027226168, Longitude = 108.2229038 },
//    new Station { Name = "40LL", Latitude = 16.078948626168, Longitude = 108.2192182 }
//};


    //8/9/2024
    //Thống kê
    public static async Task<(bool dataChanged, int totalItemsOnSQLite, int totalItemsOnServer, int totalLinesOnSQLite, int totalLinesOnServer)> CheckRecordStatisticsAsync(Label serverStatusLabel)
    {
        string thongbao = "";
        DateTime lastSyncTime = Preferences.Get("LastSyncTime", DateTime.MinValue); 

        try
        {
            // Show last sync time
            if (lastSyncTime != DateTime.MinValue)
            {
                thongbao = Environment.NewLine + $"Lần đồng bộ gần nhất: {lastSyncTime:G}" ;                
            }

            using var sqlConnection = new SqlConnection(Constants.sqlConnectionString);
            await sqlConnection.OpenAsync();

            var sqliteConnection = new SQLiteAsyncConnection(Constants.DatabasePath);
            var sqlCountItemQuery = "select count(*) from View_HSKT_CAPQUANG_HSHC where Diemdacbiet <> 'LineString'";
            var sqlCountSegmentQuery = "select count(distinct IdSegment) from View_HSKT_CAPQUANG_HSHC where Diemdacbiet <> 'LineString'";
            var sqlCountLinesQuery = "select count(*) from View_HSKT_CAPQUANG_HSHC where Diemdacbiet = 'LineString'";
            

            using var sqlCountSegmentCommand = new SqlCommand(sqlCountSegmentQuery, sqlConnection);
            object? resultSegment = await sqlCountSegmentCommand.ExecuteScalarAsync();
            int totalSegmentsOnServer = resultSegment != null && int.TryParse(resultSegment.ToString(), out var count) ? count : 0;

            using var sqlCountItemCommand = new SqlCommand(sqlCountItemQuery, sqlConnection);
            object? resultItem = await sqlCountItemCommand.ExecuteScalarAsync();
            int totalItemsOnServer = resultItem != null && int.TryParse(resultItem.ToString(), out count) ? count : 0;

            using var sqlCountLineCommand = new SqlCommand(sqlCountLinesQuery, sqlConnection);
            object? resultLine = await sqlCountLineCommand.ExecuteScalarAsync();
            int totalLinesOnServer = resultLine != null && int.TryParse(resultLine.ToString(), out count) ? count : 0;

            var tableItemExists = await sqliteConnection.ExecuteScalarAsync<int>(
                "SELECT count(*) FROM sqlite_master WHERE type='table' AND name='FiberItem'");

            int totalItemsOnSQLite = tableItemExists > 0
                ? await sqliteConnection.Table<FiberItem>().CountAsync()
                : 0;

            var tableLineExists = await sqliteConnection.ExecuteScalarAsync<int>(
               "SELECT count(*) FROM sqlite_master WHERE type='table' AND name='FiberLine'");

            int totalLinesOnSQLite = tableLineExists > 0
               ? await sqliteConnection.Table<FiberLine>().CountAsync()
               : 0;

            thongbao += Environment.NewLine + $"Dữ liệu SQL: Số tuyến cáp = {totalSegmentsOnServer}, Điểm đặc biệt = {totalItemsOnServer}, Dữ liệu vẽ tuyến cáp= {totalLinesOnServer}";            
            serverStatusLabel.Text += thongbao;

            bool dataChanged =  (totalItemsOnServer != totalItemsOnSQLite) || (totalLinesOnServer != totalLinesOnSQLite); ;

            return (dataChanged, totalItemsOnSQLite, totalItemsOnServer, totalLinesOnSQLite, totalLinesOnServer);
        }
        catch (Exception ex)
        {
            thongbao += Environment.NewLine + "Lỗi xảy ra: " + ex.Message;
            serverStatusLabel.Text += thongbao;
            return (false, 0, 0, 0,0); 
        }
    }

    public static async Task<string> SyncPinDataAsync(ProgressBar progressBar, Label progressDetailLabel, Label serverStatusLabel, int totalItemRecords)
    {
        string thongbao = "";

        try
        {
            // Connect to SQL Server
            using var sqlConnection = new SqlConnection(Constants.sqlConnectionString);
            await sqlConnection.OpenAsync();

            // Connect to SQLite
            var sqliteConnection = new SQLiteAsyncConnection(Constants.DatabasePath);

            // ============= Đồng bộ FiberItem =============
            {
                string? sqlQueryItem = "exec sp_HSKT_CAPQUANG_Mapdata";
                SqlCommand? sqlCommandItem = new(sqlQueryItem, sqlConnection);
                using var readerItem = await sqlCommandItem.ExecuteReaderAsync();

                // Delete old data and insert new data for FiberItem
                await sqliteConnection.DropTableAsync<FiberItem>();
                await sqliteConnection.CreateTableAsync<FiberItem>();
                serverStatusLabel.Text += Environment.NewLine + "Đã xóa bảng FiberItem từ SQLite";

                int itemCount = 0;
                while (await readerItem.ReadAsync())
                {
                    var fiberItem = new FiberItem
                    {
                        Id = readerItem["Id"] != DBNull.Value ? Convert.ToInt16(readerItem["Id"]) : 0,
                        IdSegment = readerItem["IdSegment"] != DBNull.Value ? Convert.ToInt16(readerItem["IdSegment"]) : 0,
                        Tuyencap = readerItem["Tuyencap"] as string,
                        Doan = readerItem["Doan"] as string,
                        //Chieudaituyen = readerItem["Chieudaituyen"] != DBNull.Value ? Convert.ToSingle(readerItem["Chieudaituyen"]) : 0,
                        //Lytrinh1 = readerItem["Lytrinh1"] != DBNull.Value ? Convert.ToSingle(readerItem["Lytrinh1"]) : 0,
                        //Lytrinh2 = readerItem["Lytrinh2"] != DBNull.Value ? Convert.ToSingle(readerItem["Lytrinh2"]) : 0,
                        //GPSLytrinh1 = readerItem["GPSLytrinh1"] as string,
                        //GPSLytrinh2 = readerItem["GPSLytrinh2"] as string,
                        Diemdacbiet = readerItem["Diemdacbiet"] as string,
                        Phuongthuclapdat = readerItem["Phuongthuclapdat"] as string,
                        //Sotrudienluc = readerItem["Sotrudienluc"] as string,
                        Chieudai = readerItem["Chieudai"] as string,
                        Dosau = readerItem["Dosau"] as string,
                        //DocaoTDL = readerItem["DocaoTDL"] as string,
                        Huongtuyen = readerItem["Huongtuyen"] as string,
                        Vitrituyen = readerItem["Vitrituyen"] as string,
                        Loaicap = readerItem["Loaicap"] != DBNull.Value ? Convert.ToInt16(readerItem["Loaicap"]) : 0,
                        Ghichu = readerItem["Ghichu"] as string,
                        Tenduong = readerItem["Tenduong"] as string,
                        Latitude = readerItem["Latitude"] != DBNull.Value ? Convert.ToSingle(readerItem["Latitude"]) : 0,
                        Longitude = readerItem["Longitude"] != DBNull.Value ? Convert.ToSingle(readerItem["Longitude"]) : 0
                        //Tencaucong = readerItem["Tencaucong"] as string,
                        //Done = false,
                    };

                    await sqliteConnection.InsertAsync(fiberItem);
                    itemCount++;

                    progressBar.Progress = (double)itemCount / totalItemRecords;
                    progressDetailLabel.Text = Environment.NewLine + $"Đã tải {itemCount}/{totalItemRecords} điểm cáp quang từ Server.";
                }
                serverStatusLabel.Text += Environment.NewLine + $"Đã đồng bộ {itemCount} điểm cáp quang về SQLite";
            }
            
            // ============= Hoàn tất đồng bộ =============
            DateTime syncTime = DateTime.Now;
            Preferences.Set("LastSyncTime", syncTime);
            thongbao += $"Đồng bộ hoàn tất vào lúc: {syncTime:G}" + Environment.NewLine;

            return thongbao;
        }
        catch (Exception ex)
        {
            thongbao += Environment.NewLine + "Lỗi xảy ra: " + ex.Message;
            serverStatusLabel.Text += thongbao;
            return thongbao;
        }
    }
    public static async Task<string> SyncLineDataAsync(ProgressBar progressBar, Label progressDetailLabel, Label serverStatusLabel, int totalItemRecords)
    {
        string thongbao = "";

        try
        {
            // Connect to SQL Server
            using var sqlConnection = new SqlConnection(Constants.sqlConnectionString);
            await sqlConnection.OpenAsync();

            // Connect to SQLite
            var sqliteConnection = new SQLiteAsyncConnection(Constants.DatabasePath);


            // ============= Đồng bộ FiberLine =============
            {
                string? sqlQueryLine = "exec sp_HSKT_CAPQUANG_Mapdata 'Line'";
                SqlCommand? sqlCommandLine = new(sqlQueryLine, sqlConnection);
                using var readerLine = await sqlCommandLine.ExecuteReaderAsync();

                await sqliteConnection.DropTableAsync<FiberLine>();
                await sqliteConnection.CreateTableAsync<FiberLine>();
                serverStatusLabel.Text += Environment.NewLine + "Đã xóa bảng FiberLine từ SQLite";

                int lineCount = 0;
                while (await readerLine.ReadAsync())
                {
                    var fiberLine = new FiberLine
                    {
                        Id = readerLine["Id"] != DBNull.Value ? Convert.ToInt32(readerLine["Id"]) : 0,
                        IdSegment = readerLine["IdSegment"] != DBNull.Value ? Convert.ToInt32(readerLine["IdSegment"]) : 0,
                        Order = readerLine["Loaicap"] != DBNull.Value ? Convert.ToInt32(readerLine["Loaicap"]) : (int?)null,
                        Tuyencap = readerLine["Tuyencap"] as string,
                        Latitude = readerLine["Latitude"] != DBNull.Value ? Convert.ToDouble(readerLine["Latitude"]) : 0,
                        Longitude = readerLine["Longitude"] != DBNull.Value ? Convert.ToDouble(readerLine["Longitude"]) : 0
                    };
                    await sqliteConnection.InsertAsync(fiberLine);
                    lineCount++;

                    progressBar.Progress = (double)(lineCount++ + lineCount) / totalItemRecords;
                    progressDetailLabel.Text = Environment.NewLine + $"Đã tải {lineCount}/{totalItemRecords} đoạn cáp quang từ Server.";
                }
                serverStatusLabel.Text += Environment.NewLine + $"Đã đồng bộ {lineCount} đoạn cáp quang về SQLite";
            }
            // ============= Hoàn tất đồng bộ =============
            DateTime syncTime = DateTime.Now;
            Preferences.Set("LastSyncTime", syncTime);
            thongbao += $"Đồng bộ hoàn tất vào lúc: {syncTime:G}" + Environment.NewLine;

            return thongbao;
        }
        catch (Exception ex)
        {
            thongbao += Environment.NewLine + "Lỗi xảy ra: " + ex.Message;
            serverStatusLabel.Text += thongbao;
            return thongbao;
        }
    }

    public static async Task<string> SyncDataAsync(ProgressBar progressBar, Label progressDetailLabel, Label serverStatusLabel, int totalItemRecords)
    {
        string thongbao = "";

        try
        {
            // Connect to SQL Server
            using var sqlConnection = new SqlConnection(Constants.sqlConnectionString);
            await sqlConnection.OpenAsync();

            // Connect to SQLite
            var sqliteConnection = new SQLiteAsyncConnection(Constants.DatabasePath);

            // ============= Đồng bộ FiberItem =============
            {
                string? sqlQueryItem = "exec sp_HSKT_CAPQUANG_Mapdata";
                SqlCommand? sqlCommandItem = new(sqlQueryItem, sqlConnection);
                using var readerItem = await sqlCommandItem.ExecuteReaderAsync();

                // Delete old data and insert new data for FiberItem
                await sqliteConnection.DropTableAsync<FiberItem>();
                await sqliteConnection.CreateTableAsync<FiberItem>();
                serverStatusLabel.Text += Environment.NewLine + "Đã xóa bảng FiberItem từ SQLite";

                int itemCount = 0;
                while (await readerItem.ReadAsync())
                {
                    var fiberItem = new FiberItem
                    {
                        Id = readerItem["Id"] != DBNull.Value ? Convert.ToInt16(readerItem["Id"]) : 0,
                        IdSegment = readerItem["IdSegment"] != DBNull.Value ? Convert.ToInt16(readerItem["IdSegment"]) : 0,
                        Tuyencap = readerItem["Tuyencap"] as string,
                        Doan = readerItem["Doan"] as string,
                        //Chieudaituyen = readerItem["Chieudaituyen"] != DBNull.Value ? Convert.ToSingle(readerItem["Chieudaituyen"]) : 0,
                        //Lytrinh1 = readerItem["Lytrinh1"] != DBNull.Value ? Convert.ToSingle(readerItem["Lytrinh1"]) : 0,
                        //Lytrinh2 = readerItem["Lytrinh2"] != DBNull.Value ? Convert.ToSingle(readerItem["Lytrinh2"]) : 0,
                        //GPSLytrinh1 = readerItem["GPSLytrinh1"] as string,
                        //GPSLytrinh2 = readerItem["GPSLytrinh2"] as string,
                        Diemdacbiet = readerItem["Diemdacbiet"] as string,
                        Phuongthuclapdat = readerItem["Phuongthuclapdat"] as string,
                        //Sotrudienluc = readerItem["Sotrudienluc"] as string,
                        Chieudai = readerItem["Chieudai"] as string,
                        Dosau = readerItem["Dosau"] as string,
                        //DocaoTDL = readerItem["DocaoTDL"] as string,
                        Huongtuyen = readerItem["Huongtuyen"] as string,
                        Vitrituyen = readerItem["Vitrituyen"] as string,
                        Loaicap = readerItem["Loaicap"] != DBNull.Value ? Convert.ToInt16(readerItem["Loaicap"]) : 0,
                        Ghichu = readerItem["Ghichu"] as string,
                        Tenduong = readerItem["Tenduong"] as string,
                        Latitude = readerItem["Latitude"] != DBNull.Value ? Convert.ToSingle(readerItem["Latitude"]) : 0,
                        Longitude = readerItem["Longitude"] != DBNull.Value ? Convert.ToSingle(readerItem["Longitude"]) : 0
                        //Tencaucong = readerItem["Tencaucong"] as string,
                        //Done = false,
                    };
                                        
                    await sqliteConnection.InsertAsync(fiberItem);
                    itemCount++;

                    progressBar.Progress = (double)itemCount / totalItemRecords;
                    progressDetailLabel.Text = Environment.NewLine + $"Đã tải {itemCount}/{totalItemRecords} điểm cáp quang từ Server.";
                }
                serverStatusLabel.Text += Environment.NewLine + $"Đã đồng bộ {itemCount} điểm cáp quang về SQLite";
            }
            // ============= Đồng bộ FiberLine =============
            {
                string? sqlQueryLine = "exec sp_HSKT_CAPQUANG_Mapdata 'Line'";
                SqlCommand? sqlCommandLine = new(sqlQueryLine, sqlConnection);
                using var readerLine = await sqlCommandLine.ExecuteReaderAsync();

                await sqliteConnection.DropTableAsync<FiberLine>();
                await sqliteConnection.CreateTableAsync<FiberLine>();
                serverStatusLabel.Text += Environment.NewLine + "Đã xóa bảng FiberLine từ SQLite";

                int lineCount = 0;
                while (await readerLine.ReadAsync())
                {
                    var fiberLine = new FiberLine
                    {
                        Id = readerLine["Id"] != DBNull.Value ? Convert.ToInt32(readerLine["Id"]) : 0,
                        IdSegment = readerLine["IdSegment"] != DBNull.Value ? Convert.ToInt32(readerLine["IdSegment"]) : 0,
                        Order = readerLine["Loaicap"] != DBNull.Value ? Convert.ToInt32(readerLine["Loaicap"]) : (int?)null,
                        Tuyencap = readerLine["Tuyencap"] as string,
                        Latitude = readerLine["Latitude"] != DBNull.Value ? Convert.ToDouble(readerLine["Latitude"]) : 0,
                        Longitude = readerLine["Longitude"] != DBNull.Value ? Convert.ToDouble(readerLine["Longitude"]) : 0
                    };
                    await sqliteConnection.InsertAsync(fiberLine);
                    lineCount++;

                    progressBar.Progress = (double)(lineCount++ + lineCount) / totalItemRecords;
                    progressDetailLabel.Text = Environment.NewLine + $"Đã tải {lineCount}/{totalItemRecords} đoạn cáp quang từ Server.";
                }
                serverStatusLabel.Text += Environment.NewLine + $"Đã đồng bộ {lineCount} đoạn cáp quang về SQLite";
            }
            // ============= Hoàn tất đồng bộ =============
            DateTime syncTime = DateTime.Now;
            Preferences.Set("LastSyncTime", syncTime);
            thongbao += $"Đồng bộ hoàn tất vào lúc: {syncTime:G}" + Environment.NewLine;

            return thongbao;
        }
        catch (Exception ex)
        {
            thongbao += Environment.NewLine + "Lỗi xảy ra: " + ex.Message;
            serverStatusLabel.Text += thongbao;
            return thongbao;
        }
    }
    public static async Task<bool> CheckServerConnectionAsync()
    {
        try
        {
            using var ping = new Ping();
            var pingReply = await ping.SendPingAsync(Constants.serverIPAddress, 1000); // Timeout 1 second

            if (pingReply.Status == IPStatus.Success)
            {
                return true; 
            }
        }
        catch (Exception)
        {
            return false;
        }
        return false;
    }
}
