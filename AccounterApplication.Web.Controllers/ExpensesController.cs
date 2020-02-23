namespace AccounterApplication.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;
    using System.Threading.Tasks;

    public class ExpensesController : Controller
    {
        private readonly IExpenseService expenses;

        public ExpensesController(IExpenseService expenses)
            => this.expenses = expenses;

        public async Task<IActionResult> Index()
            => this.Ok(await this.expenses.All());
    }
}
