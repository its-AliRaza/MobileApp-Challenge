using MobileAppChallenge.Helpers;
using MobileAppChallenge.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileAppChallenge.Client
{
    public class RestClient
    {
        string baseUrl = "";
        public RestClient()
        {
            if (Utils.User != null)
                Utils.Token = Utils.User.Token;
        }
        public async Task<string> GetAsync(string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                if (!string.IsNullOrEmpty(Utils.Token))
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Utils.Token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                var response = await client.GetAsync(baseUrl + url);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    if (Application.Current.MainPage.Navigation.ModalStack.Count == 0)
                    {
                        await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new StartPage()));
                    }
                }
                var resContent = await response.Content.ReadAsStringAsync();
                return resContent;
            }
            catch { }
            return "";
        }

        public async Task<string> PostAsync(string subUrl, object request)
        {
            try
            {
                string url = baseUrl + subUrl;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                if (!string.IsNullOrEmpty(Utils.Token))
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Utils.Token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                var json = JsonConvert.SerializeObject(request);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, stringContent);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    if (Application.Current.MainPage.Navigation.ModalStack.Count == 0)
                    {
                        Utils.Logout = true;
                        Utils.User = new Models.User();
                        Utils.Token = "";
                        await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new StartPage()));
                    }
                }
                var resContent = response.Content.ReadAsStringAsync().Result;
                return resContent;
            }
            catch { }
            return "";
        }
    }
}
