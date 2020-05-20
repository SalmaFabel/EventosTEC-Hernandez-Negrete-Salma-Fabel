using EventoTec.Web.Data.Helpers;
using EventoTec.Web.Models;
using EventoTec.Web.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventoTec.Web.Data
{
    public class SeedDb
    {
        private readonly DataDbContext dc;
        private readonly IUserHelper uh;
        public SeedDb(DataDbContext db, IUserHelper iuh)
        {
            dc = db;
            uh = iuh;
        }

        public async Task SeedAsync()
        {
            await dc.Database.EnsureCreatedAsync();
            await CheckRoles();
            var manager = await CheckUserAsync("Salma", "Hernandez", "salma.hernandez16@tectijuana.edu.mx", "664 428 21 69",
                 "Admin");
            var customer = await CheckUserAsync("Fabel", "Hernandez", "salmafabel@gmail.com", "664 428 21 69",
                 "Client");
            await CheckManagerAsync(manager);
            await CheckClientAsync(customer);

        }

        private async Task CheckRoles()
        {
            await uh.CheckRoleAsync("Admin");
            await uh.CheckRoleAsync("Client");
        }
        private async Task CheckClientAsync(User user)
        {
            if (!dc.Clients.Any())
            {
                dc.Clients.Add(new Client { User = user });
                await dc.SaveChangesAsync();
            }
        }

        private async Task CheckManagerAsync(User user)
        {
            if (!dc.Managers.Any())
            {
                dc.Managers.Add(new Manager { User = user });
                await dc.SaveChangesAsync();
            }
        }

        private async Task<User> CheckUserAsync(string firstName, string lastName, string email,
            string phone, string rol)
        {
            var user = await uh.GetUserByEMailAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FullName = firstName + " " + lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                };

                await uh.AddUserAssync(user, "admin1234");
                await uh.AddUserToRoleAsync(user, rol);
            }
            return user;
        }

    }

}
