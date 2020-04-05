namespace AccounterApplication.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Services.Contracts;
    using ViewModels.Components;

    public class ComponentsController : BaseController
    {
        private readonly IComponentsService componentsService;

        public ComponentsController(IComponentsService componentsService)
        {
            this.componentsService = componentsService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var language = this.GetCurrentLanguage();
            var userId = this.GetUserId<string>();

            var components = await this.componentsService.AllByUserIdLocalized<ComponentViewModel>(userId, language);

            var viewModel = new ComponentsListingViewModel { Components = components };

            return View(viewModel);
        }
    }
}
