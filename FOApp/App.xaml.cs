using FOApp.Views;
namespace FOApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //MainPage = new MainPage();
            //MainPage = new Shell();
            //MainPage = new NavigationPage(new MainPage());
            //MainPage = new NavigationPage(new MainPage());
            Routing.RegisterRoute("MainPage", typeof(MainPage));
            Routing.RegisterRoute("ChonTuyenCapPage", typeof(ChonTuyenCapPage));
            Routing.RegisterRoute("FiberListPage", typeof(FiberListPage));
            //Routing.RegisterRoute("FiberDetailPage", typeof(FiberDetailPage));
            Routing.RegisterRoute("LogPage", typeof(LogPage));
            // Routing.RegisterRoute("FiberDataPage", typeof(FiberDataPage));
            // Routing.RegisterRoute("DownloadDataPage", typeof(DownloadDataPage));
            //Routing.RegisterRoute("CustomPin", typeof(CustomPin));
          }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
