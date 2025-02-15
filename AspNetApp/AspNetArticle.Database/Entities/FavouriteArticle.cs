﻿namespace AspNetArticle.Database.Entities
{
    public class FavouriteArticle : IBaseEntity
    {
        public Guid Id { get; set; }

        public Guid ArticleId { get; set; }
        public Article Article { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }  
    }
}
