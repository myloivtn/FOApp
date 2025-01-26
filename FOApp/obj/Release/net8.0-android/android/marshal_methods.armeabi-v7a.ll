; ModuleID = 'marshal_methods.armeabi-v7a.ll'
source_filename = "marshal_methods.armeabi-v7a.ll"
target datalayout = "e-m:e-p:32:32-Fi8-i64:64-v128:64:128-a:0:32-n32-S64"
target triple = "armv7-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [214 x ptr] zeroinitializer, align 4

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [428 x i32] [
	i32 2616222, ; 0: System.Net.NetworkInformation.dll => 0x27eb9e => 156
	i32 10166715, ; 1: System.Net.NameResolution.dll => 0x9b21bb => 155
	i32 39109920, ; 2: Newtonsoft.Json.dll => 0x254c520 => 72
	i32 42639949, ; 3: System.Threading.Thread => 0x28aa24d => 200
	i32 57725457, ; 4: it\Microsoft.Data.SqlClient.resources => 0x370d211 => 3
	i32 57727992, ; 5: ja\Microsoft.Data.SqlClient.resources => 0x370dbf8 => 4
	i32 66541672, ; 6: System.Diagnostics.StackTrace => 0x3f75868 => 139
	i32 67008169, ; 7: zh-Hant\Microsoft.Maui.Controls.resources => 0x3fe76a9 => 43
	i32 68219467, ; 8: System.Security.Cryptography.Primitives => 0x410f24b => 189
	i32 72070932, ; 9: Microsoft.Maui.Graphics.dll => 0x44bb714 => 70
	i32 117431740, ; 10: System.Runtime.InteropServices => 0x6ffddbc => 175
	i32 122350210, ; 11: System.Threading.Channels.dll => 0x74aea82 => 198
	i32 139659294, ; 12: ja/Microsoft.Data.SqlClient.resources.dll => 0x853081e => 4
	i32 142721839, ; 13: System.Net.WebHeaderCollection => 0x881c32f => 164
	i32 149972175, ; 14: System.Security.Cryptography.Primitives.dll => 0x8f064cf => 189
	i32 166535111, ; 15: ru/Microsoft.Data.SqlClient.resources.dll => 0x9ed1fc7 => 7
	i32 182336117, ; 16: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 113
	i32 195452805, ; 17: vi/Microsoft.Maui.Controls.resources.dll => 0xba65f85 => 40
	i32 199333315, ; 18: zh-HK/Microsoft.Maui.Controls.resources.dll => 0xbe195c3 => 41
	i32 205061960, ; 19: System.ComponentModel => 0xc38ff48 => 134
	i32 209399409, ; 20: Xamarin.AndroidX.Browser.dll => 0xc7b2e71 => 94
	i32 230752869, ; 21: Microsoft.CSharp.dll => 0xdc10265 => 124
	i32 246610117, ; 22: System.Reflection.Emit.Lightweight => 0xeb2f8c5 => 172
	i32 264223668, ; 23: zh-Hans\Microsoft.Data.SqlClient.resources => 0xfbfbbb4 => 8
	i32 280992041, ; 24: cs/Microsoft.Maui.Controls.resources.dll => 0x10bf9929 => 12
	i32 298918909, ; 25: System.Net.Ping.dll => 0x11d123fd => 157
	i32 317674968, ; 26: vi\Microsoft.Maui.Controls.resources => 0x12ef55d8 => 40
	i32 318968648, ; 27: Xamarin.AndroidX.Activity.dll => 0x13031348 => 91
	i32 330147069, ; 28: Microsoft.SqlServer.Server => 0x13ada4fd => 71
	i32 336156722, ; 29: ja/Microsoft.Maui.Controls.resources.dll => 0x14095832 => 25
	i32 338609096, ; 30: Maui.GoogleMaps => 0x142ec3c8 => 73
	i32 342366114, ; 31: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 102
	i32 347068432, ; 32: SQLitePCLRaw.lib.e_sqlite3.android.dll => 0x14afd810 => 83
	i32 356389973, ; 33: it/Microsoft.Maui.Controls.resources.dll => 0x153e1455 => 24
	i32 367780167, ; 34: System.IO.Pipes => 0x15ebe147 => 150
	i32 374914964, ; 35: System.Transactions.Local => 0x1658bf94 => 203
	i32 375677976, ; 36: System.Net.ServicePoint.dll => 0x16646418 => 161
	i32 379916513, ; 37: System.Threading.Thread.dll => 0x16a510e1 => 200
	i32 385762202, ; 38: System.Memory.dll => 0x16fe439a => 153
	i32 392610295, ; 39: System.Threading.ThreadPool.dll => 0x1766c1f7 => 201
	i32 395744057, ; 40: _Microsoft.Android.Resource.Designer => 0x17969339 => 44
	i32 435591531, ; 41: sv/Microsoft.Maui.Controls.resources.dll => 0x19f6996b => 36
	i32 442565967, ; 42: System.Collections => 0x1a61054f => 131
	i32 450948140, ; 43: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 101
	i32 451504562, ; 44: System.Security.Cryptography.X509Certificates => 0x1ae969b2 => 190
	i32 459347974, ; 45: System.Runtime.Serialization.Primitives.dll => 0x1b611806 => 180
	i32 469710990, ; 46: System.dll => 0x1bff388e => 208
	i32 485463106, ; 47: Microsoft.IdentityModel.Abstractions => 0x1cef9442 => 59
	i32 498788369, ; 48: System.ObjectModel => 0x1dbae811 => 166
	i32 500358224, ; 49: id/Microsoft.Maui.Controls.resources.dll => 0x1dd2dc50 => 23
	i32 503918385, ; 50: fi/Microsoft.Maui.Controls.resources.dll => 0x1e092f31 => 17
	i32 513247710, ; 51: Microsoft.Extensions.Primitives.dll => 0x1e9789de => 56
	i32 515182489, ; 52: Google.Maps.Utils.Android => 0x1eb50f99 => 117
	i32 525008092, ; 53: SkiaSharp.dll => 0x1f4afcdc => 75
	i32 539058512, ; 54: Microsoft.Extensions.Logging => 0x20216150 => 53
	i32 546455878, ; 55: System.Runtime.Serialization.Xml => 0x20924146 => 181
	i32 548916678, ; 56: Microsoft.Bcl.AsyncInterfaces => 0x20b7cdc6 => 47
	i32 577335427, ; 57: System.Security.Cryptography.Cng => 0x22697083 => 186
	i32 592146354, ; 58: pt-BR/Microsoft.Maui.Controls.resources.dll => 0x234b6fb2 => 31
	i32 613668793, ; 59: System.Security.Cryptography.Algorithms => 0x2493d7b9 => 185
	i32 627609679, ; 60: Xamarin.AndroidX.CustomView => 0x2568904f => 99
	i32 627931235, ; 61: nl\Microsoft.Maui.Controls.resources => 0x256d7863 => 29
	i32 630587553, ; 62: SkiaSharp.Extended.Svg.dll => 0x259600a1 => 76
	i32 662205335, ; 63: System.Text.Encodings.Web.dll => 0x27787397 => 195
	i32 672442732, ; 64: System.Collections.Concurrent => 0x2814a96c => 127
	i32 683518922, ; 65: System.Net.Security => 0x28bdabca => 160
	i32 688181140, ; 66: ca/Microsoft.Maui.Controls.resources.dll => 0x2904cf94 => 11
	i32 690569205, ; 67: System.Xml.Linq.dll => 0x29293ff5 => 204
	i32 706645707, ; 68: ko/Microsoft.Maui.Controls.resources.dll => 0x2a1e8ecb => 26
	i32 709557578, ; 69: de/Microsoft.Maui.Controls.resources.dll => 0x2a4afd4a => 14
	i32 722857257, ; 70: System.Runtime.Loader.dll => 0x2b15ed29 => 176
	i32 748832960, ; 71: SQLitePCLRaw.batteries_v2 => 0x2ca248c0 => 81
	i32 759454413, ; 72: System.Net.Requests => 0x2d445acd => 159
	i32 762598435, ; 73: System.IO.Pipes.dll => 0x2d745423 => 150
	i32 775507847, ; 74: System.IO.Compression => 0x2e394f87 => 147
	i32 777317022, ; 75: sk\Microsoft.Maui.Controls.resources => 0x2e54ea9e => 35
	i32 789151979, ; 76: Microsoft.Extensions.Options => 0x2f0980eb => 55
	i32 804715423, ; 77: System.Data.Common => 0x2ff6fb9f => 136
	i32 823281589, ; 78: System.Private.Uri.dll => 0x311247b5 => 168
	i32 830298997, ; 79: System.IO.Compression.Brotli => 0x317d5b75 => 146
	i32 904024072, ; 80: System.ComponentModel.Primitives.dll => 0x35e25008 => 132
	i32 926902833, ; 81: tr/Microsoft.Maui.Controls.resources.dll => 0x373f6a31 => 38
	i32 955402788, ; 82: Newtonsoft.Json => 0x38f24a24 => 72
	i32 967690846, ; 83: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 102
	i32 975236339, ; 84: System.Diagnostics.Tracing => 0x3a20ecf3 => 142
	i32 975874589, ; 85: System.Xml.XDocument => 0x3a2aaa1d => 206
	i32 986514023, ; 86: System.Private.DataContractSerialization.dll => 0x3acd0267 => 167
	i32 992768348, ; 87: System.Collections.dll => 0x3b2c715c => 131
	i32 1012816738, ; 88: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 112
	i32 1019214401, ; 89: System.Drawing => 0x3cbffa41 => 144
	i32 1028951442, ; 90: Microsoft.Extensions.DependencyInjection.Abstractions => 0x3d548d92 => 52
	i32 1029334545, ; 91: da/Microsoft.Maui.Controls.resources.dll => 0x3d5a6611 => 13
	i32 1035644815, ; 92: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 92
	i32 1036536393, ; 93: System.Drawing.Primitives.dll => 0x3dc84a49 => 143
	i32 1044663988, ; 94: System.Linq.Expressions.dll => 0x3e444eb4 => 151
	i32 1048439329, ; 95: de/Microsoft.Data.SqlClient.resources.dll => 0x3e7dea21 => 0
	i32 1052210849, ; 96: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 104
	i32 1062017875, ; 97: Microsoft.Identity.Client.Extensions.Msal => 0x3f4d1b53 => 58
	i32 1082857460, ; 98: System.ComponentModel.TypeConverter => 0x408b17f4 => 133
	i32 1084122840, ; 99: Xamarin.Kotlin.StdLib => 0x409e66d8 => 121
	i32 1089913930, ; 100: System.Diagnostics.EventLog.dll => 0x40f6c44a => 86
	i32 1098259244, ; 101: System => 0x41761b2c => 208
	i32 1118262833, ; 102: ko\Microsoft.Maui.Controls.resources => 0x42a75631 => 26
	i32 1138436374, ; 103: Microsoft.Data.SqlClient.dll => 0x43db2916 => 48
	i32 1168523401, ; 104: pt\Microsoft.Maui.Controls.resources => 0x45a64089 => 32
	i32 1178241025, ; 105: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 109
	i32 1203215381, ; 106: pl/Microsoft.Maui.Controls.resources.dll => 0x47b79c15 => 30
	i32 1208641965, ; 107: System.Diagnostics.Process => 0x480a69ad => 138
	i32 1234928153, ; 108: nb/Microsoft.Maui.Controls.resources.dll => 0x499b8219 => 28
	i32 1260983243, ; 109: cs\Microsoft.Maui.Controls.resources => 0x4b2913cb => 12
	i32 1276684945, ; 110: Maui.GoogleMaps.Clustering => 0x4c18aa91 => 74
	i32 1292207520, ; 111: SQLitePCLRaw.core.dll => 0x4d0585a0 => 82
	i32 1293217323, ; 112: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 100
	i32 1309188875, ; 113: System.Private.DataContractSerialization => 0x4e08a30b => 167
	i32 1324164729, ; 114: System.Linq => 0x4eed2679 => 152
	i32 1335329327, ; 115: System.Runtime.Serialization.Json.dll => 0x4f97822f => 179
	i32 1373134921, ; 116: zh-Hans\Microsoft.Maui.Controls.resources => 0x51d86049 => 42
	i32 1376866003, ; 117: Xamarin.AndroidX.SavedState => 0x52114ed3 => 112
	i32 1406073936, ; 118: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 96
	i32 1408764838, ; 119: System.Runtime.Serialization.Formatters.dll => 0x53f80ba6 => 178
	i32 1430672901, ; 120: ar\Microsoft.Maui.Controls.resources => 0x55465605 => 10
	i32 1452070440, ; 121: System.Formats.Asn1.dll => 0x568cd628 => 145
	i32 1458022317, ; 122: System.Net.Security.dll => 0x56e7a7ad => 160
	i32 1460893475, ; 123: System.IdentityModel.Tokens.Jwt => 0x57137723 => 87
	i32 1461004990, ; 124: es\Microsoft.Maui.Controls.resources => 0x57152abe => 16
	i32 1461234159, ; 125: System.Collections.Immutable.dll => 0x5718a9ef => 128
	i32 1462112819, ; 126: System.IO.Compression.dll => 0x57261233 => 147
	i32 1469204771, ; 127: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 93
	i32 1470490898, ; 128: Microsoft.Extensions.Primitives => 0x57a5e912 => 56
	i32 1479771757, ; 129: System.Collections.Immutable => 0x5833866d => 128
	i32 1480492111, ; 130: System.IO.Compression.Brotli.dll => 0x583e844f => 146
	i32 1487239319, ; 131: Microsoft.Win32.Primitives => 0x58a57897 => 125
	i32 1493001747, ; 132: hi/Microsoft.Maui.Controls.resources.dll => 0x58fd6613 => 20
	i32 1498168481, ; 133: Microsoft.IdentityModel.JsonWebTokens.dll => 0x594c3ca1 => 60
	i32 1514721132, ; 134: el/Microsoft.Maui.Controls.resources.dll => 0x5a48cf6c => 15
	i32 1536373174, ; 135: System.Diagnostics.TextWriterTraceListener => 0x5b9331b6 => 140
	i32 1543031311, ; 136: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 197
	i32 1551623176, ; 137: sk/Microsoft.Maui.Controls.resources.dll => 0x5c7be408 => 35
	i32 1565310744, ; 138: System.Runtime.Caching => 0x5d4cbf18 => 89
	i32 1573704789, ; 139: System.Runtime.Serialization.Json => 0x5dccd455 => 179
	i32 1582305585, ; 140: Azure.Identity => 0x5e501131 => 46
	i32 1596263029, ; 141: zh-Hant\Microsoft.Data.SqlClient.resources => 0x5f250a75 => 9
	i32 1604827217, ; 142: System.Net.WebClient => 0x5fa7b851 => 163
	i32 1622152042, ; 143: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 106
	i32 1623212457, ; 144: SkiaSharp.Views.Maui.Controls => 0x60c041a9 => 78
	i32 1624863272, ; 145: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 115
	i32 1628113371, ; 146: Microsoft.IdentityModel.Protocols.OpenIdConnect => 0x610b09db => 63
	i32 1636350590, ; 147: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 98
	i32 1639515021, ; 148: System.Net.Http.dll => 0x61b9038d => 154
	i32 1639986890, ; 149: System.Text.RegularExpressions => 0x61c036ca => 197
	i32 1657153582, ; 150: System.Runtime => 0x62c6282e => 182
	i32 1658251792, ; 151: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 116
	i32 1677501392, ; 152: System.Net.Primitives.dll => 0x63fca3d0 => 158
	i32 1679769178, ; 153: System.Security.Cryptography => 0x641f3e5a => 191
	i32 1696967625, ; 154: System.Security.Cryptography.Csp => 0x6525abc9 => 187
	i32 1711441057, ; 155: SQLitePCLRaw.lib.e_sqlite3.android => 0x660284a1 => 83
	i32 1729485958, ; 156: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 95
	i32 1736233607, ; 157: ro/Microsoft.Maui.Controls.resources.dll => 0x677cd287 => 33
	i32 1743415430, ; 158: ca\Microsoft.Maui.Controls.resources => 0x67ea6886 => 11
	i32 1744735666, ; 159: System.Transactions.Local.dll => 0x67fe8db2 => 203
	i32 1750313021, ; 160: Microsoft.Win32.Primitives.dll => 0x6853a83d => 125
	i32 1763938596, ; 161: System.Diagnostics.TraceSource.dll => 0x69239124 => 141
	i32 1766324549, ; 162: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 113
	i32 1770582343, ; 163: Microsoft.Extensions.Logging.dll => 0x6988f147 => 53
	i32 1780572499, ; 164: Mono.Android.Runtime.dll => 0x6a216153 => 212
	i32 1782862114, ; 165: ms\Microsoft.Maui.Controls.resources => 0x6a445122 => 27
	i32 1788241197, ; 166: Xamarin.AndroidX.Fragment => 0x6a96652d => 101
	i32 1793755602, ; 167: he\Microsoft.Maui.Controls.resources => 0x6aea89d2 => 19
	i32 1794500907, ; 168: Microsoft.Identity.Client.dll => 0x6af5e92b => 57
	i32 1796167890, ; 169: Microsoft.Bcl.AsyncInterfaces.dll => 0x6b0f58d2 => 47
	i32 1808609942, ; 170: Xamarin.AndroidX.Loader => 0x6bcd3296 => 106
	i32 1809552118, ; 171: FOApp.dll => 0x6bdb92f6 => 123
	i32 1813058853, ; 172: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 121
	i32 1813201214, ; 173: Xamarin.Google.Android.Material => 0x6c13413e => 116
	i32 1818569960, ; 174: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 110
	i32 1824175904, ; 175: System.Text.Encoding.Extensions => 0x6cbab720 => 194
	i32 1824722060, ; 176: System.Runtime.Serialization.Formatters => 0x6cc30c8c => 178
	i32 1828688058, ; 177: Microsoft.Extensions.Logging.Abstractions.dll => 0x6cff90ba => 54
	i32 1842015223, ; 178: uk/Microsoft.Maui.Controls.resources.dll => 0x6dcaebf7 => 39
	i32 1853025655, ; 179: sv\Microsoft.Maui.Controls.resources => 0x6e72ed77 => 36
	i32 1858542181, ; 180: System.Linq.Expressions => 0x6ec71a65 => 151
	i32 1870277092, ; 181: System.Reflection.Primitives => 0x6f7a29e4 => 173
	i32 1871986876, ; 182: Microsoft.IdentityModel.Protocols.OpenIdConnect.dll => 0x6f9440bc => 63
	i32 1875935024, ; 183: fr\Microsoft.Maui.Controls.resources => 0x6fd07f30 => 18
	i32 1908813208, ; 184: Xamarin.GooglePlayServices.Basement => 0x71c62d98 => 119
	i32 1910275211, ; 185: System.Collections.NonGeneric.dll => 0x71dc7c8b => 129
	i32 1939592360, ; 186: System.Private.Xml.Linq => 0x739bd4a8 => 169
	i32 1968388702, ; 187: Microsoft.Extensions.Configuration.dll => 0x75533a5e => 49
	i32 1986222447, ; 188: Microsoft.IdentityModel.Tokens.dll => 0x7663596f => 64
	i32 2003115576, ; 189: el\Microsoft.Maui.Controls.resources => 0x77651e38 => 15
	i32 2011961780, ; 190: System.Buffers.dll => 0x77ec19b4 => 126
	i32 2019465201, ; 191: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 104
	i32 2025202353, ; 192: ar/Microsoft.Maui.Controls.resources.dll => 0x78b622b1 => 10
	i32 2040764568, ; 193: Microsoft.Identity.Client.Extensions.Msal.dll => 0x79a39898 => 58
	i32 2045470958, ; 194: System.Private.Xml => 0x79eb68ee => 170
	i32 2055257422, ; 195: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 103
	i32 2066184531, ; 196: de\Microsoft.Maui.Controls.resources => 0x7b277953 => 14
	i32 2070888862, ; 197: System.Diagnostics.TraceSource => 0x7b6f419e => 141
	i32 2079903147, ; 198: System.Runtime.dll => 0x7bf8cdab => 182
	i32 2090596640, ; 199: System.Numerics.Vectors => 0x7c9bf920 => 165
	i32 2103459038, ; 200: SQLitePCLRaw.provider.e_sqlite3.dll => 0x7d603cde => 84
	i32 2127167465, ; 201: System.Console => 0x7ec9ffe9 => 135
	i32 2129483829, ; 202: Xamarin.GooglePlayServices.Base.dll => 0x7eed5835 => 118
	i32 2134309359, ; 203: Maui.GoogleMaps.Clustering.dll => 0x7f36f9ef => 74
	i32 2142473426, ; 204: System.Collections.Specialized => 0x7fb38cd2 => 130
	i32 2143790110, ; 205: System.Xml.XmlSerializer.dll => 0x7fc7a41e => 207
	i32 2159891885, ; 206: Microsoft.Maui => 0x80bd55ad => 68
	i32 2169148018, ; 207: hu\Microsoft.Maui.Controls.resources => 0x814a9272 => 22
	i32 2181898931, ; 208: Microsoft.Extensions.Options.dll => 0x820d22b3 => 55
	i32 2192057212, ; 209: Microsoft.Extensions.Logging.Abstractions => 0x82a8237c => 54
	i32 2193016926, ; 210: System.ObjectModel.dll => 0x82b6c85e => 166
	i32 2201107256, ; 211: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 122
	i32 2201231467, ; 212: System.Net.Http => 0x8334206b => 154
	i32 2207618523, ; 213: it\Microsoft.Maui.Controls.resources => 0x839595db => 24
	i32 2228745826, ; 214: pt-BR\Microsoft.Data.SqlClient.resources => 0x84d7f662 => 6
	i32 2253551641, ; 215: Microsoft.IdentityModel.Protocols => 0x86527819 => 62
	i32 2265110946, ; 216: System.Security.AccessControl.dll => 0x8702d9a2 => 183
	i32 2266799131, ; 217: Microsoft.Extensions.Configuration.Abstractions => 0x871c9c1b => 50
	i32 2270573516, ; 218: fr/Microsoft.Maui.Controls.resources.dll => 0x875633cc => 18
	i32 2279755925, ; 219: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 111
	i32 2295906218, ; 220: System.Net.Sockets => 0x88d8bfaa => 162
	i32 2303942373, ; 221: nb\Microsoft.Maui.Controls.resources => 0x89535ee5 => 28
	i32 2305521784, ; 222: System.Private.CoreLib.dll => 0x896b7878 => 210
	i32 2309278602, ; 223: ko\Microsoft.Data.SqlClient.resources => 0x89a4cb8a => 5
	i32 2340441535, ; 224: System.Runtime.InteropServices.RuntimeInformation.dll => 0x8b804dbf => 174
	i32 2353062107, ; 225: System.Net.Primitives => 0x8c40e0db => 158
	i32 2364201794, ; 226: SkiaSharp.Views.Maui.Core => 0x8ceadb42 => 79
	i32 2368005991, ; 227: System.Xml.ReaderWriter.dll => 0x8d24e767 => 205
	i32 2369706906, ; 228: Microsoft.IdentityModel.Logging => 0x8d3edb9a => 61
	i32 2371007202, ; 229: Microsoft.Extensions.Configuration => 0x8d52b2e2 => 49
	i32 2378619854, ; 230: System.Security.Cryptography.Csp.dll => 0x8dc6dbce => 187
	i32 2383496789, ; 231: System.Security.Principal.Windows.dll => 0x8e114655 => 192
	i32 2395872292, ; 232: id\Microsoft.Maui.Controls.resources => 0x8ece1c24 => 23
	i32 2427813419, ; 233: hi\Microsoft.Maui.Controls.resources => 0x90b57e2b => 20
	i32 2435356389, ; 234: System.Console.dll => 0x912896e5 => 135
	i32 2458678730, ; 235: System.Net.Sockets.dll => 0x928c75ca => 162
	i32 2465273461, ; 236: SQLitePCLRaw.batteries_v2.dll => 0x92f11675 => 81
	i32 2471841756, ; 237: netstandard.dll => 0x93554fdc => 209
	i32 2475788418, ; 238: Java.Interop.dll => 0x93918882 => 211
	i32 2480646305, ; 239: Microsoft.Maui.Controls => 0x93dba8a1 => 66
	i32 2484371297, ; 240: System.Net.ServicePoint => 0x94147f61 => 161
	i32 2509217888, ; 241: System.Diagnostics.EventLog => 0x958fa060 => 86
	i32 2538310050, ; 242: System.Reflection.Emit.Lightweight.dll => 0x974b89a2 => 172
	i32 2550873716, ; 243: hr\Microsoft.Maui.Controls.resources => 0x980b3e74 => 21
	i32 2562349572, ; 244: Microsoft.CSharp => 0x98ba5a04 => 124
	i32 2570120770, ; 245: System.Text.Encodings.Web => 0x9930ee42 => 195
	i32 2585220780, ; 246: System.Text.Encoding.Extensions.dll => 0x9a1756ac => 194
	i32 2585805581, ; 247: System.Net.Ping => 0x9a20430d => 157
	i32 2589602615, ; 248: System.Threading.ThreadPool => 0x9a5a3337 => 201
	i32 2593496499, ; 249: pl\Microsoft.Maui.Controls.resources => 0x9a959db3 => 30
	i32 2605712449, ; 250: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 122
	i32 2617129537, ; 251: System.Private.Xml.dll => 0x9bfe3a41 => 170
	i32 2620871830, ; 252: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 98
	i32 2625339995, ; 253: SkiaSharp.Views.Maui.Core.dll => 0x9c7b825b => 79
	i32 2626831493, ; 254: ja\Microsoft.Maui.Controls.resources => 0x9c924485 => 25
	i32 2627185994, ; 255: System.Diagnostics.TextWriterTraceListener.dll => 0x9c97ad4a => 140
	i32 2628210652, ; 256: System.Memory.Data => 0x9ca74fdc => 88
	i32 2640290731, ; 257: Microsoft.IdentityModel.Logging.dll => 0x9d5fa3ab => 61
	i32 2640706905, ; 258: Azure.Core => 0x9d65fd59 => 45
	i32 2660759594, ; 259: System.Security.Cryptography.ProtectedData.dll => 0x9e97f82a => 90
	i32 2663698177, ; 260: System.Runtime.Loader => 0x9ec4cf01 => 176
	i32 2664396074, ; 261: System.Xml.XDocument.dll => 0x9ecf752a => 206
	i32 2665622720, ; 262: System.Drawing.Primitives => 0x9ee22cc0 => 143
	i32 2676780864, ; 263: System.Data.Common.dll => 0x9f8c6f40 => 136
	i32 2677098746, ; 264: Azure.Identity.dll => 0x9f9148fa => 46
	i32 2686887180, ; 265: System.Runtime.Serialization.Xml.dll => 0xa026a50c => 181
	i32 2717744543, ; 266: System.Security.Claims => 0xa1fd7d9f => 184
	i32 2719963679, ; 267: System.Security.Cryptography.Cng.dll => 0xa21f5a1f => 186
	i32 2724373263, ; 268: System.Runtime.Numerics.dll => 0xa262a30f => 177
	i32 2732626843, ; 269: Xamarin.AndroidX.Activity => 0xa2e0939b => 91
	i32 2735172069, ; 270: System.Threading.Channels => 0xa30769e5 => 198
	i32 2737747696, ; 271: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 93
	i32 2740051746, ; 272: Microsoft.Identity.Client => 0xa351df22 => 57
	i32 2752995522, ; 273: pt-BR\Microsoft.Maui.Controls.resources => 0xa41760c2 => 31
	i32 2755098380, ; 274: Microsoft.SqlServer.Server.dll => 0xa437770c => 71
	i32 2758225723, ; 275: Microsoft.Maui.Controls.Xaml => 0xa4672f3b => 67
	i32 2764765095, ; 276: Microsoft.Maui.dll => 0xa4caf7a7 => 68
	i32 2765824710, ; 277: System.Text.Encoding.CodePages.dll => 0xa4db22c6 => 193
	i32 2778768386, ; 278: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 114
	i32 2785988530, ; 279: th\Microsoft.Maui.Controls.resources => 0xa60ecfb2 => 37
	i32 2795602088, ; 280: SkiaSharp.Views.Android.dll => 0xa6a180a8 => 77
	i32 2801831435, ; 281: Microsoft.Maui.Graphics => 0xa7008e0b => 70
	i32 2804509662, ; 282: es/Microsoft.Data.SqlClient.resources.dll => 0xa7296bde => 1
	i32 2806116107, ; 283: es/Microsoft.Maui.Controls.resources.dll => 0xa741ef0b => 16
	i32 2810250172, ; 284: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 96
	i32 2831556043, ; 285: nl/Microsoft.Maui.Controls.resources.dll => 0xa8c61dcb => 29
	i32 2841937114, ; 286: it/Microsoft.Data.SqlClient.resources.dll => 0xa96484da => 3
	i32 2847418871, ; 287: Xamarin.GooglePlayServices.Base => 0xa9b829f7 => 118
	i32 2853208004, ; 288: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 114
	i32 2854551965, ; 289: SkiaSharp.Extended.Svg => 0xaa25019d => 76
	i32 2861189240, ; 290: Microsoft.Maui.Essentials => 0xaa8a4878 => 69
	i32 2867946736, ; 291: System.Security.Cryptography.ProtectedData => 0xaaf164f0 => 90
	i32 2909740682, ; 292: System.Private.CoreLib => 0xad6f1e8a => 210
	i32 2912489636, ; 293: SkiaSharp.Views.Android => 0xad9910a4 => 77
	i32 2916838712, ; 294: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 115
	i32 2919462931, ; 295: System.Numerics.Vectors.dll => 0xae037813 => 165
	i32 2940926066, ; 296: System.Diagnostics.StackTrace.dll => 0xaf4af872 => 139
	i32 2944313911, ; 297: System.Configuration.ConfigurationManager.dll => 0xaf7eaa37 => 85
	i32 2959614098, ; 298: System.ComponentModel.dll => 0xb0682092 => 134
	i32 2968338931, ; 299: System.Security.Principal.Windows => 0xb0ed41f3 => 192
	i32 2972252294, ; 300: System.Security.Cryptography.Algorithms.dll => 0xb128f886 => 185
	i32 2978675010, ; 301: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 100
	i32 3012788804, ; 302: System.Configuration.ConfigurationManager => 0xb3938244 => 85
	i32 3017076677, ; 303: Xamarin.GooglePlayServices.Maps => 0xb3d4efc5 => 120
	i32 3023511517, ; 304: ru\Microsoft.Data.SqlClient.resources => 0xb4371fdd => 7
	i32 3033605958, ; 305: System.Memory.Data.dll => 0xb4d12746 => 88
	i32 3035389318, ; 306: Maui.GoogleMaps.dll => 0xb4ec5d86 => 73
	i32 3038032645, ; 307: _Microsoft.Android.Resource.Designer.dll => 0xb514b305 => 44
	i32 3057625584, ; 308: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 107
	i32 3059408633, ; 309: Mono.Android.Runtime => 0xb65adef9 => 212
	i32 3059793426, ; 310: System.ComponentModel.Primitives => 0xb660be12 => 132
	i32 3077302341, ; 311: hu/Microsoft.Maui.Controls.resources.dll => 0xb76be845 => 22
	i32 3084678329, ; 312: Microsoft.IdentityModel.Tokens => 0xb7dc74b9 => 64
	i32 3090735792, ; 313: System.Security.Cryptography.X509Certificates.dll => 0xb838e2b0 => 190
	i32 3099732863, ; 314: System.Security.Claims.dll => 0xb8c22b7f => 184
	i32 3103600923, ; 315: System.Formats.Asn1 => 0xb8fd311b => 145
	i32 3121463068, ; 316: System.IO.FileSystem.AccessControl.dll => 0xba0dbf1c => 148
	i32 3124832203, ; 317: System.Threading.Tasks.Extensions => 0xba4127cb => 199
	i32 3132293585, ; 318: System.Security.AccessControl => 0xbab301d1 => 183
	i32 3147165239, ; 319: System.Diagnostics.Tracing.dll => 0xbb95ee37 => 142
	i32 3158628304, ; 320: zh-Hant/Microsoft.Data.SqlClient.resources.dll => 0xbc44d7d0 => 9
	i32 3159123045, ; 321: System.Reflection.Primitives.dll => 0xbc4c6465 => 173
	i32 3178803400, ; 322: Xamarin.AndroidX.Navigation.Fragment.dll => 0xbd78b0c8 => 108
	i32 3220365878, ; 323: System.Threading => 0xbff2e236 => 202
	i32 3230466174, ; 324: Xamarin.GooglePlayServices.Basement.dll => 0xc08d007e => 119
	i32 3258312781, ; 325: Xamarin.AndroidX.CardView => 0xc235e84d => 95
	i32 3265893370, ; 326: System.Threading.Tasks.Extensions.dll => 0xc2a993fa => 199
	i32 3268887220, ; 327: fr/Microsoft.Data.SqlClient.resources.dll => 0xc2d742b4 => 2
	i32 3276600297, ; 328: pt-BR/Microsoft.Data.SqlClient.resources.dll => 0xc34cf3e9 => 6
	i32 3286872994, ; 329: SQLite-net.dll => 0xc3e9b3a2 => 80
	i32 3290767353, ; 330: System.Security.Cryptography.Encoding => 0xc4251ff9 => 188
	i32 3305363605, ; 331: fi\Microsoft.Maui.Controls.resources => 0xc503d895 => 17
	i32 3312457198, ; 332: Microsoft.IdentityModel.JsonWebTokens => 0xc57015ee => 60
	i32 3316684772, ; 333: System.Net.Requests.dll => 0xc5b097e4 => 159
	i32 3317135071, ; 334: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 99
	i32 3330272589, ; 335: FOApp => 0xc67fed4d => 123
	i32 3340387945, ; 336: SkiaSharp => 0xc71a4669 => 75
	i32 3343947874, ; 337: fr\Microsoft.Data.SqlClient.resources => 0xc7509862 => 2
	i32 3346324047, ; 338: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 109
	i32 3357674450, ; 339: ru\Microsoft.Maui.Controls.resources => 0xc8220bd2 => 34
	i32 3358260929, ; 340: System.Text.Json => 0xc82afec1 => 196
	i32 3360279109, ; 341: SQLitePCLRaw.core => 0xc849ca45 => 82
	i32 3362522851, ; 342: Xamarin.AndroidX.Core => 0xc86c06e3 => 97
	i32 3366347497, ; 343: Java.Interop => 0xc8a662e9 => 211
	i32 3374879918, ; 344: Microsoft.IdentityModel.Protocols.dll => 0xc92894ae => 62
	i32 3374999561, ; 345: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 111
	i32 3381016424, ; 346: da\Microsoft.Maui.Controls.resources => 0xc9863768 => 13
	i32 3428513518, ; 347: Microsoft.Extensions.DependencyInjection.dll => 0xcc5af6ee => 51
	i32 3430777524, ; 348: netstandard => 0xcc7d82b4 => 209
	i32 3452344032, ; 349: Microsoft.Maui.Controls.Compatibility.dll => 0xcdc696e0 => 65
	i32 3463511458, ; 350: hr/Microsoft.Maui.Controls.resources.dll => 0xce70fda2 => 21
	i32 3471940407, ; 351: System.ComponentModel.TypeConverter.dll => 0xcef19b37 => 133
	i32 3473156932, ; 352: SkiaSharp.Views.Maui.Controls.dll => 0xcf042b44 => 78
	i32 3476120550, ; 353: Mono.Android => 0xcf3163e6 => 213
	i32 3479583265, ; 354: ru/Microsoft.Maui.Controls.resources.dll => 0xcf663a21 => 34
	i32 3484440000, ; 355: ro\Microsoft.Maui.Controls.resources => 0xcfb055c0 => 33
	i32 3485117614, ; 356: System.Text.Json.dll => 0xcfbaacae => 196
	i32 3509114376, ; 357: System.Xml.Linq => 0xd128d608 => 204
	i32 3545306353, ; 358: Microsoft.Data.SqlClient => 0xd35114f1 => 48
	i32 3555084973, ; 359: de\Microsoft.Data.SqlClient.resources => 0xd3e64aad => 0
	i32 3561949811, ; 360: Azure.Core.dll => 0xd44f0a73 => 45
	i32 3570554715, ; 361: System.IO.FileSystem.AccessControl => 0xd4d2575b => 148
	i32 3570608287, ; 362: System.Runtime.Caching.dll => 0xd4d3289f => 89
	i32 3580758918, ; 363: zh-HK\Microsoft.Maui.Controls.resources => 0xd56e0b86 => 41
	i32 3608519521, ; 364: System.Linq.dll => 0xd715a361 => 152
	i32 3624195450, ; 365: System.Runtime.InteropServices.RuntimeInformation => 0xd804d57a => 174
	i32 3641597786, ; 366: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 103
	i32 3643446276, ; 367: tr\Microsoft.Maui.Controls.resources => 0xd92a9404 => 38
	i32 3643854240, ; 368: Xamarin.AndroidX.Navigation.Fragment => 0xd930cda0 => 108
	i32 3657292374, ; 369: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd9fdda56 => 50
	i32 3660523487, ; 370: System.Net.NetworkInformation => 0xda2f27df => 156
	i32 3672681054, ; 371: Mono.Android.dll => 0xdae8aa5e => 213
	i32 3682565725, ; 372: Xamarin.AndroidX.Browser => 0xdb7f7e5d => 94
	i32 3697841164, ; 373: zh-Hant/Microsoft.Maui.Controls.resources.dll => 0xdc68940c => 43
	i32 3700591436, ; 374: Microsoft.IdentityModel.Abstractions.dll => 0xdc928b4c => 59
	i32 3724971120, ; 375: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 107
	i32 3732100267, ; 376: System.Net.NameResolution => 0xde7354ab => 155
	i32 3748608112, ; 377: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 137
	i32 3754567612, ; 378: SQLitePCLRaw.provider.e_sqlite3 => 0xdfca27bc => 84
	i32 3792276235, ; 379: System.Collections.NonGeneric => 0xe2098b0b => 129
	i32 3800979733, ; 380: Microsoft.Maui.Controls.Compatibility => 0xe28e5915 => 65
	i32 3802395368, ; 381: System.Collections.Specialized.dll => 0xe2a3f2e8 => 130
	i32 3803019198, ; 382: zh-Hans/Microsoft.Data.SqlClient.resources.dll => 0xe2ad77be => 8
	i32 3823082795, ; 383: System.Security.Cryptography.dll => 0xe3df9d2b => 191
	i32 3841636137, ; 384: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xe4fab729 => 52
	i32 3848348906, ; 385: es\Microsoft.Data.SqlClient.resources => 0xe56124ea => 1
	i32 3849253459, ; 386: System.Runtime.InteropServices.dll => 0xe56ef253 => 175
	i32 3875112723, ; 387: System.Security.Cryptography.Encoding.dll => 0xe6f98713 => 188
	i32 3876362041, ; 388: SQLite-net => 0xe70c9739 => 80
	i32 3885497537, ; 389: System.Net.WebHeaderCollection.dll => 0xe797fcc1 => 164
	i32 3889960447, ; 390: zh-Hans/Microsoft.Maui.Controls.resources.dll => 0xe7dc15ff => 42
	i32 3896106733, ; 391: System.Collections.Concurrent.dll => 0xe839deed => 127
	i32 3896760992, ; 392: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 97
	i32 3928044579, ; 393: System.Xml.ReaderWriter => 0xea213423 => 205
	i32 3931092270, ; 394: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 110
	i32 3953953790, ; 395: System.Text.Encoding.CodePages => 0xebac8bfe => 193
	i32 3955647286, ; 396: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 92
	i32 3970572422, ; 397: Google.Maps.Utils.Android.dll => 0xecaa2086 => 117
	i32 3980434154, ; 398: th/Microsoft.Maui.Controls.resources.dll => 0xed409aea => 37
	i32 3987592930, ; 399: he/Microsoft.Maui.Controls.resources.dll => 0xedadd6e2 => 19
	i32 4003436829, ; 400: System.Diagnostics.Process.dll => 0xee9f991d => 138
	i32 4025784931, ; 401: System.Memory => 0xeff49a63 => 153
	i32 4046471985, ; 402: Microsoft.Maui.Controls.Xaml.dll => 0xf1304331 => 67
	i32 4054681211, ; 403: System.Reflection.Emit.ILGeneration => 0xf1ad867b => 171
	i32 4068434129, ; 404: System.Private.Xml.Linq.dll => 0xf27f60d1 => 169
	i32 4073602200, ; 405: System.Threading.dll => 0xf2ce3c98 => 202
	i32 4094352644, ; 406: Microsoft.Maui.Essentials.dll => 0xf40add04 => 69
	i32 4099507663, ; 407: System.Drawing.dll => 0xf45985cf => 144
	i32 4100113165, ; 408: System.Private.Uri => 0xf462c30d => 168
	i32 4102112229, ; 409: pt/Microsoft.Maui.Controls.resources.dll => 0xf48143e5 => 32
	i32 4125707920, ; 410: ms/Microsoft.Maui.Controls.resources.dll => 0xf5e94e90 => 27
	i32 4126470640, ; 411: Microsoft.Extensions.DependencyInjection => 0xf5f4f1f0 => 51
	i32 4127667938, ; 412: System.IO.FileSystem.Watcher => 0xf60736e2 => 149
	i32 4147896353, ; 413: System.Reflection.Emit.ILGeneration.dll => 0xf73be021 => 171
	i32 4150914736, ; 414: uk\Microsoft.Maui.Controls.resources => 0xf769eeb0 => 39
	i32 4159265925, ; 415: System.Xml.XmlSerializer => 0xf7e95c85 => 207
	i32 4164802419, ; 416: System.IO.FileSystem.Watcher.dll => 0xf83dd773 => 149
	i32 4181436372, ; 417: System.Runtime.Serialization.Primitives => 0xf93ba7d4 => 180
	i32 4182413190, ; 418: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 105
	i32 4196529839, ; 419: System.Net.WebClient.dll => 0xfa21f6af => 163
	i32 4213026141, ; 420: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 137
	i32 4257443520, ; 421: ko/Microsoft.Data.SqlClient.resources.dll => 0xfdc36ec0 => 5
	i32 4260525087, ; 422: System.Buffers => 0xfdf2741f => 126
	i32 4263231520, ; 423: System.IdentityModel.Tokens.Jwt.dll => 0xfe1bc020 => 87
	i32 4271975918, ; 424: Microsoft.Maui.Controls.dll => 0xfea12dee => 66
	i32 4274976490, ; 425: System.Runtime.Numerics => 0xfecef6ea => 177
	i32 4278134329, ; 426: Xamarin.GooglePlayServices.Maps.dll => 0xfeff2639 => 120
	i32 4292120959 ; 427: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 105
], align 4

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [428 x i32] [
	i32 156, ; 0
	i32 155, ; 1
	i32 72, ; 2
	i32 200, ; 3
	i32 3, ; 4
	i32 4, ; 5
	i32 139, ; 6
	i32 43, ; 7
	i32 189, ; 8
	i32 70, ; 9
	i32 175, ; 10
	i32 198, ; 11
	i32 4, ; 12
	i32 164, ; 13
	i32 189, ; 14
	i32 7, ; 15
	i32 113, ; 16
	i32 40, ; 17
	i32 41, ; 18
	i32 134, ; 19
	i32 94, ; 20
	i32 124, ; 21
	i32 172, ; 22
	i32 8, ; 23
	i32 12, ; 24
	i32 157, ; 25
	i32 40, ; 26
	i32 91, ; 27
	i32 71, ; 28
	i32 25, ; 29
	i32 73, ; 30
	i32 102, ; 31
	i32 83, ; 32
	i32 24, ; 33
	i32 150, ; 34
	i32 203, ; 35
	i32 161, ; 36
	i32 200, ; 37
	i32 153, ; 38
	i32 201, ; 39
	i32 44, ; 40
	i32 36, ; 41
	i32 131, ; 42
	i32 101, ; 43
	i32 190, ; 44
	i32 180, ; 45
	i32 208, ; 46
	i32 59, ; 47
	i32 166, ; 48
	i32 23, ; 49
	i32 17, ; 50
	i32 56, ; 51
	i32 117, ; 52
	i32 75, ; 53
	i32 53, ; 54
	i32 181, ; 55
	i32 47, ; 56
	i32 186, ; 57
	i32 31, ; 58
	i32 185, ; 59
	i32 99, ; 60
	i32 29, ; 61
	i32 76, ; 62
	i32 195, ; 63
	i32 127, ; 64
	i32 160, ; 65
	i32 11, ; 66
	i32 204, ; 67
	i32 26, ; 68
	i32 14, ; 69
	i32 176, ; 70
	i32 81, ; 71
	i32 159, ; 72
	i32 150, ; 73
	i32 147, ; 74
	i32 35, ; 75
	i32 55, ; 76
	i32 136, ; 77
	i32 168, ; 78
	i32 146, ; 79
	i32 132, ; 80
	i32 38, ; 81
	i32 72, ; 82
	i32 102, ; 83
	i32 142, ; 84
	i32 206, ; 85
	i32 167, ; 86
	i32 131, ; 87
	i32 112, ; 88
	i32 144, ; 89
	i32 52, ; 90
	i32 13, ; 91
	i32 92, ; 92
	i32 143, ; 93
	i32 151, ; 94
	i32 0, ; 95
	i32 104, ; 96
	i32 58, ; 97
	i32 133, ; 98
	i32 121, ; 99
	i32 86, ; 100
	i32 208, ; 101
	i32 26, ; 102
	i32 48, ; 103
	i32 32, ; 104
	i32 109, ; 105
	i32 30, ; 106
	i32 138, ; 107
	i32 28, ; 108
	i32 12, ; 109
	i32 74, ; 110
	i32 82, ; 111
	i32 100, ; 112
	i32 167, ; 113
	i32 152, ; 114
	i32 179, ; 115
	i32 42, ; 116
	i32 112, ; 117
	i32 96, ; 118
	i32 178, ; 119
	i32 10, ; 120
	i32 145, ; 121
	i32 160, ; 122
	i32 87, ; 123
	i32 16, ; 124
	i32 128, ; 125
	i32 147, ; 126
	i32 93, ; 127
	i32 56, ; 128
	i32 128, ; 129
	i32 146, ; 130
	i32 125, ; 131
	i32 20, ; 132
	i32 60, ; 133
	i32 15, ; 134
	i32 140, ; 135
	i32 197, ; 136
	i32 35, ; 137
	i32 89, ; 138
	i32 179, ; 139
	i32 46, ; 140
	i32 9, ; 141
	i32 163, ; 142
	i32 106, ; 143
	i32 78, ; 144
	i32 115, ; 145
	i32 63, ; 146
	i32 98, ; 147
	i32 154, ; 148
	i32 197, ; 149
	i32 182, ; 150
	i32 116, ; 151
	i32 158, ; 152
	i32 191, ; 153
	i32 187, ; 154
	i32 83, ; 155
	i32 95, ; 156
	i32 33, ; 157
	i32 11, ; 158
	i32 203, ; 159
	i32 125, ; 160
	i32 141, ; 161
	i32 113, ; 162
	i32 53, ; 163
	i32 212, ; 164
	i32 27, ; 165
	i32 101, ; 166
	i32 19, ; 167
	i32 57, ; 168
	i32 47, ; 169
	i32 106, ; 170
	i32 123, ; 171
	i32 121, ; 172
	i32 116, ; 173
	i32 110, ; 174
	i32 194, ; 175
	i32 178, ; 176
	i32 54, ; 177
	i32 39, ; 178
	i32 36, ; 179
	i32 151, ; 180
	i32 173, ; 181
	i32 63, ; 182
	i32 18, ; 183
	i32 119, ; 184
	i32 129, ; 185
	i32 169, ; 186
	i32 49, ; 187
	i32 64, ; 188
	i32 15, ; 189
	i32 126, ; 190
	i32 104, ; 191
	i32 10, ; 192
	i32 58, ; 193
	i32 170, ; 194
	i32 103, ; 195
	i32 14, ; 196
	i32 141, ; 197
	i32 182, ; 198
	i32 165, ; 199
	i32 84, ; 200
	i32 135, ; 201
	i32 118, ; 202
	i32 74, ; 203
	i32 130, ; 204
	i32 207, ; 205
	i32 68, ; 206
	i32 22, ; 207
	i32 55, ; 208
	i32 54, ; 209
	i32 166, ; 210
	i32 122, ; 211
	i32 154, ; 212
	i32 24, ; 213
	i32 6, ; 214
	i32 62, ; 215
	i32 183, ; 216
	i32 50, ; 217
	i32 18, ; 218
	i32 111, ; 219
	i32 162, ; 220
	i32 28, ; 221
	i32 210, ; 222
	i32 5, ; 223
	i32 174, ; 224
	i32 158, ; 225
	i32 79, ; 226
	i32 205, ; 227
	i32 61, ; 228
	i32 49, ; 229
	i32 187, ; 230
	i32 192, ; 231
	i32 23, ; 232
	i32 20, ; 233
	i32 135, ; 234
	i32 162, ; 235
	i32 81, ; 236
	i32 209, ; 237
	i32 211, ; 238
	i32 66, ; 239
	i32 161, ; 240
	i32 86, ; 241
	i32 172, ; 242
	i32 21, ; 243
	i32 124, ; 244
	i32 195, ; 245
	i32 194, ; 246
	i32 157, ; 247
	i32 201, ; 248
	i32 30, ; 249
	i32 122, ; 250
	i32 170, ; 251
	i32 98, ; 252
	i32 79, ; 253
	i32 25, ; 254
	i32 140, ; 255
	i32 88, ; 256
	i32 61, ; 257
	i32 45, ; 258
	i32 90, ; 259
	i32 176, ; 260
	i32 206, ; 261
	i32 143, ; 262
	i32 136, ; 263
	i32 46, ; 264
	i32 181, ; 265
	i32 184, ; 266
	i32 186, ; 267
	i32 177, ; 268
	i32 91, ; 269
	i32 198, ; 270
	i32 93, ; 271
	i32 57, ; 272
	i32 31, ; 273
	i32 71, ; 274
	i32 67, ; 275
	i32 68, ; 276
	i32 193, ; 277
	i32 114, ; 278
	i32 37, ; 279
	i32 77, ; 280
	i32 70, ; 281
	i32 1, ; 282
	i32 16, ; 283
	i32 96, ; 284
	i32 29, ; 285
	i32 3, ; 286
	i32 118, ; 287
	i32 114, ; 288
	i32 76, ; 289
	i32 69, ; 290
	i32 90, ; 291
	i32 210, ; 292
	i32 77, ; 293
	i32 115, ; 294
	i32 165, ; 295
	i32 139, ; 296
	i32 85, ; 297
	i32 134, ; 298
	i32 192, ; 299
	i32 185, ; 300
	i32 100, ; 301
	i32 85, ; 302
	i32 120, ; 303
	i32 7, ; 304
	i32 88, ; 305
	i32 73, ; 306
	i32 44, ; 307
	i32 107, ; 308
	i32 212, ; 309
	i32 132, ; 310
	i32 22, ; 311
	i32 64, ; 312
	i32 190, ; 313
	i32 184, ; 314
	i32 145, ; 315
	i32 148, ; 316
	i32 199, ; 317
	i32 183, ; 318
	i32 142, ; 319
	i32 9, ; 320
	i32 173, ; 321
	i32 108, ; 322
	i32 202, ; 323
	i32 119, ; 324
	i32 95, ; 325
	i32 199, ; 326
	i32 2, ; 327
	i32 6, ; 328
	i32 80, ; 329
	i32 188, ; 330
	i32 17, ; 331
	i32 60, ; 332
	i32 159, ; 333
	i32 99, ; 334
	i32 123, ; 335
	i32 75, ; 336
	i32 2, ; 337
	i32 109, ; 338
	i32 34, ; 339
	i32 196, ; 340
	i32 82, ; 341
	i32 97, ; 342
	i32 211, ; 343
	i32 62, ; 344
	i32 111, ; 345
	i32 13, ; 346
	i32 51, ; 347
	i32 209, ; 348
	i32 65, ; 349
	i32 21, ; 350
	i32 133, ; 351
	i32 78, ; 352
	i32 213, ; 353
	i32 34, ; 354
	i32 33, ; 355
	i32 196, ; 356
	i32 204, ; 357
	i32 48, ; 358
	i32 0, ; 359
	i32 45, ; 360
	i32 148, ; 361
	i32 89, ; 362
	i32 41, ; 363
	i32 152, ; 364
	i32 174, ; 365
	i32 103, ; 366
	i32 38, ; 367
	i32 108, ; 368
	i32 50, ; 369
	i32 156, ; 370
	i32 213, ; 371
	i32 94, ; 372
	i32 43, ; 373
	i32 59, ; 374
	i32 107, ; 375
	i32 155, ; 376
	i32 137, ; 377
	i32 84, ; 378
	i32 129, ; 379
	i32 65, ; 380
	i32 130, ; 381
	i32 8, ; 382
	i32 191, ; 383
	i32 52, ; 384
	i32 1, ; 385
	i32 175, ; 386
	i32 188, ; 387
	i32 80, ; 388
	i32 164, ; 389
	i32 42, ; 390
	i32 127, ; 391
	i32 97, ; 392
	i32 205, ; 393
	i32 110, ; 394
	i32 193, ; 395
	i32 92, ; 396
	i32 117, ; 397
	i32 37, ; 398
	i32 19, ; 399
	i32 138, ; 400
	i32 153, ; 401
	i32 67, ; 402
	i32 171, ; 403
	i32 169, ; 404
	i32 202, ; 405
	i32 69, ; 406
	i32 144, ; 407
	i32 168, ; 408
	i32 32, ; 409
	i32 27, ; 410
	i32 51, ; 411
	i32 149, ; 412
	i32 171, ; 413
	i32 39, ; 414
	i32 207, ; 415
	i32 149, ; 416
	i32 180, ; 417
	i32 105, ; 418
	i32 163, ; 419
	i32 137, ; 420
	i32 5, ; 421
	i32 126, ; 422
	i32 87, ; 423
	i32 66, ; 424
	i32 177, ; 425
	i32 120, ; 426
	i32 105 ; 427
], align 4

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 4

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 4

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 4

; Functions

; Function attributes: "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 4, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 1

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-thumb-mode,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-thumb-mode,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" }

; Metadata
!llvm.module.flags = !{!0, !1, !7}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.4xx @ 68175bbe5a39140c642dab294cf184ecf72eece0"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"min_enum_size", i32 4}
