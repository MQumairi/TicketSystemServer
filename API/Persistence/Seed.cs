using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Identity;

namespace API.Persistence
{
    public class Seed
    {
        public static async Task seedTickets(ApplicationDBContext context, UserManager<User> userManager)
        {
            if (!context.tickets.Any())
            {

                List<Ticket> ticketsToAdd = new List<Ticket>()
                {
            new Ticket {
                authorId = 1,
                status = "Urgent",
                product = "Product 1",
                title = "Tries to update after every crash",
                date = "2020-01-04",
                description = "The app installation crashes after updating to version 10.15.5.",
            },

            new Ticket {
                authorId = 2,
                status = "Low",
                product = "Product 1",
                title = "Not connecting to database",
                date = "2020-01-04",
                description = "Displays a 502 error w",
            },

            new Ticket {
                authorId = 2,
                status = "Low",
                product = "Product 2",
                title = "Crashes after update",
                date = "2020-01-04",
                description = "The app installation crashes after updating to version 10.15.5.",
            },

            new Ticket {
                authorId = 1,
                status = "Done",
                product = "Product 4",
                title = "Crashes after update",
                date = "2020-01-04",
                description = "The app installation crashes after updating to version 10.15.5.",
            }
                };

                context.tickets.AddRange(ticketsToAdd);
                context.SaveChanges();
            }


            if (!userManager.Users.Any())
            {
                List<User> usersToAdd = new List<User>()
                {
                    new User{
                        UserName = "Bob",
                        Email = "Bob@email.com"
                    },
                    new User{
                        UserName = "Billy",
                        Email = "Billy@email.com"
                    }
                };

                foreach (var user in usersToAdd)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }
        }
    }
}