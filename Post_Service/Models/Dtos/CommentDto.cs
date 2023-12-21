using System.ComponentModel.DataAnnotations;

namespace Post_Service.Models.Dtos
{
    public class CommentDto
    {
        [Key]
        public Guid CommentId { get; set; } = Guid.NewGuid();
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;


        public Guid UserId { get; set; } = Guid.NewGuid();
        public Guid PostId { get; set; } = Guid.NewGuid();
    }
}
