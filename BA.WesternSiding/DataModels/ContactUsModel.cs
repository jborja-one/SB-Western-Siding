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
        public string zSHYrBwhEeJi { get; set; }
        [Required]
        public string hcDphOHFf0gM { get; set; }
        [Required]
        public string jYyWawnghJI4 { get; set; }
        public string kqL8KlYyI2wJ { get; set; }
        public List<IFormFile> Attachments { get; set; }
        [Required]
        public string uRa2Dx9xEXmy { get; set; }
        public string Page { get; set; }
    }
}
