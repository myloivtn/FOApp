using Maui.GoogleMaps;
using Maui.GoogleMaps.Clustering;
using FOApp.Models;
using SQLite;
using Position = Maui.GoogleMaps.Position;
using System.Text.RegularExpressions;
using FOApp.Views;
//using Android.Graphics.Drawables;
//using Android.Locations;
//using Android.Graphics.Drawables;
//using MapKit;

namespace FOApp.Services
{
    internal class Mapservice
    {
        public static void AddSegmentsToMap(Maui.GoogleMaps.Map map, Dictionary<int, List<LocationWithLabel>> nearbySegmentLocations, List<Pin> pins, List<Position> allLocations)
        {
            // Loop through each segment in the nearby segment locations
            foreach (var segment in nearbySegmentLocations)
            {
                // Lấy loại cáp FO của đoạn đầu tiên (nếu có)
                var loaicapFO = segment.Value.FirstOrDefault()?.LoaicapFO ?? 0;

                // Tạo màu cho đoạn tuyến dựa trên loại cáp FO
                //var segmentColor = GetColorForSegment(loaicapFO);

                // Tạo polyline với màu sắc và độ dày dựa trên loại cáp FO
                //var polyline = new Polyline
                //{
                //    StrokeColor = segmentColor,
                //    StrokeWidth = (int)(loaicapFO / 8), // Độ dày tuyến
                //};

                // Thêm các vị trí vào polyline và tạo pin cho từng điểm
                foreach (var locationWithLabel in segment.Value)
                {
                    if (locationWithLabel.Location != null)
                    {
                        // Thêm vị trí vào polyline
                        //polyline.Positions.Add((Position)locationWithLabel.Location);

                        // Thêm vị trí vào danh sách allLocations
                        allLocations.Add((Position)locationWithLabel.Location);

                        // Lấy biểu tượng và tên của điểm cáp
                        var cablePin = CablePinData.CablePins.FirstOrDefault(ct => ct.Name == locationWithLabel.Label);
                        var emoji = cablePin?.Emoji ?? "?";
                        var iconpin = cablePin?.IconFile ?? "khac";
                        //var iconpin = "pinmarker"; 
                        //var iconpin = "pin1";                        

                        // Tạo pin cho điểm cáp quang
                        var pin = new Pin
                        {
                            Icon = BitmapDescriptorFactory.FromBundle($"{iconpin}"), //dùng icon theo tùng điểm
                            //Icon = BitmapDescriptorFactory.FromView(() => new BindingPinView($"{emoji}", $"{iconpin}.png")),//dùng icon và imoji
                            Type = PinType.Place,
                            Position = (Position)locationWithLabel.Location,
                            Label = $"{emoji} {locationWithLabel.Label}",
                            Address = locationWithLabel.Address ?? string.Empty
                        };

                        // Thêm pin vào danh sách pins
                        pins.Add(pin);
                    }
                }

                //// Nếu polyline có ít nhất 2 vị trí, thêm nó vào bản đồ
                //if (!map.Polylines.Contains(polyline) && polyline.Positions.Count >= 2)
                //{
                //    map.Polylines.Add(polyline);
                //}
            }
        }
        public static void AddSegmentsToMapWithLine(Maui.GoogleMaps.Map map, Dictionary<int, List<FiberLine>> nearbySegmentLocations)
        {
            // Loop through each segment in the nearby segment locations
            foreach (var segment in nearbySegmentLocations)
            {   
                // Lấy loại cáp FO của đoạn đầu tiên (nếu có)
                var tuyencap = segment.Value.FirstOrDefault()?.Tuyencap ?? "";

                Match match = Regex.Match(tuyencap, @"^\d+");
                int loaicapFO = 0;
                if (match.Success)
                {
                    loaicapFO = int.Parse(match.Value); // Chuyển thành kiểu int
                }
                
                // Tạo màu cho đoạn tuyến dựa trên loại cáp FO
                var segmentColor = GetColorForSegment(loaicapFO);

                // Tạo polyline với màu sắc và độ dày dựa trên loại cáp FO
                var polyline = new Polyline
                {
                    StrokeColor = segmentColor,
                    StrokeWidth = (int)(loaicapFO / 8), // Độ dày tuyến
                };

                // Thêm các vị trí vào polyline và tạo pin cho từng điểm
                foreach (var point in segment.Value)
                {
                    if (point.Tuyencap != null)
                    {
                        //polyline .Positions.Add(locationWithLabel.Latitude, locationWithLabel.Longitude);
                        polyline.Positions.Add(new Position(point.Latitude, point.Longitude));
                    }
                }
                if (!map.Polylines.Contains(polyline) && polyline.Positions.Count >= 2)
                {
                    map.Polylines.Add(polyline);
                }
            }
        }
        //12/1/2025 Hiển thị line
        public static async Task DisplayPolylineClusteringAsync(ClusteredMap map, List<int> selectedSegments)
        {
            Dictionary<int, List<FiberLine>> nearbyLineLocations;
            nearbyLineLocations = await GetLinesAsync(selectedSegments);
            
            await map.Dispatcher.DispatchAsync(() =>
            {
                map.Polylines.Clear();
                AddSegmentsToMapWithLine(map, nearbyLineLocations);          
            });
        }
        public static async Task DisplayPolylineAsync(Maui.GoogleMaps.Map map, List<int> selectedSegments)
        {
            //Dictionary<int, List<FiberLine>> nearbyLineLocations;
            //nearbyLineLocations = await GetLinesAsync(selectedSegments);

            //await map.Dispatcher.DispatchAsync(() =>
            //{
            //    map.Polylines.Clear();
            //    AddSegmentsToMapWithLine(map, nearbyLineLocations);
            //});
            var segmentData = await GetGroupLinesAsync(selectedSegments);
            foreach (var segment in segmentData)
            {
                int segmentId = segment.Key;
                var tuyencapGroups = segment.Value;

                foreach (var tuyencapGroup in tuyencapGroups)
                {
                    string tuyencap = tuyencapGroup.Key;
                    var fiberLines = tuyencapGroup.Value;

                    Match match = Regex.Match(tuyencap, @"^\d+");
                    int loaicapFO = 0;
                    if (match.Success)
                    {
                        loaicapFO = int.Parse(match.Value); // Chuyển thành kiểu int
                    }

                    // Tạo màu cho đoạn tuyến dựa trên loại cáp FO
                    var segmentColor = GetColorForSegment(loaicapFO);

                    // Tạo polyline với màu sắc và độ dày dựa trên loại cáp FO
                    var polyline = new Polyline
                    {
                        StrokeColor = segmentColor,
                        StrokeWidth = (int)(loaicapFO / 8), // Độ dày tuyến
                    };

                    foreach (var line in fiberLines)
                    {
                        polyline.Positions.Add(new Position(line.Latitude, line.Longitude));
                    }

                    map.Polylines.Add(polyline);
                    // Tạo nhãn Tuyencap (Marker tại điểm giữa tuyến)
                    int midIndex = fiberLines.Count / 2;
                    var midPoint = fiberLines[midIndex];

                    var labelMarker = new Pin
                    {
                        Position = new Position(midPoint.Latitude, midPoint.Longitude),
                        Label = "", // Ẩn label mặc định
                        //Position = new Position(midPoint.Latitude, midPoint.Longitude),
                        //Label = tuyencap,  // Hiển thị Tuyencap
                        //IsDraggable = false,
                        Icon = BitmapDescriptorFactory.FromBundle("pinmarker")
                    };

                    map.Pins.Add(labelMarker);
                }
            }


        }
        public static async Task DisplayPolylineAsync_(Maui.GoogleMaps.Map map, List<int> selectedSegments)
        {
            Dictionary<int, List<FiberLine>> nearbyLineLocations;
            nearbyLineLocations = await GetLinesAsync(selectedSegments);

            await map.Dispatcher.DispatchAsync(() =>
            {
                map.Polylines.Clear();
                AddSegmentsToMapWithLine(map, nearbyLineLocations);
            });
        }
        public static async Task DisplayPolylineAsync(Maui.GoogleMaps.Map map, Position userLocation)
        {
            Dictionary<int, List<FiberLine>> nearbyLineLocations;
            nearbyLineLocations = await GetLinesAsync(userLocation, Constants.radiusInKm); //GetLinesAsync(selectedSegments);

            await map.Dispatcher.DispatchAsync(() =>
            {
                map.Polylines.Clear();
                AddSegmentsToMapWithLine(map, nearbyLineLocations);
            });
        }

