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
    public partial class DashboardView : Grid
    {
        public DashboardView()
        {
            InitializeComponent();
            fromDate.MaximumDate = toDate.MaximumDate = DateTime.Now;
            fromDate.MinimumDate = toDate.SelectedDate.AddMonths(-3);
            fromDate.OnChanged += (s, e) =>
            {
                try
                {
                    lblFrom.Text = fromDate.SelectedDate.ToString("dd-MMM-yyy");
                    toDate.MinimumDate = fromDate.SelectedDate;
                }
                catch { }
            };

            toDate.OnChanged += (s, e) =>
            {
                try
                {
                    lblTo.Text = toDate.SelectedDate.ToString("dd-MMM-yyy");
                    fromDate.MaximumDate = toDate.SelectedDate;
                    fromDate.MinimumDate = toDate.SelectedDate.AddMonths(-3);
                }
                catch { }
            };
            list.ItemSelected += (s, e) =>
            {
                try
                {
                    if (list.SelectedItem != null && ((ListView)s).SelectedItem != null)
                    {

                    }
                }
                catch { }
            };

        }

        public void InitializeData()
        {
            Utils.InitializeHistory = true;
            fromDate.Text = DateTime.Now.AddDays(-6).ToString("dd-MMM-yyy");
            toDate.Text = DateTime.Now.ToString("dd-MMM-yyy");
        }

        public void DisInitializeData()
        {
            Utils.InitializeHistory = false;
            fromDate.Text = DateTime.Now.ToString("dd-MMM-yyy");
            toDate.Text = DateTime.Now.ToString("dd-MMM-yyy");
        }
    }
}