namespace AccounterApplication.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Common.GlobalConstants;

    [Area("Administration")]
    [Authorize(Roles = AdministrationConstants.AdministratorRoleName)]
    public class NomenclaturesController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }
    }
}