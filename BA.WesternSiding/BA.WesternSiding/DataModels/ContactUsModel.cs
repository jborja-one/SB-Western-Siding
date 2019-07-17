using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BA.WesternSiding.Adapters;

namespace BA.WesternSiding.DataModels
{
    public class ContactUsModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Referral { get; set; }
        public string Comments { get; set; }
        public string Employment { get; set; }
    }
}
