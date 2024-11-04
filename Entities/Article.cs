using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LikeButton
{
    public class Article
    {
        public int Id { get; set; }
        [Column(TypeName = "TEXT")]
        public string Title { get; set; }
        [Column(TypeName = "TEXT")]
        public string Content { get; set; }
        [Column(TypeName = "INTEGER")]
        public int Likes { get; set; }

      
    }
}