using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using API.Infrastructure.Errors;
using API.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Handlers.Users
{
    public class AssignRole
    {
        public class Command : IRequest
        {
            //Properties
            public string user_id { get; set; }

            public string role_name { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ApplicationDBContext context;
            private readonly UserManager<User> userManager;
            public Handler(ApplicationDBContext context, UserManager<User> userManager)
            {
                this.userManager = userManager;
                this.context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await context.Users.FindAsync(request.user_id);

                if (user == null) throw new RestException(HttpStatusCode.NotFound, new { user = "Not found" });

                //If the user is the founder account, throw exception
                var acp_settings = (await context.acp_settings.ToListAsync())[0];

                var founder_account_id = acp_settings.founder_id;

                if(user.Id == founder_account_id) throw new RestException(HttpStatusCode.Forbidden, new {user = "Cannot demote the founder"});

                //Reset roles 
                var current_roles = await userManager.GetRolesAsync(user) as IEnumerable<string>;
                await userManager.RemoveFromRolesAsync(user, current_roles);

                var assignment = await userManager.AddToRoleAsync(user, request.role_name);

                //Handler logic
                var success = assignment.Succeeded;
                if (success) return Unit.Value;

                throw new Exception("Problem saving data");
            }

        }
    }
}