        public static async Task DisplayPinsClusteringAsync(ClusteredMap map, List<int> selectedSegments, Label resultLabel)
        {
            Dictionary<int, List<LocationWithLabel>> nearbySegmentLocations;
            map.ClusterOptions.SetMinimumClusterSize(Constants.minClusterSize);
            map.ClusterOptions.SetMaxDistanceBetweenClusteredItems(Constants.maxDistanceBetweenClustered); // android only

            nearbySegmentLocations = await GetLocationsAsync(selectedSegments);
            var allLocations = new List<Position>();
            var pins = new List<Pin>();

            
            await map.Dispatcher.DispatchAsync(() =>
            {
                //map.Polylines.Clear();
                map.Pins.Clear();

                // Gọi phương thức để thêm các đoạn cáp vào bản đồ
                //AddSegmentsToMapWithLine(map, nearbySegmentLocations);
                AddSegmentsToMap(map, nearbySegmentLocations, pins, allLocations);               


                foreach (var pin in pins)
                {
                    //if (pin.Address.Contains("344 đường 2T9") == true)
                    //    Console.WriteLine("Test");
                    map.Pins.Add(pin);
                }
            });

            // Nếu có Pin thì tính vị trí trung tâm
            if (pins.Count > 0)
            {
                // Tính vị trí trung tâm của tất cả các Pin
                double averageLatitude = pins.Average(pin => pin.Position.Latitude);
                double averageLongitude = pins.Average(pin => pin.Position.Longitude);

                // Tạo vị trí trung tâm
                var centerPosition = new Position(averageLatitude, averageLongitude);
                var maxDistance = CalculateMaxDistance(pins);
                // Di chuyển bản đồ tới vị trí trung tâm với khoảng cách (radius) mặc định
                //map.MoveToRegion(mapSpan: MapSpan.FromCenterAndRadius(centerPosition, Distance.FromKilometers(maxDistance)));
                  map.MoveToRegion(MapSpan.FromCenterAndRadius(map.Pins.First().Position, Distance.FromMeters(20_000)));
            }
            else
            {
                // Nếu không có Pin, sử dụng vị trí mặc định
                map.MoveToRegion(mapSpan: MapSpan.FromCenterAndRadius(Constants.defaultLocation, Distance.FromKilometers(Constants.defaultDistancekm)));
            }

            resultLabel.Text = $"{pins.Count}";
        }
        //Chỉ vẽ 2 măng xông gần nhất
        public static async Task DisplayTwoNearestPinsAsync(Maui.GoogleMaps.Map map, List<int> selectedSegments, Label resultLabel, Position userLocation )
        {
            Dictionary<int, List<LocationWithLabel>> nearbySegmentLocations;
            nearbySegmentLocations = await GetLocationsAsync(selectedSegments);

            var allLocations = new List<Position>();
            var pins = new List<Pin>();

            // Lấy vị trí hiện tại của người dùng từ Preferences
            double userLat = Preferences.Get("UserLat", 0.0);
            double userLon = Preferences.Get("UserLon", 0.0);
            userLocation = new Position(userLat, userLon);

            await map.Dispatcher.DispatchAsync(() =>
            {
                // Xóa các điểm cũ trên bản đồ
                map.Pins.Clear();

                // Gọi phương thức để thêm các đoạn cáp vào bản đồ
                AddSegmentsToMap(map, nearbySegmentLocations, pins, allLocations);

                // Tạo danh sách chứa khoảng cách từ userLocation đến các điểm "Măng_xông"
                var distanceList = new List<(Pin pin, double distance)>();

                foreach (var pin in pins)
                {
                    if (pin.Label == "🛠 Măng_xông")
                    {
                        double distance = GetDistance(userLocation.Latitude, userLocation.Longitude, pin.Position.Latitude, pin.Position.Longitude);
                        distanceList.Add((pin, distance));
                        map.Pins.Add(pin);
                    }
                }
                var groupedBySegment = distanceList
                    .GroupBy(d => nearbySegmentLocations
                        .FirstOrDefault(seg => seg.Value.Any(loc =>
                            loc.Location.HasValue &&  // Kiểm tra null trước
                            loc.Location.Value.Latitude == d.pin.Position.Latitude &&
                            loc.Location.Value.Longitude == d.pin.Position.Longitude))
                        .Key)
                    .Select(g => g.OrderBy(d => d.distance).Take(2));

                // Hiển thị khoảng cách trên các pin được chọn
                foreach (var group in groupedBySegment)
                {
                    foreach (var (pin, distance) in group)
                    {
                        var emoji = $"({distance:F2})";
                        //pin.Icon = BitmapDescriptorFactory.FromBundle("pinmarker");
                        pin.Icon = BitmapDescriptorFactory.FromView(() => new BindingPinView($"{emoji}", $"pinmarker.png"));
                        pin.Label = $"{emoji} {pin.Label}";
                        pin.Address += $" ({distance:F2} km)"; 
                    }
                }
            });
            
            if (pins.Count > 0)
            {
                double averageLatitude = pins.Average(pin => pin.Position.Latitude);
                double averageLongitude = pins.Average(pin => pin.Position.Longitude);
                var centerPosition = new Position(averageLatitude, averageLongitude);
                var maxDistance = CalculateMaxDistance(pins);
                map.MoveToRegion(MapSpan.FromCenterAndRadius(centerPosition, Distance.FromKilometers(maxDistance)));
            }
            else
            {
                map.MoveToRegion(MapSpan.FromCenterAndRadius(Constants.defaultLocation, Distance.FromKilometers(Constants.defaultDistancekm)));
            }

            resultLabel.Text = $"{pins.Count}";
        }
        public static async Task DisplayPinsAsync(Maui.GoogleMaps.Map map, List<int> selectedSegments, Label resultLabel, Position userLocation)
        {
            Dictionary<int, List<LocationWithLabel>> nearbySegmentLocations;
            nearbySegmentLocations = await GetLocationsAsync(selectedSegments);

            var allLocations = new List<Position>();
            var pins = new List<Pin>();

            await map.Dispatcher.DispatchAsync(() =>
            {
                // Xóa các điểm cũ trên bản đồ
                map.Pins.Clear();

                // Gọi phương thức để thêm các đoạn cáp vào bản đồ
                AddSegmentsToMap(map, nearbySegmentLocations, pins, allLocations);

                // Danh sách lưu khoảng cách từ userLocation đến các điểm "Măng_xông"
                var distanceList = new List<(Pin pin, double distance)>();

                foreach (var pin in pins)
                {
                    if (pin.Label == "🛠 Măng_xông")
                    {
                        double distance = GetDistance(userLocation.Latitude, userLocation.Longitude, pin.Position.Latitude, pin.Position.Longitude);
                        distanceList.Add((pin, distance));
                    }
                    map.Pins.Add(pin); // Vẽ tất cả các điểm lên bản đồ
                }

                // Nhóm theo tuyến cáp (Segment) và lấy 2 điểm gần nhất trên mỗi tuyến
                var groupedBySegment = distanceList
                    .GroupBy(d => nearbySegmentLocations
                        .FirstOrDefault(seg => seg.Value.Any(loc =>
                            loc.Location.HasValue &&
                            loc.Location.Value.Latitude == d.pin.Position.Latitude &&
                            loc.Location.Value.Longitude == d.pin.Position.Longitude))
                        .Key)
                    .Select(g => g.OrderBy(d => d.distance).Take(2));

                // Hiển thị khoảng cách trên các pin gần nhất
                foreach (var group in groupedBySegment)
                {
                    foreach (var (pin, distance) in group)
                    {
                        var emoji = $"({distance:F2})";
                        pin.Icon = BitmapDescriptorFactory.FromView(() => new BindingPinView($"{emoji}", $"pinmarker.png"));
                        pin.Label = $"{emoji} {pin.Label}";
                        pin.Address += $" ({distance:F2} km)";

                        // Vẽ đè lên bản đồ
                        map.Pins.Add(pin);
                    }
                }
            });

            // Nếu có Pin thì tính vị trí trung tâm
            if (pins.Count > 0)
            {
                double averageLatitude = pins.Average(pin => pin.Position.Latitude);
                double averageLongitude = pins.Average(pin => pin.Position.Longitude);
                var centerPosition = new Position(averageLatitude, averageLongitude);
                var maxDistance = CalculateMaxDistance(pins);
                map.MoveToRegion(MapSpan.FromCenterAndRadius(centerPosition, Distance.FromKilometers(maxDistance)));
            }
            else
            {
                map.MoveToRegion(MapSpan.FromCenterAndRadius(Constants.defaultLocation, Distance.FromKilometers(Constants.defaultDistancekm)));
            }

            resultLabel.Text = $"{pins.Count}";
        }

