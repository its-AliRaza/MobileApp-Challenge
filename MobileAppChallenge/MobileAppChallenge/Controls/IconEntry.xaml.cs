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
    public partial class IconEntry : Grid
    {
        public IconEntry()
        {
            InitializeComponent();
        }

        public void OnAppearing()
        {
            btnVisibility.Clicked += VisiblityClicked;
            txt.TextChanged += TextChanged;
            txt.Unfocused += UnFocused;
        }

        public void OnDisappearing()
        {
            btnVisibility.Clicked -= VisiblityClicked;
            txt.TextChanged -= TextChanged;
            txt.Unfocused -= UnFocused;
        }

        public int MaxLength
        {
            get { return txt.MaxLength; }
            set { txt.MaxLength = value; }
        }
        public string Text
        {
            get { return txt.Text; }
            set { txt.Text = value; }
        }

        public string ErrorText
        {
            get { return txtError.Text; }
            set { txtError.Text = value; }
        }

        public bool ErrorVisibility
        {
            get { return txtError.IsVisible; }
            set { txtError.IsVisible = value; }
        }
        public string Placeholder
        {
            get { return txt.Placeholder; }
            set { txt.Placeholder = value; }
        }

        public ImageSource Image
        {
            get { return img.Source; }
            set { img.Source = value; }
        }

        public Keyboard Keyboard
        {
            get { return txt.Keyboard; }
            set { txt.Keyboard = value; }
        }

        public bool IsPasswordEntry
        {
            set { if (value) { btnVisibility.IsVisible = lblVisibility.IsVisible = true; } }
        }

        public bool IsPassword
        {
            get { return txt.IsPassword; }
            set { txt.IsPassword = value; }
        }
        private void VisiblityClicked(object sender, EventArgs e)
        {
            try
            {
                if (lblVisibility.Text == "Show")
                {
                    lblVisibility.Text = "Hide";
                    txt.IsPassword = false;
                }
                else
                {
                    lblVisibility.Text = "Show";
                    txt.IsPassword = true;
                }
            }
            catch { }
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
                else if (new List<string> { "Password", "Current Password", "New Password", "Confirm Password" }.Contains(txt.Placeholder) && txt.Text.Length < 8)
                {
                    txtError.Text = "Password must contain 8 characters, combination of letter, numbers with no spaces.";
                    txtError.IsVisible = true;
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

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtError.IsVisible)
                IsValid();
        }

        private void UnFocused(object sender, FocusEventArgs e)
        {
            IsValid();
        }
    }
}