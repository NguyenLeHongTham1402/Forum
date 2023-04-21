using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.Common.Req
{
    public class _ImageReq
    {
        public int Id { get; set; }
        public string UrlImg { get; set; }
        public IFormFile file { get; set; }
        public int? PostId { get; set; }
        public bool? IsActive { get; set; }

        public _ImageReq()
        {
            IsActive = true;
        }
    }
}
