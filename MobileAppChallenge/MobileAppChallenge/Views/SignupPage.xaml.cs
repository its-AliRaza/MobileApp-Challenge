using MobileAppChallenge.Client;
using MobileAppChallenge.Helpers;
using MobileAppChallenge.Models;
using Newtonsoft.Json;
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
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
        }
        public async void Verify(object sender, EventArgs e)
        {
            try
            {
                loader.IsVisible = true;
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    loader.IsVisible = true;
                    if (txtName.IsValid() && txtEmail.IsValid() && txtPassword.IsValid())
                    {
                        var client = new RestClient();
                        var request = new User()
                        {
                            U_Name = txtName.Text,
                            U_Email = txtEmail.Text,
                            U_Password = txtPassword.Text,
                        };
                        var resContent = await client.PostAsync("Account/Signup", request);
                        var response = JsonConvert.DeserializeObject<Response>(resContent.ToString());
                        if (response.Code == "200")
                        {
                            await DisplayAlert("Success", "Account Register Successfully", "Ok");
                            SecureStorage.RemoveAll();
                            Preferences.Clear();

                            await Navigation.PushAsync(new LoginPage());
                            Navigation.RemovePage(this);
                        }
                        else if (response.Code == "1")
                        {
                            txtEmail.ErrorText = response.Message;
                            txtEmail.ErrorVisibility = true;
                        }
                        else if (response.Code == "3")
                            await DisplayAlert("Attention", response.Message, "Ok");
                        else
                            await DisplayAlert("Attention", response.Message, "Ok");
                    }
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
            loader.IsVisible = false;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                txtName.OnAppearing();
                txtEmail.OnAppearing();
                txtPassword.OnAppearing();
                btnRegister.Clicked += Verify;
            }
            catch { }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            try
            {
                txtName.OnDisappearing();
                txtEmail.OnDisappearing();
                txtPassword.OnDisappearing();
                btnRegister.Clicked -= Verify;

            }
            catch { }
        }

    }
}