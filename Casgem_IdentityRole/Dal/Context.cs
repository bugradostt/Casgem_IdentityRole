using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Casgem_IdentityRole.Dal
{
    public class Context:IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=BUDOTEKNO\SQLEXPRESS; initial Catalog=CasgemIdentityRoleDb; integrated Security=true");
        }

        public DbSet<Product> Products { get; set; }
    }
}
