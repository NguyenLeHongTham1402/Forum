using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.Common.Req
{
    public class _CommentReq
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Username { get; set; }
        public int? PostId { get; set; }
        public int? Like { get; set; }
        public bool? IsActive { get; set; }

        public _CommentReq()
        {
            this.IsActive = true;
            this.Like = 0;
        }
    }
}
