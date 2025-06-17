using GestorTF.Data.Mapping;
using GestorTF.Models;
using GestoTF2.Data.Mapping;
using GestoTF2.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;

namespace GestoTF2.Data
{
    public class ContextApp : DbContext
    {
        public ContextApp(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Chamado> Chamados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
           new Role { Id = 1, Name = "Admin", Description = "Acesso total ao sistema" },
           new Role { Id = 2, Name = "User", Description = "Acesso restrito ao sistema" }
       );
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new ChamadoMap());
        }
    }
}