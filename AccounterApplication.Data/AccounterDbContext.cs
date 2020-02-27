namespace AccounterApplication.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    using Models;

    public class AccounterDbContext : IdentityDbContext<ApplicationUser>
    {
        public AccounterDbContext(DbContextOptions<AccounterDbContext> options)
            : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Expense>()
                .HasOne(a => a.User)
                .WithMany(a => a.Expenses)
                .HasForeignKey(a => a.UserId);
            builder
                .Entity<MonthlyIncome>()
                .HasOne(a => a.User)
                .WithMany(a => a.MonthlyIncomes)
                .HasForeignKey(a => a.UserId);

            base.OnModelCreating(builder);
        }
    }
}
