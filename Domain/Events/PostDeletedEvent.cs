namespace Sitanshu.Blogs.Domain.Events;

public class PostDeletedEvent : BaseEvent
{
    public PostDeletedEvent(Post item)
    {
        Item = item;
    }

    public Post Item { get; }
}
