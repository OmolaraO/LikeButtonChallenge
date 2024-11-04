using LikeButton;
using LikeButton.Services.Data;
using LikeButton.Services.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LikeButton.Services
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly AppDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cacheOptions = new MemoryCacheEntryOptions()
        .SetSlidingExpiration(TimeSpan.FromMinutes(5));

        public ArticleRepository(AppDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<bool> HasUserLikedAsync(int articleId, string userId)
        {
            return await _context.Likes
                .AnyAsync(l => l.ArticleId == articleId && l.UserId == userId);
        }

        public async Task<bool> IncrementCountAsync(int articleId, string userId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var article = await _context.Articles.FindAsync(articleId);
                if (article == null)
                    return false;

                var existingLike = await _context.Likes
                    .AnyAsync(l => l.ArticleId == articleId && l.UserId == userId);
                if (existingLike)
                    return false;

                article.Likes++;
                await _context.Likes.AddAsync(new Like
                {
                    ArticleId = articleId,
                    UserId = userId,
                    DateLiked = DateTime.UtcNow
                });

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                _cache.Set($"article_likes_{articleId}", article.Likes, _cacheOptions);

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }



        public async Task<int> TotalLikesCountAsync(int articleId)
        {
            if (!_cache.TryGetValue(articleId, out int likeCount))
            {
                var article = await _context.Articles.FindAsync(articleId);
                likeCount = article?.Likes ?? 0;

                
                _cache.Set(articleId, likeCount, _cacheOptions);
            }

            return likeCount;
        }
    }
}


