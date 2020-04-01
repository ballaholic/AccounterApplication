namespace AccounterApplication.Web.Helpers
{
    using System.Security.Claims;

    public static class ClaimsPrincipalHelper
    {
        public static string GetShortName(this ClaimsPrincipal principal)
            => principal.Identity.Name.Split("@")[0];
    }
}
