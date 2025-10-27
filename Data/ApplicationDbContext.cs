using Microsoft.EntityFrameworkCore;
using oneSTIOnlineTuitionPayment.Models;

namespace oneSTIOnlineTuitionPayment.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
        {
        }

        public DbSet<PaymentModel> PaymentTable { get; set; }
        public DbSet<RemBalanceModel> RemBalanceTable { get; set; }
        public DbSet<TransactionModel> TransactionTable { get; set; }
        public DbSet<PaymentSchedModel> PaymentSchedTable { get; set; }
        public DbSet<GCashModel> GCashTable { get; set; }
        public DbSet<MayaModel> MayaTable { get; set; }
    }

}


