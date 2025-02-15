﻿using AspNetArticle.Database.Entities;

namespace AspNetArticle.Data.Abstractions.Repositories
{
    public interface IExtendedArticleRepository : IRepository<Article>
    {
        Task UpdateArticleTextAsync(Guid id, string text);
        Task UpdateArticleImageUrlAsync(Guid id, string imageUrl);
        Task UpdateArticleRateAsync(Guid id, double rate);
    }
}
