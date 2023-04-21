using System;
using System.Collections.Generic;

#nullable disable

namespace Forum.DAL.Models
{
    public partial class Image
    {
        public int Id { get; set; }
        public string UrlImg { get; set; }
        public int? PostId { get; set; }
        public bool? IsActive { get; set; }

        public virtual Post Post { get; set; }

        public Image() { }
        public Image(int id, string url, int post_id, bool isActive)
        {
            this.Id = id;
            this.UrlImg = url;
            this.PostId = post_id;
            this.IsActive = isActive;
        }
    }
}
