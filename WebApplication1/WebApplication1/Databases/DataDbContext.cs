using Microsoft.EntityFrameworkCore; //Import EF
using WebApplication1.Models;  //Import Models

namespace WebApplication1.Databases
{
    public class DataDbContext:DbContext
    {
        // Constructure Method
        public DataDbContext(DbContextOptions<DataDbContext> options):base(options){ }
        //Table manufacturers
        //Table devices
        public DbSet<positions> positions { get; set; }

        public DbSet<employees>  employees { get; set; }
    }
}
