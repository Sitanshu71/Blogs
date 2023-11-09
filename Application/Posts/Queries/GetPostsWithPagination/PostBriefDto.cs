using Sitanshu.Blogs.Application.Common.Mappings;
using Sitanshu.Blogs.Domain.Entities;

namespace Sitanshu.Blogs.Application.Posts.Queries.GetPostsWithPagination;

public class PostBriefDto : IMapFrom<Post>
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public IList<CommentDto> Comments { get; private set; } = new List<CommentDto>();
}
