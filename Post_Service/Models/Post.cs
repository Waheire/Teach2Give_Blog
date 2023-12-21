using Post_Service.Models.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Post_Service.Models
{
    public class Post
    {
        [Key]
        public Guid PostId { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public Guid UserId { get; set; } = Guid.NewGuid();
        public Guid CommentId { get; set; } = Guid.NewGuid();
        //public List<CommentDto> ? Comments { get; set; } = new List<CommentDto>();
    }
}
