using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Sitanshu.Blogs.Application.Common.Interfaces;
using Sitanshu.Blogs.Application.Common.Mappings;
using Sitanshu.Blogs.Application.Common.Models;

namespace Sitanshu.Blogs.Application.Posts.Queries.GetPostsWithPagination;

public record GetPostsWithPaginationQuery : IRequest<PaginatedList<PostBriefDto>>
{
    public int Id { get; set; }

    public int PageNumber { get; init; } = 1;

    public int PageSize { get; init; } = 10;
}

public class GetTodoItemsWithPaginationQueryHandler : IRequestHandler<GetPostsWithPaginationQuery, PaginatedList<PostBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTodoItemsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<PostBriefDto>> Handle(GetPostsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Posts
            .Where(x => x.Id == request.Id)
            .OrderBy(x => x.Title)
            .ProjectTo<PostBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
