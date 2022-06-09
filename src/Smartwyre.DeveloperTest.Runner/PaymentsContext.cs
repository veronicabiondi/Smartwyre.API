using Microsoft.EntityFrameworkCore;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Runner
{
    public class PaymentsContext : DbContext
    {
        private const string connectionString = "Server=localhost\\SQLEXPRESS;Initial Catalog=Smartwyre;Trusted_Connection=True;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Accounts
            modelBuilder.Entity<Account>().HasData(new Account() 
            {
                Id = 1,
                AccountNumber = "123456",
                AllowedPaymentSchemes = AllowedPaymentSchemes.ExpeditedPayments,
                Balance = 10000,
                Status = AccountStatus.Live
            });
        }
    }
}
