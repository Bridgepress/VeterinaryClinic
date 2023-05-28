using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.DataAccess;
using VeterinaryClinic.Domain.Entities;

namespace VeterinaryСlinic.Tests.Integrations
{
    public class DatabaseFixture : IDisposable
    {
        public static ApplicationDbContext Context => new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite("DataSource=file::memory:?cache=shared").Options);

        public DatabaseFixture()
        {
            using (var context = Context)
            {
                context.Database.EnsureCreated();
                context.AddRange(
                new Dog
                {
                    Id = Guid.NewGuid(),
                    Name = "Нео",
                    Color = "red & amber",
                    TailLength = 22,
                    Weight = 32

                },
                new Dog
                {
                    Id = Guid.NewGuid(),
                    Name = "Jessy",
                    Color = "black & white",
                    TailLength = 7,
                    Weight = 14
                }
                );

                context.SaveChanges();
            }
        }

        public void Dispose()
        {
            using var context = Context;
            context.Database.EnsureDeleted();
        }
    }
}
