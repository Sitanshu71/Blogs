namespace Sitanshu.Blogs.Domain.Events;

public class PostCompletedEvent : BaseEvent
{
    public PostCompletedEvent(Post item)
    {
        Item = item;
    }

    public Post Item { get; }
}
