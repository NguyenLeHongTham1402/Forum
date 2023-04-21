using System;
using System.Collections.Generic;

#nullable disable

namespace Forum.DAL.Models
{
    public partial class PostTag
    {
        public int PostId { get; set; }
        public int TagId { get; set; }

        public virtual Post Post { get; set; }
        public virtual Tag Tag { get; set; }

        public PostTag() { }

        public PostTag(int post_id, int tag_id)
        {
            PostId = post_id;
            TagId = tag_id;
        }
    }
}
