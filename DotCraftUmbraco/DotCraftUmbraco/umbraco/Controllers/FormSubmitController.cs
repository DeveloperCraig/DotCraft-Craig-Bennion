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
        public IActionResult SubmitForm(string name, DateTime dob, string email)
        {
            try
            {
                string pattern = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

                if (Regex.IsMatch(email, pattern) && !name.IsNullOrEmpty())
                {
                    // Getting Connect Ready for TextFile
                    string content = $"Name: {name}\nDate of Birth: {dob:yyyy-MM-dd}\nEmail: {email}";

                    // Getting FilePath & Saving
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", $"{name}_Details.txt");
                    System.IO.File.WriteAllText(filePath, content);

                    // Set a cookie to indicate successful submission (Maybe Remove this depending if it needs to stop multiple submitions)
                    Response.Cookies.Append("FormSubmitted", "true", new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(1),
                        HttpOnly = true,
                        Secure = true
                    });

                    TempData["Success"] = "Form submitted successfully!";
                }
            }
            catch (Exception)
            {
                TempData["Error"] = "An error occurred while saving the form.";
            }

            // Redirect to the referring page
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}