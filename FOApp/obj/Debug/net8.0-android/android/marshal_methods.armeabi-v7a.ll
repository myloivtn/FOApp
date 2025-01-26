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

@assembly_image_cache = dso_local local_unnamed_addr global [357 x ptr] zeroinitializer, align 4

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [714 x i32] [
	i32 2616222, ; 0: System.Net.NetworkInformation.dll => 0x27eb9e => 68
	i32 10166715, ; 1: System.Net.NameResolution.dll => 0x9b21bb => 67
	i32 15721112, ; 2: System.Runtime.Intrinsics.dll => 0xefe298 => 108
	i32 32687329, ; 3: Xamarin.AndroidX.Lifecycle.Runtime => 0x1f2c4e1 => 265
	i32 34715100, ; 4: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 299
	i32 34839235, ; 5: System.IO.FileSystem.DriveInfo => 0x2139ac3 => 48
	i32 39109920, ; 6: Newtonsoft.Json.dll => 0x254c520 => 204
	i32 39485524, ; 7: System.Net.WebSockets.dll => 0x25a8054 => 80
	i32 42639949, ; 8: System.Threading.Thread => 0x28aa24d => 145
	i32 57725457, ; 9: it\Microsoft.Data.SqlClient.resources => 0x370d211 => 315
	i32 57727992, ; 10: ja\Microsoft.Data.SqlClient.resources => 0x370dbf8 => 316
	i32 66541672, ; 11: System.Diagnostics.StackTrace => 0x3f75868 => 30
	i32 67008169, ; 12: zh-Hant\Microsoft.Maui.Controls.resources => 0x3fe76a9 => 355
	i32 68219467, ; 13: System.Security.Cryptography.Primitives => 0x410f24b => 124
	i32 72070932, ; 14: Microsoft.Maui.Graphics.dll => 0x44bb714 => 202
	i32 82292897, ; 15: System.Runtime.CompilerServices.VisualC.dll => 0x4e7b0a1 => 102
	i32 101534019, ; 16: Xamarin.AndroidX.SlidingPaneLayout => 0x60d4943 => 283
	i32 117431740, ; 17: System.Runtime.InteropServices => 0x6ffddbc => 107
	i32 120558881, ; 18: Xamarin.AndroidX.SlidingPaneLayout.dll => 0x72f9521 => 283
	i32 122350210, ; 19: System.Threading.Channels.dll => 0x74aea82 => 139
	i32 134690465, ; 20: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x80736a1 => 308
	i32 139659294, ; 21: ja/Microsoft.Data.SqlClient.resources.dll => 0x853081e => 316
	i32 142721839, ; 22: System.Net.WebHeaderCollection => 0x881c32f => 77
	i32 149972175, ; 23: System.Security.Cryptography.Primitives.dll => 0x8f064cf => 124
	i32 159306688, ; 24: System.ComponentModel.Annotations => 0x97ed3c0 => 13
	i32 165246403, ; 25: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 238
	i32 166535111, ; 26: ru/Microsoft.Data.SqlClient.resources.dll => 0x9ed1fc7 => 319
	i32 176265551, ; 27: System.ServiceProcess => 0xa81994f => 132
	i32 182336117, ; 28: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 285
	i32 184328833, ; 29: System.ValueTuple.dll => 0xafca281 => 151
	i32 195452805, ; 30: vi/Microsoft.Maui.Controls.resources.dll => 0xba65f85 => 352
	i32 199333315, ; 31: zh-HK/Microsoft.Maui.Controls.resources.dll => 0xbe195c3 => 353
	i32 205061960, ; 32: System.ComponentModel => 0xc38ff48 => 18
	i32 209399409, ; 33: Xamarin.AndroidX.Browser.dll => 0xc7b2e71 => 236
	i32 220171995, ; 34: System.Diagnostics.Debug => 0xd1f8edb => 26
	i32 230216969, ; 35: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0xdb8d509 => 259
	i32 230752869, ; 36: Microsoft.CSharp.dll => 0xdc10265 => 1
	i32 231409092, ; 37: System.Linq.Parallel => 0xdcb05c4 => 59
	i32 231814094, ; 38: System.Globalization => 0xdd133ce => 42
	i32 246610117, ; 39: System.Reflection.Emit.Lightweight => 0xeb2f8c5 => 91
	i32 261689757, ; 40: Xamarin.AndroidX.ConstraintLayout.dll => 0xf99119d => 242
	i32 264223668, ; 41: zh-Hans\Microsoft.Data.SqlClient.resources => 0xfbfbbb4 => 320
	i32 276479776, ; 42: System.Threading.Timer.dll => 0x107abf20 => 147
	i32 278686392, ; 43: Xamarin.AndroidX.Lifecycle.LiveData.dll => 0x109c6ab8 => 261
	i32 280482487, ; 44: Xamarin.AndroidX.Interpolator => 0x10b7d2b7 => 258
	i32 280992041, ; 45: cs/Microsoft.Maui.Controls.resources.dll => 0x10bf9929 => 324
	i32 291076382, ; 46: System.IO.Pipes.AccessControl.dll => 0x1159791e => 54
	i32 298918909, ; 47: System.Net.Ping.dll => 0x11d123fd => 69
	i32 317674968, ; 48: vi\Microsoft.Maui.Controls.resources => 0x12ef55d8 => 352
	i32 318968648, ; 49: Xamarin.AndroidX.Activity.dll => 0x13031348 => 227
	i32 321597661, ; 50: System.Numerics => 0x132b30dd => 83
	i32 330147069, ; 51: Microsoft.SqlServer.Server => 0x13ada4fd => 203
	i32 336156722, ; 52: ja/Microsoft.Maui.Controls.resources.dll => 0x14095832 => 337
	i32 338609096, ; 53: Maui.GoogleMaps => 0x142ec3c8 => 205
	i32 342366114, ; 54: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 260
	i32 347068432, ; 55: SQLitePCLRaw.lib.e_sqlite3.android.dll => 0x14afd810 => 215
	i32 356389973, ; 56: it/Microsoft.Maui.Controls.resources.dll => 0x153e1455 => 336
	i32 360082299, ; 57: System.ServiceModel.Web => 0x15766b7b => 131
	i32 367780167, ; 58: System.IO.Pipes => 0x15ebe147 => 55
	i32 374914964, ; 59: System.Transactions.Local => 0x1658bf94 => 149
	i32 375677976, ; 60: System.Net.ServicePoint.dll => 0x16646418 => 74
	i32 379916513, ; 61: System.Threading.Thread.dll => 0x16a510e1 => 145
	i32 385762202, ; 62: System.Memory.dll => 0x16fe439a => 62
	i32 392610295, ; 63: System.Threading.ThreadPool.dll => 0x1766c1f7 => 146
	i32 395744057, ; 64: _Microsoft.Android.Resource.Designer => 0x17969339 => 356
	i32 403441872, ; 65: WindowsBase => 0x180c08d0 => 165
	i32 435591531, ; 66: sv/Microsoft.Maui.Controls.resources.dll => 0x19f6996b => 348
	i32 441335492, ; 67: Xamarin.AndroidX.ConstraintLayout.Core => 0x1a4e3ec4 => 243
	i32 442565967, ; 68: System.Collections => 0x1a61054f => 12
	i32 450948140, ; 69: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 256
	i32 451504562, ; 70: System.Security.Cryptography.X509Certificates => 0x1ae969b2 => 125
	i32 456227837, ; 71: System.Web.HttpUtility.dll => 0x1b317bfd => 152
	i32 459347974, ; 72: System.Runtime.Serialization.Primitives.dll => 0x1b611806 => 113
	i32 465846621, ; 73: mscorlib => 0x1bc4415d => 166
	i32 469710990, ; 74: System.dll => 0x1bff388e => 164
	i32 476646585, ; 75: Xamarin.AndroidX.Interpolator.dll => 0x1c690cb9 => 258
	i32 485463106, ; 76: Microsoft.IdentityModel.Abstractions => 0x1cef9442 => 191
	i32 486930444, ; 77: Xamarin.AndroidX.LocalBroadcastManager.dll => 0x1d05f80c => 271
	i32 498788369, ; 78: System.ObjectModel => 0x1dbae811 => 84
	i32 500358224, ; 79: id/Microsoft.Maui.Controls.resources.dll => 0x1dd2dc50 => 335
	i32 503918385, ; 80: fi/Microsoft.Maui.Controls.resources.dll => 0x1e092f31 => 329
	i32 513247710, ; 81: Microsoft.Extensions.Primitives.dll => 0x1e9789de => 188
	i32 515182489, ; 82: Google.Maps.Utils.Android => 0x1eb50f99 => 300
	i32 525008092, ; 83: SkiaSharp.dll => 0x1f4afcdc => 207
	i32 526420162, ; 84: System.Transactions.dll => 0x1f6088c2 => 150
	i32 527452488, ; 85: Xamarin.Kotlin.StdLib.Jdk7 => 0x1f704948 => 308
	i32 530272170, ; 86: System.Linq.Queryable => 0x1f9b4faa => 60
	i32 539058512, ; 87: Microsoft.Extensions.Logging => 0x20216150 => 184
	i32 540030774, ; 88: System.IO.FileSystem.dll => 0x20303736 => 51
	i32 545304856, ; 89: System.Runtime.Extensions => 0x2080b118 => 103
	i32 546455878, ; 90: System.Runtime.Serialization.Xml => 0x20924146 => 114
	i32 548916678, ; 91: Microsoft.Bcl.AsyncInterfaces => 0x20b7cdc6 => 177
	i32 549171840, ; 92: System.Globalization.Calendars => 0x20bbb280 => 40
	i32 557405415, ; 93: Jsr305Binding => 0x213954e7 => 296
	i32 569601784, ; 94: Xamarin.AndroidX.Window.Extensions.Core.Core => 0x21f36ef8 => 294
	i32 577335427, ; 95: System.Security.Cryptography.Cng => 0x22697083 => 120
	i32 592146354, ; 96: pt-BR/Microsoft.Maui.Controls.resources.dll => 0x234b6fb2 => 343
	i32 601371474, ; 97: System.IO.IsolatedStorage.dll => 0x23d83352 => 52
	i32 605376203, ; 98: System.IO.Compression.FileSystem => 0x24154ecb => 44
	i32 613668793, ; 99: System.Security.Cryptography.Algorithms => 0x2493d7b9 => 119
	i32 627609679, ; 100: Xamarin.AndroidX.CustomView => 0x2568904f => 248
	i32 627931235, ; 101: nl\Microsoft.Maui.Controls.resources => 0x256d7863 => 341
	i32 630587553, ; 102: SkiaSharp.Extended.Svg.dll => 0x259600a1 => 208
	i32 639843206, ; 103: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 0x26233b86 => 254
	i32 643868501, ; 104: System.Net => 0x2660a755 => 81
	i32 662205335, ; 105: System.Text.Encodings.Web.dll => 0x27787397 => 136
	i32 663517072, ; 106: Xamarin.AndroidX.VersionedParcelable => 0x278c7790 => 290
	i32 666292255, ; 107: Xamarin.AndroidX.Arch.Core.Common.dll => 0x27b6d01f => 234
	i32 672442732, ; 108: System.Collections.Concurrent => 0x2814a96c => 8
	i32 683518922, ; 109: System.Net.Security => 0x28bdabca => 73
	i32 688181140, ; 110: ca/Microsoft.Maui.Controls.resources.dll => 0x2904cf94 => 323
	i32 690569205, ; 111: System.Xml.Linq.dll => 0x29293ff5 => 155
	i32 691348768, ; 112: Xamarin.KotlinX.Coroutines.Android.dll => 0x29352520 => 310
	i32 693804605, ; 113: System.Windows => 0x295a9e3d => 154
	i32 699345723, ; 114: System.Reflection.Emit => 0x29af2b3b => 92
	i32 700284507, ; 115: Xamarin.Jetbrains.Annotations => 0x29bd7e5b => 305
	i32 700358131, ; 116: System.IO.Compression.ZipFile => 0x29be9df3 => 45
	i32 706645707, ; 117: ko/Microsoft.Maui.Controls.resources.dll => 0x2a1e8ecb => 338
	i32 709557578, ; 118: de/Microsoft.Maui.Controls.resources.dll => 0x2a4afd4a => 326
	i32 720511267, ; 119: Xamarin.Kotlin.StdLib.Jdk8 => 0x2af22123 => 309
	i32 722857257, ; 120: System.Runtime.Loader.dll => 0x2b15ed29 => 109
	i32 735137430, ; 121: System.Security.SecureString.dll => 0x2bd14e96 => 129
	i32 748832960, ; 122: SQLitePCLRaw.batteries_v2 => 0x2ca248c0 => 213
	i32 752232764, ; 123: System.Diagnostics.Contracts.dll => 0x2cd6293c => 25
	i32 755313932, ; 124: Xamarin.Android.Glide.Annotations.dll => 0x2d052d0c => 224
	i32 759454413, ; 125: System.Net.Requests => 0x2d445acd => 72
	i32 762598435, ; 126: System.IO.Pipes.dll => 0x2d745423 => 55
	i32 775507847, ; 127: System.IO.Compression => 0x2e394f87 => 46
	i32 777317022, ; 128: sk\Microsoft.Maui.Controls.resources => 0x2e54ea9e => 347
	i32 789151979, ; 129: Microsoft.Extensions.Options => 0x2f0980eb => 187
	i32 790371945, ; 130: Xamarin.AndroidX.CustomView.PoolingContainer.dll => 0x2f1c1e69 => 249
	i32 804715423, ; 131: System.Data.Common => 0x2ff6fb9f => 22
	i32 807930345, ; 132: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx.dll => 0x302809e9 => 263
	i32 823281589, ; 133: System.Private.Uri.dll => 0x311247b5 => 86
	i32 830298997, ; 134: System.IO.Compression.Brotli => 0x317d5b75 => 43
	i32 832635846, ; 135: System.Xml.XPath.dll => 0x31a103c6 => 160
	i32 834051424, ; 136: System.Net.Quic => 0x31b69d60 => 71
	i32 843511501, ; 137: Xamarin.AndroidX.Print => 0x3246f6cd => 276
	i32 873119928, ; 138: Microsoft.VisualBasic => 0x340ac0b8 => 3
	i32 877678880, ; 139: System.Globalization.dll => 0x34505120 => 42
	i32 878954865, ; 140: System.Net.Http.Json => 0x3463c971 => 63
	i32 904024072, ; 141: System.ComponentModel.Primitives.dll => 0x35e25008 => 16
	i32 911108515, ; 142: System.IO.MemoryMappedFiles.dll => 0x364e69a3 => 53
	i32 926902833, ; 143: tr/Microsoft.Maui.Controls.resources.dll => 0x373f6a31 => 350
	i32 928116545, ; 144: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 299
	i32 952186615, ; 145: System.Runtime.InteropServices.JavaScript.dll => 0x38c136f7 => 105
	i32 955402788, ; 146: Newtonsoft.Json => 0x38f24a24 => 204
	i32 956575887, ; 147: Xamarin.Kotlin.StdLib.Jdk8.dll => 0x3904308f => 309
	i32 966729478, ; 148: Xamarin.Google.Crypto.Tink.Android => 0x399f1f06 => 297
	i32 967690846, ; 149: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 260
	i32 975236339, ; 150: System.Diagnostics.Tracing => 0x3a20ecf3 => 34
	i32 975874589, ; 151: System.Xml.XDocument => 0x3a2aaa1d => 158
	i32 986514023, ; 152: System.Private.DataContractSerialization.dll => 0x3acd0267 => 85
	i32 987214855, ; 153: System.Diagnostics.Tools => 0x3ad7b407 => 32
	i32 992768348, ; 154: System.Collections.dll => 0x3b2c715c => 12
	i32 994442037, ; 155: System.IO.FileSystem => 0x3b45fb35 => 51
	i32 1001831731, ; 156: System.IO.UnmanagedMemoryStream.dll => 0x3bb6bd33 => 56
	i32 1012816738, ; 157: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 280
	i32 1019214401, ; 158: System.Drawing => 0x3cbffa41 => 36
	i32 1028951442, ; 159: Microsoft.Extensions.DependencyInjection.Abstractions => 0x3d548d92 => 183
	i32 1029334545, ; 160: da/Microsoft.Maui.Controls.resources.dll => 0x3d5a6611 => 325
	i32 1031528504, ; 161: Xamarin.Google.ErrorProne.Annotations.dll => 0x3d7be038 => 298
	i32 1035644815, ; 162: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 232
	i32 1036536393, ; 163: System.Drawing.Primitives.dll => 0x3dc84a49 => 35
	i32 1044663988, ; 164: System.Linq.Expressions.dll => 0x3e444eb4 => 58
	i32 1048439329, ; 165: de/Microsoft.Data.SqlClient.resources.dll => 0x3e7dea21 => 312
	i32 1052210849, ; 166: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 267
	i32 1062017875, ; 167: Microsoft.Identity.Client.Extensions.Msal => 0x3f4d1b53 => 190
	i32 1067306892, ; 168: GoogleGson => 0x3f9dcf8c => 176
	i32 1082857460, ; 169: System.ComponentModel.TypeConverter => 0x408b17f4 => 17
	i32 1084122840, ; 170: Xamarin.Kotlin.StdLib => 0x409e66d8 => 306
	i32 1089913930, ; 171: System.Diagnostics.EventLog.dll => 0x40f6c44a => 218
	i32 1098259244, ; 172: System => 0x41761b2c => 164
	i32 1118262833, ; 173: ko\Microsoft.Maui.Controls.resources => 0x42a75631 => 338
	i32 1121599056, ; 174: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.dll => 0x42da3e50 => 266
	i32 1127624469, ; 175: Microsoft.Extensions.Logging.Debug => 0x43362f15 => 186
	i32 1138436374, ; 176: Microsoft.Data.SqlClient.dll => 0x43db2916 => 178
	i32 1149092582, ; 177: Xamarin.AndroidX.Window => 0x447dc2e6 => 293
	i32 1168523401, ; 178: pt\Microsoft.Maui.Controls.resources => 0x45a64089 => 344
	i32 1170634674, ; 179: System.Web.dll => 0x45c677b2 => 153
	i32 1175144683, ; 180: Xamarin.AndroidX.VectorDrawable.Animated => 0x460b48eb => 289
	i32 1178241025, ; 181: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 274
	i32 1203215381, ; 182: pl/Microsoft.Maui.Controls.resources.dll => 0x47b79c15 => 342
	i32 1204270330, ; 183: Xamarin.AndroidX.Arch.Core.Common => 0x47c7b4fa => 234
	i32 1208641965, ; 184: System.Diagnostics.Process => 0x480a69ad => 29
	i32 1214827643, ; 185: CommunityToolkit.Mvvm => 0x4868cc7b => 175
	i32 1219128291, ; 186: System.IO.IsolatedStorage => 0x48aa6be3 => 52
	i32 1234928153, ; 187: nb/Microsoft.Maui.Controls.resources.dll => 0x499b8219 => 340
	i32 1243150071, ; 188: Xamarin.AndroidX.Window.Extensions.Core.Core.dll => 0x4a18f6f7 => 294
	i32 1246548578, ; 189: Xamarin.AndroidX.Collection.Jvm.dll => 0x4a4cd262 => 239
	i32 1253011324, ; 190: Microsoft.Win32.Registry => 0x4aaf6f7c => 5
	i32 1260983243, ; 191: cs\Microsoft.Maui.Controls.resources => 0x4b2913cb => 324
	i32 1264511973, ; 192: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0x4b5eebe5 => 284
	i32 1267360935, ; 193: Xamarin.AndroidX.VectorDrawable => 0x4b8a64a7 => 288
	i32 1273260888, ; 194: Xamarin.AndroidX.Collection.Ktx => 0x4be46b58 => 240
	i32 1275534314, ; 195: Xamarin.KotlinX.Coroutines.Android => 0x4c071bea => 310
	i32 1276684945, ; 196: Maui.GoogleMaps.Clustering => 0x4c18aa91 => 206
	i32 1278448581, ; 197: Xamarin.AndroidX.Annotation.Jvm => 0x4c3393c5 => 231
	i32 1292207520, ; 198: SQLitePCLRaw.core.dll => 0x4d0585a0 => 214
	i32 1293217323, ; 199: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 251
	i32 1309188875, ; 200: System.Private.DataContractSerialization => 0x4e08a30b => 85
	i32 1322716291, ; 201: Xamarin.AndroidX.Window.dll => 0x4ed70c83 => 293
	i32 1324164729, ; 202: System.Linq => 0x4eed2679 => 61
	i32 1335329327, ; 203: System.Runtime.Serialization.Json.dll => 0x4f97822f => 112
	i32 1364015309, ; 204: System.IO => 0x514d38cd => 57
	i32 1373134921, ; 205: zh-Hans\Microsoft.Maui.Controls.resources => 0x51d86049 => 354
	i32 1376866003, ; 206: Xamarin.AndroidX.SavedState => 0x52114ed3 => 280
	i32 1379779777, ; 207: System.Resources.ResourceManager => 0x523dc4c1 => 99
	i32 1402170036, ; 208: System.Configuration.dll => 0x53936ab4 => 19
	i32 1406073936, ; 209: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 244
	i32 1408764838, ; 210: System.Runtime.Serialization.Formatters.dll => 0x53f80ba6 => 111
	i32 1411638395, ; 211: System.Runtime.CompilerServices.Unsafe => 0x5423e47b => 101
	i32 1422545099, ; 212: System.Runtime.CompilerServices.VisualC => 0x54ca50cb => 102
	i32 1430672901, ; 213: ar\Microsoft.Maui.Controls.resources => 0x55465605 => 322
	i32 1434145427, ; 214: System.Runtime.Handles => 0x557b5293 => 104
	i32 1435222561, ; 215: Xamarin.Google.Crypto.Tink.Android.dll => 0x558bc221 => 297
	i32 1439761251, ; 216: System.Net.Quic.dll => 0x55d10363 => 71
	i32 1452070440, ; 217: System.Formats.Asn1.dll => 0x568cd628 => 38
	i32 1453312822, ; 218: System.Diagnostics.Tools.dll => 0x569fcb36 => 32
	i32 1457743152, ; 219: System.Runtime.Extensions.dll => 0x56e36530 => 103
	i32 1458022317, ; 220: System.Net.Security.dll => 0x56e7a7ad => 73
	i32 1460893475, ; 221: System.IdentityModel.Tokens.Jwt => 0x57137723 => 219
	i32 1461004990, ; 222: es\Microsoft.Maui.Controls.resources => 0x57152abe => 328
	i32 1461234159, ; 223: System.Collections.Immutable.dll => 0x5718a9ef => 9
	i32 1461719063, ; 224: System.Security.Cryptography.OpenSsl => 0x57201017 => 123
	i32 1462112819, ; 225: System.IO.Compression.dll => 0x57261233 => 46
	i32 1469204771, ; 226: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 233
	i32 1470490898, ; 227: Microsoft.Extensions.Primitives => 0x57a5e912 => 188
	i32 1479771757, ; 228: System.Collections.Immutable => 0x5833866d => 9
	i32 1480492111, ; 229: System.IO.Compression.Brotli.dll => 0x583e844f => 43
	i32 1487239319, ; 230: Microsoft.Win32.Primitives => 0x58a57897 => 4
	i32 1490025113, ; 231: Xamarin.AndroidX.SavedState.SavedState.Ktx.dll => 0x58cffa99 => 281
	i32 1490351284, ; 232: Microsoft.Data.Sqlite.dll => 0x58d4f4b4 => 179
	i32 1493001747, ; 233: hi/Microsoft.Maui.Controls.resources.dll => 0x58fd6613 => 332
	i32 1498168481, ; 234: Microsoft.IdentityModel.JsonWebTokens.dll => 0x594c3ca1 => 192
	i32 1514721132, ; 235: el/Microsoft.Maui.Controls.resources.dll => 0x5a48cf6c => 327
	i32 1536373174, ; 236: System.Diagnostics.TextWriterTraceListener => 0x5b9331b6 => 31
	i32 1543031311, ; 237: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 138
	i32 1543355203, ; 238: System.Reflection.Emit.dll => 0x5bfdbb43 => 92
	i32 1550322496, ; 239: System.Reflection.Extensions.dll => 0x5c680b40 => 93
	i32 1551623176, ; 240: sk/Microsoft.Maui.Controls.resources.dll => 0x5c7be408 => 347
	i32 1565310744, ; 241: System.Runtime.Caching => 0x5d4cbf18 => 221
	i32 1565862583, ; 242: System.IO.FileSystem.Primitives => 0x5d552ab7 => 49
	i32 1566207040, ; 243: System.Threading.Tasks.Dataflow.dll => 0x5d5a6c40 => 141
	i32 1573704789, ; 244: System.Runtime.Serialization.Json => 0x5dccd455 => 112
	i32 1580037396, ; 245: System.Threading.Overlapped => 0x5e2d7514 => 140
	i32 1582305585, ; 246: Azure.Identity => 0x5e501131 => 174
	i32 1582372066, ; 247: Xamarin.AndroidX.DocumentFile.dll => 0x5e5114e2 => 250
	i32 1592978981, ; 248: System.Runtime.Serialization.dll => 0x5ef2ee25 => 115
	i32 1596263029, ; 249: zh-Hant\Microsoft.Data.SqlClient.resources => 0x5f250a75 => 321
	i32 1597949149, ; 250: Xamarin.Google.ErrorProne.Annotations => 0x5f3ec4dd => 298
	i32 1601112923, ; 251: System.Xml.Serialization => 0x5f6f0b5b => 157
	i32 1604827217, ; 252: System.Net.WebClient => 0x5fa7b851 => 76
	i32 1618516317, ; 253: System.Net.WebSockets.Client.dll => 0x6078995d => 79
	i32 1622152042, ; 254: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 270
	i32 1622358360, ; 255: System.Dynamic.Runtime => 0x60b33958 => 37
	i32 1623212457, ; 256: SkiaSharp.Views.Maui.Controls => 0x60c041a9 => 210
	i32 1624863272, ; 257: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 292
	i32 1628113371, ; 258: Microsoft.IdentityModel.Protocols.OpenIdConnect => 0x610b09db => 195
	i32 1635184631, ; 259: Xamarin.AndroidX.Emoji2.ViewsHelper => 0x6176eff7 => 254
	i32 1636350590, ; 260: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 247
	i32 1639515021, ; 261: System.Net.Http.dll => 0x61b9038d => 64
	i32 1639986890, ; 262: System.Text.RegularExpressions => 0x61c036ca => 138
	i32 1641389582, ; 263: System.ComponentModel.EventBasedAsync.dll => 0x61d59e0e => 15
	i32 1657153582, ; 264: System.Runtime => 0x62c6282e => 116
	i32 1658241508, ; 265: Xamarin.AndroidX.Tracing.Tracing.dll => 0x62d6c1e4 => 286
	i32 1658251792, ; 266: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 295
	i32 1670060433, ; 267: Xamarin.AndroidX.ConstraintLayout => 0x638b1991 => 242
	i32 1675553242, ; 268: System.IO.FileSystem.DriveInfo.dll => 0x63dee9da => 48
	i32 1677501392, ; 269: System.Net.Primitives.dll => 0x63fca3d0 => 70
	i32 1678508291, ; 270: System.Net.WebSockets => 0x640c0103 => 80
	i32 1679769178, ; 271: System.Security.Cryptography => 0x641f3e5a => 126
	i32 1688112883, ; 272: Microsoft.Data.Sqlite => 0x649e8ef3 => 179
	i32 1691477237, ; 273: System.Reflection.Metadata => 0x64d1e4f5 => 94
	i32 1696967625, ; 274: System.Security.Cryptography.Csp => 0x6525abc9 => 121
	i32 1698840827, ; 275: Xamarin.Kotlin.StdLib.Common => 0x654240fb => 307
	i32 1701541528, ; 276: System.Diagnostics.Debug.dll => 0x656b7698 => 26
	i32 1711441057, ; 277: SQLitePCLRaw.lib.e_sqlite3.android => 0x660284a1 => 215
	i32 1720223769, ; 278: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx => 0x66888819 => 263
	i32 1726116996, ; 279: System.Reflection.dll => 0x66e27484 => 97
	i32 1728033016, ; 280: System.Diagnostics.FileVersionInfo.dll => 0x66ffb0f8 => 28
	i32 1729485958, ; 281: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 237
	i32 1736233607, ; 282: ro/Microsoft.Maui.Controls.resources.dll => 0x677cd287 => 345
	i32 1743415430, ; 283: ca\Microsoft.Maui.Controls.resources => 0x67ea6886 => 323
	i32 1744735666, ; 284: System.Transactions.Local.dll => 0x67fe8db2 => 149
	i32 1746316138, ; 285: Mono.Android.Export => 0x6816ab6a => 169
	i32 1750313021, ; 286: Microsoft.Win32.Primitives.dll => 0x6853a83d => 4
	i32 1758240030, ; 287: System.Resources.Reader.dll => 0x68cc9d1e => 98
	i32 1763938596, ; 288: System.Diagnostics.TraceSource.dll => 0x69239124 => 33
	i32 1765942094, ; 289: System.Reflection.Extensions => 0x6942234e => 93
	i32 1766324549, ; 290: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 285
	i32 1770582343, ; 291: Microsoft.Extensions.Logging.dll => 0x6988f147 => 184
	i32 1776026572, ; 292: System.Core.dll => 0x69dc03cc => 21
	i32 1777075843, ; 293: System.Globalization.Extensions.dll => 0x69ec0683 => 41
	i32 1780572499, ; 294: Mono.Android.Runtime.dll => 0x6a216153 => 170
	i32 1782862114, ; 295: ms\Microsoft.Maui.Controls.resources => 0x6a445122 => 339
	i32 1788241197, ; 296: Xamarin.AndroidX.Fragment => 0x6a96652d => 256
	i32 1793755602, ; 297: he\Microsoft.Maui.Controls.resources => 0x6aea89d2 => 331
	i32 1794500907, ; 298: Microsoft.Identity.Client.dll => 0x6af5e92b => 189
	i32 1796167890, ; 299: Microsoft.Bcl.AsyncInterfaces.dll => 0x6b0f58d2 => 177
	i32 1808609942, ; 300: Xamarin.AndroidX.Loader => 0x6bcd3296 => 270
	i32 1809552118, ; 301: FOApp.dll => 0x6bdb92f6 => 0
	i32 1813058853, ; 302: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 306
	i32 1813201214, ; 303: Xamarin.Google.Android.Material => 0x6c13413e => 295
	i32 1818569960, ; 304: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 275
	i32 1818787751, ; 305: Microsoft.VisualBasic.Core => 0x6c687fa7 => 2
	i32 1824175904, ; 306: System.Text.Encoding.Extensions => 0x6cbab720 => 134
	i32 1824722060, ; 307: System.Runtime.Serialization.Formatters => 0x6cc30c8c => 111
	i32 1828688058, ; 308: Microsoft.Extensions.Logging.Abstractions.dll => 0x6cff90ba => 185
	i32 1842015223, ; 309: uk/Microsoft.Maui.Controls.resources.dll => 0x6dcaebf7 => 351
	i32 1847515442, ; 310: Xamarin.Android.Glide.Annotations => 0x6e1ed932 => 224
	i32 1853025655, ; 311: sv\Microsoft.Maui.Controls.resources => 0x6e72ed77 => 348
	i32 1858542181, ; 312: System.Linq.Expressions => 0x6ec71a65 => 58
	i32 1870277092, ; 313: System.Reflection.Primitives => 0x6f7a29e4 => 95
	i32 1871986876, ; 314: Microsoft.IdentityModel.Protocols.OpenIdConnect.dll => 0x6f9440bc => 195
	i32 1875935024, ; 315: fr\Microsoft.Maui.Controls.resources => 0x6fd07f30 => 330
	i32 1879696579, ; 316: System.Formats.Tar.dll => 0x7009e4c3 => 39
	i32 1885316902, ; 317: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0x705fa726 => 235
	i32 1888955245, ; 318: System.Diagnostics.Contracts => 0x70972b6d => 25
	i32 1889954781, ; 319: System.Reflection.Metadata.dll => 0x70a66bdd => 94
	i32 1898237753, ; 320: System.Reflection.DispatchProxy => 0x7124cf39 => 89
	i32 1900610850, ; 321: System.Resources.ResourceManager.dll => 0x71490522 => 99
	i32 1908813208, ; 322: Xamarin.GooglePlayServices.Basement => 0x71c62d98 => 302
	i32 1910275211, ; 323: System.Collections.NonGeneric.dll => 0x71dc7c8b => 10
	i32 1939592360, ; 324: System.Private.Xml.Linq => 0x739bd4a8 => 87
	i32 1956758971, ; 325: System.Resources.Writer => 0x74a1c5bb => 100
	i32 1961813231, ; 326: Xamarin.AndroidX.Security.SecurityCrypto.dll => 0x74eee4ef => 282
	i32 1968388702, ; 327: Microsoft.Extensions.Configuration.dll => 0x75533a5e => 180
	i32 1983156543, ; 328: Xamarin.Kotlin.StdLib.Common.dll => 0x7634913f => 307
	i32 1985761444, ; 329: Xamarin.Android.Glide.GifDecoder => 0x765c50a4 => 226
	i32 1986222447, ; 330: Microsoft.IdentityModel.Tokens.dll => 0x7663596f => 196
	i32 2003115576, ; 331: el\Microsoft.Maui.Controls.resources => 0x77651e38 => 327
	i32 2011961780, ; 332: System.Buffers.dll => 0x77ec19b4 => 7
	i32 2019465201, ; 333: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 267
	i32 2025202353, ; 334: ar/Microsoft.Maui.Controls.resources.dll => 0x78b622b1 => 322
	i32 2031763787, ; 335: Xamarin.Android.Glide => 0x791a414b => 223
	i32 2040764568, ; 336: Microsoft.Identity.Client.Extensions.Msal.dll => 0x79a39898 => 190
	i32 2045470958, ; 337: System.Private.Xml => 0x79eb68ee => 88
	i32 2055257422, ; 338: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 262
	i32 2060060697, ; 339: System.Windows.dll => 0x7aca0819 => 154
	i32 2066184531, ; 340: de\Microsoft.Maui.Controls.resources => 0x7b277953 => 326
	i32 2070888862, ; 341: System.Diagnostics.TraceSource => 0x7b6f419e => 33
	i32 2079903147, ; 342: System.Runtime.dll => 0x7bf8cdab => 116
	i32 2090596640, ; 343: System.Numerics.Vectors => 0x7c9bf920 => 82
	i32 2103459038, ; 344: SQLitePCLRaw.provider.e_sqlite3.dll => 0x7d603cde => 216
	i32 2127167465, ; 345: System.Console => 0x7ec9ffe9 => 20
	i32 2129483829, ; 346: Xamarin.GooglePlayServices.Base.dll => 0x7eed5835 => 301
	i32 2134309359, ; 347: Maui.GoogleMaps.Clustering.dll => 0x7f36f9ef => 206
	i32 2142473426, ; 348: System.Collections.Specialized => 0x7fb38cd2 => 11
	i32 2143790110, ; 349: System.Xml.XmlSerializer.dll => 0x7fc7a41e => 162
	i32 2146852085, ; 350: Microsoft.VisualBasic.dll => 0x7ff65cf5 => 3
	i32 2159891885, ; 351: Microsoft.Maui => 0x80bd55ad => 200
	i32 2169148018, ; 352: hu\Microsoft.Maui.Controls.resources => 0x814a9272 => 334
	i32 2181898931, ; 353: Microsoft.Extensions.Options.dll => 0x820d22b3 => 187
	i32 2192057212, ; 354: Microsoft.Extensions.Logging.Abstractions => 0x82a8237c => 185
	i32 2193016926, ; 355: System.ObjectModel.dll => 0x82b6c85e => 84
	i32 2201107256, ; 356: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 311
	i32 2201231467, ; 357: System.Net.Http => 0x8334206b => 64
	i32 2207618523, ; 358: it\Microsoft.Maui.Controls.resources => 0x839595db => 336
	i32 2217644978, ; 359: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x842e93b2 => 289
	i32 2222056684, ; 360: System.Threading.Tasks.Parallel => 0x8471e4ec => 143
	i32 2228745826, ; 361: pt-BR\Microsoft.Data.SqlClient.resources => 0x84d7f662 => 318
	i32 2244775296, ; 362: Xamarin.AndroidX.LocalBroadcastManager => 0x85cc8d80 => 271
	i32 2252106437, ; 363: System.Xml.Serialization.dll => 0x863c6ac5 => 157
	i32 2253551641, ; 364: Microsoft.IdentityModel.Protocols => 0x86527819 => 194
	i32 2256313426, ; 365: System.Globalization.Extensions => 0x867c9c52 => 41
	i32 2265110946, ; 366: System.Security.AccessControl.dll => 0x8702d9a2 => 117
	i32 2266799131, ; 367: Microsoft.Extensions.Configuration.Abstractions => 0x871c9c1b => 181
	i32 2267999099, ; 368: Xamarin.Android.Glide.DiskLruCache.dll => 0x872eeb7b => 225
	i32 2270573516, ; 369: fr/Microsoft.Maui.Controls.resources.dll => 0x875633cc => 330
	i32 2279755925, ; 370: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 278
	i32 2293034957, ; 371: System.ServiceModel.Web.dll => 0x88acefcd => 131
	i32 2295906218, ; 372: System.Net.Sockets => 0x88d8bfaa => 75
	i32 2298471582, ; 373: System.Net.Mail => 0x88ffe49e => 66
	i32 2303942373, ; 374: nb\Microsoft.Maui.Controls.resources => 0x89535ee5 => 340
	i32 2305521784, ; 375: System.Private.CoreLib.dll => 0x896b7878 => 172
	i32 2309278602, ; 376: ko\Microsoft.Data.SqlClient.resources => 0x89a4cb8a => 317
	i32 2315684594, ; 377: Xamarin.AndroidX.Annotation.dll => 0x8a068af2 => 229
	i32 2320631194, ; 378: System.Threading.Tasks.Parallel.dll => 0x8a52059a => 143
	i32 2340441535, ; 379: System.Runtime.InteropServices.RuntimeInformation.dll => 0x8b804dbf => 106
	i32 2344264397, ; 380: System.ValueTuple => 0x8bbaa2cd => 151
	i32 2353062107, ; 381: System.Net.Primitives => 0x8c40e0db => 70
	i32 2364201794, ; 382: SkiaSharp.Views.Maui.Core => 0x8ceadb42 => 211
	i32 2368005991, ; 383: System.Xml.ReaderWriter.dll => 0x8d24e767 => 156
	i32 2369706906, ; 384: Microsoft.IdentityModel.Logging => 0x8d3edb9a => 193
	i32 2371007202, ; 385: Microsoft.Extensions.Configuration => 0x8d52b2e2 => 180
	i32 2378619854, ; 386: System.Security.Cryptography.Csp.dll => 0x8dc6dbce => 121
	i32 2383496789, ; 387: System.Security.Principal.Windows.dll => 0x8e114655 => 127
	i32 2395872292, ; 388: id\Microsoft.Maui.Controls.resources => 0x8ece1c24 => 335
	i32 2401565422, ; 389: System.Web.HttpUtility => 0x8f24faee => 152
	i32 2403452196, ; 390: Xamarin.AndroidX.Emoji2.dll => 0x8f41c524 => 253
	i32 2421380589, ; 391: System.Threading.Tasks.Dataflow => 0x905355ed => 141
	i32 2423080555, ; 392: Xamarin.AndroidX.Collection.Ktx.dll => 0x906d466b => 240
	i32 2427813419, ; 393: hi\Microsoft.Maui.Controls.resources => 0x90b57e2b => 332
	i32 2435356389, ; 394: System.Console.dll => 0x912896e5 => 20
	i32 2435904999, ; 395: System.ComponentModel.DataAnnotations.dll => 0x9130f5e7 => 14
	i32 2454642406, ; 396: System.Text.Encoding.dll => 0x924edee6 => 135
	i32 2458678730, ; 397: System.Net.Sockets.dll => 0x928c75ca => 75
	i32 2459001652, ; 398: System.Linq.Parallel.dll => 0x92916334 => 59
	i32 2465273461, ; 399: SQLitePCLRaw.batteries_v2.dll => 0x92f11675 => 213
	i32 2465532216, ; 400: Xamarin.AndroidX.ConstraintLayout.Core.dll => 0x92f50938 => 243
	i32 2471841756, ; 401: netstandard.dll => 0x93554fdc => 167
	i32 2475788418, ; 402: Java.Interop.dll => 0x93918882 => 168
	i32 2480646305, ; 403: Microsoft.Maui.Controls => 0x93dba8a1 => 198
	i32 2483903535, ; 404: System.ComponentModel.EventBasedAsync => 0x940d5c2f => 15
	i32 2484371297, ; 405: System.Net.ServicePoint => 0x94147f61 => 74
	i32 2490993605, ; 406: System.AppContext.dll => 0x94798bc5 => 6
	i32 2501346920, ; 407: System.Data.DataSetExtensions => 0x95178668 => 23
	i32 2505896520, ; 408: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x955cf248 => 265
	i32 2509217888, ; 409: System.Diagnostics.EventLog => 0x958fa060 => 218
	i32 2522472828, ; 410: Xamarin.Android.Glide.dll => 0x9659e17c => 223
	i32 2538310050, ; 411: System.Reflection.Emit.Lightweight.dll => 0x974b89a2 => 91
	i32 2550873716, ; 412: hr\Microsoft.Maui.Controls.resources => 0x980b3e74 => 333
	i32 2562349572, ; 413: Microsoft.CSharp => 0x98ba5a04 => 1
	i32 2570120770, ; 414: System.Text.Encodings.Web => 0x9930ee42 => 136
	i32 2581783588, ; 415: Xamarin.AndroidX.Lifecycle.Runtime.Ktx => 0x99e2e424 => 266
	i32 2581819634, ; 416: Xamarin.AndroidX.VectorDrawable.dll => 0x99e370f2 => 288
	i32 2585220780, ; 417: System.Text.Encoding.Extensions.dll => 0x9a1756ac => 134
	i32 2585805581, ; 418: System.Net.Ping => 0x9a20430d => 69
	i32 2589602615, ; 419: System.Threading.ThreadPool => 0x9a5a3337 => 146
	i32 2593496499, ; 420: pl\Microsoft.Maui.Controls.resources => 0x9a959db3 => 342
	i32 2605712449, ; 421: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 311
	i32 2615233544, ; 422: Xamarin.AndroidX.Fragment.Ktx => 0x9be14c08 => 257
	i32 2616218305, ; 423: Microsoft.Extensions.Logging.Debug.dll => 0x9bf052c1 => 186
	i32 2617129537, ; 424: System.Private.Xml.dll => 0x9bfe3a41 => 88
	i32 2618712057, ; 425: System.Reflection.TypeExtensions.dll => 0x9c165ff9 => 96
	i32 2620871830, ; 426: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 247
	i32 2624644809, ; 427: Xamarin.AndroidX.DynamicAnimation => 0x9c70e6c9 => 252
	i32 2625339995, ; 428: SkiaSharp.Views.Maui.Core.dll => 0x9c7b825b => 211
	i32 2626831493, ; 429: ja\Microsoft.Maui.Controls.resources => 0x9c924485 => 337
	i32 2627185994, ; 430: System.Diagnostics.TextWriterTraceListener.dll => 0x9c97ad4a => 31
	i32 2628210652, ; 431: System.Memory.Data => 0x9ca74fdc => 220
	i32 2629843544, ; 432: System.IO.Compression.ZipFile.dll => 0x9cc03a58 => 45
	i32 2633051222, ; 433: Xamarin.AndroidX.Lifecycle.LiveData => 0x9cf12c56 => 261
	i32 2640290731, ; 434: Microsoft.IdentityModel.Logging.dll => 0x9d5fa3ab => 193
	i32 2640706905, ; 435: Azure.Core => 0x9d65fd59 => 173
	i32 2660759594, ; 436: System.Security.Cryptography.ProtectedData.dll => 0x9e97f82a => 222
	i32 2663391936, ; 437: Xamarin.Android.Glide.DiskLruCache => 0x9ec022c0 => 225
	i32 2663698177, ; 438: System.Runtime.Loader => 0x9ec4cf01 => 109
	i32 2664396074, ; 439: System.Xml.XDocument.dll => 0x9ecf752a => 158
	i32 2665622720, ; 440: System.Drawing.Primitives => 0x9ee22cc0 => 35
	i32 2676780864, ; 441: System.Data.Common.dll => 0x9f8c6f40 => 22
	i32 2677098746, ; 442: Azure.Identity.dll => 0x9f9148fa => 174
	i32 2686887180, ; 443: System.Runtime.Serialization.Xml.dll => 0xa026a50c => 114
	i32 2693849962, ; 444: System.IO.dll => 0xa090e36a => 57
	i32 2701096212, ; 445: Xamarin.AndroidX.Tracing.Tracing => 0xa0ff7514 => 286
	i32 2715334215, ; 446: System.Threading.Tasks.dll => 0xa1d8b647 => 144
	i32 2717744543, ; 447: System.Security.Claims => 0xa1fd7d9f => 118
	i32 2719963679, ; 448: System.Security.Cryptography.Cng.dll => 0xa21f5a1f => 120
	i32 2724373263, ; 449: System.Runtime.Numerics.dll => 0xa262a30f => 110
	i32 2732626843, ; 450: Xamarin.AndroidX.Activity => 0xa2e0939b => 227
	i32 2735172069, ; 451: System.Threading.Channels => 0xa30769e5 => 139
	i32 2737747696, ; 452: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 233
	i32 2740051746, ; 453: Microsoft.Identity.Client => 0xa351df22 => 189
	i32 2740948882, ; 454: System.IO.Pipes.AccessControl => 0xa35f8f92 => 54
	i32 2748088231, ; 455: System.Runtime.InteropServices.JavaScript => 0xa3cc7fa7 => 105
	i32 2752995522, ; 456: pt-BR\Microsoft.Maui.Controls.resources => 0xa41760c2 => 343
	i32 2755098380, ; 457: Microsoft.SqlServer.Server.dll => 0xa437770c => 203
	i32 2758225723, ; 458: Microsoft.Maui.Controls.Xaml => 0xa4672f3b => 199
	i32 2764765095, ; 459: Microsoft.Maui.dll => 0xa4caf7a7 => 200
	i32 2765824710, ; 460: System.Text.Encoding.CodePages.dll => 0xa4db22c6 => 133
	i32 2770495804, ; 461: Xamarin.Jetbrains.Annotations.dll => 0xa522693c => 305
	i32 2778768386, ; 462: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 291
	i32 2779977773, ; 463: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 0xa5b3182d => 279
	i32 2785988530, ; 464: th\Microsoft.Maui.Controls.resources => 0xa60ecfb2 => 349
	i32 2788224221, ; 465: Xamarin.AndroidX.Fragment.Ktx.dll => 0xa630ecdd => 257
	i32 2795602088, ; 466: SkiaSharp.Views.Android.dll => 0xa6a180a8 => 209
	i32 2801831435, ; 467: Microsoft.Maui.Graphics => 0xa7008e0b => 202
	i32 2803228030, ; 468: System.Xml.XPath.XDocument.dll => 0xa715dd7e => 159
	i32 2804509662, ; 469: es/Microsoft.Data.SqlClient.resources.dll => 0xa7296bde => 313
	i32 2806116107, ; 470: es/Microsoft.Maui.Controls.resources.dll => 0xa741ef0b => 328
	i32 2810250172, ; 471: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 244
	i32 2819470561, ; 472: System.Xml.dll => 0xa80db4e1 => 163
	i32 2821205001, ; 473: System.ServiceProcess.dll => 0xa8282c09 => 132
	i32 2821294376, ; 474: Xamarin.AndroidX.ResourceInspection.Annotation => 0xa8298928 => 279
	i32 2824502124, ; 475: System.Xml.XmlDocument => 0xa85a7b6c => 161
	i32 2831556043, ; 476: nl/Microsoft.Maui.Controls.resources.dll => 0xa8c61dcb => 341
	i32 2838993487, ; 477: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx.dll => 0xa9379a4f => 268
	i32 2841937114, ; 478: it/Microsoft.Data.SqlClient.resources.dll => 0xa96484da => 315
	i32 2847418871, ; 479: Xamarin.GooglePlayServices.Base => 0xa9b829f7 => 301
	i32 2849599387, ; 480: System.Threading.Overlapped.dll => 0xa9d96f9b => 140
	i32 2853208004, ; 481: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 291
	i32 2854551965, ; 482: SkiaSharp.Extended.Svg => 0xaa25019d => 208
	i32 2855708567, ; 483: Xamarin.AndroidX.Transition => 0xaa36a797 => 287
	i32 2861098320, ; 484: Mono.Android.Export.dll => 0xaa88e550 => 169
	i32 2861189240, ; 485: Microsoft.Maui.Essentials => 0xaa8a4878 => 201
	i32 2867946736, ; 486: System.Security.Cryptography.ProtectedData => 0xaaf164f0 => 222
	i32 2870099610, ; 487: Xamarin.AndroidX.Activity.Ktx.dll => 0xab123e9a => 228
	i32 2875164099, ; 488: Jsr305Binding.dll => 0xab5f85c3 => 296
	i32 2875220617, ; 489: System.Globalization.Calendars.dll => 0xab606289 => 40
	i32 2884993177, ; 490: Xamarin.AndroidX.ExifInterface => 0xabf58099 => 255
	i32 2887636118, ; 491: System.Net.dll => 0xac1dd496 => 81
	i32 2899753641, ; 492: System.IO.UnmanagedMemoryStream => 0xacd6baa9 => 56
	i32 2900621748, ; 493: System.Dynamic.Runtime.dll => 0xace3f9b4 => 37
	i32 2901442782, ; 494: System.Reflection => 0xacf080de => 97
	i32 2905242038, ; 495: mscorlib.dll => 0xad2a79b6 => 166
	i32 2909740682, ; 496: System.Private.CoreLib => 0xad6f1e8a => 172
	i32 2912489636, ; 497: SkiaSharp.Views.Android => 0xad9910a4 => 209
	i32 2916838712, ; 498: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 292
	i32 2919462931, ; 499: System.Numerics.Vectors.dll => 0xae037813 => 82
	i32 2921128767, ; 500: Xamarin.AndroidX.Annotation.Experimental.dll => 0xae1ce33f => 230
	i32 2936416060, ; 501: System.Resources.Reader => 0xaf06273c => 98
	i32 2940926066, ; 502: System.Diagnostics.StackTrace.dll => 0xaf4af872 => 30
	i32 2942453041, ; 503: System.Xml.XPath.XDocument => 0xaf624531 => 159
	i32 2944313911, ; 504: System.Configuration.ConfigurationManager.dll => 0xaf7eaa37 => 217
	i32 2959614098, ; 505: System.ComponentModel.dll => 0xb0682092 => 18
	i32 2968338931, ; 506: System.Security.Principal.Windows => 0xb0ed41f3 => 127
	i32 2972252294, ; 507: System.Security.Cryptography.Algorithms.dll => 0xb128f886 => 119
	i32 2978675010, ; 508: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 251
	i32 2987532451, ; 509: Xamarin.AndroidX.Security.SecurityCrypto => 0xb21220a3 => 282
	i32 2996846495, ; 510: Xamarin.AndroidX.Lifecycle.Process.dll => 0xb2a03f9f => 264
	i32 3012788804, ; 511: System.Configuration.ConfigurationManager => 0xb3938244 => 217
	i32 3016983068, ; 512: Xamarin.AndroidX.Startup.StartupRuntime => 0xb3d3821c => 284
	i32 3017076677, ; 513: Xamarin.GooglePlayServices.Maps => 0xb3d4efc5 => 303
	i32 3023353419, ; 514: WindowsBase.dll => 0xb434b64b => 165
	i32 3023511517, ; 515: ru\Microsoft.Data.SqlClient.resources => 0xb4371fdd => 319
	i32 3024354802, ; 516: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xb443fdf2 => 259
	i32 3033605958, ; 517: System.Memory.Data.dll => 0xb4d12746 => 220
	i32 3035389318, ; 518: Maui.GoogleMaps.dll => 0xb4ec5d86 => 205
	i32 3038032645, ; 519: _Microsoft.Android.Resource.Designer.dll => 0xb514b305 => 356
	i32 3056245963, ; 520: Xamarin.AndroidX.SavedState.SavedState.Ktx => 0xb62a9ccb => 281
	i32 3057625584, ; 521: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 272
	i32 3058099980, ; 522: Xamarin.GooglePlayServices.Tasks => 0xb646e70c => 304
	i32 3059408633, ; 523: Mono.Android.Runtime => 0xb65adef9 => 170
	i32 3059793426, ; 524: System.ComponentModel.Primitives => 0xb660be12 => 16
	i32 3075834255, ; 525: System.Threading.Tasks => 0xb755818f => 144
	i32 3077302341, ; 526: hu/Microsoft.Maui.Controls.resources.dll => 0xb76be845 => 334
	i32 3084678329, ; 527: Microsoft.IdentityModel.Tokens => 0xb7dc74b9 => 196
	i32 3090735792, ; 528: System.Security.Cryptography.X509Certificates.dll => 0xb838e2b0 => 125
	i32 3099732863, ; 529: System.Security.Claims.dll => 0xb8c22b7f => 118
	i32 3103600923, ; 530: System.Formats.Asn1 => 0xb8fd311b => 38
	i32 3111772706, ; 531: System.Runtime.Serialization => 0xb979e222 => 115
	i32 3121463068, ; 532: System.IO.FileSystem.AccessControl.dll => 0xba0dbf1c => 47
	i32 3124832203, ; 533: System.Threading.Tasks.Extensions => 0xba4127cb => 142
	i32 3132293585, ; 534: System.Security.AccessControl => 0xbab301d1 => 117
	i32 3147165239, ; 535: System.Diagnostics.Tracing.dll => 0xbb95ee37 => 34
	i32 3148237826, ; 536: GoogleGson.dll => 0xbba64c02 => 176
	i32 3158628304, ; 537: zh-Hant/Microsoft.Data.SqlClient.resources.dll => 0xbc44d7d0 => 321
	i32 3159123045, ; 538: System.Reflection.Primitives.dll => 0xbc4c6465 => 95
	i32 3160747431, ; 539: System.IO.MemoryMappedFiles => 0xbc652da7 => 53
	i32 3178803400, ; 540: Xamarin.AndroidX.Navigation.Fragment.dll => 0xbd78b0c8 => 273
	i32 3192346100, ; 541: System.Security.SecureString => 0xbe4755f4 => 129
	i32 3193515020, ; 542: System.Web => 0xbe592c0c => 153
	i32 3204380047, ; 543: System.Data.dll => 0xbefef58f => 24
	i32 3209718065, ; 544: System.Xml.XmlDocument.dll => 0xbf506931 => 161
	i32 3211777861, ; 545: Xamarin.AndroidX.DocumentFile => 0xbf6fd745 => 250
	i32 3220365878, ; 546: System.Threading => 0xbff2e236 => 148
	i32 3226221578, ; 547: System.Runtime.Handles.dll => 0xc04c3c0a => 104
	i32 3230466174, ; 548: Xamarin.GooglePlayServices.Basement.dll => 0xc08d007e => 302
	i32 3251039220, ; 549: System.Reflection.DispatchProxy.dll => 0xc1c6ebf4 => 89
	i32 3258312781, ; 550: Xamarin.AndroidX.CardView => 0xc235e84d => 237
	i32 3265493905, ; 551: System.Linq.Queryable.dll => 0xc2a37b91 => 60
	i32 3265893370, ; 552: System.Threading.Tasks.Extensions.dll => 0xc2a993fa => 142
	i32 3268887220, ; 553: fr/Microsoft.Data.SqlClient.resources.dll => 0xc2d742b4 => 314
	i32 3276600297, ; 554: pt-BR/Microsoft.Data.SqlClient.resources.dll => 0xc34cf3e9 => 318
	i32 3277815716, ; 555: System.Resources.Writer.dll => 0xc35f7fa4 => 100
	i32 3279906254, ; 556: Microsoft.Win32.Registry.dll => 0xc37f65ce => 5
	i32 3280506390, ; 557: System.ComponentModel.Annotations.dll => 0xc3888e16 => 13
	i32 3286872994, ; 558: SQLite-net.dll => 0xc3e9b3a2 => 212
	i32 3290767353, ; 559: System.Security.Cryptography.Encoding => 0xc4251ff9 => 122
	i32 3299363146, ; 560: System.Text.Encoding => 0xc4a8494a => 135
	i32 3303498502, ; 561: System.Diagnostics.FileVersionInfo => 0xc4e76306 => 28
	i32 3305363605, ; 562: fi\Microsoft.Maui.Controls.resources => 0xc503d895 => 329
	i32 3312457198, ; 563: Microsoft.IdentityModel.JsonWebTokens => 0xc57015ee => 192
	i32 3316684772, ; 564: System.Net.Requests.dll => 0xc5b097e4 => 72
	i32 3317135071, ; 565: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 248
	i32 3317144872, ; 566: System.Data => 0xc5b79d28 => 24
	i32 3330272589, ; 567: FOApp => 0xc67fed4d => 0
	i32 3340387945, ; 568: SkiaSharp => 0xc71a4669 => 207
	i32 3340431453, ; 569: Xamarin.AndroidX.Arch.Core.Runtime => 0xc71af05d => 235
	i32 3343947874, ; 570: fr\Microsoft.Data.SqlClient.resources => 0xc7509862 => 314
	i32 3345895724, ; 571: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xc76e512c => 277
	i32 3346324047, ; 572: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 274
	i32 3357674450, ; 573: ru\Microsoft.Maui.Controls.resources => 0xc8220bd2 => 346
	i32 3358260929, ; 574: System.Text.Json => 0xc82afec1 => 137
	i32 3360279109, ; 575: SQLitePCLRaw.core => 0xc849ca45 => 214
	i32 3362336904, ; 576: Xamarin.AndroidX.Activity.Ktx => 0xc8693088 => 228
	i32 3362522851, ; 577: Xamarin.AndroidX.Core => 0xc86c06e3 => 245
	i32 3366347497, ; 578: Java.Interop => 0xc8a662e9 => 168
	i32 3374879918, ; 579: Microsoft.IdentityModel.Protocols.dll => 0xc92894ae => 194
	i32 3374999561, ; 580: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 278
	i32 3381016424, ; 581: da\Microsoft.Maui.Controls.resources => 0xc9863768 => 325
	i32 3395150330, ; 582: System.Runtime.CompilerServices.Unsafe.dll => 0xca5de1fa => 101
	i32 3403906625, ; 583: System.Security.Cryptography.OpenSsl.dll => 0xcae37e41 => 123
	i32 3405233483, ; 584: Xamarin.AndroidX.CustomView.PoolingContainer => 0xcaf7bd4b => 249
	i32 3428513518, ; 585: Microsoft.Extensions.DependencyInjection.dll => 0xcc5af6ee => 182
	i32 3429136800, ; 586: System.Xml => 0xcc6479a0 => 163
	i32 3430777524, ; 587: netstandard => 0xcc7d82b4 => 167
	i32 3441283291, ; 588: Xamarin.AndroidX.DynamicAnimation.dll => 0xcd1dd0db => 252
	i32 3445260447, ; 589: System.Formats.Tar => 0xcd5a809f => 39
	i32 3452344032, ; 590: Microsoft.Maui.Controls.Compatibility.dll => 0xcdc696e0 => 197
	i32 3463511458, ; 591: hr/Microsoft.Maui.Controls.resources.dll => 0xce70fda2 => 333
	i32 3471940407, ; 592: System.ComponentModel.TypeConverter.dll => 0xcef19b37 => 17
	i32 3473156932, ; 593: SkiaSharp.Views.Maui.Controls.dll => 0xcf042b44 => 210
	i32 3476120550, ; 594: Mono.Android => 0xcf3163e6 => 171
	i32 3479583265, ; 595: ru/Microsoft.Maui.Controls.resources.dll => 0xcf663a21 => 346
	i32 3484440000, ; 596: ro\Microsoft.Maui.Controls.resources => 0xcfb055c0 => 345
	i32 3485117614, ; 597: System.Text.Json.dll => 0xcfbaacae => 137
	i32 3486566296, ; 598: System.Transactions => 0xcfd0c798 => 150
	i32 3493954962, ; 599: Xamarin.AndroidX.Concurrent.Futures.dll => 0xd0418592 => 241
	i32 3509114376, ; 600: System.Xml.Linq => 0xd128d608 => 155
	i32 3515174580, ; 601: System.Security.dll => 0xd1854eb4 => 130
	i32 3530912306, ; 602: System.Configuration => 0xd2757232 => 19
	i32 3539954161, ; 603: System.Net.HttpListener => 0xd2ff69f1 => 65
	i32 3545306353, ; 604: Microsoft.Data.SqlClient => 0xd35114f1 => 178
	i32 3555084973, ; 605: de\Microsoft.Data.SqlClient.resources => 0xd3e64aad => 312
	i32 3560100363, ; 606: System.Threading.Timer => 0xd432d20b => 147
	i32 3561949811, ; 607: Azure.Core.dll => 0xd44f0a73 => 173
	i32 3570554715, ; 608: System.IO.FileSystem.AccessControl => 0xd4d2575b => 47
	i32 3570608287, ; 609: System.Runtime.Caching.dll => 0xd4d3289f => 221
	i32 3580758918, ; 610: zh-HK\Microsoft.Maui.Controls.resources => 0xd56e0b86 => 353
	i32 3597029428, ; 611: Xamarin.Android.Glide.GifDecoder.dll => 0xd6665034 => 226
	i32 3598340787, ; 612: System.Net.WebSockets.Client => 0xd67a52b3 => 79
	i32 3608519521, ; 613: System.Linq.dll => 0xd715a361 => 61
	i32 3624195450, ; 614: System.Runtime.InteropServices.RuntimeInformation => 0xd804d57a => 106
	i32 3627220390, ; 615: Xamarin.AndroidX.Print.dll => 0xd832fda6 => 276
	i32 3633644679, ; 616: Xamarin.AndroidX.Annotation.Experimental => 0xd8950487 => 230
	i32 3638274909, ; 617: System.IO.FileSystem.Primitives.dll => 0xd8dbab5d => 49
	i32 3641597786, ; 618: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 262
	i32 3643446276, ; 619: tr\Microsoft.Maui.Controls.resources => 0xd92a9404 => 350
	i32 3643854240, ; 620: Xamarin.AndroidX.Navigation.Fragment => 0xd930cda0 => 273
	i32 3645089577, ; 621: System.ComponentModel.DataAnnotations => 0xd943a729 => 14
	i32 3657292374, ; 622: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd9fdda56 => 181
	i32 3660523487, ; 623: System.Net.NetworkInformation => 0xda2f27df => 68
	i32 3672681054, ; 624: Mono.Android.dll => 0xdae8aa5e => 171
	i32 3682565725, ; 625: Xamarin.AndroidX.Browser => 0xdb7f7e5d => 236
	i32 3684561358, ; 626: Xamarin.AndroidX.Concurrent.Futures => 0xdb9df1ce => 241
	i32 3697841164, ; 627: zh-Hant/Microsoft.Maui.Controls.resources.dll => 0xdc68940c => 355
	i32 3700591436, ; 628: Microsoft.IdentityModel.Abstractions.dll => 0xdc928b4c => 191
	i32 3700866549, ; 629: System.Net.WebProxy.dll => 0xdc96bdf5 => 78
	i32 3706696989, ; 630: Xamarin.AndroidX.Core.Core.Ktx.dll => 0xdcefb51d => 246
	i32 3716563718, ; 631: System.Runtime.Intrinsics => 0xdd864306 => 108
	i32 3718780102, ; 632: Xamarin.AndroidX.Annotation => 0xdda814c6 => 229
	i32 3724971120, ; 633: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 272
	i32 3732100267, ; 634: System.Net.NameResolution => 0xde7354ab => 67
	i32 3737834244, ; 635: System.Net.Http.Json.dll => 0xdecad304 => 63
	i32 3748608112, ; 636: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 27
	i32 3751444290, ; 637: System.Xml.XPath => 0xdf9a7f42 => 160
	i32 3754567612, ; 638: SQLitePCLRaw.provider.e_sqlite3 => 0xdfca27bc => 216
	i32 3786282454, ; 639: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 238
	i32 3792276235, ; 640: System.Collections.NonGeneric => 0xe2098b0b => 10
	i32 3800979733, ; 641: Microsoft.Maui.Controls.Compatibility => 0xe28e5915 => 197
	i32 3802395368, ; 642: System.Collections.Specialized.dll => 0xe2a3f2e8 => 11
	i32 3803019198, ; 643: zh-Hans/Microsoft.Data.SqlClient.resources.dll => 0xe2ad77be => 320
	i32 3819260425, ; 644: System.Net.WebProxy => 0xe3a54a09 => 78
	i32 3823082795, ; 645: System.Security.Cryptography.dll => 0xe3df9d2b => 126
	i32 3829621856, ; 646: System.Numerics.dll => 0xe4436460 => 83
	i32 3841636137, ; 647: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xe4fab729 => 183
	i32 3844307129, ; 648: System.Net.Mail.dll => 0xe52378b9 => 66
	i32 3848348906, ; 649: es\Microsoft.Data.SqlClient.resources => 0xe56124ea => 313
	i32 3849253459, ; 650: System.Runtime.InteropServices.dll => 0xe56ef253 => 107
	i32 3870376305, ; 651: System.Net.HttpListener.dll => 0xe6b14171 => 65
	i32 3873536506, ; 652: System.Security.Principal => 0xe6e179fa => 128
	i32 3875112723, ; 653: System.Security.Cryptography.Encoding.dll => 0xe6f98713 => 122
	i32 3876362041, ; 654: SQLite-net => 0xe70c9739 => 212
	i32 3885497537, ; 655: System.Net.WebHeaderCollection.dll => 0xe797fcc1 => 77
	i32 3885922214, ; 656: Xamarin.AndroidX.Transition.dll => 0xe79e77a6 => 287
	i32 3888767677, ; 657: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0xe7c9e2bd => 277
	i32 3889960447, ; 658: zh-Hans/Microsoft.Maui.Controls.resources.dll => 0xe7dc15ff => 354
	i32 3896106733, ; 659: System.Collections.Concurrent.dll => 0xe839deed => 8
	i32 3896760992, ; 660: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 245
	i32 3901907137, ; 661: Microsoft.VisualBasic.Core.dll => 0xe89260c1 => 2
	i32 3910130544, ; 662: Xamarin.AndroidX.Collection.Jvm => 0xe90fdb70 => 239
	i32 3920810846, ; 663: System.IO.Compression.FileSystem.dll => 0xe9b2d35e => 44
	i32 3921031405, ; 664: Xamarin.AndroidX.VersionedParcelable.dll => 0xe9b630ed => 290
	i32 3928044579, ; 665: System.Xml.ReaderWriter => 0xea213423 => 156
	i32 3930554604, ; 666: System.Security.Principal.dll => 0xea4780ec => 128
	i32 3931092270, ; 667: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 275
	i32 3945713374, ; 668: System.Data.DataSetExtensions.dll => 0xeb2ecede => 23
	i32 3953953790, ; 669: System.Text.Encoding.CodePages => 0xebac8bfe => 133
	i32 3955647286, ; 670: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 232
	i32 3959773229, ; 671: Xamarin.AndroidX.Lifecycle.Process => 0xec05582d => 264
	i32 3970018735, ; 672: Xamarin.GooglePlayServices.Tasks.dll => 0xeca1adaf => 304
	i32 3970572422, ; 673: Google.Maps.Utils.Android.dll => 0xecaa2086 => 300
	i32 3980434154, ; 674: th/Microsoft.Maui.Controls.resources.dll => 0xed409aea => 349
	i32 3987592930, ; 675: he/Microsoft.Maui.Controls.resources.dll => 0xedadd6e2 => 331
	i32 4003436829, ; 676: System.Diagnostics.Process.dll => 0xee9f991d => 29
	i32 4015948917, ; 677: Xamarin.AndroidX.Annotation.Jvm.dll => 0xef5e8475 => 231
	i32 4025784931, ; 678: System.Memory => 0xeff49a63 => 62
	i32 4046471985, ; 679: Microsoft.Maui.Controls.Xaml.dll => 0xf1304331 => 199
	i32 4054681211, ; 680: System.Reflection.Emit.ILGeneration => 0xf1ad867b => 90
	i32 4068434129, ; 681: System.Private.Xml.Linq.dll => 0xf27f60d1 => 87
	i32 4073602200, ; 682: System.Threading.dll => 0xf2ce3c98 => 148
	i32 4094352644, ; 683: Microsoft.Maui.Essentials.dll => 0xf40add04 => 201
	i32 4099507663, ; 684: System.Drawing.dll => 0xf45985cf => 36
	i32 4100113165, ; 685: System.Private.Uri => 0xf462c30d => 86
	i32 4101593132, ; 686: Xamarin.AndroidX.Emoji2 => 0xf479582c => 253
	i32 4102112229, ; 687: pt/Microsoft.Maui.Controls.resources.dll => 0xf48143e5 => 344
	i32 4125707920, ; 688: ms/Microsoft.Maui.Controls.resources.dll => 0xf5e94e90 => 339
	i32 4126470640, ; 689: Microsoft.Extensions.DependencyInjection => 0xf5f4f1f0 => 182
	i32 4127667938, ; 690: System.IO.FileSystem.Watcher => 0xf60736e2 => 50
	i32 4130442656, ; 691: System.AppContext => 0xf6318da0 => 6
	i32 4147896353, ; 692: System.Reflection.Emit.ILGeneration.dll => 0xf73be021 => 90
	i32 4150914736, ; 693: uk\Microsoft.Maui.Controls.resources => 0xf769eeb0 => 351
	i32 4151237749, ; 694: System.Core => 0xf76edc75 => 21
	i32 4159265925, ; 695: System.Xml.XmlSerializer => 0xf7e95c85 => 162
	i32 4161255271, ; 696: System.Reflection.TypeExtensions => 0xf807b767 => 96
	i32 4164802419, ; 697: System.IO.FileSystem.Watcher.dll => 0xf83dd773 => 50
	i32 4181436372, ; 698: System.Runtime.Serialization.Primitives => 0xf93ba7d4 => 113
	i32 4182413190, ; 699: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 269
	i32 4185676441, ; 700: System.Security => 0xf97c5a99 => 130
	i32 4196529839, ; 701: System.Net.WebClient.dll => 0xfa21f6af => 76
	i32 4213026141, ; 702: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 27
	i32 4256097574, ; 703: Xamarin.AndroidX.Core.Core.Ktx => 0xfdaee526 => 246
	i32 4257443520, ; 704: ko/Microsoft.Data.SqlClient.resources.dll => 0xfdc36ec0 => 317
	i32 4258378803, ; 705: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx => 0xfdd1b433 => 268
	i32 4260525087, ; 706: System.Buffers => 0xfdf2741f => 7
	i32 4263231520, ; 707: System.IdentityModel.Tokens.Jwt.dll => 0xfe1bc020 => 219
	i32 4271975918, ; 708: Microsoft.Maui.Controls.dll => 0xfea12dee => 198
	i32 4274623895, ; 709: CommunityToolkit.Mvvm.dll => 0xfec99597 => 175
	i32 4274976490, ; 710: System.Runtime.Numerics => 0xfecef6ea => 110
	i32 4278134329, ; 711: Xamarin.GooglePlayServices.Maps.dll => 0xfeff2639 => 303
	i32 4292120959, ; 712: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 269
	i32 4294763496 ; 713: Xamarin.AndroidX.ExifInterface.dll => 0xfffce3e8 => 255
], align 4

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [714 x i32] [
	i32 68, ; 0
	i32 67, ; 1
	i32 108, ; 2
	i32 265, ; 3
	i32 299, ; 4
	i32 48, ; 5
	i32 204, ; 6
	i32 80, ; 7
	i32 145, ; 8
	i32 315, ; 9
	i32 316, ; 10
	i32 30, ; 11
	i32 355, ; 12
	i32 124, ; 13
	i32 202, ; 14
	i32 102, ; 15
	i32 283, ; 16
	i32 107, ; 17
	i32 283, ; 18
	i32 139, ; 19
	i32 308, ; 20
	i32 316, ; 21
	i32 77, ; 22
	i32 124, ; 23
	i32 13, ; 24
	i32 238, ; 25
	i32 319, ; 26
	i32 132, ; 27
	i32 285, ; 28
	i32 151, ; 29
	i32 352, ; 30
	i32 353, ; 31
	i32 18, ; 32
	i32 236, ; 33
	i32 26, ; 34
	i32 259, ; 35
	i32 1, ; 36
	i32 59, ; 37
	i32 42, ; 38
	i32 91, ; 39
	i32 242, ; 40
	i32 320, ; 41
	i32 147, ; 42
	i32 261, ; 43
	i32 258, ; 44
	i32 324, ; 45
	i32 54, ; 46
	i32 69, ; 47
	i32 352, ; 48
	i32 227, ; 49
	i32 83, ; 50
	i32 203, ; 51
	i32 337, ; 52
	i32 205, ; 53
	i32 260, ; 54
	i32 215, ; 55
	i32 336, ; 56
	i32 131, ; 57
	i32 55, ; 58
	i32 149, ; 59
	i32 74, ; 60
	i32 145, ; 61
	i32 62, ; 62
	i32 146, ; 63
	i32 356, ; 64
	i32 165, ; 65
	i32 348, ; 66
	i32 243, ; 67
	i32 12, ; 68
	i32 256, ; 69
	i32 125, ; 70
	i32 152, ; 71
	i32 113, ; 72
	i32 166, ; 73
	i32 164, ; 74
	i32 258, ; 75
	i32 191, ; 76
	i32 271, ; 77
	i32 84, ; 78
	i32 335, ; 79
	i32 329, ; 80
	i32 188, ; 81
	i32 300, ; 82
	i32 207, ; 83
	i32 150, ; 84
	i32 308, ; 85
	i32 60, ; 86
	i32 184, ; 87
	i32 51, ; 88
	i32 103, ; 89
	i32 114, ; 90
	i32 177, ; 91
	i32 40, ; 92
	i32 296, ; 93
	i32 294, ; 94
	i32 120, ; 95
	i32 343, ; 96
	i32 52, ; 97
	i32 44, ; 98
	i32 119, ; 99
	i32 248, ; 100
	i32 341, ; 101
	i32 208, ; 102
	i32 254, ; 103
	i32 81, ; 104
	i32 136, ; 105
	i32 290, ; 106
	i32 234, ; 107
	i32 8, ; 108
	i32 73, ; 109
	i32 323, ; 110
	i32 155, ; 111
	i32 310, ; 112
	i32 154, ; 113
	i32 92, ; 114
	i32 305, ; 115
	i32 45, ; 116
	i32 338, ; 117
	i32 326, ; 118
	i32 309, ; 119
	i32 109, ; 120
	i32 129, ; 121
	i32 213, ; 122
	i32 25, ; 123
	i32 224, ; 124
	i32 72, ; 125
	i32 55, ; 126
	i32 46, ; 127
	i32 347, ; 128
	i32 187, ; 129
	i32 249, ; 130
	i32 22, ; 131
	i32 263, ; 132
	i32 86, ; 133
	i32 43, ; 134
	i32 160, ; 135
	i32 71, ; 136
	i32 276, ; 137
	i32 3, ; 138
	i32 42, ; 139
	i32 63, ; 140
	i32 16, ; 141
	i32 53, ; 142
	i32 350, ; 143
	i32 299, ; 144
	i32 105, ; 145
	i32 204, ; 146
	i32 309, ; 147
	i32 297, ; 148
	i32 260, ; 149
	i32 34, ; 150
	i32 158, ; 151
	i32 85, ; 152
	i32 32, ; 153
	i32 12, ; 154
	i32 51, ; 155
	i32 56, ; 156
	i32 280, ; 157
	i32 36, ; 158
	i32 183, ; 159
	i32 325, ; 160
	i32 298, ; 161
	i32 232, ; 162
	i32 35, ; 163
	i32 58, ; 164
	i32 312, ; 165
	i32 267, ; 166
	i32 190, ; 167
	i32 176, ; 168
	i32 17, ; 169
	i32 306, ; 170
	i32 218, ; 171
	i32 164, ; 172
	i32 338, ; 173
	i32 266, ; 174
	i32 186, ; 175
	i32 178, ; 176
	i32 293, ; 177
	i32 344, ; 178
	i32 153, ; 179
	i32 289, ; 180
	i32 274, ; 181
	i32 342, ; 182
	i32 234, ; 183
	i32 29, ; 184
	i32 175, ; 185
	i32 52, ; 186
	i32 340, ; 187
	i32 294, ; 188
	i32 239, ; 189
	i32 5, ; 190
	i32 324, ; 191
	i32 284, ; 192
	i32 288, ; 193
	i32 240, ; 194
	i32 310, ; 195
	i32 206, ; 196
	i32 231, ; 197
	i32 214, ; 198
	i32 251, ; 199
	i32 85, ; 200
	i32 293, ; 201
	i32 61, ; 202
	i32 112, ; 203
	i32 57, ; 204
	i32 354, ; 205
	i32 280, ; 206
	i32 99, ; 207
	i32 19, ; 208
	i32 244, ; 209
	i32 111, ; 210
	i32 101, ; 211
	i32 102, ; 212
	i32 322, ; 213
	i32 104, ; 214
	i32 297, ; 215
	i32 71, ; 216
	i32 38, ; 217
	i32 32, ; 218
	i32 103, ; 219
	i32 73, ; 220
	i32 219, ; 221
	i32 328, ; 222
	i32 9, ; 223
	i32 123, ; 224
	i32 46, ; 225
	i32 233, ; 226
	i32 188, ; 227
	i32 9, ; 228
	i32 43, ; 229
	i32 4, ; 230
	i32 281, ; 231
	i32 179, ; 232
	i32 332, ; 233
	i32 192, ; 234
	i32 327, ; 235
	i32 31, ; 236
	i32 138, ; 237
	i32 92, ; 238
	i32 93, ; 239
	i32 347, ; 240
	i32 221, ; 241
	i32 49, ; 242
	i32 141, ; 243
	i32 112, ; 244
	i32 140, ; 245
	i32 174, ; 246
	i32 250, ; 247
	i32 115, ; 248
	i32 321, ; 249
	i32 298, ; 250
	i32 157, ; 251
	i32 76, ; 252
	i32 79, ; 253
	i32 270, ; 254
	i32 37, ; 255
	i32 210, ; 256
	i32 292, ; 257
	i32 195, ; 258
	i32 254, ; 259
	i32 247, ; 260
	i32 64, ; 261
	i32 138, ; 262
	i32 15, ; 263
	i32 116, ; 264
	i32 286, ; 265
	i32 295, ; 266
	i32 242, ; 267
	i32 48, ; 268
	i32 70, ; 269
	i32 80, ; 270
	i32 126, ; 271
	i32 179, ; 272
	i32 94, ; 273
	i32 121, ; 274
	i32 307, ; 275
	i32 26, ; 276
	i32 215, ; 277
	i32 263, ; 278
	i32 97, ; 279
	i32 28, ; 280
	i32 237, ; 281
	i32 345, ; 282
	i32 323, ; 283
	i32 149, ; 284
	i32 169, ; 285
	i32 4, ; 286
	i32 98, ; 287
	i32 33, ; 288
	i32 93, ; 289
	i32 285, ; 290
	i32 184, ; 291
	i32 21, ; 292
	i32 41, ; 293
	i32 170, ; 294
	i32 339, ; 295
	i32 256, ; 296
	i32 331, ; 297
	i32 189, ; 298
	i32 177, ; 299
	i32 270, ; 300
	i32 0, ; 301
	i32 306, ; 302
	i32 295, ; 303
	i32 275, ; 304
	i32 2, ; 305
	i32 134, ; 306
	i32 111, ; 307
	i32 185, ; 308
	i32 351, ; 309
	i32 224, ; 310
	i32 348, ; 311
	i32 58, ; 312
	i32 95, ; 313
	i32 195, ; 314
	i32 330, ; 315
	i32 39, ; 316
	i32 235, ; 317
	i32 25, ; 318
	i32 94, ; 319
	i32 89, ; 320
	i32 99, ; 321
	i32 302, ; 322
	i32 10, ; 323
	i32 87, ; 324
	i32 100, ; 325
	i32 282, ; 326
	i32 180, ; 327
	i32 307, ; 328
	i32 226, ; 329
	i32 196, ; 330
	i32 327, ; 331
	i32 7, ; 332
	i32 267, ; 333
	i32 322, ; 334
	i32 223, ; 335
	i32 190, ; 336
	i32 88, ; 337
	i32 262, ; 338
	i32 154, ; 339
	i32 326, ; 340
	i32 33, ; 341
	i32 116, ; 342
	i32 82, ; 343
	i32 216, ; 344
	i32 20, ; 345
	i32 301, ; 346
	i32 206, ; 347
	i32 11, ; 348
	i32 162, ; 349
	i32 3, ; 350
	i32 200, ; 351
	i32 334, ; 352
	i32 187, ; 353
	i32 185, ; 354
	i32 84, ; 355
	i32 311, ; 356
	i32 64, ; 357
	i32 336, ; 358
	i32 289, ; 359
	i32 143, ; 360
	i32 318, ; 361
	i32 271, ; 362
	i32 157, ; 363
	i32 194, ; 364
	i32 41, ; 365
	i32 117, ; 366
	i32 181, ; 367
	i32 225, ; 368
	i32 330, ; 369
	i32 278, ; 370
	i32 131, ; 371
	i32 75, ; 372
	i32 66, ; 373
	i32 340, ; 374
	i32 172, ; 375
	i32 317, ; 376
	i32 229, ; 377
	i32 143, ; 378
	i32 106, ; 379
	i32 151, ; 380
	i32 70, ; 381
	i32 211, ; 382
	i32 156, ; 383
	i32 193, ; 384
	i32 180, ; 385
	i32 121, ; 386
	i32 127, ; 387
	i32 335, ; 388
	i32 152, ; 389
	i32 253, ; 390
	i32 141, ; 391
	i32 240, ; 392
	i32 332, ; 393
	i32 20, ; 394
	i32 14, ; 395
	i32 135, ; 396
	i32 75, ; 397
	i32 59, ; 398
	i32 213, ; 399
	i32 243, ; 400
	i32 167, ; 401
	i32 168, ; 402
	i32 198, ; 403
	i32 15, ; 404
	i32 74, ; 405
	i32 6, ; 406
	i32 23, ; 407
	i32 265, ; 408
	i32 218, ; 409
	i32 223, ; 410
	i32 91, ; 411
	i32 333, ; 412
	i32 1, ; 413
	i32 136, ; 414
	i32 266, ; 415
	i32 288, ; 416
	i32 134, ; 417
	i32 69, ; 418
	i32 146, ; 419
	i32 342, ; 420
	i32 311, ; 421
	i32 257, ; 422
	i32 186, ; 423
	i32 88, ; 424
	i32 96, ; 425
	i32 247, ; 426
	i32 252, ; 427
	i32 211, ; 428
	i32 337, ; 429
	i32 31, ; 430
	i32 220, ; 431
	i32 45, ; 432
	i32 261, ; 433
	i32 193, ; 434
	i32 173, ; 435
	i32 222, ; 436
	i32 225, ; 437
	i32 109, ; 438
	i32 158, ; 439
	i32 35, ; 440
	i32 22, ; 441
	i32 174, ; 442
	i32 114, ; 443
	i32 57, ; 444
	i32 286, ; 445
	i32 144, ; 446
	i32 118, ; 447
	i32 120, ; 448
	i32 110, ; 449
	i32 227, ; 450
	i32 139, ; 451
	i32 233, ; 452
	i32 189, ; 453
	i32 54, ; 454
	i32 105, ; 455
	i32 343, ; 456
	i32 203, ; 457
	i32 199, ; 458
	i32 200, ; 459
	i32 133, ; 460
	i32 305, ; 461
	i32 291, ; 462
	i32 279, ; 463
	i32 349, ; 464
	i32 257, ; 465
	i32 209, ; 466
	i32 202, ; 467
	i32 159, ; 468
	i32 313, ; 469
	i32 328, ; 470
	i32 244, ; 471
	i32 163, ; 472
	i32 132, ; 473
	i32 279, ; 474
	i32 161, ; 475
	i32 341, ; 476
	i32 268, ; 477
	i32 315, ; 478
	i32 301, ; 479
	i32 140, ; 480
	i32 291, ; 481
	i32 208, ; 482
	i32 287, ; 483
	i32 169, ; 484
	i32 201, ; 485
	i32 222, ; 486
	i32 228, ; 487
	i32 296, ; 488
	i32 40, ; 489
	i32 255, ; 490
	i32 81, ; 491
	i32 56, ; 492
	i32 37, ; 493
	i32 97, ; 494
	i32 166, ; 495
	i32 172, ; 496
	i32 209, ; 497
	i32 292, ; 498
	i32 82, ; 499
	i32 230, ; 500
	i32 98, ; 501
	i32 30, ; 502
	i32 159, ; 503
	i32 217, ; 504
	i32 18, ; 505
	i32 127, ; 506
	i32 119, ; 507
	i32 251, ; 508
	i32 282, ; 509
	i32 264, ; 510
	i32 217, ; 511
	i32 284, ; 512
	i32 303, ; 513
	i32 165, ; 514
	i32 319, ; 515
	i32 259, ; 516
	i32 220, ; 517
	i32 205, ; 518
	i32 356, ; 519
	i32 281, ; 520
	i32 272, ; 521
	i32 304, ; 522
	i32 170, ; 523
	i32 16, ; 524
	i32 144, ; 525
	i32 334, ; 526
	i32 196, ; 527
	i32 125, ; 528
	i32 118, ; 529
	i32 38, ; 530
	i32 115, ; 531
	i32 47, ; 532
	i32 142, ; 533
	i32 117, ; 534
	i32 34, ; 535
	i32 176, ; 536
	i32 321, ; 537
	i32 95, ; 538
	i32 53, ; 539
	i32 273, ; 540
	i32 129, ; 541
	i32 153, ; 542
	i32 24, ; 543
	i32 161, ; 544
	i32 250, ; 545
	i32 148, ; 546
	i32 104, ; 547
	i32 302, ; 548
	i32 89, ; 549
	i32 237, ; 550
	i32 60, ; 551
	i32 142, ; 552
	i32 314, ; 553
	i32 318, ; 554
	i32 100, ; 555
	i32 5, ; 556
	i32 13, ; 557
	i32 212, ; 558
	i32 122, ; 559
	i32 135, ; 560
	i32 28, ; 561
	i32 329, ; 562
	i32 192, ; 563
	i32 72, ; 564
	i32 248, ; 565
	i32 24, ; 566
	i32 0, ; 567
	i32 207, ; 568
	i32 235, ; 569
	i32 314, ; 570
	i32 277, ; 571
	i32 274, ; 572
	i32 346, ; 573
	i32 137, ; 574
	i32 214, ; 575
	i32 228, ; 576
	i32 245, ; 577
	i32 168, ; 578
	i32 194, ; 579
	i32 278, ; 580
	i32 325, ; 581
	i32 101, ; 582
	i32 123, ; 583
	i32 249, ; 584
	i32 182, ; 585
	i32 163, ; 586
	i32 167, ; 587
	i32 252, ; 588
	i32 39, ; 589
	i32 197, ; 590
	i32 333, ; 591
	i32 17, ; 592
	i32 210, ; 593
	i32 171, ; 594
	i32 346, ; 595
	i32 345, ; 596
	i32 137, ; 597
	i32 150, ; 598
	i32 241, ; 599
	i32 155, ; 600
	i32 130, ; 601
	i32 19, ; 602
	i32 65, ; 603
	i32 178, ; 604
	i32 312, ; 605
	i32 147, ; 606
	i32 173, ; 607
	i32 47, ; 608
	i32 221, ; 609
	i32 353, ; 610
	i32 226, ; 611
	i32 79, ; 612
	i32 61, ; 613
	i32 106, ; 614
	i32 276, ; 615
	i32 230, ; 616
	i32 49, ; 617
	i32 262, ; 618
	i32 350, ; 619
	i32 273, ; 620
	i32 14, ; 621
	i32 181, ; 622
	i32 68, ; 623
	i32 171, ; 624
	i32 236, ; 625
	i32 241, ; 626
	i32 355, ; 627
	i32 191, ; 628
	i32 78, ; 629
	i32 246, ; 630
	i32 108, ; 631
	i32 229, ; 632
	i32 272, ; 633
	i32 67, ; 634
	i32 63, ; 635
	i32 27, ; 636
	i32 160, ; 637
	i32 216, ; 638
	i32 238, ; 639
	i32 10, ; 640
	i32 197, ; 641
	i32 11, ; 642
	i32 320, ; 643
	i32 78, ; 644
	i32 126, ; 645
	i32 83, ; 646
	i32 183, ; 647
	i32 66, ; 648
	i32 313, ; 649
	i32 107, ; 650
	i32 65, ; 651
	i32 128, ; 652
	i32 122, ; 653
	i32 212, ; 654
	i32 77, ; 655
	i32 287, ; 656
	i32 277, ; 657
	i32 354, ; 658
	i32 8, ; 659
	i32 245, ; 660
	i32 2, ; 661
	i32 239, ; 662
	i32 44, ; 663
	i32 290, ; 664
	i32 156, ; 665
	i32 128, ; 666
	i32 275, ; 667
	i32 23, ; 668
	i32 133, ; 669
	i32 232, ; 670
	i32 264, ; 671
	i32 304, ; 672
	i32 300, ; 673
	i32 349, ; 674
	i32 331, ; 675
	i32 29, ; 676
	i32 231, ; 677
	i32 62, ; 678
	i32 199, ; 679
	i32 90, ; 680
	i32 87, ; 681
	i32 148, ; 682
	i32 201, ; 683
	i32 36, ; 684
	i32 86, ; 685
	i32 253, ; 686
	i32 344, ; 687
	i32 339, ; 688
	i32 182, ; 689
	i32 50, ; 690
	i32 6, ; 691
	i32 90, ; 692
	i32 351, ; 693
	i32 21, ; 694
	i32 162, ; 695
	i32 96, ; 696
	i32 50, ; 697
	i32 113, ; 698
	i32 269, ; 699
	i32 130, ; 700
	i32 76, ; 701
	i32 27, ; 702
	i32 246, ; 703
	i32 317, ; 704
	i32 268, ; 705
	i32 7, ; 706
	i32 219, ; 707
	i32 198, ; 708
	i32 175, ; 709
	i32 110, ; 710
	i32 303, ; 711
	i32 269, ; 712
	i32 255 ; 713
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
