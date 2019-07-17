using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BA.WesternSiding.DataModels;
using BA.WesternSiding.Adapters;
using BA.WesternSiding.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using BA.Common.Services;

namespace BA.WesternSiding.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly ISmtpService _smtpService;

        [BindProperty]
        public ContactUsModel contactUsModel { get; set; }

        public IndexModel(IConfiguration config, ISmtpService smtpService)
        {
            _config = config;
            _smtpService = smtpService;
        }

        public void OnGet() { }

        public void OnPost()
        {
            ContactUsAdapter _contactUs = new ContactUsAdapter(_config, _smtpService);
            _contactUs.CreateAndSendEmail(contactUsModel);
        }

    }
}
