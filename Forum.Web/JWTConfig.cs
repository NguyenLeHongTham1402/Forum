using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Web
{
    public class JWTConfig
    {
        public string key { get; set; }
        public string issuer { get; set; }
        public string audience { get; set; }
    }
}
