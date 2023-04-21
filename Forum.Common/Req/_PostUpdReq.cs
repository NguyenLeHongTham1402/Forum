using System;

namespace Forum.Common.Req
{
    public class _PostUpdReq
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime UpdatedDate { get; set; }

        public _PostUpdReq()
        {
            UpdatedDate = DateTime.Now;
        }
    }
}