using MobileAppChallenge.Client;
using MobileAppChallenge.Helpers;
using MobileAppChallenge.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Extended;

namespace MobileAppChallenge.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private bool _isBusy;
        private bool _hasHistory;
        private bool _notFound;
        private int Page = 0;
        private string _fromDate;
        private string _toDate;
        private int TotalPages = 0;
        public InfiniteScrollCollection<Developer> Items { get; }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public bool HasHistory
        {
            get => _hasHistory;
            set
            {
                _hasHistory = value;
                OnPropertyChanged();
            }
        }

        public bool NotFound
        {
            get => _notFound;
            set
            {
                _notFound = value;
                OnPropertyChanged();
            }
        }

        public string From
        {
            set
            {
                if (_fromDate != value && Utils.InitializeHistory)
                {
                    _fromDate = value;
                    DownloadDataAsync();
                }
                else if (_fromDate != value && !Utils.InitializeHistory)
                    _fromDate = value;
            }
            get
            {
                return _fromDate;
            }
        }

        public string To
        {
            set
            {
                if (_toDate != value)
                {
                    _toDate = value;
                    DownloadDataAsync();
                }
            }
            get
            {
                return _toDate;
            }
        }
        public DashboardViewModel()
        {
            _fromDate = DateTime.Now.ToString("dd-MMM-yyy");
            _toDate = DateTime.Now.ToString("dd-MMM-yyy");
            Items = new InfiniteScrollCollection<Developer>
            {
                OnLoadMore = async () =>
                {
                    var items = new List<Developer>();
                    try
                    {
                        IsBusy = true;
                        items = await GetData(Page);

                        IsBusy = false;
                    }
                    catch { IsBusy = false; }
                    // return the items that need to be added
                    return items;
                },
                OnCanLoadMore = () =>
                {
                    return TotalPages >= Page;

                }
            };
        }

        private async Task DownloadDataAsync()
        {
            try
            {
                IsBusy = true;
                HasHistory = true;
                NotFound = false;
                Items.Clear();
                Page = 1;
                if (Utils.User != null)
                    Items.AddRange(await GetData(Page));
                IsBusy = false;
                GC.Collect();
            }
            catch { IsBusy = false; }
        }

        private async Task<List<Developer>> GetData(int page)
        {
            var items = new List<Developer>();
            try
            {
                var client = new RestClient();
                var request = new DeveloperRequest()
                {
                    From = _fromDate,
                    To = _toDate,
                    Page = page
                };
                var resContent = await client.PostAsync("Developer/GetDevelopers", request);
                var response = JsonConvert.DeserializeObject<Response>(resContent.ToString());
                if (response.Code == "200")
                {
                    Page++;
                    var data = JsonConvert.DeserializeObject<TotalDeveloper>(response.Data.ToString());
                    if (data.Reports.Count == 0)
                    {
                        HasHistory = false;
                        NotFound = true;
                    }
                    else
                    {
                        HasHistory = true;
                        NotFound = false;
                    }

                    TotalPages = data.TotalPages;
                    return data.Reports;
                }
            }
            catch { IsBusy = false; }
            return items;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
