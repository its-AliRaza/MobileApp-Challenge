using MobileAppChallenge.Helpers;
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
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            try
            {
                if (Preferences.ContainsKey("QuickAccess"))
                    switchQuickAccess.IsToggled = true;
                else
                    switchQuickAccess.IsToggled = false;
            }
            catch { }
        }
        private async void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            try
            {
                if (!switchQuickAccess.IsToggled)
                {
                    SecureStorage.RemoveAll();
                    Preferences.Clear();
                }
                else
                {
                    await SecureStorage.SetAsync("U_Email", Utils.User.U_Email);
                    await SecureStorage.SetAsync("U_Password", Utils.User.U_Password);
                    Preferences.Set("QuickAccess", true);
                }
            }
            catch { }
        }
    }
}