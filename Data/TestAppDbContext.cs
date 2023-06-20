using Microsoft.EntityFrameworkCore;
using TestApp.Data.Entities;

namespace TestApp.Data
{
    public class TestAppDbContext : DbContext
    {
        public TestAppDbContext(DbContextOptions<TestAppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<WeatherData> WeatherDatas { get; set; }
    }
}
