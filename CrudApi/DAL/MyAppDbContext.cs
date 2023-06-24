using CrudApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrudApi.DAL
{
    public class MyAppDbContext : DbContext
    {
        public MyAppDbContext(DbContextOptions<MyAppDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
