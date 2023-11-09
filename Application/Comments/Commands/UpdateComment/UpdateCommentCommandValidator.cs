using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sitanshu.Blogs.Application.Common.Interfaces;

namespace Sitanshu.Blogs.Application.Comments.Commands.UpdateComment;

public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCommentCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Description)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
            .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
    }

    public async Task<bool> BeUniqueTitle(UpdateCommentCommand model, string title, CancellationToken cancellationToken)
    {
        return await _context.Comments
            .Where(l => l.Id != model.Id)
            .AllAsync(l => l.Description != title, cancellationToken);
    }
}
