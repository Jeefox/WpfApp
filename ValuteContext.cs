using WpfApp2.Models;
using System.Data.Entity;

namespace WpfApp2
{

    public class ValuteContext : DbContext
    {
        public ValuteContext() : base("DefaultConnection") { }
        public DbSet<Valute> Valutes { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
