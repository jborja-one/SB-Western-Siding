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
    public class ContactModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly ISmtpService _smtpService;

        [BindProperty]
        public ContactUsModel contactUsModel { get; set; }

        public ContactModel(IConfiguration config, ISmtpService smtpService)
        {
            _config = config;
            _smtpService = smtpService;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if(ModelState.IsValid)
            {
                ContactUsAdapter _contactUs = new ContactUsAdapter(_config, _smtpService);
                await _contactUs.CreateAndSendEmail(contactUsModel);
                ViewData["Message"] = "Your message has been recieved.";
                return Page();
            }
                else
            {
                ViewData["Message"] = "There was a problem.  Try again!";
                return Page();
            }
        }

    }
}
