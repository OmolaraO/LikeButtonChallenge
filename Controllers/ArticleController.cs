using LikeButton.Services.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LikeButton.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleController(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }


        [HttpGet("{articleId}/likes")]
        public async Task<IActionResult> GetLikeCount(int articleId)
        {
            var likeCount = await _articleRepository.TotalLikesCountAsync(articleId);
            return Ok(new { likeCount });
        }


        [HttpPost("{articleId}/likes")]
        public async Task<IActionResult> LikeArticle(int  articleId, [FromQuery] string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest("userId is required");
             
           var result =  await _articleRepository.IncrementCountAsync(articleId, userId);

            if (!result)
                return BadRequest("User has already liked this article");

            return Ok(new {Message = "Article liked successfully"});
             
        }

       
    }
    
    
}

