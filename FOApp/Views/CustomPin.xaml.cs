using CQApp.ViewModel;
using System.ComponentModel;
using CQApp.Data;
using CQApp.Models;
using static CQApp.Data.FiberItemDatabase;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;
namespace CQApp.Views;

public partial class CustomPin : ContentPage
{
    Map map;
    MapViewModel viewModel;

    readonly FiberItemDatabase database;
    public CustomPin()
	{
		InitializeComponent();
        BindingContext = new CustomPinViewModel();
       //SaveCableType();
    }
    
    private async void SaveCableType()
    {
        var cableTypes = new List<CableType>
        {
            new() { Name = "Chôn_sang_treo", Emoji = "🔀" },
            new() { Name = "Chôn", Emoji = "⚕️" },
            new() { Name = "Treo_sang_chôn", Emoji = "⬇" },
            new() { Name = "Khác", Emoji = "➗" },
            new() { Name = "Đi_nổi", Emoji = "⤴️" },
            new() { Name = "Qua_cống", Emoji = "↪️" },
            new() { Name = "Treo", Emoji = "🔝" },
            new() { Name = "Măng_xông", Emoji = "🔗" },
            new() { Name = "Chuyển_hướng", Emoji = "✖️" },
            new() { Name = "Cống_bể", Emoji = "➰" },
            new() { Name = "Dự_trữ_cáp", Emoji = "➿" },
            new() { Name = "Qua_cầu", Emoji = "↩️" }
        };

        foreach (var cableType in cableTypes)
        {
            await database.SaveCableTypeAsync(cableType);
        }

        var allCableTypes = await database.GetCableTypesAsync();
        foreach (var cable in allCableTypes)
        {
            Console.WriteLine($"{cable.Name} - {cable.Emoji}");
        }


    }
}