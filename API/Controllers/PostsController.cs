using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Sitanshu.Blogs.Application.Common.Models;
using Sitanshu.Blogs.Application.Posts.Commands.CreatePost;
using Sitanshu.Blogs.Application.Posts.Queries.GetPostsWithPagination;

namespace Sitanshu.Blogs.API.Controllers
{
    [EnableRateLimiting("fixed")]
    public class PostsController : ApiControllerBase
    {
        [Authorize(Policy = "Reader")]
        [HttpGet("{id}")]
        public async Task<PaginatedList<PostBriefDto>> Get(int id)
        {
            return await Mediator.Send(new GetPostsWithPaginationQuery { Id = id });
        }

        [Authorize(Policy = "Contributor")]
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreatePostCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
