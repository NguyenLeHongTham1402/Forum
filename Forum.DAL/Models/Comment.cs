using System;
using System.Collections.Generic;

#nullable disable

namespace Forum.DAL.Models
{
    public partial class Comment
    {
        public Comment()
        {
            Replies = new HashSet<Reply>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Username { get; set; }
        public int? PostId { get; set; }
        public int? Like { get; set; }
        public bool? IsActive { get; set; }

        public virtual Post Post { get; set; }
        public virtual User UsernameNavigation { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }
    }
}
