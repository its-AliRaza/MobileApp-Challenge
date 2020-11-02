using MobileAppChallenge.Helpers;
using MobileAppChallenge.Models;
using MobileAppChallenge.Views;
using MonkeyCache.FileStore;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("Roboto-Bold.ttf", Alias = "RobotoBold")]
[assembly: ExportFont("Roboto-Light.ttf", Alias = "RobotoLight")]
[assembly: ExportFont("Roboto-Medium.ttf", Alias = "RobotoMedium")]
[assembly: ExportFont("Roboto-Regular.ttf", Alias = "RobotoRegular")]
namespace MobileAppChallenge
{
    public partial class App : Xamarin.Forms.Application
    {
        public App()
        {
            InitializeComponent();
            Barrel.ApplicationId = "com.mobileappchallenge.android";
            App.Current.On<Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            if (Barrel.Current.Exists("U_Data") && !Barrel.Current.IsExpired("U_Data"))
                Utils.User = Barrel.Current.Get<User>("U_Data");
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
