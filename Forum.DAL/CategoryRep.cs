using Forum.Common.DAL;
using Forum.Common.Rsp;
using Forum.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forum.DAL
{
    public class CategoryRep : GenericRep<ForumContext, Category>
    {
        private ForumContext context;

        #region CRUD
        public SingleRsp CreateCategory(Category category)
        {
            var res = new SingleRsp();
            if (GetCategoryByName(category.Name) == null)
            {
                using (context = new ForumContext())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.Categories.Add(category);
                            int i = context.SaveChanges();
                            if (i > 0)
                            {
                                trans.Commit();
                                res.SetMessage("Create Category Success.");
                            }
                            else
                            {
                                trans.Rollback();
                                res.SetError("Create Category Failure.");
                            }
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            res.SetError("System error. Please come back later.");
                        }
                    }
                }
            }
            else
            {
                res.SetError("Category already exists.");
            }
            return res;
        }

        public SingleRsp UpdateCategory(Category category)
        {
            var res = new SingleRsp();
            if (GetCategoryByName(category.Name) == null)
            {
                using (context = new ForumContext())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.Categories.Update(category);
                            int i = context.SaveChanges();
                            if (i > 0)
                            {
                                trans.Commit();
                                res.SetMessage("Update Category Success.");
                            }
                            else
                            {
                                trans.Rollback();
                                res.SetError("Update Category Failure.");
                            }
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            res.SetError("System error. Please come back later.");
                        }
                    }
                }
            }
            else
            {
                res.SetError("Category already exists.");
            }
            return res;
        }

        public SingleRsp DeleteCategory(int id)
        {
            var res = new SingleRsp();
            var cate = GetCategoryById(id);
            using (context = new ForumContext())
            {
                using (var trans = context.Database.BeginTransaction())
                {
                    cate.IsActive = false;

                    try
                    {
                        context.Categories.Update(cate);
                        int i = context.SaveChanges();
                        if (i > 0)
                        {
                            trans.Commit();
                            res.SetMessage("Delete Category Success.");
                        }
                        else
                        {
                            trans.Rollback();
                            res.SetError("Delete Category Failure.");
                        }
                    }
                    catch (Exception ex)
                    {

                        trans.Rollback();
                        res.SetError("System error. Please come back later.");
                    }
                }
            }
            return res;
        }
        #endregion

        #region FUNCTION
        public Category GetCategoryByName(string name)
        {
            var cate = new Category();
            using (context = new ForumContext())
            {
                try
                {
                    cate = context.Categories.AsEnumerable().SingleOrDefault(x => x.Name.Equals(name, StringComparison.InvariantCulture));
                }
                catch (Exception)
                {
                    throw;

                }
            }
            return cate;
        }

        public Category GetCategoryById(int id)
        {
            var cate = new Category();
            using (context = new ForumContext())
            {
                try
                {
                    cate = context.Categories.SingleOrDefault(x => x.Id == id);

                }
                catch (Exception ex)
                {
                    throw;

                }
            }
            return cate;
        }

        public List<Category> GetListCategories()
        {
            List<Category> categories = new List<Category>();
            using (context = new ForumContext())
            {
                try
                {
                    categories = context.Categories.Where(x => x.IsActive == true).ToList();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return categories;
        } 
        #endregion
    }
}
