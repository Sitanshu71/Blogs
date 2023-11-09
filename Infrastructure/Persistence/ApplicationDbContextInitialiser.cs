using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sitanshu.Blogs.Domain.Entities;

namespace Sitanshu.Blogs.Infrastructure.Persistence
{
    public class ApplicationDbContextInitialiser
    {
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly ApplicationDbContext _context;

        public ApplicationDbContextInitialiser(
            ILogger<ApplicationDbContextInitialiser> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.EnsureCreatedAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            if (!_context.Posts.Any())
            {
                _context.Posts.Add(new Post
                {
                    Title = "Test",
                    Description = "Test",
                    Comments =
                    {
                        new Comment { Description = "Test Comment" }
                    }
                });

                await _context.SaveChangesAsync();
            }
        }
    }
}
