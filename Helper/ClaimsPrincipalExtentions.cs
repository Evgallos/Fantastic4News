using System.Security.Claims;

namespace Fantastic4News.Helper
{
    public static class ClaimsPrincipalExtentions
    {
        public static string GetUserId(ClaimsPrincipal principal)
        {

            return principal.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
