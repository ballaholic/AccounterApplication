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
    public class ComponentTypesController : BaseController
    {
        private readonly AccounterDbContext context;

        public ComponentTypesController(AccounterDbContext context) => this.context = context;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await this.context.ComponentTypes.ToListAsync();
            return this.View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NameEN, NameBG, IsMain")]ComponentType componentType)
        {
            if (ModelState.IsValid)
            {
                this.context.Add(componentType);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Index");
            }

            return this.View(componentType);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var componentType = await this.context.ComponentTypes.FindAsync(id);

            if (componentType == null)
            {
                return this.NotFound();
            }

            return this.View(componentType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NameEN, NameBG, IsMain, Id")]ComponentType componentType)
        {
            if (id != componentType.Id)
            {
                return this.NotFound();
            }

            if (ModelState.IsValid)
            {
                this.context.Update(componentType);
                await this.context.SaveChangesAsync();

                return this.RedirectToAction("Index");
            }

            return this.View(componentType);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var componentType = await this.context.ComponentTypes.FindAsync(id);

            if (componentType == null)
            {
                return this.NotFound();
            }

            return this.View(componentType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var componentType = await this.context.ComponentTypes.FindAsync(id);
            this.context.ComponentTypes.Remove(componentType);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }
    }
}
