using Microsoft.EntityFrameworkCore;
using Sitanshu.Blogs.Domain.Entities;

namespace Sitanshu.Blogs.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Post> Posts { get; }

    DbSet<Comment> Comments { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
