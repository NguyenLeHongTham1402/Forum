using Forum.Common.BLL;
using Forum.Common.Req;
using Forum.Common.Rsp;
using Forum.DAL;
using Forum.DAL.Models;
using System.Collections.Generic;

namespace Forum.BLL
{
    public class TagSvc : GenericSvc<TagRep, Tag>
    {
        private readonly TagRep tagRep = new TagRep();

        public SingleRsp CreateTag(_TagReq tagReq)
        {
            Tag tag = new Tag();
            tag.Name = tagReq.Name;
            tag.CreatedDate = tagReq.CreatedDate;

            return tagRep.CreateTag(tag);
        }

        public SingleRsp UpdateTag(_TagReq tagReq)
        {
            Tag tag = new Tag();
            tag.Id = tagReq.Id;
            tag.Name = tagReq.Name;
            tag.CreatedDate = tagReq.CreatedDate;

            return tagRep.UpdateTag(tag);
        }

        public Tag GetTagById(int id)
        {
            return tagRep.GetTagById(id);
        }

        public List<Tag> GetListTags()
        {
            return tagRep.GetListTags();
        }

        public List<Tag> GetListTagsByPostId(int postId)
        {
            return tagRep.GetListTagByPostId(postId);
        }
    }
}