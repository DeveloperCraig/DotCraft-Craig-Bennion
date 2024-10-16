using DotCraftUmbraco.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace Umbraco.Controllers
{
    public class FormSubmitController : SurfaceController
    {
        public FormSubmitController(
            IUmbracoContextAccessor umbracoContextAccessor,
            IUmbracoDatabaseFactory databaseFactory,
            ServiceContext services,
            AppCaches appCaches,
            IProfilingLogger profilingLogger,
            IPublishedUrlProvider publishedUrlProvider)
            : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitForm(string name, string dob, string email)
        {
            try
            {
                List<string> errorList = new();
                // Validation
                if (!Validation.IsNameValid(name, out string nameError))
                {
                    errorList.Add(nameError);
                }

                if (!Validation.IsDobValid(dob, out string dobError, out DateTime userDate))
                {
                    errorList.Add(dobError);
                }

                if (!Validation.IsEmailValid(email, out string emailError))
                {
                    errorList.Add(emailError);
                }

                if (errorList.Count > 0)
                {
                    TempData["Error"] = string.Join(Environment.NewLine, errorList);
                    return Redirect(Request.Headers["Referer"].ToString());
                }



                // All validations passed
                string content = $"Name: {name}\nDate of Birth: {userDate:dd-MM-yyyy}\nEmail: {email}";
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "FormSubmistions", $"{name}_{DateTime.Now:dd-MM-yyyy}_Details.txt");
                System.IO.File.WriteAllText(filePath, content);

                Response.Cookies.Append("FormSubmitted", "true", new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(1),
                    HttpOnly = true,
                    Secure = true
                });

                TempData["Success"] = "Form submitted successfully!";
            }
            catch (Exception)
            {
                TempData["Error"] = "An error occurred while saving the form.";
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