        public static async Task DisplayPinsAsync(Maui.GoogleMaps.Map map, List<int> selectedSegments, Label resultLabel)
        {
            Dictionary<int, List<LocationWithLabel>> nearbySegmentLocations;

            nearbySegmentLocations = await GetLocationsAsync(selectedSegments);
            var allLocations = new List<Position>();
            var pins = new List<Pin>();

            await map.Dispatcher.DispatchAsync(() =>
            {
                //map.Polylines.Clear();
                map.Pins.Clear();

                // Gọi phương thức để thêm các đoạn cáp vào bản đồ
                AddSegmentsToMap(map, nearbySegmentLocations, pins, allLocations);

                foreach (var pin in pins)
                {
                    //if (pin.Address.Contains("344 đường 2T9") == true)
                    //    Console.WriteLine("Test");
                    map.Pins.Add(pin);
                }
            });

            // Nếu có Pin thì tính vị trí trung tâm
            if (pins.Count > 0)
            {
                // Tính vị trí trung tâm của tất cả các Pin
                double averageLatitude = pins.Average(pin => pin.Position.Latitude);
                double averageLongitude = pins.Average(pin => pin.Position.Longitude);

                // Tạo vị trí trung tâm
                var centerPosition = new Position(averageLatitude, averageLongitude);
                var maxDistance = CalculateMaxDistance(pins);
                // Di chuyển bản đồ tới vị trí trung tâm với khoảng cách (radius) mặc định
                map.MoveToRegion(mapSpan: MapSpan.FromCenterAndRadius(centerPosition, Distance.FromKilometers(maxDistance)));
            }
            else
            {
                // Nếu không có Pin, sử dụng vị trí mặc định
                map.MoveToRegion(mapSpan: MapSpan.FromCenterAndRadius(Constants.defaultLocation, Distance.FromKilometers(Constants.defaultDistancekm)));
            }

            resultLabel.Text = $"{pins.Count}";
        }
        public static async Task DisplayPinsClusteringAsync(ClusteredMap map, List<int> selectedSegments, string searchText, Label resultLabel)
        {
            Dictionary<int, List<LocationWithLabel>> nearbySegmentLocations;

            nearbySegmentLocations = await GetLocationsAsync(selectedSegments);
            nearbySegmentLocations = await GetLocationsAsync(selectedSegments, searchText);

            var allLocations = new List<Position>();
            var pins = new List<Pin>();

            await map.Dispatcher.DispatchAsync(() =>
            {
                //map.Polylines.Clear();
                map.Pins.Clear();

                // Gọi phương thức để thêm các đoạn cáp vào bản đồ
                AddSegmentsToMap(map, nearbySegmentLocations, pins, allLocations);

                foreach (var pin in pins)
                {
                    map.Pins.Add(pin);
                }
            });

            // Nếu có Pin thì tính vị trí trung tâm
            if (pins.Count > 0)
            {
                // Tính vị trí trung tâm của tất cả các Pin
                double averageLatitude = pins.Average(pin => pin.Position.Latitude);
                double averageLongitude = pins.Average(pin => pin.Position.Longitude);

                // Tạo vị trí trung tâm
                var centerPosition = new Position(averageLatitude, averageLongitude);
                var maxDistance = CalculateMaxDistance(pins);
                // Di chuyển bản đồ tới vị trí trung tâm với khoảng cách (radius) mặc định
                map.MoveToRegion(mapSpan: MapSpan.FromCenterAndRadius(centerPosition, Distance.FromKilometers(maxDistance)));
            }
            else
            {
                // Nếu không có Pin, sử dụng vị trí mặc định
                map.MoveToRegion(mapSpan: MapSpan.FromCenterAndRadius(Constants.defaultLocation, Distance.FromKilometers(Constants.defaultDistancekm)));
            }

            resultLabel.Text = $"{pins.Count}";
        }
        public static async Task DisplayPinsAsync(Maui.GoogleMaps.Map map, List<int> selectedSegments, string searchText, Label resultLabel)
        {
            Dictionary<int, List<LocationWithLabel>> nearbySegmentLocations;

            nearbySegmentLocations = await GetLocationsAsync(selectedSegments);
            nearbySegmentLocations = await GetLocationsAsync(selectedSegments, searchText);

            var allLocations = new List<Position>();
            var pins = new List<Pin>();

            await map.Dispatcher.DispatchAsync(() =>
            {
                //map.Polylines.Clear();
                map.Pins.Clear();

                // Gọi phương thức để thêm các đoạn cáp vào bản đồ
                AddSegmentsToMap(map, nearbySegmentLocations, pins, allLocations);

                foreach (var pin in pins)
                {
                    map.Pins.Add(pin);
                }
            });

            // Nếu có Pin thì tính vị trí trung tâm
            if (pins.Count > 0)
            {
                // Tính vị trí trung tâm của tất cả các Pin
                double averageLatitude = pins.Average(pin => pin.Position.Latitude);
                double averageLongitude = pins.Average(pin => pin.Position.Longitude);

                // Tạo vị trí trung tâm
                var centerPosition = new Position(averageLatitude, averageLongitude);
                var maxDistance = CalculateMaxDistance(pins);
                // Di chuyển bản đồ tới vị trí trung tâm với khoảng cách (radius) mặc định
                map.MoveToRegion(mapSpan: MapSpan.FromCenterAndRadius(centerPosition, Distance.FromKilometers(maxDistance)));
            }
            else
            {
                // Nếu không có Pin, sử dụng vị trí mặc định
                map.MoveToRegion(mapSpan: MapSpan.FromCenterAndRadius(Constants.defaultLocation, Distance.FromKilometers(Constants.defaultDistancekm)));
            }

            resultLabel.Text = $"{pins.Count}";
        }
        public static async Task DisplayPinsAsync(Maui.GoogleMaps.Map map, Position userLocation, Label resultLabel)
        {  
            Dictionary<int, List<LocationWithLabel>> nearbySegmentLocations;
            nearbySegmentLocations = await GetLocationsAsync(userLocation, Constants.radiusInKm);
          
            var allLocations = new List<Position>();
            var pins = new List<Pin>();

            await map.Dispatcher.DispatchAsync(() =>
            {
                //map.Polylines.Clear();
                map.Pins.Clear();
                
                // Gọi phương thức để thêm các đoạn cáp vào bản đồ
                AddSegmentsToMap(map, nearbySegmentLocations, pins, allLocations);

                foreach (var pin in pins)
                {
                    map.Pins.Add(pin);

                }
            });

            // Move the map to the user's location with the adjusted radius
            //map.MoveToRegion(mapSpan: MapSpan.FromCenterAndRadius(userLocation, Distance.FromKilometers(Constants.radiusInKm)));

            resultLabel.Text = $"{pins.Count}";
        }
        public static async Task DisplayPinsAsync(Maui.GoogleMaps.Map map, Position userLocation, string searchText, Label resultLabel)
        {
           
            Dictionary<int, List<LocationWithLabel>> nearbySegmentLocations;

            nearbySegmentLocations = await GetLocationsAsync(userLocation, searchText);
            var allLocations = new List<Position>();
            var pins = new List<Pin>();
            
            await map.Dispatcher.DispatchAsync(() =>
            {
                //map.Polylines.Clear();
                map.Pins.Clear();
 
                // Gọi phương thức để thêm các đoạn cáp vào bản đồ
                AddSegmentsToMap(map, nearbySegmentLocations, pins, allLocations);
               
                foreach (var pin in pins)
                {
                    map.Pins.Add(pin);

                }
            });
            resultLabel.Text = $"{pins.Count}";
            // Move the map to the user's location with the adjusted radius
            //map.MoveToRegion(mapSpan: MapSpan.FromCenterAndRadius(userLocation, Distance.FromKilometers(Constants.defaultDistancekm)));
        }

