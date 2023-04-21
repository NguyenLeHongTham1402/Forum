using System;
using System.Collections.Generic;

#nullable disable

namespace Forum.DAL.Models
{
    public partial class Reply
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Username { get; set; }
        public int? ParentId { get; set; }
        public int? Like { get; set; }
        public int? PostId { get; set; }

        public virtual Comment Parent { get; set; }
        public virtual Post Post { get; set; }
        public virtual User UsernameNavigation { get; set; }
    }
}
