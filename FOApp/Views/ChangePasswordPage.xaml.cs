using Microsoft.Data.SqlClient;

namespace CQApp.Views;

public partial class ChangePasswordPage : ContentPage
{
	public ChangePasswordPage()
	{
            InitializeComponent();
    }
    private async void OnChangePasswordButtonClicked(object sender, EventArgs e)
    {
        string currentPassword = currentPasswordEntry.Text;
        string newPassword = newPasswordEntry.Text;
        string confirmPassword = confirmPasswordEntry.Text;

        // Kiểm tra mật khẩu mới và xác nhận mật khẩu mới có khớp nhau không
        if (newPassword != confirmPassword)
        {
            await DisplayAlert("Lỗi", "Mật khẩu mới và xác nhận mật khẩu không khớp.", "OK");
            return;
        }

        // Gọi hàm để thực hiện đổi mật khẩu
        bool isPasswordChanged = await ChangePasswordAsync(currentPassword, newPassword);

        if (isPasswordChanged)
        {
            await DisplayAlert("Thành công", "Đổi mật khẩu thành công.", "OK");
            await Navigation.PopAsync(); // Quay lại trang trước đó
        }
        else
        {
            await DisplayAlert("Lỗi", "Không thể đổi mật khẩu. Vui lòng thử lại.", "OK");
        }
    }
    private async Task<bool> ChangePasswordAsync(string currentPassword, string newPassword)
    {
        // Mã hóa mật khẩu
        currentPassword = CommonFunctions.EncryptString(currentPassword, CommonFunctions.EncryptProvider.MD5);
        newPassword = CommonFunctions.EncryptString(newPassword, CommonFunctions.EncryptProvider.MD5);


        try
        {
            using var connection = new SqlConnection(Constants.sqlConnectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand("sp_HSKT_ChangeUserPassword", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserName", Preferences.Get("UserName", string.Empty));
            command.Parameters.AddWithValue("@CurrentPassword", currentPassword);
            command.Parameters.AddWithValue("@NewPassword", newPassword);

            using var reader = await command.ExecuteReaderAsync();
            if (reader.RecordsAffected > 0)
                return true;
            else return false;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Lỗi", $"Không thể thay đổi mật khẩu: {ex.Message}", "OK");
            return false;
        }
    }
    
}