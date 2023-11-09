namespace Sitanshu.Blogs.Application.Comments.Queries.GetComments;

public class CommentsVm
{
    public IList<CommentDto> Lists { get; set; } = new List<CommentDto>();
}
