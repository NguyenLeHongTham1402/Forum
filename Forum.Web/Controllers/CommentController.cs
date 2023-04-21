using Forum.BLL;
using Forum.Common.Req;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly CommentSvc svc = new CommentSvc();

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles ="ADMIN, USER")]
        [HttpPost("create-comment")]
        public IActionResult CreateComment([FromBody] _CommentReq req)
        {
            return Ok(svc.CreateComment(req));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMIN, USER")]
        [HttpDelete("delete-comment/{id}")]
        public IActionResult DeleteComment(int id)
        {
            return Ok(svc.DeleteComment(id));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMIN, USER")]
        [HttpPatch("like-comment/{id}")]
        public IActionResult UpdateLikeComment(int id)
        {
            svc.UpdateLikeComment(id);
            return Ok();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMIN, USER")]
        [HttpPost("create-reply")]
        public IActionResult CreateReply([FromBody] _ReplyReq req)
        {
            return Ok(svc.CreateReply(req));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMIN, USER")]
        [HttpDelete("delete-reply/{id}")]
        public IActionResult DeleteReply(int id)
        {
            return Ok(svc.DeleteReply(id));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMIN, USER")]
        [HttpPatch("like-reply/{id}")]
        public IActionResult UpdateLikeReply(int id)
        {
            svc.UpdateLikeReply(id);
            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMIN, USER")]
        [HttpGet("comments/{postId}")]
        public IActionResult GetListCommentsByPostId(int postId)
        {
            return Ok(svc.GetListCommentByPostId(postId));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMIN, USER")]
        [HttpGet("replies/{parentId}")]
        public IActionResult GetReplyByParentId(int parentId)
        {
            return Ok(svc.GetReplyByParentId(parentId));
        }
    }
}
