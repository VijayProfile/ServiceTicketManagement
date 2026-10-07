using Microsoft.EntityFrameworkCore;
using ServiceTicketManagement.API.Model;

namespace ServiceTicketManagement.API.Data
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext>options):base(options)
        {
            
        }

        public DbSet<Users> Users { get; set; }
    }
}
