namespace AccounterApplication.Data
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    using Models;
    using Common.Models;

    public class AccounterDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
            typeof(AccounterDbContext).GetMethod(
                nameof(SetIsDeletedQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static);

        public AccounterDbContext(DbContextOptions<AccounterDbContext> options)
            : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<ExpenseGroup> ExpenseGroups { get; set; }

        public DbSet<MonthlyIncome> MonthlyIncomes { get; set; }

        public DbSet<ComponentType> ComponentTypes { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Component> Components { get; set; }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Needed for Identity models configuration
            base.OnModelCreating(builder);

            ConfigureUserIdentityRelations(builder);

            EntityIndexesConfiguration.Configure(builder);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            // Set global query filter for not deleted entities only
            var deletableEntityTypes = entityTypes
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));

            foreach (var deletableEntityType in deletableEntityTypes)
            {
                var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));

            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder
                .Entity<Expense>()
                .HasOne(a => a.User)
                .WithMany(a => a.Expenses)
                .HasForeignKey(a => a.UserId);
            builder
                .Entity<Expense>()
                .HasOne(a => a.ExpenseGroup)
                .WithMany()
                .HasForeignKey(a => a.ExpenseGroupId);
            builder
                .Entity<MonthlyIncome>()
                .HasOne(a => a.User)
                .WithMany(a => a.MonthlyIncomes)
                .HasForeignKey(a => a.UserId);
            builder
                .Entity<MonthlyIncome>()
                .HasOne(a => a.Component)
                .WithMany(a => a.Incomes)
                .HasForeignKey(a => a.ComponentId);
            builder
                .Entity<Component>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();
            builder
                .Entity<Component>()
                .HasOne(c => c.ComponentType)
                .WithMany()
                .HasForeignKey(c => c.ComponentTypeId);
            builder
                .Entity<Component>()
                .HasOne(c => c.Currency)
                .WithMany()
                .HasForeignKey(c => c.CurrencyId);
            builder
                .Entity<Component>()
                .HasOne(c => c.User)
                .WithMany(a => a.Components)
                .HasForeignKey(e => e.UserId);
        }

        private static void ConfigureUserIdentityRelations(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
            where T : class, IDeletableEntity
            => builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
