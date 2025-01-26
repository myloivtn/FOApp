
using Microsoft.Maui.Controls;

namespace FOApp.Views
{
    public class BindingPinView : Grid
    {
        public BindingPinView(string displayText, string iconFile)
        {
            // Thiết lập kích thước cho Grid
            WidthRequest = 37;
            HeightRequest = 48;

            // Tạo Image
            var image = new Image
            {
                Source = iconFile,
            };

            // Tạo Label để hiển thị text
            var label = new Label
            {
                Text = displayText,
                TextColor = Colors.Black,
                FontSize = 8,
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor = Colors.Transparent,
                Margin = new Thickness(0, 10, 0, 0) // Căn chỉnh Margin giống trong XAML
            };

            // Thêm Image và Label vào Grid
            this.Children.Add(image);
            this.Children.Add(label);
        }
    }
}
