using Microsoft.AspNetCore.Localization;

namespace ARC.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/Settings")]
    public class SettingsController : ControllerBase
    {
        [HttpPost("set-language")]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return LocalRedirect(returnUrl);
        }
    }
}
