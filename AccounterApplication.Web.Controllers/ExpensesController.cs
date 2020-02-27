namespace AccounterApplication.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;
    using System.Threading.Tasks;

    public class ExpensesController : BaseController
    {
        private readonly IExpenseService expenses;

        public ExpensesController(IExpenseService expenses)
            => this.expenses = expenses;

        public IActionResult Index()
        {
            return View();
        }


    }
}
