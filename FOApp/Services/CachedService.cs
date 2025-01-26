using SQLite;
using FOApp.Models;
using Maui.GoogleMaps;

namespace FOApp.Services
{
    internal class CachedService
    {
        public static class SegmentItemCache
        {
            public static List<SegmentItem>? CachedSegmentItems { get; private set; }

            //    public static async Task<List<SegmentItem>> GetSegmentFromSQLiteAsync(List<int> segmentSelectlist)
            //    {
            //        if (CachedSegmentItems != null && CachedSegmentItems.Any())
            //        {
            //            return CachedSegmentItems;
            //        }

            //        var sqliteConnection = new SQLiteAsyncConnection(Constants.DatabasePath);
            //        var segmentItems = new List<SegmentItem>();

            //        try
            //        {
            //            await sqliteConnection.CreateTableAsync<FiberSegment>();
            //            var fiberSegments = await sqliteConnection.Table<FiberSegment>()
            //                                                       .Where(segment => segmentSelectlist.Contains(segment.IdSegment))
            //                                                       .ToListAsync();

            //            segmentItems = fiberSegments.Select(segment => new SegmentItem
            //            {
            //                IdSegment = segment.IdSegment,
            //                Tuyen = segment.Tuyen,
            //                LoaicapFO = segment.LoaicapFO,
            //                IsSelected = false
            //            }).ToList();

            //            CachedSegmentItems = segmentItems;
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine("Lỗi khi lấy thông tin từ SQLite: " + ex.Message);
            //        }

            //        return segmentItems;
            //    }
            public static async Task<List<SegmentItem>> LoadSegmentItemsAsync(ListView segmentListView, Position userLocation, double radiusInKm)
            {
                var selectedSegmentsString = Preferences.Get("IdSegmentList", string.Empty);
                var selectedSegmentIds = string.IsNullOrEmpty(selectedSegmentsString)
                    ? []
                    : selectedSegmentsString.Split(',')
                                            .Select(int.Parse)
                                            .ToList();

                var sqliteConnection = new SQLiteAsyncConnection(Constants.DatabasePath);
                var segmentItems = new List<SegmentItem>();

                try
                {
                    // Nếu userLocation chưa có dữ liệu, lấy từ Preferences
                    if (userLocation.Latitude == 0.0 || userLocation.Longitude == 0.0)
                    {
                        double cachedLat = Preferences.Get("UserLat", 0.0);
                        double cachedLon = Preferences.Get("UserLon", 0.0);
                        userLocation = new Position(cachedLat, cachedLon);
                    }
                    // Bán kính tính bằng độ (1 độ = ~111.32 km)
                    double radiusInDegrees = radiusInKm / 111.32;

                    // Tính các giá trị giới hạn
                    double minLatitude = userLocation.Latitude - radiusInDegrees;
                    double maxLatitude = userLocation.Latitude + radiusInDegrees;
                    double minLongitude = userLocation.Longitude - radiusInDegrees;
                    double maxLongitude = userLocation.Longitude + radiusInDegrees;

                    // Lấy tất cả dữ liệu từ bảng FiberItem
                    var fiberItems = await sqliteConnection.Table<FiberItem>().ToListAsync();

                    // Lọc các tuyến có điểm gần nhất trong phạm vi radiusKm
                    var filteredFiberItems = fiberItems
                        .Where(f => f.Latitude >= minLatitude && f.Latitude <= maxLatitude &&
                                    f.Longitude >= minLongitude && f.Longitude <= maxLongitude)
                        .GroupBy(f => f.IdSegment)
                        .Select(g => new
                        {
                            IdSegment = g.Key,
                            TenTuyen = g.First().Tuyencap,
                            LoaiCap = g.First().Loaicap ?? 0,

                            // Tính số lượng điểm "Măng_xông" trên tuyến
                            SoMangXong = g.Count(item => item.Diemdacbiet == "Măng_xông"),

                            // Tính tổng độ dài tuyến dựa vào GPS
                            TongDoDai = Mapservice.CalculateTotalLength([.. g])
                        })
                        .ToList();


                    segmentItems = filteredFiberItems.Select(fiberItem => new SegmentItem
                    {
                        IdSegment = fiberItem.IdSegment,
                        Tuyen = fiberItem.TenTuyen,
                        LoaicapFO = fiberItem.LoaiCap,
                        SoMangXong = fiberItem.SoMangXong,
                        TongDoDai = fiberItem?.TongDoDai ?? 0,
                        IsSelected = false
                    }).ToList();

                    
                    CachedSegmentItems = segmentItems;
                    segmentListView.ItemsSource = segmentItems;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi lấy thông tin từ SQLite: " + ex.Message);
                }

                return segmentItems;
            }
            //23/01/2025: Lấy tất cả tuyến cáp
            public static async Task<List<SegmentItem>> LoadSegmentItemsAsync(ListView segmentListView)
            {
                //CachedSegmentItems = null;
                //Lấy danh sách IdSegment từ Preferences theo iduser
                var selectedSegmentsString = Preferences.Get("IdSegmentList", string.Empty);
                var selectedSegmentIds = string.IsNullOrEmpty(selectedSegmentsString)
                    ? []
                    : selectedSegmentsString.Split(',')
                                            .Select(int.Parse)
                                            .ToList();

                var sqliteConnection = new SQLiteAsyncConnection(Constants.DatabasePath);
                var segmentItems = new List<SegmentItem>();

                // Ánh xạ từ FiberItem sang SegmentItem
                try
                {
                    // Lấy tất cả dữ liệu từ bảng FiberItem
                    var fiberItems = await sqliteConnection.Table<FiberItem>().ToListAsync();

                    // Nhóm dữ liệu theo IdSegment và lấy bản ghi đầu tiên của mỗi nhóm
                    var uniqueFiberItems = fiberItems
                        .GroupBy(f => f.IdSegment)   // Nhóm theo IdSegment
                        .Select(g => g.First())      // Lấy bản ghi đầu tiên trong mỗi nhóm
                        .Select(f => new
                        {
                            f.IdSegment,  // Chọn IdSegment
                            TenTuyen = f.Tuyencap,       // Chọn TenTuyen
                            LoaiCap = f.Loaicap     // Chọn LoaiCap
                        })
                        .ToList();

                    segmentItems = uniqueFiberItems.Select(fiberItem => new SegmentItem
                    {
                        IdSegment = fiberItem.IdSegment,
                        Tuyen = fiberItem.TenTuyen,
                        LoaicapFO = fiberItem.LoaiCap,
                        IsSelected = false
                    }).ToList();
                    // Lưu danh sách vào bộ nhớ đệm
                    CachedSegmentItems = segmentItems;
                    // Gán danh sách segmentItems cho ListView
                    segmentListView.ItemsSource = segmentItems;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi lấy thông tin từ SQLite: " + ex.Message);
                }

                return segmentItems;
            }
            
