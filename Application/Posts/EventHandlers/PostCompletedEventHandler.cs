using MediatR;
using Microsoft.Extensions.Logging;
using Sitanshu.Blogs.Domain.Events;

namespace Sitanshu.Blogs.Application.Posts.EventHandlers;

public class PostCompletedEventHandler : INotificationHandler<PostCompletedEvent>
{
    private readonly ILogger<PostCompletedEventHandler> _logger;

    public PostCompletedEventHandler(ILogger<PostCompletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(PostCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Blogs Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
