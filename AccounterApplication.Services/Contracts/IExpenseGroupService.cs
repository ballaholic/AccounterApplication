namespace AccounterApplication.Services.Contracts
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IExpenseGroupService
    {
        Task<IEnumerable<T>> All<T>();
    }
}
