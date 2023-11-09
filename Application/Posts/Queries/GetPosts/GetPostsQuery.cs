using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sitanshu.Blogs.Application.Common.Interfaces;

namespace Sitanshu.Blogs.Application.Posts.Queries.GetPosts
{
    public class GetPostsQuery : IRequest<List<PostBriefDto>>
    {

    }

    public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, List<PostBriefDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPostsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PostBriefDto>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Posts
                .OrderBy(x => x.Title)
                .ProjectTo<PostBriefDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
