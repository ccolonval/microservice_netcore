using Microsoft.EntityFrameworkCore;

namespace myData.Context
{
    public class PersonDbContext : DbContext
    {
        public DbSet<DTO.Person> Persons { get; set; }

        public PersonDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}