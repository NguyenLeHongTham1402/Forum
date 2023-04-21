using System;
using System.Collections.Generic;

#nullable disable

namespace Forum.DAL.Models
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            Images = new HashSet<Image>();
            PostTags = new HashSet<PostTag>();
            Replies = new HashSet<Reply>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? CategoryId { get; set; }
        public int? View { get; set; }
        public int? Like { get; set; }
        public string Username { get; set; }
        public bool? IsActive { get; set; }

        public virtual Category Category { get; set; }
        public virtual User UsernameNavigation { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<PostTag> PostTags { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }
    }
}
