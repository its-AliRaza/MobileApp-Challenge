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
    public partial class AddDeveloperView : Grid
    {
        public AddDeveloperView()
        {
            InitializeComponent();
        }

        public void InitializeData()
        {
            txtName.Text = txtEmail.Text = txtAge.Text = "";
            txtName.OnAppearing();
            txtEmail.OnAppearing();
            txtAge.OnAppearing();
            btnAdd.Clicked += Add;
        }

        public void DisInitializeData()
        {
            txtName.OnDisappearing();
            txtEmail.OnDisappearing();
            txtAge.OnDisappearing();
            btnAdd.Clicked -= Add;
        }

        private async void Add(object sender, EventArgs e)
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    loader.IsVisible = true;
                    if (txtName.IsValid() && txtEmail.IsValid() && txtAge.IsValid())
                    {
                        var client = new RestClient();
                        var request = new
                        {
                            D_Name = txtName.Text,
                            D_Email = txtEmail.Text,
                            D_Age = txtAge.Text,
                            D_CreatedBy = Utils.User.U_Email,
                            D_CreatedAt = DateTime.Now.Date.ToString("yyyy-MM-dd")
                        };
                        var resContent = await client.PostAsync("Developer/AddDeveloper", request);
                        var response = JsonConvert.DeserializeObject<Response>(resContent.ToString());
                        if (response.Code == "200")
                        {
                            AppServices.LongAlert("Developer Added Successfully.");
                            await Navigation.PopAsync();
                        }
                        else
                            AppServices.LongAlert("Something went wrong.");

                    }
                    else
                    {

                        if (Device.RuntimePlatform == Device.Android)
                            AppServices.ShortAlert("Check your Internet Connection.");

                    }
                }
            }
            catch { }
            loader.IsVisible = false;
        }
    }
}