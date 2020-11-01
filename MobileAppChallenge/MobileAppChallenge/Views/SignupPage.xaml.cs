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
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                txtName.OnAppearing();
                txtEmail.OnAppearing();
                txtPassword.OnAppearing();
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

            }
            catch { }
        }

    }
}