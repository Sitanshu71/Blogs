using FluentValidation;

namespace Sitanshu.Blogs.Application.Posts.Commands.UpdatePost;

public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
{
    public UpdatePostCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(v => v.Title)
            .MaximumLength(500)
            .NotEmpty();
    }
}