            //public static async Task<List<SegmentItem>> GetSegmentFromSQLiteAsync()
            //{
            //    // Lấy danh sách IdSegment từ Preferences theo iduser
            //    var selectedSegmentsString = Preferences.Get("IdSegmentList", string.Empty);
            //    var selectedSegmentIds = string.IsNullOrEmpty(selectedSegmentsString)
            //        ? []
            //        : selectedSegmentsString.Split(',')
            //                                .Select(int.Parse)
            //                                .ToList();

            //    var sqliteConnection = new SQLiteAsyncConnection(Constants.DatabasePath);
            //    var segmentItems = new List<SegmentItem>();

            //    try
            //    {
            //        await sqliteConnection.CreateTableAsync<FiberSegment>();
            //        var fiberSegments = await sqliteConnection.Table<FiberSegment>()
            //                                                   .Where(segment => selectedSegmentIds.Contains(segment.IdSegment))
            //                                                   .ToListAsync();

            //        segmentItems = fiberSegments.Select(segment => new SegmentItem
            //        {
            //            IdSegment = segment.IdSegment,
            //            Tuyen = segment.Tuyen,
            //            LoaicapFO = segment.LoaicapFO,
            //            //IsSelected = false
            //        }).ToList();

            //        // Lưu danh sách vào bộ nhớ đệm
            //        CachedSegmentItems = segmentItems;
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("Lỗi khi lấy thông tin từ SQLite: " + ex.Message);
            //    }

            //    return segmentItems;
            //}

            public static void ClearCache()
            {
                CachedSegmentItems = null;
            }
            //}
            //public static class PolylineCache
            //{

            //}

        }

    }
}
