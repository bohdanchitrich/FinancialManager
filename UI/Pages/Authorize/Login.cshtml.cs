using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UI.Pages.Authorize
{
    public class LoginModel : PageModel
    {


        public async Task OnGetAsync(Uri redirectUri)
        {
            ArgumentNullException.ThrowIfNull(redirectUri);
            await HttpContext.ChallengeAsync("oidc",
                new AuthenticationProperties { RedirectUri = redirectUri.ToString() });
        }
    }
}
