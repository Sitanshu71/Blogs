using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sitanshu.Blogs.Domain.Entities;

namespace Sitanshu.Blogs.Infrastructure.Persistence.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(t => t.Description)
            .HasMaxLength(200)
            .IsRequired();
        }
    }
}
