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
            btnLogin.Clicked += async (s, e) =>
            {
                try
                {
                    await Navigation.PopModalAsync();
                }
                catch { }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            txtEmail.OnAppearing();
            txtPassword.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            txtEmail.OnDisappearing();
            txtPassword.OnDisappearing();
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