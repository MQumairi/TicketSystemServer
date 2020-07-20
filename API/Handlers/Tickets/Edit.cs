using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace API.Handlers.Tickets
{
    public class Edit
    {
        public class Command : IRequest
        {
            public int Id { get; set; }

            public string status { get; set; }

            public string product { get; set; }

            public string title { get; set; }

            public string description { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ApplicationDBContext context;
            public Handler(ApplicationDBContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {               
                Ticket ticket = await context.tickets.FindAsync(request.Id);
                if(ticket == null) throw new Exception("Ticket not found!");

                ticket.status = request.status ?? ticket.status;
                ticket.product = request.product ?? ticket.product;
                ticket.title = request.title ?? ticket.title;
                ticket.description = request.description ?? ticket.description;
                
                var success = await context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception("Problem saving data");
            }
        }
    }
}