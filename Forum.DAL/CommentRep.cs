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
    public class CommentRep : GenericRep<ForumContext, Comment>
    {
        private ForumContext context;
        public SingleRsp CreateComment(Comment comment)
        {
            var res = new SingleRsp();

            using (context = new ForumContext())
            {
                using(var trans = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Comments.Add(comment);
                        int i = context.SaveChanges();
                        if (i > 0)
                        {
                            trans.Commit();
                            res.SetMessage("Create Comment Success.");
                        }
                        else
                        {
                            trans.Rollback();
                            res.SetError("Create Comment Failure.");
                        }
                    }
                    catch(Exception ex)
                    {
                        trans.Rollback();
                        res.SetError("System error. Please come back later.");
                    }
                }
            }
            return res;
        }

        public void UpdateLikeComment(int id)
        {
            using(context = new ForumContext())
            {
                using(var trans = context.Database.BeginTransaction())
                {
                    try
                    {
                        var cmt = context.Comments.SingleOrDefault(x => x.Id == id);
                        cmt.Like += 1;

                        context.Comments.Update(cmt);
                        context.SaveChanges();
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                    }
                }
            }
        }

        public SingleRsp DeleteComment(int id)
        {
            var res = new SingleRsp();
            using (context = new ForumContext())
            {
                using (var trans = context.Database.BeginTransaction())
                {
                    try
                    {
                        var cmt = context.Comments.SingleOrDefault(x => x.Id == id);
                        cmt.IsActive = false;
                        context.Comments.Update(cmt);
                        int i = context.SaveChanges();
                        if (i > 0)
                        {
                            trans.Commit();
                            res.SetMessage("Delete Comment Success.");
                        }
                        else
                        {
                            trans.Rollback();
                            res.SetError("Delete Comment Failure.");
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

        public List<Comment> GetCommentByPostId(int postId)
        {
            List<Comment> cmts = new List<Comment>();
            using(context = new ForumContext())
            {
                try
                {
                    cmts = context.Comments.Include(x=>x.UsernameNavigation).Include(x=>x.Replies).ThenInclude(x=>x.UsernameNavigation).Where(x => x.PostId == postId && x.IsActive==true).ToList();
                    cmts.ForEach(x => x.UsernameNavigation.Replies = null);
                }
                catch(Exception ex)
                {
                    throw;
                }
            }
            return cmts;
        }

        public List<Reply> GetReplyByParentId(int parentId)
        {
            List<Reply> rls = new List<Reply>();
            using (context = new ForumContext())
            {
                try
                {
                    rls = context.Replies.Where(x => x.ParentId == parentId).ToList();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return rls;
        }

        public SingleRsp CreateReply(Reply reply)
        {
            var res = new SingleRsp();

            using (context = new ForumContext())
            {
                using (var trans = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Replies.Add(reply);
                        int i = context.SaveChanges();
                        if (i > 0)
                        {
                            trans.Commit();
                            res.SetMessage("Create Reply Success.");
                        }
                        else
                        {
                            trans.Rollback();
                            res.SetError("Create Reply Failure.");
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
        public void UpdateLikeReply(int id)
        {
            using (context = new ForumContext())
            {
                using (var trans = context.Database.BeginTransaction())
                {
                    try
                    {
                        var cmt = context.Replies.SingleOrDefault(x => x.Id == id);
                        cmt.Like += 1;

                        context.Replies.Update(cmt);
                        context.SaveChanges();
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                    }
                }
            }
        }
        public SingleRsp DeleteReply(int id)
        {
            var res = new SingleRsp();
            using (context = new ForumContext())
            {
                using (var trans = context.Database.BeginTransaction())
                {
                    try
                    {
                        var cmt = context.Replies.SingleOrDefault(x => x.Id == id);
                        context.Replies.Remove(cmt);
                        int i = context.SaveChanges();
                        if (i > 0)
                        {
                            trans.Commit();
                            res.SetMessage("Delete Comment Success.");
                        }
                        else
                        {
                            trans.Rollback();
                            res.SetError("Delete Comment Failure.");
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
    }
}
