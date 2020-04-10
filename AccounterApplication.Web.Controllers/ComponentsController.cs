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
    using Common.Enumerations;

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
                this.AddAlertMessageToTempData(AlertMessageTypes.Success, Resources.Success, Resources.ComponentAddSuccess);
            }
            catch (Exception)
            {
                this.AddAlertMessageToTempData(AlertMessageTypes.Error, Resources.Error, Resources.ComponentAddError);
            }

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SaveOrWithdraw([FromRoute(Name = "id")]string componentId)
        {
            var language = this.GetCurrentLanguage();
            var userId = this.GetUserId<string>();
            var componentTypeId = (int)ComponentTypes.PaymentComponent;

            var targetComponent = await this.componentsService.GetByIdAsync<ComponentViewModel>(userId, componentId);
            var paymentComponents = await this.componentsService.AllByUserIdAndTypeIdLocalized<ComponentsSelectListItem>(userId, componentTypeId , language);

            var viewModel = new ComponentsSaveWithdrawInputModel
            {
                TargetComponentId = targetComponent.Id,
                TargetComponentName = targetComponent.Name,
                TargetComponentAmount = targetComponent.Amount,
                TargetComponentCurrencyCode = targetComponent.CurrencyCode,
                UserPaymentComponents = paymentComponents
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SaveOrWithdraw(ComponentsSaveWithdrawInputModel model, string transactionType)
        {
            var language = this.GetCurrentLanguage();
            var userId = this.GetUserId<string>();
            var paymentComponentTypeId = (int)ComponentTypes.PaymentComponent;

            if (!ModelState.IsValid ||
                (int.TryParse(transactionType, out int transactionTypeId) && !Enum.IsDefined(typeof(TransactionTypes), transactionTypeId)))
            {
                model.UserPaymentComponents = await this.componentsService.AllByUserIdAndTypeIdLocalized<ComponentsSelectListItem>(userId, paymentComponentTypeId, language);

                var targetComponent = await this.componentsService.GetByIdAsync(userId, model.TargetComponentId);
                model.TargetComponentAmount = targetComponent.Amount;

                return View(model);
            }

            var savingsComponent = await this.componentsService.GetByIdAsync(userId, model.TargetComponentId);
            var paymentComponent = await this.componentsService.GetByIdAsync(userId, model.UserPaymentComponentId);
            bool result = false;

            switch ((TransactionTypes)transactionTypeId)
            {
                case TransactionTypes.Save:
                    result = await this.componentsService.TransactionBetweenComponents(paymentComponent, savingsComponent, model.Amount);
                    break;
                case TransactionTypes.Withdraw:
                    result = await this.componentsService.TransactionBetweenComponents(savingsComponent, paymentComponent, model.Amount);
                    break;
                default:
                    break;
            }

            if (result)
            {
                this.AddAlertMessageToTempData(AlertMessageTypes.Success, Resources.Success, Resources.TransactionResultSuccess);
            }
            else
            {
                this.AddAlertMessageToTempData(AlertMessageTypes.Error, Resources.Error, Resources.TransactionResultError);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddAmount(string id)
        {
            return View();
        }
    }
}
