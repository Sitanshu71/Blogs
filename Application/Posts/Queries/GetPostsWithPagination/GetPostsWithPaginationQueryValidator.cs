﻿using FluentValidation;

namespace Sitanshu.Blogs.Application.Posts.Queries.GetPostsWithPagination;

public class GetPostsWithPaginationQueryValidator : AbstractValidator<GetPostsWithPaginationQuery>
{
    public GetPostsWithPaginationQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
