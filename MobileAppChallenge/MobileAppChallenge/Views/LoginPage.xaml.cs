using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAppChallenge.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void RegisterHereTapped(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new SignupPage());
            }
            catch { }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushModalAsync(new NavigationPage(new ForgotPasswordPage()));
            }
            catch { }
        }
    }
}