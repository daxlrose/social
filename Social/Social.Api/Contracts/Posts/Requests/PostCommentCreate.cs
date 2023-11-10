using System.ComponentModel.DataAnnotations;

namespace Social.Api.Contracts.Posts.Requests
{
    public class PostCommentCreate
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public string UserProfileId { get; set; }
    }
}
