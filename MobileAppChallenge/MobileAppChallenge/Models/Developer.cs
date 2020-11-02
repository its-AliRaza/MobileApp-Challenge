using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAppChallenge.Models
{
    public class Developer
    {
        public int D_ID { get; set; }
        public string D_Name { get; set; }
        public string D_Email{ get; set; }
        public string D_Age { get; set; }
        public string D_ComName { get; set; }
        public string D_CreatedBy { get; set; }
        public string D_CreatedAt { get; set; }
    }

    public class TotalDeveloper
    {
        public List<Developer> Reports { get; set; } = new List<Developer>();
        public int TotalPages { get; set; }
    }

    public class DeveloperRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public int Page { get; set; }
    }
}
