using Forum.Common.DAL;
using Forum.Common.Rsp;
using Forum.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Forum.DAL
{
    public class UserRep : GenericRep<ForumContext, User>
    {
        private ForumContext context;
        private readonly string key = "hjjkdlkr^%$jddj3mmt&%dhd#>Rjd";

        #region CRUD
        public SingleRsp CreateUser(User user)
        {
            SingleRsp res = new SingleRsp();
            if (GetUserByUsername(user.Username) == null)
            {
                using (context = new ForumContext())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.Users.Add(user);

                            int i = context.SaveChanges();

                            if (i > 0)
                            {
                                trans.Commit();
                                res.SetMessage("Sign Up Account Success.");
                            }

                            else
                            {
                                trans.Rollback();
                                res.SetError("Sign Up Account Failure.");
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
                res.SetError("Username already exists.");
            }
            return res;
            
        }

        public SingleRsp UpdateUser (User user)
        {
            var res = new SingleRsp();

            using(context = new ForumContext())
            {
                using(var trans = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Users.Update(user);
                        int i = context.SaveChanges();
                        if (i > 0)
                        {
                            trans.Commit();
                            res.SetMessage("Update User Success");
                        }
                        else
                        {
                            trans.Rollback();
                            res.SetError("Update User Failure.");
                        }

                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        res.SetError("System error occurred. Please come back later.");
                    }
                }
            }
            return res;
        }

        public SingleRsp DeleteUser(string username)
        {
            var res = new SingleRsp();
            var u = GetUserByUsername(username);
            using (context = new ForumContext())
            {
                using (var trans = context.Database.BeginTransaction())
                {
                    try
                    {
                        u.IsActive = false;

                        context.Users.Update(u);
                        int i = context.SaveChanges();
                        if (i > 0)
                        {
                            trans.Commit();
                            res.SetMessage("Delete User Success.");
                        }
                        else
                        {
                            trans.Rollback();
                            res.SetError("Delete User Failure.");
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        res.SetError("System error occurred. Please come back later.");
                    }
                }
            }
            return res;
        }
        #endregion



        #region Function
        public List<User> GetListUsers()
        {
            List<User> users = new List<User>();
            using(context=new ForumContext())
            {
                try
                {
                    users = context.Users.Select(x => x).ToList();
                }
                catch (Exception)
                {
                    users = null;
                }
            }
            return users;
        }

        public User GetUserByUsername(string username)
        {
            using(context = new ForumContext())
            {
                try
                {
                    return context.Users.AsEnumerable().SingleOrDefault(x => x.Username.Equals(username, StringComparison.InvariantCulture));
                }
                catch(Exception ex)
                {
                    throw;
                }
            }
        }

        public SingleRsp SignIn(string username, string password)
        {
            SingleRsp res = new SingleRsp();
            
            using(context = new ForumContext())
            {
                try
                {
                    User u = context.Users.AsEnumerable().SingleOrDefault(x => x.Password.Equals(password, StringComparison.InvariantCulture)
                            && x.Username.Equals(username, StringComparison.InvariantCulture) && x.IsActive == true);
                    if (u != null)
                    {
                        res.SetMessage("Sign In Success.");
                        res.Data = u;
                    }
                    else
                    {
                        res.SetError("Sign in failure. Invalid username or password.");
                    }
                }
                catch (Exception ex)
                {

                    res.SetError("System error occurred. Please come back later.");
                }
            }
            return res;
        }

        public string Encrypt(string text)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        public string Decrypt(string cipher)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(cipher);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }

        } 

        #endregion
    }
}
