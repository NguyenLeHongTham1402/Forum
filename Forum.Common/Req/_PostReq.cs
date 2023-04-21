using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.Common.Req
{
    public class _PostReq
    {
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
        public List<_ImageReq> Images { get; set; }
        public List<_PostTagReq> PostTags { get; set; }

        public _PostReq()
        {
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
            View = 0;
            Like = 0;
            IsActive = true;
        }


    }
}
