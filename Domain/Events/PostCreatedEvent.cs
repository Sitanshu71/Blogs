namespace Sitanshu.Blogs.Domain.Events;

public class PostCreatedEvent : BaseEvent
{
    public PostCreatedEvent(Post item)
    {
        Item = item;
    }

    public Post Item { get; }
}
