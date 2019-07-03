using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcForum.Web.ViewModels
{
    public class StaffModel
    {
        public string ID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string displayName { get; set; }
        public string position { get; set; }
        public string workPhone { get; set; }
        public string workFax { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string office { get; set; }
        public string company { get; set; }
        public string department { get; set; }
        public string state { get; set; }
    }
}