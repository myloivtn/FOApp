namespace FOApp.Views;

using global::FOApp.Services;
using Microsoft.Maui.Controls;

public partial class LogPage : ContentPage
    {
        private readonly Label logLabel;

        public LogPage()
        {
        
        logLabel = new Label
            {
                FontSize = 14,
                Text = "Loading logs...",
                //VerticalOptions = LayoutOptions.StartAndExpand,
                //HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(10)
            };

            // Create the back button icon
            var backButton = new Button
            {
                Text = "🔙", // Icon or text for the back button
                FontSize = 24,
                BackgroundColor = Colors.Transparent,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                Margin = new Thickness(10)
            };
            backButton.Clicked += OnBackButtonClicked;
            // Bọc Label hiển thị log trong ScrollView để hỗ trợ cuộn
            var scrollView = new ScrollView
            {
                Content = logLabel
            };
            // Tạo layout với nút quay lại và ScrollView chứa log
            var mainStack = new StackLayout
            {
                Children = { backButton, scrollView }
            };

        Content = mainStack;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            logLabel.Text = LoggingService.GetLogContents();
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(); // Go back to the previous page
        }
    }
