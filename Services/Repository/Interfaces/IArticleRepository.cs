using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LikeButton.Services.Repository.Interfaces
{
    public interface IArticleRepository
    {
        Task<int> TotalLikesCountAsync(int articleId);
        Task <bool>IncrementCountAsync(int articleId, string userId);
        Task<bool> HasUserLikedAsync(int articleId, string userId);
    }
}
