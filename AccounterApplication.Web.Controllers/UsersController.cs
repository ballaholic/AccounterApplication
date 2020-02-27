namespace AccounterApplication.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Data.Models;
    using ViewModels.Users;
    using Controllers.Infrastructure;

    using System.Threading.Tasks;

    public class UsersController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        public UsersController(UserManager<ApplicationUser> userManager)
            => this.userManager = userManager;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> OverviewAsync()
        {
            var userId = this.User.GetLoggedInUserId<string>();
            var user = await userManager.FindByIdAsync(userId);

            var viewModel = UserOverviewViewModel.FromApplicationUser(user);

            return View(viewModel);
        }
    }
}
