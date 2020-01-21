using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BA.WesternSiding.Adapters;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BA.WesternSiding.DataModels
{
    public class ContactUsModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Referral { get; set; }
        public List<IFormFile> Attachments { get; set; }
        [Required]
        public string Comments { get; set; }
        public string Page { get; set; }
    }
}
