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
    public class PostController : ControllerBase
    {
        private readonly PostSvc postSvc = new PostSvc();
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMIN, USER")]
        [HttpPost("create-post")]
        public IActionResult CreatePost([FromBody] _PostReq postReq)
        {
            return Ok(postSvc.CreatePost(postReq));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMIN")]
        [HttpDelete("delete-post/{id}")]
        public IActionResult DeletePost(int id)
        {
            return Ok(postSvc.DeletePost(id));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMIN, USER")]
        [HttpPatch("update-post/{id}")]
        public IActionResult UpdatePost([FromBody] _PostUpdReq post, int id)
        {
            return Ok(postSvc.UpdatePost(post, id));
        }

        [HttpGet("detail-post/{id}")]
        public IActionResult GetPostById(int id)
        {
            return Ok(postSvc.GetPostById(id));
        }

        [HttpGet("list-posts")]
        public IActionResult GetListPosts([FromQuery] string kw)
        {
            var res = postSvc.GetListPosts();
            if (kw != null)
            {
                res = postSvc.GetPostByKeyword(kw);
            }
            return Ok(res);
        }

        [HttpGet("posts-cate/{categoryId}")]
        public IActionResult GetListByCategoryId(int categoryId)
        {
            return Ok(postSvc.GetListPostsByCategoryId(categoryId));
        }

        [HttpPatch("update-like/{id}")]
        public IActionResult UpdateLikePost(int id)
        {
            postSvc.UpdateLikePost(id);
            return Ok();
        }

        [HttpPatch("update-view/{id}")]
        public IActionResult UpdateViewPost(int id)
        {
            postSvc.UpdateViewPost(id);
            return Ok();
        }

        [HttpGet("posts-tag/{tagId}")]
        public IActionResult GetListPostsByTagId(int tagId)
        {
            return Ok(postSvc.GetListPostsByTagId(tagId));
        }

        [HttpGet("category/{postId}")]
        public IActionResult GetCategoryByPostId(int postId)
        {
            return Ok(postSvc.GetCategoryByPostId(postId));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMIN, USER")]
        [HttpGet("posts-user/{username}")]
        public IActionResult GetPostByUsername(string username)
        {
            return Ok(postSvc.GetPostByUsername(username));
        }
    }
}
