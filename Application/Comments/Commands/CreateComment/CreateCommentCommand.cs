using MediatR;
using Sitanshu.Blogs.Application.Common.Interfaces;
using Sitanshu.Blogs.Domain.Entities;

namespace Sitanshu.Blogs.Application.Comments.Commands.CreateComment;

public record CreateCommentCommand : IRequest<int>
{
    public int PostId { get; init; }

    public string? Description { get; init; }
}

public class CreateTodoListCommandHandler : IRequestHandler<CreateCommentCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var entity = new Comment();

        entity.PostId = request.PostId;
        entity.Description = request.Description;

        _context.Comments.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
