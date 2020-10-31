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
    public partial class Loader : Grid
    {
        public Loader()
        {
            InitializeComponent();
        }

        public bool LoaderVisibility
        {
            get { return loader.IsVisible; }
            set { loader.IsVisible = value; }
        }
    }
}