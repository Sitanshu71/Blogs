using MediatR;
using Sitanshu.Blogs.Application.Common.Interfaces;
using Sitanshu.Blogs.Application.Common.Security;

namespace Sitanshu.Blogs.Application.Comments.Commands.PurgeComments;

[Authorize(Roles = "Administrator")]
[Authorize(Policy = "CanPurge")]
public record PurgeCommentsCommand : IRequest;

public class PurgeTodoListsCommandHandler : IRequestHandler<PurgeCommentsCommand>
{
    private readonly IApplicationDbContext _context;

    public PurgeTodoListsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(PurgeCommentsCommand request, CancellationToken cancellationToken)
    {
        _context.Comments.RemoveRange(_context.Comments);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
