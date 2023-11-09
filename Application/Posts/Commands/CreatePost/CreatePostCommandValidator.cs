using FluentValidation;

namespace Sitanshu.Blogs.Application.Posts.Commands.CreatePost;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(v => v.Title)
            .MaximumLength(500)
            .NotEmpty();
    }
}
