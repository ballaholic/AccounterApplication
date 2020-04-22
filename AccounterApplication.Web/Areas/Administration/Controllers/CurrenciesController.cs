namespace AccounterApplication.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Authorization;

    using Data;
    using Common.GlobalConstants;
    using AccounterApplication.Data.Models;

    [Area("Administration")]
    [Authorize(Roles = AdministrationConstants.AdministratorRoleName)]
    public class CurrenciesController : BaseController
    {
        private readonly AccounterDbContext context;

        public CurrenciesController(AccounterDbContext context) => this.context = context;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await this.context.Currencies.ToListAsync();
            return this.View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Sign, Code, NameEN, NameBG, IsMain")]Currency currency)
        {
            if (ModelState.IsValid)
            {
                this.context.Add(currency);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Index");
            }

            return this.View(currency);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var currency = await this.context.Currencies.FindAsync(id);

            if (currency == null)
            {
                return this.NotFound();
            }

            return this.View(currency);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Sign, Code, NameEN, NameBG, IsMain, Id")]Currency currency)
        {
            if (id != currency.Id)
            {
                return this.NotFound();
            }

            if (ModelState.IsValid)
            {
                this.context.Update(currency);
                await this.context.SaveChangesAsync();

                return this.RedirectToAction("Index");
            }

            return this.View(currency);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var currency = await this.context.Currencies.FindAsync(id);

            if (currency == null)
            {
                return this.NotFound();
            }

            return this.View(currency);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var currency = await this.context.Currencies.FindAsync(id);
            this.context.Currencies.Remove(currency);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }
    }
}
