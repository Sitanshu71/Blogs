namespace Sitanshu.Blogs.Domain.Entities;

public class Post : BaseAuditableEntity
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public IList<Comment> Comments { get; private set; } = new List<Comment>();
}
