using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAppChallenge.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DateEntry : Grid
    {
        public delegate void OnDateChnaged(object sender, DateChangedEventArgs e);
        public OnDateChnaged OnChanged;
        public DateEntry()
        {
            InitializeComponent();
            datePicker.DateSelected += (s, e) =>
            {
                try
                {
                    txt.Text = datePicker.Date.ToString("dd-MMM-yyy");
                    OnChanged.Invoke(s, e);
                }
                catch { }
            };

            btnDatePicker.Clicked += (s, e) =>
            {
                try
                {
                    txt.Text = datePicker.Date.ToString("dd-MMM-yyyy");
                    datePicker.Focus();
                }
                catch { }
            };
        }

        public string Text
        {
            get { return datePicker.Date.ToString("dd-MMM-yyyy"); }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    var date = DateTime.Parse(value);
                    datePicker.Date = date;
                    txt.Text = date.ToString("dd-MMM-yyyy");
                }
            }
        }

        public string Placeholder
        {
            get { return txt.Placeholder; }
            set { txt.Placeholder = value; }
        }

        public DateTime MaximumDate
        {
            get { return datePicker.MaximumDate; }
            set { datePicker.MaximumDate = value; }
        }

        public DateTime MinimumDate
        {
            get { return datePicker.MinimumDate; }
            set { datePicker.MinimumDate = value; }
        }

        public DateTime SelectedDate
        {
            get { return datePicker.Date; }
            set { datePicker.Date = value; }
        }

        public bool IsValid()
        {
            try
            {
                if (string.IsNullOrEmpty(txt.Text))
                    return false;
            }
            catch { }
            return true;
        }
    }
}