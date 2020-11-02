using MobileAppChallenge.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAppChallenge.Helpers
{
    public static class Utils
    {

        public static bool Logout { get; set; } = false;
        public static Developer Report { get; set; } = new Developer();
        public static bool MoreView { get; set; } = false;
        public static bool InitializeHistory { get; set; } = true;
        public static User User { get; set; } = new User();
        public static string Token { get; set; }
    }
}
