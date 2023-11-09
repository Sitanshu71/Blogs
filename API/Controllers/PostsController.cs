using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Sitanshu.Blogs.API.Services;
using Sitanshu.Blogs.Application.Common.Models;
using Sitanshu.Blogs.Application.Posts.Commands.CreatePost;
using Sitanshu.Blogs.Application.Posts.Queries;
using Sitanshu.Blogs.Application.Posts.Queries.GetPosts;
using Sitanshu.Blogs.Application.Posts.Queries.GetPostsWithPagination;

namespace Sitanshu.Blogs.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [EnableRateLimiting("fixed")]
    public class PostsController : ApiControllerBase
    {
        private readonly RedisCacheService _cache;
        private readonly ILogger<PostsController> _logger;

        const string CacheKeyPosts = "Posts";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="logger"></param>
        public PostsController(
            RedisCacheService cache,
            ILogger<PostsController> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        /// <summary>
        /// Get All Posts.
        /// </summary>
        /// <returns>Returns Post Collection.</returns>
        [Authorize(Policy = "Reader")]
        [HttpGet]
        [ProducesResponseType(typeof(List<PostBriefDto>), StatusCodes.Status200OK)]
        public async Task<List<PostBriefDto>> Get()
        {
            _logger.LogInformation("GET All method was hit.");

            var result = _cache.GetCachedData<List<PostBriefDto>>(CacheKeyPosts);

            if (result is null)
            {
                _logger.LogInformation("Cache empty.");
            }
            else
            {
                _logger.LogInformation("Cache was hit.");
                return result;
            }

            result = await Mediator.Send(new GetPostsQuery());

            _cache.SetCachedData(CacheKeyPosts, result, TimeSpan.FromSeconds(60));

            return result;
        }

        /// <summary>
        /// Get Post by Post Id.
        /// </summary>
        /// <param name="id">Post Id</param>
        /// <returns>Returns specific Post.</returns>
        [Authorize(Policy = "Reader")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PaginatedList<PostBriefDto>), StatusCodes.Status200OK)]
        public async Task<PaginatedList<PostBriefDto>> Get(int id)
        {
            _logger.LogInformation("GET method was hit.");

            return await Mediator.Send(new GetPostsWithPaginationQuery { Id = id });
        }

        /// <summary>
        /// Create Post.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Returns Post Id.</returns>
        [Authorize(Policy = "Contributor")]
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> Create(CreatePostCommand command)
        {
            _logger.LogInformation("POST method was hit.");

            return await Mediator.Send(command);
        }
    }
}
