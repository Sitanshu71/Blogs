using MediatR;
using Sitanshu.Blogs.Application.Common.Exceptions;
using Sitanshu.Blogs.Application.Common.Interfaces;
using Sitanshu.Blogs.Domain.Entities;
using Sitanshu.Blogs.Domain.Events;

namespace Sitanshu.Blogs.Application.Posts.Commands.DeletePost;

public record DeletePostCommand(int Id) : IRequest;

public class DeleteTodoItemCommandHandler : IRequestHandler<DeletePostCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Posts
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Comment), request.Id);
        }

        _context.Posts.Remove(entity);

        entity.AddDomainEvent(new PostDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
