using MediatR;
using Sitanshu.Blogs.Application.Common.Exceptions;
using Sitanshu.Blogs.Application.Common.Interfaces;
using Sitanshu.Blogs.Domain.Entities;

namespace Sitanshu.Blogs.Application.Comments.Commands.UpdateComment;

public record UpdateCommentCommand : IRequest
{
    public int Id { get; init; }

    public string? Description { get; init; }
}

public class UpdateTodoListCommandHandler : IRequestHandler<UpdateCommentCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Comments
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Post), request.Id);
        }

        entity.Description = request.Description;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
