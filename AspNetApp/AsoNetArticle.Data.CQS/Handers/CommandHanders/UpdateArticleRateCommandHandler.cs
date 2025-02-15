﻿using AsoNetArticle.Data.CQS.Commands;
using AspNetArticle.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AsoNetArticle.Data.CQS.Handers.CommandHanders
{
    public class UpdateArticleRateCommandHandler : IRequestHandler<UpdateArticleRateCommand, Unit>
    {
        private readonly AggregatorContext _context;

        public UpdateArticleRateCommandHandler(AggregatorContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateArticleRateCommand request, CancellationToken cancellationToken)
        {
            var article = await _context.Articles.FirstOrDefaultAsync(a => a.Id.Equals(request.ArticleId));

            if (article != null)
            {
                article.Rate = request.Rate;
                await _context.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }
}
