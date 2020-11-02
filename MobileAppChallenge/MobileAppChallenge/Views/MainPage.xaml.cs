using MobileAppChallenge.Helpers;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAppChallenge.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private bool BackPressed = false;
        Dictionary<string, string> Checked = new Dictionary<string, string>
        {
            {"#D5DBDB","#08415C" }
        };
        Dictionary<string, string> UnChecked = new Dictionary<string, string>
        {
            {"#08415C","#D5DBDB" }
        };
        public MainPage()
        {
            InitializeComponent();
            if (Barrel.Current.IsExpired("U_Data"))
                Navigation.PushModalAsync(new NavigationPage(new StartPage()));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                btnDashboard.Clicked += DashboardClicked;
                btnDeveloper.Clicked += DeveloperClicked;
                btnMore.Clicked += MoreClicked;
                if (!dashboardView.IsVisible && Utils.Logout == true && Utils.MoreView == true)
                    DashboardClicked(null, null);
            }
            catch { }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            btnDashboard.Clicked -= DashboardClicked;
            btnDeveloper.Clicked -= DeveloperClicked;
            btnMore.Clicked -= MoreClicked;
        }

        private void DashboardClicked(object sender, EventArgs e)
        {
            try
            {
                txtTitle.Text = "Dashboard";
                dashboardView.InitializeData();
                imgDashboard.ReplaceStringMap = Checked;
                imgMore.ReplaceStringMap = UnChecked;
                txtDashboard.TextColor = Color.FromHex("#08415C");
                txtMore.TextColor = Color.FromHex("#D5DBDB");
                dashboardView.IsVisible = true;
                developerView.IsVisible = false;
                moreView.IsVisible = false;
                Utils.MoreView = false;
                moreView.OnDisappearing();
            }
            catch (Exception ex) { }
        }

        private async void DeveloperClicked(object sender, EventArgs e)
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    txtTitle.Text = "Add Developer";
                    developerView.IsVisible = true;
                    moreView.IsVisible = false;
                    dashboardView.IsVisible = false;
                    Utils.MoreView = false;
                    imgDashboard.ReplaceStringMap = UnChecked;
                    imgMore.ReplaceStringMap = UnChecked;
                    txtDashboard.TextColor = Color.FromHex("#D5DBDB");
                    txtMore.TextColor = Color.FromHex("#D5DBDB");
                    moreView.OnDisappearing();
                }
                else
                {

                    if (Device.RuntimePlatform == Device.Android)
                        AppServices.ShortAlert("Check your Internet Connection.");
                    else
                        await DisplayAlert("No Internet Connection!", "Please Check your Internet Connection.", "Ok");
                }
            }
            catch { };
        }

        private async void MoreClicked(object sender, EventArgs e)
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    Utils.Logout = false;
                    txtTitle.Text = "More";
                    moreView.IsVisible = true;
                    Utils.MoreView = true;
                    dashboardView.IsVisible = false;
                    developerView.IsVisible = false;
                    imgMore.ReplaceStringMap = Checked;
                    dashboardView.DisInitializeData();
                    imgDashboard.ReplaceStringMap = UnChecked;
                    txtMore.TextColor = Color.FromHex("#08415C");
                    txtDashboard.TextColor = Color.FromHex("#D5DBDB");
                    moreView.OnAppearing();
                }
                else
                {

                    if (Device.RuntimePlatform == Device.Android)
                        AppServices.ShortAlert("Check your Internet Connection.");
                    else
                        await DisplayAlert("No Internet Connection!", "Please Check your Internet Connection.", "Ok");
                }
            }
            catch { }
        }

        protected override bool OnBackButtonPressed()
        {
            try
            {
                if (moreView.IsVisible)
                {
                    DashboardClicked(null, null);
                }

                if (BackPressed)
                    return false;
                if (Device.RuntimePlatform == Device.Android)
                    AppServices.ShortAlert("Press Again to Exit App");
                BackPressed = true;
                DoubleTap();
            }
            catch { }
            return true;
        }
        private async void DoubleTap()
        {
            await Task.Delay(2000);
            BackPressed = false;
        }
    }
}