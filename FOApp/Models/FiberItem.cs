//using Microsoft.Maui.Controls.Maps;
using Maui.GoogleMaps;
using SQLite;
namespace FOApp.Models;

public class FiberLine
{
    [PrimaryKey] 
    public int Id { get; set; } 
    public int IdSegment { get; set; }
    public int? Order { get; set; } // == Loaicap
    public string? Tuyencap { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

//20/10/2024: lưu vị trí các trạm favorite
public class Station
{
    public string? Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
public static class CablePinData
{
    public static readonly List<CablePin> CablePins =
    [
    new() { Name = "Chôn_sang_treo", Emoji = "⬆️", IconFile = "chonsangtreo" },
    new() { Name = "Chôn", Emoji = "🌐", IconFile = "chon" },
    new() { Name = "Treo_sang_chôn", Emoji = "⬇", IconFile = "treosangchon" },
    new() { Name = "Khác", Emoji = "❓", IconFile = "khac" },
    new() { Name = "Đi_nổi", Emoji = "⤴️", IconFile = "dinoi" },
    new() { Name = "Qua_cống", Emoji = "🚰", IconFile = "quacong" },
    new() { Name = "Treo", Emoji = "🔝", IconFile = "treo" },
    new() { Name = "Măng_xông", Emoji = "🛠", IconFile = "mangxong" },
    new() { Name = "Chuyển_hướng", Emoji = "🔀", IconFile = "chuyenhuong" },
    new() { Name = "Cống_bể", Emoji = "🕳️", IconFile = "congbe" },
    new() { Name = "Dự_trữ_cáp", Emoji = "➰", IconFile = "dutrucap" },
    new() { Name = "Qua_cầu", Emoji = "🌉", IconFile = "quacau" },
    new() { Name = "ODF", Emoji = "⭐", IconFile = "odf" }
    ];  
}

public class CablePin
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Emoji { get; set; } // giữ emoji
    public string? IconFile { get; set; } // Đường dẫn hoặc tên tệp hình ảnh .png
}
//public class FiberSegment
//{
//    //[PrimaryKey, AutoIncrement]
//    [PrimaryKey]
//    public int IdSegment { get; set; }
//    public string? Tuyen { get; set; }
//    public int? LoaicapFO { get; set; }
//}
public class LocationWithLabel
{
    public Position? Location { get; set; } // Changed Location to Position (from Maui.GoogleMaps)
    public string? Label { get; set; }
    public string? Address { get; set; }
    public int LoaicapFO { get; set; } // chuyển từ IsFavorite field
    public string? CableType { get; set; } // Added CableType field    
}
public class PointInfo //xử lý khi điểm thuộc nhiều tuyến cáp
{
    public string? Label { get; set; }
    public string? Address { get; set; }
    public int LoaicapFO { get; set; }
    public string? CableType { get; set; } 
}


public class SegmentItem
{
    public int IdSegment { get; set; }
    public string? Tuyen { get; set; }
    public int? LoaicapFO { get; set; }
    public bool IsSelected { get; set; }
    // Thêm cột tính toán
    public int SoMangXong { get; set; }   // Số điểm "Măng_xông"
    public double TongDoDai { get; set; } // Tổng độ dài tuyến
    public string DisplayText
    {
        get
        {
            return $"{IdSegment} - {Tuyen} 🛠{SoMangXong}mx ➰{Math.Round(TongDoDai, 2)}km";
            //return $"{IdSegment} - {Tuyen} - {LoaicapFO}FO";
            //return $"✔️ {Tuyen} - {LoaicapFO}";
        }
    }
}
public class FiberItemAll
{
    //[PrimaryKey, AutoIncrement] 
    [PrimaryKey] //'AUTOINCREMENT is only allowed on an INTEGER PRIMARY KEY -> chạy lần đầu để đồng bộ dữ liệu, Sau này lỗi nữa không
    public int Id { get; set; } //không dùng decimail  Id = Convert.ToInt16(reader["Id"]), 
    public int IdSegment { get; set; }
    public string? Tuyencap { get; set; }
    public string? Doan { get; set; }
    //public double Chieudaituyen { get; set; }
    //public double Lytrinh1 { get; set; }
    //public double Lytrinh2 { get; set; }
    //public string? GPSLytrinh1 { get; set; }
    //public string? GPSLytrinh2 { get; set; }
    public string? Diemdacbiet { get; set; }
    public string? Phuongthuclapdat { get; set; }
    //public string? Sotrudienluc { get; set; }
    public string? Chieudai { get; set; }
    public string? Dosau { get; set; }
    //public string? DocaoTDL { get; set; }
    public string? Huongtuyen { get; set; }
    public string? Vitrituyen { get; set; }
    public int? Loaicap { get; set; } // chuyển sang lưu LaoicapFO
    public string? Ghichu { get; set; }
    public string? Tenduong { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    //public string? Tencaucong { get; set; }
    //public bool Done { get; set; }
    public string? UpdatedDate { get; set; }
}

public class FiberItem
{    
    [PrimaryKey] //'AUTOINCREMENT is only allowed on an INTEGER PRIMARY KEY -> chạy lần đầu để đồng bộ dữ liệu, Sau này lỗi nữa không
    public int Id { get; set; } //không dùng decimail  Id = Convert.ToInt16(reader["Id"]), 
    public int IdSegment { get; set; }
    public string? Tuyencap { get; set; }
    public string? Doan { get; set; }
    public string? Diemdacbiet { get; set; }
    public string? Phuongthuclapdat { get; set; }
    public string? Chieudai { get; set; }
    public string? Dosau { get; set; }
    public string? Huongtuyen { get; set; }
    public string? Vitrituyen { get; set; }
    public int? Loaicap { get; set; } // chuyển sang lưu LaoicapFO
    public string? Ghichu { get; set; }
    public string? Tenduong { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string? UpdatedDate { get; set; }
}
//public class ImageItem
//{
//    [PrimaryKey, AutoIncrement]
//    public int IdImage { get; set; }
//    public int Id { get; set; } // Khóa ngoại liên kết với Id của bảng FiberItem
//    public byte[]? Photo { get; set; } // Dữ liệu hình ảnh dưới dạng byte array
//    public string? Img { get; set; } // Dữ liệu hình ảnh dưới dạng string
//}
public class UserInfo
{
    public string? IdUser { get; set; }
    public string? UserName { get; set; }
    public string? FullName { get; set; }
    public byte[]? Image { get; set; }
    public string? IdSegmentList { get; set; } // Thêm thuộc tính này
}
