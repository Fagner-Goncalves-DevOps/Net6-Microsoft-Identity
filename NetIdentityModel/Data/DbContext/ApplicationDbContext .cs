using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetIdentityModel.Models;

namespace NetIdentityModel.Data.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>  // padrão seria dbcontxt para app sem auth
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        
    }


}
