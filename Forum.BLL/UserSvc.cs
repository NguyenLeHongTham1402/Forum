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
    public class UserSvc : GenericSvc<UserRep, User>
    {
        private UserRep userRep = new UserRep();

        #region CRUD
        public SingleRsp CreateUser(_UserReq userReq)
        {
            var res = new SingleRsp();

            User user = new User();
            user.Username = userReq.Username;
            user.Password = userRep.Encrypt(userReq.Password);
            if (userReq.Image != null)
            {
                user.Avatar = UploadImageToCloudinary(userReq.Image);
            }
            else
            {
                user.Avatar = "/assets/av" + new Random().Next(2, 10) + ".png";
            }
            user.Email = userReq.Email;
            user.Role = userReq.Role;
            user.RealName = userReq.RealName;
            user.IsActive = userReq.IsActive;

            res = userRep.CreateUser(user);

            return res;
        }

        public SingleRsp UpdateUser(_UserReq userReq, string username)
        {
            var res = new SingleRsp();

            User user_exists = userRep.GetUserByUsername(username);

            User user = new User();
            user.Username = userReq.Username;
            user.Password = userRep.Encrypt(userReq.Password);
            if (userReq.Avatar != null)
                user.Avatar = UploadImageToCloudinary(userReq.Image);
            else
                user.Avatar = user_exists.Avatar;
            user.Email = userReq.Email;
            user.Role = userReq.Role;
            user.RealName = userReq.RealName;
            user.IsActive = userReq.IsActive;

            res = userRep.UpdateUser(user);

            return res;
        } 

        public SingleRsp DeleteUser(string username)
        {
            return userRep.DeleteUser(username);
        }

        public SingleRsp SignIn(_UserReq userReq)
        {
            var res = new SingleRsp();
            res = userRep.SignIn(userReq.Username, userRep.Encrypt(userReq.Password));
            if (res.Success)
            {
                res.Data = GetUserByUsername(userReq.Username);
            }
            return res;
        }
        #endregion

        public _UserReq GetUserByUsername(string username)
        {
            _UserReq userReq = new _UserReq();

            var user = userRep.GetUserByUsername(username);

            userReq.Username = user.Username;
            userReq.Password = userRep.Decrypt(user.Password);
            userReq.Avatar = user.Avatar;
            userReq.Email = user.Email;
            userReq.RealName = user.RealName;
            userReq.Role = user.Role;
            userReq.IsActive = user.IsActive;

            return userReq;
        }

        public List<User> GetListUsers()
        {
            return userRep.GetListUsers();
        }

        private string UploadImageToCloudinary(IFormFile file)
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
