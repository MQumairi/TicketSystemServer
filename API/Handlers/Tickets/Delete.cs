using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace API.Handlers.Tickets
{
    public class Delete
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
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
                if(ticket == null) throw new Exception("This ticket doesn't even exist!");

                context.tickets.Remove(ticket);

                var success = await context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception("Problem saving data");
            }
        }
    }
}