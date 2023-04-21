using Forum.Common.BLL;
using Forum.Common.Req;
using Forum.Common.Rsp;
using Forum.DAL;
using Forum.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.BLL
{
    public class CategorySvc : GenericSvc<CategoryRep, Category>
    {
        private readonly CategoryRep categoryRep = new CategoryRep();
        public SingleRsp CreateCategory(_CategoryReq categoryReq)
        {
            Category category = new Category();
            category.Name = categoryReq.Name;
            category.CreatedDate = categoryReq.CreatedDate;
            category.IsActive = categoryReq.IsActive;

            return categoryRep.CreateCategory(category);
        }

        public SingleRsp UpdateCategory(_CategoryReq categoryReq)
        {
            Category category = new Category();
            category.Id = categoryReq.Id;
            category.Name = categoryReq.Name;
            category.CreatedDate = categoryReq.CreatedDate;
            category.IsActive = categoryReq.IsActive;

            return categoryRep.UpdateCategory(category); 
        }

        public SingleRsp DeleteCategory(int id)
        {
            return categoryRep.DeleteCategory(id);
        }

        public Category GetCategoryById(int id)
        {
            return categoryRep.GetCategoryById(id);
        }

        public List<Category> GetListCategories()
        {
            return categoryRep.GetListCategories();
        }
    }
}
