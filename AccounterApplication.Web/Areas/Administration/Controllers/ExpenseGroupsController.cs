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
    public class ExpenseGroupsController : BaseController
    {
        private readonly AccounterDbContext context;

        public ExpenseGroupsController(AccounterDbContext context) => this.context = context;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await this.context.ExpenseGroups.ToListAsync();
            return this.View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("NameEN, NameBG, IsMain")]ExpenseGroup expenseGroup)
        {
            if (ModelState.IsValid)
            {
                this.context.Add(expenseGroup);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Index");
            }

            return this.View(expenseGroup);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var expenseGroup = await this.context.ExpenseGroups.FindAsync(id);

            if (expenseGroup == null)
            {
                return this.NotFound();
            }

            return this.View(expenseGroup);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("NameEN, NameBG, IsMain, Id")]ExpenseGroup expenseGroup)
        {
            if (id != expenseGroup.Id)
            {
                return this.NotFound();
            }

            if (ModelState.IsValid)
            {
                this.context.Update(expenseGroup);
                await this.context.SaveChangesAsync();

                return this.RedirectToAction("Index");
            }

            return this.View(expenseGroup);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var expenseGroup = await this.context.ExpenseGroups.FindAsync(id);

            if (expenseGroup == null)
            {
                return this.NotFound();
            }

            return this.View(expenseGroup);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var expenseGroup = await this.context.ExpenseGroups.FindAsync(id);
            this.context.ExpenseGroups.Remove(expenseGroup);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }
    }
}
