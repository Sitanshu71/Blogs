using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sitanshu.Blogs.Application.Common.Interfaces;
using Sitanshu.Blogs.Application.Common.Security;

namespace Sitanshu.Blogs.Application.Comments.Queries.GetComments;

[Authorize]
public record GetCommentsQuery : IRequest<CommentsVm>;

public class GetTodosQueryHandler : IRequestHandler<GetCommentsQuery, CommentsVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTodosQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CommentsVm> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
    {
        return new CommentsVm
        {
            Lists = await _context.Comments
                .AsNoTracking()
                .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Title)
                .ToListAsync(cancellationToken)
        };
    }
}
