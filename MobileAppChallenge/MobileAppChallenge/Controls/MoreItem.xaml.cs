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
    public partial class MoreItem : Grid
    {
        public MoreItem()
        {
            InitializeComponent();
        }
        public string Text
        {
            get { return txt.Text; }
            set { txt.Text = value; }
        }

        public bool ArrowVisibility
        {
            get { return imgArrow.IsVisible = true; }
            set { imgArrow.IsVisible = value; }
        }

        public EventHandler Clicked
        {
            set { btnView.Clicked += value; }
        }
        public EventHandler UnClicked
        {
            set { btnView.Clicked -= value; }
        }
    }
}