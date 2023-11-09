using Sitanshu.Blogs.Application.Common.Mappings;
using Sitanshu.Blogs.Domain.Entities;

namespace Sitanshu.Blogs.Application.Posts.Queries.GetPostsWithPagination
{
    public class CommentDto : IMapFrom<Comment>
    {
        public string? Description { get; set; }
    }
}
