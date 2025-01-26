using static CQApp.Data.FiberItemDatabase;
using static CQApp.Services.CachedService;

namespace CQApp.Views;

public partial class LogoutPage : ContentPage
{
    public LogoutPage()
    {
        Title = "Đăng xuất";

        var label = new Label
        {
            Text = "Bạn có chắc chắn muốn đăng xuất?",
            FontSize = 20,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.CenterAndExpand
        };

        var buttonLogout = new Button
        {
            Text = "Đăng xuất",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.CenterAndExpand
        };
        buttonLogout.Clicked += OnLogoutClicked;

        var buttonCancel = new Button
        {
            Text = "Hủy",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.CenterAndExpand
        };
        buttonCancel.Clicked += OnCancelClicked;

        Content = new StackLayout
        {
            Padding = new Thickness(10),
            Children = {
                    label,
                    buttonLogout,
                    buttonCancel
                }
        };
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        // Xóa thông tin người dùng khỏi Preferences
        Preferences.Remove("iduser");
        Preferences.Remove("fullname");
        Preferences.Remove("image");
        SegmentItemCache.ClearCache(); //Xóa cache
        // Chuyển hướng đến trang LoginPage
        Application.Current.MainPage = new NavigationPage(new LoginPage());
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        // Quay lại trang trước đó
        await Navigation.PopAsync();
    }
}