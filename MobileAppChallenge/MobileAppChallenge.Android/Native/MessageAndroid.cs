using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MobileAppChallenge.Droid.Native;
using Plugin.CurrentActivity;
using static MobileAppChallenge.Helpers.AppServices;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace MobileAppChallenge.Droid.Native
{
    public class MessageAndroid : IMessage
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }

        public void ExitApp()
        {
            CrossCurrentActivity.Current.Activity.Finish();
        }
    }
}