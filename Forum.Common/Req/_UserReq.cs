using Microsoft.AspNetCore.Http;

namespace Forum.Common.Req
{
    public class _UserReq
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public IFormFile Image { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string RealName { get; set; }
        public bool? IsActive { get; set; }
        public string Token { get; set; }
        public _UserReq()
        {
            Role = "USER";
            IsActive = true;
        }
    }
}
