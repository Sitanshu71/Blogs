using MediatR;
using Sitanshu.Blogs.Application.Common.Exceptions;
using Sitanshu.Blogs.Application.Common.Interfaces;
using Sitanshu.Blogs.Domain.Entities;

namespace Sitanshu.Blogs.Application.Posts.Commands.UpdatePost;

public record UpdatePostCommand : IRequest
{
    public int Id { get; init; }

    public string? Title { get; init; }

    public string? Description { get; init; }
}

public class UpdateTodoItemCommandHandler : IRequestHandler<UpdatePostCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Posts
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Comment), request.Id);
        }

        entity.Title = request.Title;
        entity.Description = request.Description;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
