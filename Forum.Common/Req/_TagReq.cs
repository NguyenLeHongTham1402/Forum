using System;

namespace Forum.Common.Req
{
    public class _TagReq
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }

        public _TagReq()
        {
            CreatedDate = DateTime.Now;
        }
    }
}