using MediatR;
using Sitanshu.Blogs.Application.Common.Interfaces;
using Sitanshu.Blogs.Domain.Entities;
using Sitanshu.Blogs.Domain.Events;

namespace Sitanshu.Blogs.Application.Posts.Commands.CreatePost;

public record CreatePostCommand : IRequest<int>
{
    public string? Title { get; init; }

    public string? Description { get; init; }
}

public class CreateTodoItemCommandHandler : IRequestHandler<CreatePostCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var entity = new Post
        {
            Title = request.Title,
            Description = request.Description
        };

        entity.AddDomainEvent(new PostCreatedEvent(entity));

        _context.Posts.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
