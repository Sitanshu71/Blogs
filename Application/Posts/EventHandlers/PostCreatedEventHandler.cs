using MediatR;
using Microsoft.Extensions.Logging;
using Sitanshu.Blogs.Domain.Events;

namespace Sitanshu.Blogs.Application.Posts.EventHandlers;

public class PostCreatedEventHandler : INotificationHandler<PostCreatedEvent>
{
    private readonly ILogger<PostCreatedEventHandler> _logger;

    public PostCreatedEventHandler(ILogger<PostCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(PostCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Blogs Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
