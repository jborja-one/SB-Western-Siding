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
using reCAPTCHA.AspNetCore;

namespace BA.WesternSiding.Pages
{
    public class ContactModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly ISmtpService _smtpService;
        private IRecaptchaService _recaptcha;

        [BindProperty]
        public ContactUsModel contactUsModel { get; set; }

        [BindProperty]
        public string Message { get; set; }

        public ContactModel(IConfiguration config, ISmtpService smtpService, IRecaptchaService recaptcha)
        {
            _config = config;
            _smtpService = smtpService;
            _recaptcha = recaptcha;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                RecaptchaResponse recaptcha = await _recaptcha.Validate(Request);
                if (!recaptcha.success)
                {
                    ModelState.AddModelError("Recaptcha", "There was an error validating the Recaptcha code.  Please try Again!");
                    return Page();
                }
                else
                {
                    ContactUsAdapter _contactUs = new ContactUsAdapter(_config, _smtpService);
                    await _contactUs.CreateAndSendEmail(contactUsModel);
                    ViewData["Message"] = "Your message has been recieved.";
                    return Page();
                }
            }
            else
            {
                ViewData["Message"] = "There was a problem.  Try again!";
                return Page();
            }
        }

    }
}
