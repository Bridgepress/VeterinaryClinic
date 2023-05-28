using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Domain.Entities;

namespace VeterinaryClinic.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public const string ConnectionStringKey = "VCSystem";

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Dog> Dogs { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
