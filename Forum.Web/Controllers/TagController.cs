using Forum.BLL;
using Forum.Common.Req;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly TagSvc tagSvc = new TagSvc();

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMIN")]
        [HttpPost("create-tag")]
        public IActionResult CreateTag([FromBody] _TagReq tagReq)
        {
            return Ok(tagSvc.CreateTag(tagReq));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMIN")]
        [HttpPatch("update-tag/{id}")]
        public IActionResult UpdateTag([FromBody] _TagReq tagReq)
        {
            return Ok(tagSvc.UpdateTag(tagReq));
        }

        [HttpGet("get-tag/{id}")]
        public IActionResult GetTagbyId(int id)
        {
            return Ok(tagSvc.GetTagById(id));
        }

        [HttpGet("list-tags")]
        public IActionResult GetListTags()
        {
            return Ok(tagSvc.GetListTags());
        }

        [HttpGet("post/list-tags/{postId}")]
        public IActionResult GetListTagsByPostId(int postId)
        {
            return Ok(tagSvc.GetListTagsByPostId(postId));
        }
    }
}