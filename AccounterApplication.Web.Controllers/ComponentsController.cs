namespace AccounterApplication.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Data.Models;
    using Services.Contracts;
    using ViewModels.Components;
    using ViewModels.Currencies;
    using ViewModels.ComponentTypes;

    using AlertType = Common.Enumerations.AlertMessageTypes;
    using Resources = Common.LocalizationResources.Shared.Messages.MessagesResources;

    public class ComponentsController : BaseController
    {
        private readonly IComponentsService componentsService;
        private readonly ICurrenciesService currenciesService;
        private readonly IComponentTypesService componentTypesService;

        public ComponentsController(IComponentsService componentsService, ICurrenciesService currenciesService, IComponentTypesService componentTypesService)
        {
            this.componentsService = componentsService;
            this.currenciesService = currenciesService;
            this.componentTypesService = componentTypesService;
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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CreateComponent()
        {
            var language = this.GetCurrentLanguage();

            var viewModel = new ComponentInputModel 
            {
                CurrencyListItems = await this.currenciesService.AllLocalized<CurrencySelectListItem>(language),
                ComponentTypeListItems = await this.componentTypesService.AllLocalized<ComponentTypeSelectListItem>(language)
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateComponent(ComponentInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                var language = this.GetCurrentLanguage();

                model.CurrencyListItems = await this.currenciesService.AllLocalized<CurrencySelectListItem>(language);
                model.ComponentTypeListItems = await this.componentTypesService.AllLocalized<ComponentTypeSelectListItem>(language);

                return View(model);
            }

            try
            {
                var entityToAdd = new Component
                {
                    UserId = this.GetUserId<string>(),
                    CurrencyId = model.CurrencyId,
                    ComponentTypeId = model.ComponentTypeId,
                    Name = model.Name,
                    Amount = model.Amount,
                    IsActive = model.IsActive
                };

                await this.componentsService.AddAsync(entityToAdd);
                this.AddAlertMessageToTempData(AlertType.Success, Resources.Success, Resources.ComponentAddSuccess);
            }
            catch (Exception)
            {
                this.AddAlertMessageToTempData(AlertType.Error, Resources.Error, Resources.ComponentAddError);
            }

            return this.RedirectToAction("Index");
        }
    }
}
