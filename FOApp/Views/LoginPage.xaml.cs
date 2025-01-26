using CQApp.Models;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace CQApp.Views;

public partial class LoginPage : ContentPage //, IPage
{
    Entry entryUsername;
    Entry entryPassword;
    Button buttonLogin;

    public LoginPage()
    {

        Title = "Đăng nhập";

        entryUsername = new Entry
        {
            Placeholder = "Tên đăng nhập"
        };

        entryPassword = new Entry
        {
            Placeholder = "Mật khẩu",
            IsPassword = true
        };

        buttonLogin = new Button
        {
            Text = "Đăng nhập"
        };
        buttonLogin.Clicked += OnLoginClicked;

        Content = new StackLayout
        {
            Padding = new Thickness(10),
            Children = {
                    new Label { Text = "Đăng nhập", FontSize = 24, HorizontalOptions = LayoutOptions.Center },
                    entryUsername,
                    entryPassword,
                    buttonLogin
                }
        };
    }
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string username = entryUsername.Text;
        string password = entryPassword.Text;

        // Thực hiện đăng nhập và lấy thông tin người dùng
        var userInfo = await AuthenticateUserAsync(username, password);

        if (userInfo != null)
        {
            // Gọi phương thức lưu thông tin sau khi xác thực thành công
            SaveUserInfo(userInfo);

            // Chuyển hướng đến MainPage

            //await Shell.Current.GoToAsync("MainPage");
            if (Shell.Current != null)
            {

                await Shell.Current.GoToAsync("MainPage");
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(new MainPage());
                //Shell.Current.CurrentItem = Shell.Current.Items.FirstOrDefault(item => item.Route == "MainPage");
            }
        }
        else
        {
            // Hiển thị thông báo lỗi nếu đăng nhập thất bại
            await DisplayAlert("Đăng nhập thất bại", "Tên đăng nhập hoặc mật khẩu không đúng", "OK");
        }
    }
    private void SaveUserInfo(UserInfo userInfo)
    {
        Preferences.Set("IdUser", userInfo.IdUser);
        Preferences.Set("UserName", userInfo.UserName);
        Preferences.Set("FullName", userInfo.FullName);
        Preferences.Set("IdSegmentList", userInfo.IdSegmentList);
        // Lưu ảnh dưới dạng base64 string
        if (userInfo.Image != null)
        {
            Preferences.Set("Image", Convert.ToBase64String(userInfo.Image));
        }
    }
    private async Task<UserInfo> AuthenticateUserAsync(string username, string password)
    {
        
        password = CommonFunctions.EncryptString(password, CommonFunctions.EncryptProvider.MD5);
        
        UserInfo? userInfo = null;
        
        try
        {
            using var connection = new SqlConnection(Constants.sqlConnectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand("sp_HSKT_UsersLoginCQApp", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserName", username);
            command.Parameters.AddWithValue("@UserPassword", password);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                userInfo = new UserInfo
                {
                    IdUser = reader.GetInt32(0).ToString(),
                    UserName = reader.GetString(1),
                    FullName = reader.GetString(3),
                    Image = reader["Image"] != DBNull.Value ? (byte[])reader["Image"] : [], 
                    IdSegmentList = reader["idsegmentList"] as string ?? string.Empty 
                };
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Lỗi", $"Không thể lấy dữ liệu: {ex.Message}", "OK");
        }

        userInfo ??= new UserInfo
            {
                IdUser = "0",
                UserName = "guest",
                FullName = "guest",
                Image = [],
                IdSegmentList = string.Empty
            };
        return userInfo;
    }
}

public class CommonFunctions
{
    public static string CreateKeyCode(int KeyCodeLength)
    {
        const string KeyCodePattern = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        Random indexRandom = new();

        char[] keycodeChar = new char[KeyCodeLength];

        for (int i = 0; i < KeyCodeLength; i++)
        {
            keycodeChar[i] = KeyCodePattern[(int)(KeyCodePattern.Length * indexRandom.NextDouble())];
        }

        return new string(keycodeChar);
    }

    public enum EncryptProvider
    {
        SHA1,
        SHA256,
        SHA384,
        SHA512,
        MD5
    }

    public static string EncryptString(string strInput, EncryptProvider p)
    {
        byte[] _buffer = Encoding.UTF8.GetBytes(strInput);
        HashAlgorithm _Hash;

        switch (p)
        {
            case EncryptProvider.MD5:
                _Hash = MD5.Create();
                break;
            case EncryptProvider.SHA1:
                _Hash = SHA1.Create();
                break;
            case EncryptProvider.SHA256:
                _Hash = SHA256.Create();
                break;
            case EncryptProvider.SHA384:
                _Hash = SHA384.Create();
                break;
            case EncryptProvider.SHA512:
                _Hash = SHA512.Create();
                break;
            default:
                throw new ArgumentException("Invalid encryption provider specified");
        }

        byte[] _HashValue = _Hash.ComputeHash(_buffer);
        StringBuilder strEncrypt = new StringBuilder();

        for (int i = 0; i < _HashValue.Length; i++)
        {
            strEncrypt.Append(_HashValue[i].ToString("x2")); // Use hexadecimal format for better readability
        }

        return strEncrypt.ToString();
    }
}


