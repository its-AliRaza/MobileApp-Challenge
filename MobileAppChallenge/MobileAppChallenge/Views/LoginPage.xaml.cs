using MobileAppChallenge.Client;
using MobileAppChallenge.Helpers;
using MobileAppChallenge.Models;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAppChallenge.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        bool isChecked = false;

        Dictionary<string, string> Checked = new Dictionary<string, string>
        {
            {"#D5DBDB","#27AE60" }
        };

        Dictionary<string, string> UnChecked = new Dictionary<string, string>
        {
            {"#27AE60","#D5DBDB" }
        };

        public LoginPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            bool isFingerprintAvailable = await CrossFingerprint.Current.IsAvailableAsync();
            if (isFingerprintAvailable)
            {
                var authType = await CrossFingerprint.Current.GetAuthenticationTypeAsync();
                if (authType == AuthenticationType.Fingerprint)
                {
                    if (Preferences.ContainsKey("QuickAccess"))
                    {
                        authSwitch.IsToggled = true;
                        imgID.Source = "touchID.svg";
                        imgID.ReplaceStringMap = Checked;
                        txtID.Text = "Touch ID Enabeld";
                    }
                    else
                    {
                        authSwitch.IsToggled = false;
                        imgID.Source = "touchID.svg";
                        txtID.Text = "Enable Touch ID";
                    }
                }
                else if (authType == AuthenticationType.Face)
                {
                    if (Preferences.ContainsKey("QuickAccess"))
                    {
                        authSwitch.IsToggled = true;
                        imgID.Source = "faceID.svg";
                        imgID.ReplaceStringMap = UnChecked;
                        txtID.Text = "Face ID Enabeld";
                    }
                    else
                    {
                        authSwitch.IsToggled = false;
                        imgID.Source = "faceID.svg";
                        txtID.Text = "Enable Face ID";
                    }
                }
                else
                {
                    imgID.IsVisible = false;
                    txtID.IsVisible = false;
                    authSwitch.IsVisible = false;
                }
                if (Preferences.ContainsKey("QuickAccess"))
                {
                    var request = new AuthenticationRequestConfiguration("Authentication", "Authenticate to Login.");
                    var result = await CrossFingerprint.Current.AuthenticateAsync(request);
                    if (result.Authenticated)
                    {
                        txtEmail.Text = await SecureStorage.GetAsync("U_Email");
                        txtPassword.Text = await SecureStorage.GetAsync("U_Password");
                        Login(true);
                    }
                }
            }
            else
            {
                imgID.IsVisible = false;
                txtID.IsVisible = false;
                authSwitch.IsVisible = false;
            }
            txtEmail.OnAppearing();
            txtPassword.OnAppearing();
            btnLogin.Clicked += Login;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            txtEmail.OnDisappearing();
            txtPassword.OnDisappearing();
            btnLogin.Clicked -= Login;
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

        private async void TouchIDTapped(object sender, EventArgs e)
        {
            try
            {
                if (Preferences.ContainsKey("QuickAccess"))
                {
                    var request = new AuthenticationRequestConfiguration("Authentication", "Authenticate to Login.");
                    var result = await CrossFingerprint.Current.AuthenticateAsync(request);
                    if (result.Authenticated)
                    {
                        txtEmail.Text = await SecureStorage.GetAsync("U_Email");
                        txtPassword.Text = await SecureStorage.GetAsync("U_Password");
                        Login(true);
                    }
                }
            }
            catch { }
        }

        private void Login(object sender, EventArgs e)
        {
            Login();
        }

        private async void Login(bool Auth = false)
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    loader.IsVisible = true;
                    if (txtEmail.IsValid() && txtPassword.IsValid())
                    {
                        var request = new Login();
                        var client = new RestClient();
                        if (!Auth)
                        {
                            request.U_Email = txtEmail.Text;
                            request.U_Password = txtPassword.Text;
                        }
                        else
                        {
                            request.U_Email = await SecureStorage.GetAsync("U_Email");
                            request.U_Password = await SecureStorage.GetAsync("U_Password");
                        }
                        var resContent = await client.PostAsync("Account/Login", request);
                        var response = JsonConvert.DeserializeObject<Response>(resContent.ToString());
                        if (response.Code == "200")
                        {
                            Utils.User = JsonConvert.DeserializeObject<User>(response.Data.ToString());
                            Utils.User.U_Email = txtEmail.Text;
                            Utils.User.U_Password = request.U_Password;
                            Utils.Token = Utils.User.Token;
                            Barrel.Current.EmptyAll();
                            Barrel.Current.Add<User>("U_Data", Utils.User, new TimeSpan(0, 2, 0, 0));
                            if (isChecked)
                            {
                                await SecureStorage.SetAsync("U_Email", txtEmail.Text);
                                await SecureStorage.SetAsync("U_Password", request.U_Password);
                                Preferences.Set("QuickAccess", true);
                            }
                            MessagingCenter.Send(this, "LoginSuccess");
                            await Navigation.PopModalAsync();
                        }
                        else if (response.Code == "400")
                            await DisplayAlert("Attention", response.Message, "Ok");
                        else
                            await DisplayAlert("Attention", "Something went wrong, Please try again later.", "Ok");
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

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            try
            {
                if (!isChecked)
                {
                    imgID.ReplaceStringMap = Checked;
                    isChecked = true;
                }
                else
                {
                    imgID.ReplaceStringMap = UnChecked;
                    isChecked = false;
                    SecureStorage.RemoveAll();
                    Preferences.Clear();
                }
            }
            catch { }
        }
    }
}