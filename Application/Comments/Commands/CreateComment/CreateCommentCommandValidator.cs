using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sitanshu.Blogs.Application.Common.Interfaces;

namespace Sitanshu.Blogs.Application.Comments.Commands.CreateComment;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateCommentCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.PostId)
            .NotNull().WithMessage("Post Id is required.");

        RuleFor(v => v.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(200).WithMessage("Description must not exceed 200 characters.")
            .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
    }

    public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return await _context.Comments
            .AllAsync(l => l.Description != title, cancellationToken);
    }
}
