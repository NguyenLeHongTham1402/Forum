using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.Common.Req
{
    public class _CategoryReq
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsActive { get; set; }

        public _CategoryReq()
        {
            CreatedDate = DateTime.Now;
            IsActive = true;
        }
    }
}
