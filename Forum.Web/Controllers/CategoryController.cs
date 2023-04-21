using Forum.BLL;
using Forum.Common.Req;
using Forum.DAL.Models;
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
    public class CategoryController : ControllerBase
    {
        private readonly CategorySvc categorySvc = new CategorySvc();

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMIN")]
        [HttpPost("create-category")]
        public IActionResult CreateCategory([FromBody] _CategoryReq categoryReq)
        {
            return Ok(categorySvc.CreateCategory(categoryReq));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMIN")]
        [HttpPatch("update-category/{id}")]
        public IActionResult UpdateCategory([FromBody] _CategoryReq categoryReq)
        {
            return Ok(categorySvc.UpdateCategory(categoryReq));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMIN")]
        [HttpDelete("delete-category/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            return Ok(categorySvc.DeleteCategory(id));
        }

        [HttpGet("categories/{id}")]
        public IActionResult GetCategoryById(int id)
        {
            return Ok(categorySvc.GetCategoryById(id));
        }

        [HttpGet("list-categories")]
        public IActionResult GetListCategories()
        {
            return Ok(categorySvc.GetListCategories());
        }
    }
}
