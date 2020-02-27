namespace AccounterApplication.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Data.Models;
    using Web.ViewModels.Users;
    using Controllers.Infrastructure;

    public class UsersController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        public UsersController(UserManager<ApplicationUser> userManager)
            => this.userManager = userManager;

        [HttpGet]
        [Authorize]
        public IActionResult Overview(string userId)
        {
            ApplicationUser currentUser = this.userManager.FindByIdAsync(userId).Result;
            UserOverviewViewModel viewModel = new UserOverviewViewModel();

            return View(viewModel);
        }

        private string GetCurrentUserId()
        {
            string userId = User.GetLoggedInUserId<string>();

            return userId;
        }
    }
}
