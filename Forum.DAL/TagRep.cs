using Forum.Common.DAL;
using Forum.Common.Rsp;
using Forum.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Forum.DAL
{
    public class TagRep : GenericRep<ForumContext, Tag>
    {
        private ForumContext context;

        public SingleRsp CreateTag(Tag tag)
        {
            var res = new SingleRsp();
            if (GetTagByName(tag.Name) == null)
            {
                using (context = new ForumContext())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.Tags.Add(tag);

                            int i = context.SaveChanges();
                            if (i > 0)
                            {
                                trans.Commit();
                                res.SetMessage("Create Tag Success.");
                            }
                            else
                            {
                                trans.Rollback();
                                res.SetError("Create Tag Failure.");
                            }
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            res.SetError("System error occurred. Please come back later.");
                        }
                    }
                }
            }
            else
            {
                res.SetError("Tag already exists.");
            }
            return res;
        }

        public SingleRsp UpdateTag(Tag tag)
        {
            var res = new SingleRsp();
            if (GetTagByName(tag.Name) == null)
            {
                using (context = new ForumContext())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.Tags.Update(tag);

                            int i = context.SaveChanges();
                            if (i > 0)
                            {
                                trans.Commit();
                                res.SetMessage("Update Tag Success.");
                            }
                            else
                            {
                                trans.Rollback();
                                res.SetError("Update Tag Failure.");
                            }
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            res.SetError("System error occurred. Please come back later.");
                        }
                    }
                }
            }
            else
            {
                res.SetError("Tag already exists.");
            }
            return res;
        }

        public Tag GetTagByName(string name)
        {
            var tag = new Tag();

            using (context = new ForumContext())
            {
                try
                {
                    tag = context.Tags.AsEnumerable().SingleOrDefault(x => x.Name.Equals(name, StringComparison.InvariantCulture));
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return tag;
        }

        public Tag GetTagById(int id)
        {
            var tag = new Tag();

            using (context = new ForumContext())
            {
                try
                {
                    tag = context.Tags.AsEnumerable().SingleOrDefault(x => x.Id == id);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return tag;
        }

        public List<Tag> GetListTags()
        {
            List<Tag> tags = new List<Tag>();

            using (context = new ForumContext())
            {
                try
                {
                    tags = context.Tags.Select(x => x).ToList();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return tags;
        }

        public List<Tag> GetListTagByPostId(int id)
        {
            List<Tag> tags = new List<Tag>();

            using (context = new ForumContext())
            {
                try
                {
                    var t = from tg in context.Tags
                            join tp in context.PostTags on tg.Id equals tp.TagId
                            where tp.PostId == id
                            select tg;
                    tags = t.ToList();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return tags;
        }
    }
}