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
    public class CommentSvc : GenericSvc<CommentRep, Comment>
    {
        private readonly CommentRep rep = new CommentRep();

        public SingleRsp CreateComment(_CommentReq req)
        {
            Comment cmt = new Comment();
            cmt.Title = req.Title;
            cmt.Content = req.Content;
            cmt.IsActive = req.IsActive;
            cmt.PostId = req.PostId;
            cmt.Like = req.Like;
            cmt.Username = req.Username;

            return rep.CreateComment(cmt);
        }

        public SingleRsp DeleteComment(int id)
        {
            return rep.DeleteComment(id);
        }

        public void UpdateLikeComment(int id)
        {
            rep.UpdateLikeComment(id);
        }

        public List<Comment> GetListCommentByPostId(int postId)
        {
            return rep.GetCommentByPostId(postId);
        }

        public List<Reply> GetReplyByParentId(int parentId)
        {
            return rep.GetReplyByParentId(parentId);
        }
        public SingleRsp CreateReply(_ReplyReq req)
        {
            Reply reply = new Reply();
            reply.Title = req.Title;
            reply.Content = req.Content;
            reply.Username = req.Username;
            reply.Like = req.Like;
            reply.ParentId = req.ParentId;
            reply.PostId = req.PostId;
            return rep.CreateReply(reply);
        }

        public SingleRsp DeleteReply(int id)
        {
            return rep.DeleteReply(id);
        }

        public void UpdateLikeReply(int id)
        {
            rep.UpdateLikeReply(id);
        }
    }
}
