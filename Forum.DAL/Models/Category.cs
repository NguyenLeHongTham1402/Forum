using System;
using System.Collections.Generic;

#nullable disable

namespace Forum.DAL.Models
{
    public partial class Category
    {
        public Category()
        {
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
