using CQApp.Models;

namespace CQApp.Views;

public partial class FiberPage : ContentPage
{
    public FiberPage(FiberItem fiberItem)
    {
        Title = "Chi ti?t tuy?n cáp";

        var stackLayout = new StackLayout
        {
            Padding = new Thickness(10)
        };

        stackLayout.Children.Add(CreateLabel("?o?n", fiberItem.Doan));
        stackLayout.Children.Add(CreateLabel("?i?m ??c bi?t", fiberItem.Diemdacbiet));
        stackLayout.Children.Add(CreateLabel("Ph??ng th?c l?p ??t", fiberItem.Phuongthuclapdat));
        stackLayout.Children.Add(CreateLabel("H??ng tuy?n", fiberItem.Huongtuyen));
        stackLayout.Children.Add(CreateLabel("V? trí tuy?n", fiberItem.Vitrituyen));
        stackLayout.Children.Add(CreateLabel("Chi?u dài tuy?n", fiberItem.Chieudaituyen.ToString()));
        stackLayout.Children.Add(CreateLabel("Chi?u dài", fiberItem.Chieudai));
        stackLayout.Children.Add(CreateLabel("?? sâu", fiberItem.Dosau));
        stackLayout.Children.Add(CreateLabel("S? tr? ?i?n l?c", fiberItem.Sotrudienluc));
        stackLayout.Children.Add(CreateLabel("?? cao TDL", fiberItem.DocaoTDL));
        stackLayout.Children.Add(CreateLabel("Tên ???ng", fiberItem.Tenduong));
        stackLayout.Children.Add(CreateLabel("Tên c?u c?ng", fiberItem.Tencaucong));
        stackLayout.Children.Add(CreateLabel("Ghi chú", fiberItem.Ghichu));

        Content = new ScrollView
        {
            Content = stackLayout
        };
    }

    private View CreateLabel(string labelText, string value)
    {
        return new StackLayout
        {
            Orientation = StackOrientation.Horizontal,
            Children =
                {
                    new Label { Text = $"{labelText}: ", FontAttributes = FontAttributes.Bold },
                    new Label { Text = value }
                }
        };
    }
}