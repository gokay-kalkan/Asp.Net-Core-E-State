using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data
{
    public class DataContext:IdentityDbContext<UserAdmin>
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("server=DESKTOP-U1FS87R\\SQLEXPRESS; database=EState; integrated security=true;").UseLazyLoadingProxies();
        }

        public DbSet<Advert> Adverts { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Neighbourhood> Neighbourhoods { get; set; }
        public DbSet<Situation> Situations { get; set; }
        public DbSet<EntityLayer.Entities.Type> Types { get; set; }
        public DbSet<UserAdmin> UserAdmins { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<GeneralSettings> GeneralSettings { get; set; }

      
    }
}
