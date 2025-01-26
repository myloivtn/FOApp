using FOApp.Helpers;
using FOApp.Models;
namespace FOApp.ViewModel;
public class CustomPinViewModel : ObservableObject
{
   
        public CustomPinViewModel()
        {
            Emoji = new Emoji(0x1F914).ToString();
        }        
        private string _emoji = string.Empty; 
        public string Emoji
        {
            get { return _emoji; }
            set { SetProperty(ref _emoji, value); }
        }
}

