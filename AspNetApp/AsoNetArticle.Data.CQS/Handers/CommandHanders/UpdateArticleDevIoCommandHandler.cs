﻿using AsoNetArticle.Data.CQS.Commands;
using AspNetArticle.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AsoNetArticle.Data.CQS.Handers.CommandHanders
{
    public class UpdateArticleDevIoCommandHandler : IRequestHandler<UpdateArticleDevIoCommand, Unit>
    {
        private readonly AggregatorContext _context;

        public UpdateArticleDevIoCommandHandler(AggregatorContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateArticleDevIoCommand request, CancellationToken cancellationToken)
        {
            var article = await _context.Articles.FirstOrDefaultAsync(art => art.Id.Equals(request.ArticleId), cancellationToken);

            if (article != null)
            {
                article.Text = request.Text;
                await _context.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }
}
