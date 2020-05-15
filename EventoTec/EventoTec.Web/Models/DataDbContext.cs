using EventoTec.Web.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventoTec.Web.Models
{
    public class DataDbContext: IdentityDbContext<User>
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<City> cities { get; set; }
        public DbSet<Event> events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Manager> Managers { get; set; }


    }
}
