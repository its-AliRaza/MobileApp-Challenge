using MobileAppChallenge.Helpers;
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
    public partial class PersonalInfoPage : ContentPage
    {
        public PersonalInfoPage()
        {
            InitializeComponent();
            txtName.Text = Utils.User.U_Name;
            txtEmail.Text = Utils.User.U_Email;
        }
    }
}