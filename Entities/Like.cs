using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace LikeButton
{

    public class Like
    {
        public int Id { get; set; }
        [Column(TypeName = "TEXT")]
        public string UserId { get; set; }
        [Column(TypeName = "INTEGER")]
        public int ArticleId { get; set; }
        [Column(TypeName = "TEXT")]
        public DateTime DateLiked { get; set; }
        public Article Article { get; set; }
    }
}