        //14/1/2025 : Lấy data vẽ line
        public static async Task<Dictionary<int, Dictionary<string, List<FiberLine>>>> GetGroupLinesAsync(List<int> segmentIds)
        {
            var sqliteConnection = new SQLiteAsyncConnection(Constants.DatabasePath);
            var segmentLocations = new Dictionary<int, Dictionary<string, List<FiberLine>>>();

            try
            {
                await sqliteConnection.CreateTableAsync<FiberLine>();

                foreach (var segmentId in segmentIds)
                {
                    // Lấy tất cả FiberLine của idsegment hiện tại
                    var fiberLines = await sqliteConnection.Table<FiberLine>()
                                                            .Where(t => t.IdSegment == segmentId)
                                                            .OrderBy(t => t.Tuyencap) // Sắp xếp theo Tuyencap trước
                                                            .ThenBy(t => t.Order)     // Sau đó sắp xếp theo Order
                                                            .ToListAsync();

                    // Nhóm các FiberLine theo Tuyencap
                    var groupedLines = fiberLines
                        .GroupBy(line => line.Tuyencap) // Nhóm theo Tuyencap
                        .ToDictionary(group => group.Key, group => group.ToList());

                    // Lưu kết quả vào từ điển theo segmentId
                    if (groupedLines.Count > 0)
                    {
                        segmentLocations[segmentId] = groupedLines;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi dữ liệu SQLite: " + ex.Message);
            }

            return segmentLocations;
        }

        public static async Task<Dictionary<int, List<FiberLine>>> GetLinesAsync(List<int> segmentIds)
        {
            var sqliteConnection = new SQLiteAsyncConnection(Constants.DatabasePath);
            var segmentLocations = new Dictionary<int, List<FiberLine>>();
            
            try
            {
                await sqliteConnection.CreateTableAsync<FiberLine>();

                foreach (var segmentId in segmentIds)
                {
                    var fiberLines = await sqliteConnection.Table<FiberLine>()
                                                            .Where(t => t.IdSegment == segmentId)
                                                            .OrderBy(t => t.Order)
                                                            .ToListAsync();
                    var locations = new List<FiberLine>();
                   
                    foreach (var line in fiberLines) 
                    { 
                        locations.Add(line);
                    }

                    if (locations.Count > 0)
                    {
                        segmentLocations[segmentId] = locations;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi dữ liệu SQLite: " + ex.Message);
            }

            return segmentLocations;
        }
        public static async Task<Dictionary<int, List<FiberLine>>> GetLinesAsync(Position userLocation, double radiusInKm)
        {
            var sqliteConnection = new SQLiteAsyncConnection(Constants.DatabasePath);
            var nearbySegmentLocations = new Dictionary<int, List<FiberLine>>();

            try
            {
                await sqliteConnection.CreateTableAsync<FiberLine>();

                // Bán kính tính bằng độ (1 độ = ~111.32 km)
                double radiusInDegrees = radiusInKm / 111.32;

                // Tính các giá trị giới hạn trước
                double minLatitude = userLocation.Latitude - radiusInDegrees;
                double maxLatitude = userLocation.Latitude + radiusInDegrees;
                double minLongitude = userLocation.Longitude - radiusInDegrees;
                double maxLongitude = userLocation.Longitude + radiusInDegrees;

                // Truy vấn dữ liệu với giới hạn đã tính toán
                var fiberLines = await sqliteConnection.Table<FiberLine>()
                    .Where(line =>
                        line.Latitude >= minLatitude &&
                        line.Latitude <= maxLatitude &&
                        line.Longitude >= minLongitude &&
                        line.Longitude <= maxLongitude)
                    .ToListAsync();

                foreach (var fiberLine in fiberLines)
                {
                    if (!nearbySegmentLocations.ContainsKey(fiberLine.IdSegment))
                    {
                        nearbySegmentLocations[fiberLine.IdSegment] = new List<FiberLine>();
                    }
                    nearbySegmentLocations[fiberLine.IdSegment].Add(fiberLine);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi dữ liệu SQLite: " + ex.Message);
            }

            return nearbySegmentLocations;
        }
        public static async Task<Dictionary<int, List<LocationWithLabel>>> GetLocationsAsync(List<int> segmentIds)
        {
            var sqliteConnection = new SQLiteAsyncConnection(Constants.DatabasePath);
            var segmentLocations = new Dictionary<int, List<LocationWithLabel>>();

            try
            {
                await sqliteConnection.CreateTableAsync<FiberItem>();

                foreach (var segmentId in segmentIds)
                {
                    var fiberItems = await sqliteConnection.Table<FiberItem>()
                                                            .Where(t => t.IdSegment == segmentId)
                                                            .ToListAsync();

                    var locations = new List<LocationWithLabel>();

                    foreach (var fiberItem in fiberItems)
                    {
                        var locationWithLabel = new LocationWithLabel
                        {
                            Location = (fiberItem.Latitude.HasValue && fiberItem.Longitude.HasValue)
                                ? new Position(fiberItem.Latitude.Value, fiberItem.Longitude.Value)
                                : null,
                            Label = fiberItem.Diemdacbiet,
                            //Address = $"{fiberItem.Id} {(fiberItem.Huongtuyen?[..1] ?? "")} " +
                            Address = $"{fiberItem.Id} " +
                                        $"{(string.IsNullOrEmpty(fiberItem.Vitrituyen) ? "" : $"➡️{fiberItem.Vitrituyen}")} " +
                                      $"{(string.IsNullOrEmpty(fiberItem.Dosau) ? "" : $"⬇️{fiberItem.Dosau}")} " +
                                      $"{(string.IsNullOrEmpty(fiberItem.Chieudai) ? "" : $"➰{fiberItem.Chieudai}")} " +
                                      $"{(Regex.Match(fiberItem.Doan, @"(?<=Km)\s*\d+\+\d+").Success ? Regex.Match(fiberItem.Doan, @"(?<=Km)\s*\d+\+\d+").Value : fiberItem.Doan)} " +
                                      $"{fiberItem.Phuongthuclapdat} {fiberItem.Tenduong} {fiberItem.Ghichu}",
                            LoaicapFO = fiberItem.Loaicap != null ? Convert.ToInt16(fiberItem.Loaicap) : 0,
                        };

                        if (locationWithLabel.Location != null)
                        {
                            locations.Add(locationWithLabel);
                        }
                    }
                   

                    if (locations.Count > 0)
                    {
                        var sortedLocations = SortLocationsByDistance(locations);

                        segmentLocations[segmentId] = sortedLocations;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi dữ liệu SQLite: " + ex.Message);
            }

            return segmentLocations;
        }
        public static async Task<Dictionary<int, List<LocationWithLabel>>> GetLocationsAsync(List<int> segmentIds, string searchText)
        {
            var sqliteConnection = new SQLiteAsyncConnection(Constants.DatabasePath);
            var segmentLocations = new Dictionary<int, List<LocationWithLabel>>();

            try
            {
                await sqliteConnection.CreateTableAsync<FiberItem>();

                var searchTextLower = searchText.ToLowerInvariant();
                foreach (var segmentId in segmentIds)
                {
                    var fiberItems = await sqliteConnection.Table<FiberItem>()
                        .Where(fiberItem =>
                            fiberItem.IdSegment == segmentId && // Điều kiện với segmentId
                            (fiberItem.Tuyencap.ToLower().Contains(searchTextLower) ||
                             fiberItem.Diemdacbiet.ToLower().Contains(searchTextLower) ||
                             fiberItem.Phuongthuclapdat.ToLower().Contains(searchTextLower) ||
                             fiberItem.Doan.ToLower().Contains(searchTextLower) ||
                             fiberItem.Huongtuyen.ToLower().Contains(searchTextLower) ||
                             fiberItem.Vitrituyen.ToLower().Contains(searchTextLower) ||
                             fiberItem.Ghichu.ToLower().Contains(searchTextLower) ||
                             fiberItem.Tenduong.ToLower().Contains(searchTextLower)))
                        .ToListAsync();

                    var locations = new List<LocationWithLabel>();

                    foreach (var fiberItem in fiberItems)
                    {

                        var locationsWithLabel = new LocationWithLabel
                        {
                            Location = (fiberItem.Latitude.HasValue && fiberItem.Longitude.HasValue)
                                ? new Position(fiberItem.Latitude.Value, fiberItem.Longitude.Value)
                                : null, // Handle the case where either value is null
                            Label = fiberItem.Diemdacbiet,
                            //Address = $"{fiberItem.Tuyencap} {fiberItem.Doan} {fiberItem.Huongtuyen}:{fiberItem.Phuongthuclapdat} {fiberItem.Vitrituyen} ⬇️{fiberItem.Dosau} ➰{fiberItem.Chieudai}m {fiberItem.Tenduong} {fiberItem.Ghichu} ",
                            //Address = $"{fiberItem.Id} {(fiberItem.Huongtuyen?[..1] ?? "")} " +  // Huongtuyen (nếu không null, lấy ký tự đầu tiên)
                            Address = $"{fiberItem.Id} " +
                              $"{(string.IsNullOrEmpty(fiberItem.Vitrituyen) ? "" : $"➡️{fiberItem.Vitrituyen}")} " + // Nếu Vitrituyen có giá trị, thêm emoji
                              $"{(string.IsNullOrEmpty(fiberItem.Dosau) ? "" : $"⬇️{fiberItem.Dosau}")} " +
                              $"{(string.IsNullOrEmpty(fiberItem.Chieudai) ? "" : $"➰{fiberItem.Chieudai}")} " +
                              $"{(Regex.Match(fiberItem.Doan, @"(?<=Km)\s*\d+\+\d+").Success ? Regex.Match(fiberItem.Doan, @"(?<=Km)\s*\d+\+\d+").Value : fiberItem.Doan)} {fiberItem.Phuongthuclapdat} {fiberItem.Tenduong} {fiberItem.Ghichu}",
                            LoaicapFO = fiberItem.Loaicap != null ? Convert.ToInt16(fiberItem.Loaicap) : 0,
                        };
                        if (locationsWithLabel.Location != null)
                        {
                            if (!segmentLocations.TryGetValue(fiberItem.IdSegment, out List<LocationWithLabel>? value))
                            {
                                value = new List<LocationWithLabel>();
                                segmentLocations[fiberItem.IdSegment] = value;
                            }
                            value.Add(locationsWithLabel);

                            locations.Add(locationsWithLabel);
                        }
                    }

                    if (locations.Count > 0)
                    {
                        var sortedLocations = SortLocationsByDistance(locations);

                        segmentLocations[segmentId] = sortedLocations;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi dữ liệu SQLite: " + ex.Message);
            }
            return segmentLocations;
        }

        public static async Task<Dictionary<int, List<LocationWithLabel>>> GetLocationsAsync(Position userLocation, double radiusInKm)
        {
            var sqliteConnection = new SQLiteAsyncConnection(Constants.DatabasePath);
            var nearbySegmentLocations = new Dictionary<int, List<LocationWithLabel>>();

            try
            {
                await sqliteConnection.CreateTableAsync<FiberItem>();

                // Bán kính tính bằng độ (1 độ = ~111.32 km)
                double radiusInDegrees = radiusInKm / 111.32;

                // Tính các giá trị giới hạn
                double minLatitude = userLocation.Latitude - radiusInDegrees;
                double maxLatitude = userLocation.Latitude + radiusInDegrees;
                double minLongitude = userLocation.Longitude - radiusInDegrees;
                double maxLongitude = userLocation.Longitude + radiusInDegrees;

                // Lấy các điểm trong phạm vi Bounding Box
                var fiberItems = await sqliteConnection.Table<FiberItem>()
                    .Where(f => f.Latitude >= minLatitude && f.Latitude <= maxLatitude &&
                                f.Longitude >= minLongitude && f.Longitude <= maxLongitude)
                    .ToListAsync();

                foreach (var fiberItem in fiberItems)
                {
                    var locationsWithLabels = new LocationWithLabel
                    {
                        Location = (fiberItem.Latitude.HasValue && fiberItem.Longitude.HasValue)
                            ? new Position(fiberItem.Latitude.Value, fiberItem.Longitude.Value)
                            : null,
                        Label = fiberItem.Diemdacbiet,
                        Address = $"{fiberItem.Tuyencap} {(fiberItem.Huongtuyen?[..1] ?? "")}" +
                                  $"{(string.IsNullOrEmpty(fiberItem.Vitrituyen) ? "" : $"➡️{fiberItem.Vitrituyen}")} " +
                                  $"{(string.IsNullOrEmpty(fiberItem.Dosau) ? "" : $"⬇️{fiberItem.Dosau}")} " +
                                  $"{(string.IsNullOrEmpty(fiberItem.Chieudai) ? "" : $"➰{fiberItem.Chieudai}")} " +
                                  $"{(Regex.Match(fiberItem.Doan, @"(?<=Km)\s*\d+\+\d+").Success ? Regex.Match(fiberItem.Doan, @"(?<=Km)\s*\d+\+\d+").Value : fiberItem.Doan)} {fiberItem.Phuongthuclapdat} {fiberItem.Tenduong} {fiberItem.Ghichu}",
                        LoaicapFO = fiberItem.Loaicap != null ? Convert.ToInt16(fiberItem.Loaicap) : 0,
                    };

                    if (!nearbySegmentLocations.TryGetValue(fiberItem.IdSegment, out List<LocationWithLabel>? value))
                    {
                        value = new List<LocationWithLabel>();
                        nearbySegmentLocations[fiberItem.IdSegment] = value;
                    }
                    value.Add(locationsWithLabels);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy GPS lý trình từ SQLite: " + ex.Message);
            }

            return nearbySegmentLocations;
        }
        public static async Task<Dictionary<int, List<LocationWithLabel>>> GetLocationsAsync_(Position userLocation, double radiusInKm)
        {
            var sqliteConnection = new SQLiteAsyncConnection(Constants.DatabasePath);
            var nearbySegmentLocations = new Dictionary<int, List<LocationWithLabel>>();

            try
            {
                await sqliteConnection.CreateTableAsync<FiberItem>();
                var fiberItems = await sqliteConnection.Table<FiberItem>()
                    .ToListAsync();

                foreach (var fiberItem in fiberItems)
                {
                    var locationsWithLabels = new LocationWithLabel
                    {
                        Location = (fiberItem.Latitude.HasValue && fiberItem.Longitude.HasValue)
                            ? new Position(fiberItem.Latitude.Value, fiberItem.Longitude.Value)
                            : null, // Handle the case where either value is null
                        Label = fiberItem.Diemdacbiet,
                        //Address = $"{fiberItem.Tuyencap} {fiberItem.Doan} {fiberItem.Huongtuyen}:{fiberItem.Phuongthuclapdat} {fiberItem.Vitrituyen} ⬇️{fiberItem.Dosau} ➰{fiberItem.Chieudai}m {fiberItem.Tenduong} {fiberItem.Ghichu} ",
                        Address = $"{fiberItem.Tuyencap} {(fiberItem.Huongtuyen?[..1] ?? "")}" +  // Huongtuyen (nếu không null, lấy ký tự đầu tiên)
                              $"{(string.IsNullOrEmpty(fiberItem.Vitrituyen) ? "" : $"➡️{fiberItem.Vitrituyen}")} " + // Nếu Vitrituyen có giá trị, thêm emoji
                              $"{(string.IsNullOrEmpty(fiberItem.Dosau) ? "" : $"⬇️{fiberItem.Dosau}")} " +
                              $"{(string.IsNullOrEmpty(fiberItem.Chieudai) ? "" : $"➰{fiberItem.Chieudai}")} " +
                              $"{(Regex.Match(fiberItem.Doan, @"(?<=Km)\s*\d+\+\d+").Success ? Regex.Match(fiberItem.Doan, @"(?<=Km)\s*\d+\+\d+").Value : fiberItem.Doan)} {fiberItem.Phuongthuclapdat} {fiberItem.Tenduong} {fiberItem.Ghichu}",

                        LoaicapFO = fiberItem.Loaicap != null ? Convert.ToInt16(fiberItem.Loaicap) : 0,
                    };

                    if (locationsWithLabels?.Location != null) // Kiểm tra xem vị trí có null không
                    {
                        double InKm = CalculateDistance(userLocation, (Position)locationsWithLabels.Location, DistanceUnits.Kilometers);
                        if ((double)InKm <= radiusInKm)
                        {
                            if (!nearbySegmentLocations.TryGetValue(fiberItem.IdSegment, out List<LocationWithLabel>? value))
                            {
                                value = new List<LocationWithLabel>();
                                nearbySegmentLocations[fiberItem.IdSegment] = value;
                            }
                            value.Add(locationsWithLabels);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy GPS lý trình từ SQLite: " + ex.Message);
            }

            return nearbySegmentLocations;
        }
        public static async Task<Dictionary<int, List<LocationWithLabel>>> GetLocationsAsync(Position userLocation, string searchText)
        {
            var sqliteConnection = new SQLiteAsyncConnection(Constants.DatabasePath);
            var nearbySegmentLocations = new Dictionary<int, List<LocationWithLabel>>();
           
            try
            {
                await sqliteConnection.CreateTableAsync<FiberItem>();

                var searchTextLower = searchText.ToLowerInvariant();

                var fiberItems = await sqliteConnection.Table<FiberItem>()
                    .Where(fiberItem => fiberItem.Tuyencap.ToLower().Contains(searchTextLower) ||
                                        //fiberItem.GPSLytrinh1.ToLower().Contains(searchTextLower) ||
                                        fiberItem.Diemdacbiet.ToLower().Contains(searchTextLower) ||
                                        fiberItem.Phuongthuclapdat.ToLower().Contains(searchTextLower) ||
                                        fiberItem.Doan.ToLower().Contains(searchTextLower) ||
                                        fiberItem.Huongtuyen.ToLower().Contains(searchTextLower) ||
                                        fiberItem.Vitrituyen.ToLower().Contains(searchTextLower) ||
                                        fiberItem.Ghichu.ToLower().Contains(searchTextLower) ||
                                        fiberItem.Tenduong.ToLower().Contains(searchTextLower))
                    .ToListAsync();

                foreach (var fiberItem in fiberItems)
                {
                    var locationsWithLabels = new LocationWithLabel
                    {
                        Location = (fiberItem.Latitude.HasValue && fiberItem.Longitude.HasValue)
                            ? new Position(fiberItem.Latitude.Value, fiberItem.Longitude.Value)
                            : null, // Handle the case where either value is null
                        Label = fiberItem.Diemdacbiet,
                        //Address = $"{fiberItem.Tuyencap} {fiberItem.Doan} {fiberItem.Huongtuyen}:{fiberItem.Phuongthuclapdat} {fiberItem.Vitrituyen} ⬇️{fiberItem.Dosau} ➰{fiberItem.Chieudai}m {fiberItem.Tenduong} {fiberItem.Ghichu} ",
                        Address = $"{fiberItem.Tuyencap} {(fiberItem.Huongtuyen?[..1] ?? "")}" +  // Huongtuyen (nếu không null, lấy ký tự đầu tiên)
                              $"{(string.IsNullOrEmpty(fiberItem.Vitrituyen) ? "" : $"➡️{fiberItem.Vitrituyen}")} " + // Nếu Vitrituyen có giá trị, thêm emoji
                              $"{(string.IsNullOrEmpty(fiberItem.Dosau) ? "" : $"⬇️{fiberItem.Dosau}")} " +
                              $"{(string.IsNullOrEmpty(fiberItem.Chieudai) ? "" : $"➰{fiberItem.Chieudai}")} " +
                              $"{(Regex.Match(fiberItem.Doan, @"(?<=Km)\s*\d+\+\d+").Success ? Regex.Match(fiberItem.Doan, @"(?<=Km)\s*\d+\+\d+").Value : fiberItem.Doan)} {fiberItem.Phuongthuclapdat} {fiberItem.Tenduong} {fiberItem.Ghichu}",
                        LoaicapFO = fiberItem.Loaicap != null ? Convert.ToInt16(fiberItem.Loaicap) : 0,
                    };

                    if (locationsWithLabels?.Location != null) // Kiểm tra xem vị trí có null không
                    {
                        double InKm = CalculateDistance(userLocation, (Position)locationsWithLabels.Location, DistanceUnits.Kilometers);
                        if ((double)InKm <= Constants.defaultDistancekm)
                            {
                            if (!nearbySegmentLocations.TryGetValue(fiberItem.IdSegment, out List<LocationWithLabel>? value))
                            {
                                value = new List<LocationWithLabel>();
                                nearbySegmentLocations[fiberItem.IdSegment] = value;
                            }
                            value.Add(locationsWithLabels);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingService.Log($"Lỗi khi lấy GPS lý trình từ SQLite: {ex.Message}");
                return nearbySegmentLocations;
            }

            return nearbySegmentLocations;
        }
        public static List<LocationWithLabel> SortLocationsByDistance(List<LocationWithLabel> locations)
        {
            if (locations == null || locations.Count == 0)
                return new List<LocationWithLabel>();

            // Khởi tạo danh sách đã sắp xếp và thêm vị trí đầu tiên làm điểm bắt đầu
            var sortedLocations = new List<LocationWithLabel> { locations.First() };
            var remainingLocations = locations.Skip(1).ToList();

            while (remainingLocations.Count > 0)
            {
                // Lấy vị trí hiện tại là vị trí cuối cùng trong danh sách đã sắp xếp
                var currentLocation = sortedLocations.Last().Location;

                // Tìm vị trí gần nhất với vị trí hiện tại
                var nearestLocation = remainingLocations
                    .OrderBy(loc => CalculateDistance((Position)currentLocation, (Position)loc.Location, DistanceUnits.Kilometers))
                    .First();

                // Thêm vị trí gần nhất vào danh sách đã sắp xếp và loại bỏ nó khỏi danh sách còn lại
                sortedLocations.Add(nearestLocation);
                remainingLocations.Remove(nearestLocation);
            }

            return sortedLocations;
        }
        private static double HaversineDistance(Position pos1, Position pos2)
        {
            const double EarthRadiusKm = 6371.0; // Bán kính Trái đất (km)

            var lat1Rad = DegreesToRadians(pos1.Latitude);
            var lat2Rad = DegreesToRadians(pos2.Latitude);
            var deltaLat = DegreesToRadians(pos2.Latitude - pos1.Latitude);
            var deltaLon = DegreesToRadians(pos2.Longitude - pos1.Longitude);

            var a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                    Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                    Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return EarthRadiusKm * c; // Trả về khoảng cách giữa hai điểm (km)
        }
        public static double CalculateMaxDistance(List<Pin> pins)
        {
            double maxDistance = 0;

            // Duyệt qua tất cả các cặp Pin để tìm khoảng cách lớn nhất
            for (int i = 0; i < pins.Count; i++)
            {
                for (int j = i + 1; j < pins.Count; j++)
                {
                    double distance = HaversineDistance(pins[i].Position, pins[j].Position);
                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                    }
                }
            }

            return maxDistance;
        }
        // Hàm tính khoảng cách giữa hai điểm theo công thức Haversine
        private static double GetDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double EarthRadius = 6371; // Bán kính Trái Đất (km)

            double dLat = (lat2 - lat1) * (Math.PI / 180);
            double dLon = (lon2 - lon1) * (Math.PI / 180);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(lat1 * (Math.PI / 180)) * Math.Cos(lat2 * (Math.PI / 180)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return EarthRadius * c;
        }
        public static double CalculateTotalLength(List<FiberItem> fiberItems)
        {
            double totalLength = 0.0;

            var orderedPoints = fiberItems
                .Where(f => f.Latitude.HasValue && f.Longitude.HasValue) // Lọc bỏ giá trị null
                .OrderBy(f => f.Id)  // Sắp xếp theo ID để tính tuyến đúng thứ tự
                .ToList();

            for (int i = 1; i < orderedPoints.Count; i++)
            {
                var prev = orderedPoints[i - 1];
                var current = orderedPoints[i];

                // Kiểm tra nếu có tọa độ hợp lệ mới tính toán
                if (prev.Latitude.HasValue && prev.Longitude.HasValue &&
                    current.Latitude.HasValue && current.Longitude.HasValue)
                {
                    totalLength += GetDistance(
                        prev.Latitude.Value, prev.Longitude.Value,
                        current.Latitude.Value, current.Longitude.Value
                    );
                }
            }

            return totalLength;
        }

        public static double CalculateTotalLength_(List<FiberItem> fiberItems)
        {
            double totalLength = 0.0;

            // Sắp xếp các điểm theo thứ tự xuất hiện trong tuyến
            var orderedPoints = fiberItems
                .Where(f => f.Latitude.HasValue && f.Longitude.HasValue)
                .OrderBy(f => f.Id) // Hoặc có thể thay bằng một trường sắp xếp phù hợp
                .ToList();

            for (int i = 1; i < orderedPoints.Count; i++)
            {
                var prev = orderedPoints[i - 1];
                var current = orderedPoints[i];

                if (prev.Latitude.HasValue && prev.Longitude.HasValue &&
                    current.Latitude.HasValue && current.Longitude.HasValue)
                {
                    totalLength += GetDistance(
                        prev.Latitude.Value, prev.Longitude.Value,
                        current.Latitude.Value, current.Longitude.Value
                    );
                }
            }           
           return totalLength;
        }

        public static double CalculateDistance(Position userLocation, Position targetLocation, DistanceUnits distanceUnits)
        {
            const double EarthRadiusKm = 6371.0; // Earth's radius in kilometers

            double dLat = DegreesToRadians(targetLocation.Latitude - userLocation.Latitude);
            double dLon = DegreesToRadians(targetLocation.Longitude - userLocation.Longitude);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(DegreesToRadians(userLocation.Latitude)) * Math.Cos(DegreesToRadians(targetLocation.Latitude)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distanceInKm = EarthRadiusKm * c; // Distance in kilometers

            // Convert to miles if needed
            if (distanceUnits == DistanceUnits.Miles)
            {
                return distanceInKm * 0.621371; // Convert km to miles
            }

            return distanceInKm; // Return distance in kilometers
        }
        private static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }
       
        public static Dictionary<int, Color> FiberSegmentColors = new Dictionary<int, Color>
        {
            { 8, Colors.Red },      
            { 24, Colors.Green },    
            { 48, Colors.Blue },     
            { 96, Colors.Orange },   
            { 144, Colors.Purple },
            { 60, Colors.Yellow },
            { 36,  Colors.Cyan },
            { 72, Colors.Magenta }
        };
        public static Color GetColorForSegment(int loaicapFO)
        {
            if (FiberSegmentColors.ContainsKey(loaicapFO))
            {
                return FiberSegmentColors[loaicapFO];
            }

            // Nếu không tìm thấy màu nào tương ứng, trả về màu mặc định
            return Colors.Gray;
        }

    }
    public class CustomPin : Pin
    {
        public string? Emoji { get; set; }
    }
}
