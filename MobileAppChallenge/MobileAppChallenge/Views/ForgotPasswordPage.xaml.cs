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
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();
        }

        private async void Next(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new ResetPassword());

            }
            catch { }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            txtEmail.OnAppearing();
            btnNext.Clicked += Next;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            txtEmail.OnDisappearing();
            btnNext.Clicked -= Next;
        }
    }
}