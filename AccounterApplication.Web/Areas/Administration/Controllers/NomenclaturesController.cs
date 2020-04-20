namespace AccounterApplication.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using AccounterApplication.Common.GlobalConstants;

    [Area("Administration")]
    [Authorize(Roles = AdministrationConstants.AdministratorRoleName)]
    public class NomenclaturesController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult ComponentTypes()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Currencies()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult ExpenseGroups()
        {
            return this.View();
        }
    }
}