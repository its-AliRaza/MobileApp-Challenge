using MobileAppChallenge.Helpers;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAppChallenge.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoreView : ContentView
    {
        public MoreView()
        {
            InitializeComponent();
        }
        public void OnAppearing()
        {
            try
            {
                btnPersonalInfo.Clicked = PersonalInfo;
                btnSettings.Clicked = Settings;
                btnLogout.Clicked = Logout;
            }
            catch { }
        }
        private async void PersonalInfo(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PersonalInfoPage());
        }
        private async void Settings(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

        private async void Logout(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushModalAsync(new NavigationPage(new StartPage()));
                Utils.Logout = true;
                Utils.User = new Models.User();
                Utils.Token = "";
                Barrel.Current.EmptyAll();
                OnDisappearing();
            }
            catch { }
        }
        public void OnDisappearing()
        {
            try
            {
                btnPersonalInfo.UnClicked = PersonalInfo;
                btnSettings.UnClicked = Settings;
                btnLogout.UnClicked = Logout;
            }
            catch { }
        }
    }
}