using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using API.Infrastructure.Errors;
using API.Models;
using MediatR;

namespace API.Handlers.Comments
{
    public class Edit
    {
        public class Command : IRequest
        {
            public int post_id { get; set; }
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
                //Find the comment
                Comment comment = await context.comments.FindAsync(request.post_id);

                if(comment == null)
                    throw new RestException (HttpStatusCode.NotFound, new {comment = "Not found"});

                //Edit it
                comment.description = request.description ?? comment.description;
                
                //Save
                var success = await context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception("Problem saving data");
            }

        }
    }
}