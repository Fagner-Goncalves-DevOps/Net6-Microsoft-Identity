﻿using Microsoft.AspNetCore.Identity;
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
            // Customize the ASP.NET Core Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Core Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            foreach (var foreignKey in builder.Model.GetEntityTypes().
                               SelectMany(e => e.GetForeignKeys())) 
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
        //essa classe pode ser extendia para um melhor funcionamento
        // para uma melhor organização do databae
    }
}
