using MediatR;
using Microsoft.EntityFrameworkCore;
using Sitanshu.Blogs.Application.Common.Exceptions;
using Sitanshu.Blogs.Application.Common.Interfaces;
using Sitanshu.Blogs.Domain.Entities;

namespace Sitanshu.Blogs.Application.Comments.Commands.DeleteComment;

public record DeleteCommentCommand(int Id) : IRequest;

public class DeleteTodoListCommandHandler : IRequestHandler<DeleteCommentCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTodoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Comments
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Post), request.Id);
        }

        _context.Comments.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
