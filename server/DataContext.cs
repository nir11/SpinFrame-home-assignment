using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
    }
}
