using System;
using System.Collections.Generic;

#nullable disable

namespace Forum.DAL.Models
{
    public partial class Tag
    {
        public Tag()
        {
            PostTags = new HashSet<PostTag>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}
