using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileAppChallenge.Helpers
{
    public static class AppServices
    {
        public static void ShortAlert(string message)
        {
            DependencyService.Get<IMessage>().ShortAlert(message);
        }
        public static void LongAlert(string message)
        {
            DependencyService.Get<IMessage>().LongAlert(message);
        }
        public interface IMessage
        {
            void LongAlert(string message);
            void ShortAlert(string message);
            void ExitApp();
        }

        public static void ExitApp()
        {
            DependencyService.Get<IMessage>().ExitApp();
        }
    }
}