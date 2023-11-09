namespace Sitanshu.Blogs.Domain.Entities;

public class Comment : BaseAuditableEntity
{
    public int PostId { get; set; }

    public string? Description { get; set; }

    public Post Post { get; set; } = null!;
}
