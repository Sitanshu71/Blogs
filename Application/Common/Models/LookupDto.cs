using Sitanshu.Blogs.Application.Common.Mappings;
using Sitanshu.Blogs.Domain.Entities;

namespace Sitanshu.Blogs.Application.Common.Models;

// Note: This is currently just used to demonstrate applying multiple IMapFrom attributes.
public class LookupDto : IMapFrom<Domain.Entities.Post>, IMapFrom<Comment>
{
    public int Id { get; set; }

    public string? Title { get; set; }
}
