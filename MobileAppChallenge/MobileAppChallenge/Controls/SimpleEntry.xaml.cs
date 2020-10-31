using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAppChallenge.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SimpleEntry : StackLayout
    {
        public SimpleEntry()
        {
            InitializeComponent();
        }

        public string Text
        {
            get { return txt.Text; }
            set { txt.Text = value; }
        }

        public string Placeholde
        {
            get { return txt.Placeholder; }
            set { txt.Placeholder = value; }
        }

        public bool IsDisabled
        {
            set { txt.TextColor = Color.FromHex("#85929E"); txt.IsReadOnly = true; }
        }
        public void OnAppearing()
        {
            txt.TextChanged += TxtTextChanged;
            txt.Unfocused += TxtUnFocused;
        }
        public void OnDisappearing()
        {
            txt.TextChanged -= TxtTextChanged;
            txt.Unfocused -= TxtUnFocused;
        }
        private void TxtTextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtError.IsVisible)
                IsValid();
        }
        private void TxtUnFocused(object sender, FocusEventArgs e)
        {
            IsValid();
        }
        public bool IsValid()
        {
            try
            {
                if (string.IsNullOrEmpty(txt.Text))
                {
                    txtError.Text = "This feild is required";
                    txtError.IsVisible = true;
                    return false;
                }
                else if (txt.Placeholder == "Email" && !Regex.IsMatch(txt.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                {
                    txtError.IsVisible = true;
                    txtError.Text = "Please enter valid Email Address";
                    return false;
                }
                else
                {
                    txtError.Text = "";
                    txtError.IsVisible = false;
                }
            }
            catch { }
            return true;
        }
    }
}