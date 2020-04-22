namespace AccounterApplication.Tests.Web.Controllers.Tests.UserDashboard
{ 
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Moq;
    using Xunit;
    using Microsoft.AspNetCore.Mvc;

    using Common.Enumerations;
    using Services.Contracts;
    using AccounterApplication.Web.Controllers;
    using AccounterApplication.Web.ViewModels.Expenses;
    using AccounterApplication.Web.ViewModels.UserDashboard;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;

    public class UserDashboardControllerTests
    {
        [Fact]
        public async Task Index_ReturnsAViewResult_WithAViewModel()
        {
            // Arrange
            string testUserId = string.Empty;
            Languages testLanguage = default;
            int testCount = default;
            ComponentTypes testComponentType = default;

            var mockExpenseService = new Mock<IExpenseService>();
            mockExpenseService
                .Setup(service => service.NewestByUserIdLocalized<ExpenseViewModel>(testUserId, testLanguage, testCount))
                .ReturnsAsync(this.GetNewestExpenses());

            var mockComponentsService = new Mock<IComponentsService>();
            mockComponentsService
                .Setup(service => service.AmountSumOfActiveComponentsByTypeAndUserId(testUserId, testComponentType))
                .ReturnsAsync(this.GetSumOfActiveComponents());

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "example name"),
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim("custom-claim", "example-claim-value"),
            }, "mock"));

            var controller = new UserDashboardController(mockExpenseService.Object, mockComponentsService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<UserDashboardViewModel>(viewResult.ViewData.Model);
        }

        private IEnumerable<ExpenseViewModel> GetNewestExpenses() => new List<ExpenseViewModel>();

        private decimal GetSumOfActiveComponents() => default;
    }
}
