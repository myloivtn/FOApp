using Maui.GoogleMaps;

namespace FOApp;

public static class Constants
{
    public const string sqlConnectionString = "Data Source=10.147.28.92;Initial Catalog=NET3DB;User ID=sa;Password=Kharkyf215;Persist Security Info=True;Encrypt=true;TrustServerCertificate=true;";
    public const string GOOGLE_PLACES_API_KEY = "AIzaSyCvARxyqOR8SQB-X8jMHNnRSFLZdSeEmM8";
    public const string serverIPAddress = "10.147.28.92";
    public static readonly double radiusInKm = 2, desiredPinCount = 50, defaultDistancekm = 2;
    public static readonly int topSegments = 5, minClusterSize = 3, maxDistanceBetweenClustered =20;

    //public static readonly Location defaultLocation = new(16.075363, 108.234106);
    public static readonly Position defaultLocation = new Position(16.075363, 108.234106);

    public const string DatabaseFilename = "FiberSQLite.db3";
    public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
    
    //userLocation = new Location(15.439440, 107.784975); // PSN 
    //userLocation = new Location(16.042025, 108.222715); //  2T9
    //userLocation = new Location(16.075363, 108.234106);  // ADN 

    public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

   
}
