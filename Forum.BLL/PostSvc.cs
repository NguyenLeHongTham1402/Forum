using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Forum.Common.BLL;
using Forum.Common.Req;
using Forum.Common.Rsp;
using Forum.DAL;
using Forum.DAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Forum.BLL
{
    public class PostSvc : GenericSvc<PostRep, Post>
    {
        private readonly PostRep postRep = new PostRep();
        public SingleRsp CreatePost(_PostReq postReq)
        {
            List<Image> images = new List<Image>();
            if (postReq.Images != null)
            {
                foreach (var item in postReq.Images)
                {
                    images.Add(new Image(item.PostId ?? 0, UploadImageToCloudinary(item.file), item.PostId ?? 0, item.IsActive ?? true));
                }
            }
            

            List<PostTag> postTags = new List<PostTag>();
            if (postReq.PostTags != null)
            {
                foreach (var item in postReq.PostTags)
                {
                    postTags.Add(new PostTag(item.PostId, item.TagId));
                }
            }
            

            Post post = new Post();
            post.Title = postReq.Title;
            post.Content = postReq.Content;
            post.CreatedDate = postReq.CreatedDate;
            post.UpdatedDate = postReq.UpdatedDate;
            post.CategoryId = postReq.CategoryId;
            post.View = postReq.View;
            post.Like = postReq.Like;
            post.Username = postReq.Username;
            post.IsActive = postReq.IsActive;

            var res = postRep.CreatePost(post, postTags, images);
            return res;
        }

        public SingleRsp DeletePost(int id)
        {
            var res = postRep.DeletePost(id);
            return res;
        }

        public void UpdateViewPost(int id)
        {
            postRep.UpdateViewPost(id);
        }

        public void UpdateLikePost(int id)
        {
            postRep.UpdateLikePost(id);
        }

        public SingleRsp UpdatePost(_PostUpdReq post, int id)
        {
            return postRep.UpdatePost(id, post.Title, post.Content, post.UpdatedDate);
        }

        public List<Post> GetListPosts()
        {
            return postRep.GetListPosts();
        }

        public Post GetPostById(int id)
        {
            return postRep.GetPostById(id);
        }

        public List<Post> GetListPostsByCategoryId(int categoryid)
        {
            return postRep.GetPostByCategoryId(categoryid);
        }

        public List<Post> GetPostByKeyword(string kw)
        {
            return postRep.GetPostByKeyword(kw);
        }

        public List<Post> GetListPostsByTagId(int tagId)
        {
            return postRep.GetListPostsByTagId(tagId);
        }

        public Category GetCategoryByPostId(int postid)
        {
            return postRep.GetCategoryByPostId(postid);
        }

        public List<Post> GetPostByUsername(string username)
        {
            return postRep.GetPostByUsername(username);
        }

        public string UploadImageToCloudinary(IFormFile file)
        {
            if (file != null)
            {
                Account account = new Account(
                "dp50hyprx",
                "919543544232649",
                "UCT8SrEd9xOE3FuTYo1f4AUamhk"
                );
                Cloudinary cloudinary = new Cloudinary(account);
                cloudinary.Api.Secure = true;

                byte[] bytes;
                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    bytes = stream.ToArray();
                }
                string base64 = Convert.ToBase64String(bytes);

                var prefix = @"data:image/png;base64,";
                var imagePath = prefix + base64;
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(imagePath),
                    Folder = "Forum/img"
                };
                var uploadResult = cloudinary.Upload(uploadParams);

                return uploadResult.SecureUrl.AbsoluteUri;
            }
            return "";
        }
    }
}
