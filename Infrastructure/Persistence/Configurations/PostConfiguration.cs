using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sitanshu.Blogs.Domain.Entities;

namespace Sitanshu.Blogs.Infrastructure.Persistence.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(t => t.Title)
            .HasMaxLength(200)
            .IsRequired();

            builder.Property(t => t.Description)
            .HasMaxLength(500)
            .IsRequired();
        }
    }
}
