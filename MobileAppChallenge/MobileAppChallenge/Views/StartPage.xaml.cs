using MobileAppChallenge.Helpers;
using MobileAppChallenge.Models;
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
    public partial class StartPage : ContentPage
    {
        private bool BackPressed = false;
        public StartPage()
        {
            InitializeComponent();
            try
            {
                var list = new List<Feature>
            {
                new Feature{Description="Add Developers", Image="Developer.svg" },
                new Feature{Description="Developers List", Image="List.svg" },
                new Feature{Description="Login Authentication", Image="Auth.svg" }
            };
                featuresList.ItemsSource = list;
                btnLogin.Clicked += async (s, e) =>
                {
                    try
                    {
                        await Navigation.PushAsync(new LoginPage());
                    }
                    catch { }
                };

                btnRegister.Clicked += async (s, e) =>
                {
                    try
                    {
                        await Navigation.PushAsync(new SignupPage());
                    }
                    catch { }
                };
            }
            catch { }
        }

        protected override bool OnBackButtonPressed()
        {
            try
            {
                if (BackPressed)
                    AppServices.ExitApp();

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