using Microsoft.EntityFrameworkCore;

namespace VeterinaryClinic.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public const string ConnectionStringKey = "VeterinaryClinic";

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
