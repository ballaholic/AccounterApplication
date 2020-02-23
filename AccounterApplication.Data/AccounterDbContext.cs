namespace AccounterApplication.Web.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class AccounterDbContext : IdentityDbContext
    {
        public AccounterDbContext(DbContextOptions<AccounterDbContext> options)
            : base(options)
        {
        }
    }
}
