using Forum.Common.DAL;
using Forum.Common.Rsp;
using Forum.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forum.DAL
{
    public class PostRep : GenericRep<ForumContext, Post>
    {
        private ForumContext context;

        public SingleRsp CreatePost(Post post, List<PostTag> postTags, List<Image> images)
        {
            var res = new SingleRsp();

            using (context = new ForumContext())
            {
                using (var trans = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.Posts.Add(post);
                        int i = context.SaveChanges();
                        if (i > 0)
                        {
                            if (postTags != null)
                            {
                                postTags.ForEach(x => x.PostId = p.Entity.Id);
                                context.PostTags.AddRange(postTags);
                            }

                            if (images != null)
                            {
                                images.ForEach(x => x.PostId = p.Entity.Id);
                                context.Images.AddRange(images);
                            }


                            int j = context.SaveChanges();
                            if (j > 0 && j == (postTags.Count + images.Count))
                            {
                                trans.Commit();
                                res.SetMessage("Create Post Success.");
                            }
                            else
                            {
                                trans.Rollback();
                                res.SetError("Create Post Failure.");
                            }
                        }
                        else
                        {
                            trans.Rollback();
                            res.SetError("Create Post Failure.");
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

        public SingleRsp DeletePost(int id)
        {
            var res = new SingleRsp();

            using (context = new ForumContext())
            {
                using (var trans = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.Posts.SingleOrDefault(x => x.Id == id);
                        var images = context.Images.Where(x => x.PostId == id).ToList();

                        p.IsActive = false;
                        images.ForEach(x => x.IsActive = false);

                        context.Posts.Update(p);
                        context.Images.UpdateRange(images);

                        int i = context.SaveChanges();

                        if (i > 0 && i == images.Count + 1)
                        {
                            trans.Commit();
                            res.SetMessage("Delete Post Success.");
                        }
                        else
                        {
                            trans.Rollback();
                            res.SetError("Delete Post Failure.");
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

        public SingleRsp UpdatePost(int id, string title, string content, DateTime date)
        {
            SingleRsp res = new SingleRsp();
            using(context = new ForumContext())
            {
                using(var trans = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.Posts.SingleOrDefault(x => x.Id == id);
                        if (p != null)
                        {
                            p.Title = title;
                            p.Content = content;
                            p.UpdatedDate = date;

                            context.Posts.Update(p);

                            int i = context.SaveChanges();
                            if (i > 0)
                            {
                                trans.Commit();
                                res.SetMessage("Update Post Success.");

                            }
                            else
                            {
                                trans.Rollback();
                                res.SetError("Update Post Failure.");
                            }
                        }
                        else
                        {
                            res.SetError("Post not found. Please check again.");
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        res.SetError("System Error. Please come back later.");
                    }
                }
            }
            return res;
        }

        public void UpdateViewPost(int id)
        {
            using (context = new ForumContext())
            {
                using (var trans = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.Posts.SingleOrDefault(x => x.Id == id);
                        p.View = p.View + 1;

                        context.Posts.Update(p);
                        context.SaveChanges();
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public void UpdateLikePost(int id)
        {
            using (context = new ForumContext())
            {
                using (var trans = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.Posts.SingleOrDefault(x => x.Id == id);
                        p.Like = p.Like + 1;

                        context.Posts.Update(p);
                        context.SaveChanges();
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw;

                    }
                }
            }
        }

        public List<Post> GetListPosts()
        {
            List<Post> posts = new List<Post>();

            using (context = new ForumContext())
            {
                try
                {
                    posts = context.Posts.Where(a => a.IsActive == true).ToList();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return posts;
        }

        public Post GetPostById(int id)
        {
            Post p = new Post();
            using (context = new ForumContext())
            {
                try
                {
                    p = context.Posts.Include(x => x.Category).Include(x => x.UsernameNavigation).Include(x => x.PostTags).ThenInclude(x => x.Tag).SingleOrDefault(x => x.Id == id);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return p;
        }

        public List<Post> GetPostByCategoryId(int categoryId)
        {
            List<Post> posts = new List<Post>();

            using (context = new ForumContext())
            {
                try
                {
                    posts = context.Posts.Where(x => x.IsActive == true && x.CategoryId == categoryId).ToList();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return posts;
        }

        public List<Post> GetPostByKeyword(string kw)
        {
            List<Post> posts = new List<Post>();

            using (context = new ForumContext())
            {
                try
                {
                    posts = context.Posts.Where(x => x.IsActive == true && (x.Title.Contains(kw.ToLower()) || x.Content.Contains(kw.ToLower()))).ToList();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return posts;
        }

        public List<Post> GetListPostsByTagId(int tagId)
        {
            List<Post> posts = new List<Post>();

            using (context = new ForumContext())
            {
                try
                {
                    var pt = from tp in context.PostTags
                             join po in context.Posts on tp.PostId equals po.Id
                             where tp.TagId == tagId
                             select po;
                    if (pt != null)
                    {
                        posts = pt.ToList();
                    }

                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return posts;
        }

        public Category GetCategoryByPostId(int postId)
        {
            Category cate = new Category();

            using (context = new ForumContext())
            {
                try
                {
                    var c = from p in context.Posts
                            join ct in context.Categories on p.CategoryId equals ct.Id
                            where p.Id == postId
                            select ct;
                    if (c != null)
                    {
                        cate = c.FirstOrDefault();
                    }

                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return cate;
        }

        public List<Post> GetPostByUsername(string username)
        {
            List<Post> posts = new List<Post>();
            using(context=new ForumContext())
            {
                try
                {
                    posts = context.Posts.AsEnumerable().Where(x => x.Username.Equals(username, StringComparison.InvariantCulture)).ToList();
                }
                catch(Exception ex)
                {
                    throw;
                }
            }
            return posts;

        }
    }
}
