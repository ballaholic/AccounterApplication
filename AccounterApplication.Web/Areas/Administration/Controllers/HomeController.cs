namespace AccounterApplication.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using AccounterApplication.Common.GlobalConstants;

    [Area("Administration")]
    [Authorize(Roles = AdministrationConstants.AdministratorRoleName)]
    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["routeInfo"] = "Hello";
            return this.View();
        }
    }
}