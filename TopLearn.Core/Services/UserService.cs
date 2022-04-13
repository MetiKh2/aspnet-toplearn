using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Convertors;
using TopLearn.Core.DTOs.UserInAdminViewModels;
using TopLearn.Core.DTOs.UserPanelViewModel;
using TopLearn.Core.DTOs.UsersInAdminViewModel;
using TopLearn.Core.DTOs.WalletViewModels;
using TopLearn.Core.Generators;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.User;
using TopLearn.DataLayer.Entities.Wallet;

namespace TopLearn.Core.Services
{
    public class UserService : IUserService
    {
        private readonly TopLearnContext _context;
        public UserService(TopLearnContext context)
        {
            _context = context;
        }

        public bool ActiveUser(string ActivationCode)
        {
            var user = _context.Users.SingleOrDefault(p => p.ActiveCode == ActivationCode);
            if (user == null || user.IsActive)
            {
                return false;
            }
            else
            {
                user.IsActive = true;
                user.ActiveCode = NameGenerator.GenerateUniqCode();
                _context.SaveChanges();
                return true;
            }
        }


        public long AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserID;
        }

        public long AddUserForAdmin(AddUserViewModel user)
        {
            User newUser = new User()
            {
                Email = user.Email,
                ActiveCode = NameGenerator.GenerateUniqCode(),
                UserName = user.UserName,
                RegisterTime = DateTime.Now,
                Password = MyPasswordHasher.EncodePasswordMd5(user.Password),
                IsActive = true,

            };

            if (user.UserAvatar != null)
            {
                string imagePath = "";


                newUser.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(user.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", newUser.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    user.UserAvatar.CopyTo(stream);
                }

            }
            else
            {
                newUser.UserAvatar = "Defult.jpg";
            }

            return AddUser(newUser);
        }

        public long AddWallet(Wallet wallet)
        {
            _context.Wallets.Add(wallet);
            _context.SaveChanges();
            return wallet.WalletID;
        }

        public void ChangeUserPassword(string username, string newPassword)
        {
            var user = GetUserByUserName(username);

            user.Password = MyPasswordHasher.EncodePasswordMd5(newPassword);
            UpdateUser(user);
        }

        public long ChargeWallet(string username, long amount, string description, bool isPay = false)
        {
            var userId = GetUserIDByUserName(username);

            Wallet wallet = new Wallet
            {
                TypeID = 1,
                UserID = userId,
                IsPay = isPay,
                Description = description,
                Amount = amount,
                DateTime = DateTime.Now,

            };

            return AddWallet(wallet);
        }

        public bool CompareCurrentPassword(string username, string currentPassword)
        {
            var hashPassword = MyPasswordHasher.EncodePasswordMd5(currentPassword);
            return _context.Users.Any(p => p.UserName == username && p.Password == hashPassword);
        }

        public void DeleteImageFromRoot(string ImagePathInRoot)
        {
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", ImagePathInRoot);
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }

        public void DeleteUser(long userId)
        {
            var user = GetUserByID(userId);

            user.IsRemoved = true;
            user.RemovedDate = DateTime.Now;
            UpdateUser(user);
        }

        public void EditProfile(string username, EditProfileViewModel profile)
        {
            if (profile.NewUserAvatar != null)
            {
                string imagePath = "";
                if (profile.UserAvatar != "Defult.jpg")
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", profile.UserAvatar);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }

                profile.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(profile.NewUserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", profile.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    profile.NewUserAvatar.CopyTo(stream);
                }

            }
            var user = GetUserByUserName(username);
            user.UserName = profile.UserName;

            user.UserAvatar = profile.UserAvatar;

            UpdateUser(user);
        }

        public void EditUserForAdmin(EditUserForAdminViewModel editUser)
        {
            var user = _context.Users.Find(editUser.UserID);
            user.Email = editUser.Email;
            if (!string.IsNullOrEmpty(editUser.Password))
            {
                user.Password = MyPasswordHasher.EncodePasswordMd5(editUser.Password);
            }
            if (editUser.NewAvatar != null)
            {
                //Delete old Image
                if (editUser.Avater != "Defult.jpg")
                {
                    string deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", editUser.Avater);
                    if (File.Exists(deletePath))
                    {
                        File.Delete(deletePath);
                    }
                }

                //Save New Image
                user.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(editUser.NewAvatar.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", user.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    editUser.NewAvatar.CopyTo(stream);
                }
            }
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public EditProfileViewModel GetDataForEditProfile(string username)
        {
            var user = GetUserByUserName(username);

            return new EditProfileViewModel
            {
                UserAvatar = user.UserAvatar,
                UserName = user.UserName
            };
        }

        public InformationUserViewModel GetInformation(string username)
        {
            var user = GetUserByUserName(username);
            InformationUserViewModel information = new InformationUserViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                RegisterDate = user.RegisterTime,
                Wallet = RemainUserWallet(username)
            };

            return information;
        }

        public InformationUserViewModel GetInformation(long id)
        {
            var user = GetUserByID(id);
            InformationUserViewModel information = new InformationUserViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                RegisterDate = user.RegisterTime,
                Wallet = RemainUserWallet(user.UserName)
            };

            return information;
        }

        public UserForAdminViewModel GetRemovedUsers(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {
            IQueryable<User> result = _context.Users.IgnoreQueryFilters().Where(p => p.IsRemoved);

            if (!string.IsNullOrEmpty(filterEmail))
            {
                result = result.Where(p => p.Email.Contains(filterEmail));
            }
            if (!string.IsNullOrEmpty(filterUserName))
            {
                result = result.Where(p => p.UserName.Contains(filterUserName));
            }
            // Show Item In Page
            int take = 5;
            int skip = (pageId - 1) * take;

            UserForAdminViewModel list = new UserForAdminViewModel
            {
                Users = result.OrderByDescending(p => p.UserID).Skip(skip).Take(take).ToList(),
                CurrentPage = pageId,
                PageCount = result.Count() / take
            };

            return list;
        }

        public GetUserPanelSideBarViewModel GetSideBarViewModel(string username)
        {
            return _context.Users.Where(p => p.UserName == username).Select(p => new GetUserPanelSideBarViewModel
            {
                UserName = p.UserName,
                ImageName = p.UserAvatar,
                RegisterDate = p.RegisterTime
            }).SingleOrDefault();
        }

        public User GetUserByActiveCode(string activeCode)
        {
            return _context.Users.SingleOrDefault(p => p.ActiveCode == activeCode);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(p => p.Email == email);
        }

        public User GetUserByID(long userId)
        {
            return _context.Users.Find(userId);
        }

        public User GetUserByUserName(string username)
        {
            return _context.Users.SingleOrDefault(p => p.UserName == username);
        }

        public long GetUserIDByUserName(string username)
        {
            return _context.Users.Single(p => p.UserName == username).UserID;
        }

        public UserForAdminViewModel GetUsers(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {
            IQueryable<User> result = _context.Users;

            if (!string.IsNullOrEmpty(filterEmail))
            {
                result = result.Where(p => p.Email.Contains(filterEmail));
            }
            if (!string.IsNullOrEmpty(filterUserName))
            {
                result = result.Where(p => p.UserName.Contains(filterUserName));
            }

            if (string.IsNullOrEmpty(filterUserName) && string.IsNullOrEmpty(filterEmail))
            {
                int take = 5;
                int skip = (pageId - 1) * take;

                UserForAdminViewModel list = new UserForAdminViewModel
                {
                    Users = result.OrderByDescending(p => p.UserID).Skip(skip).Take(take).ToList(),
                    CurrentPage = pageId,
                    PageCount = result.Count() / take
                };
                return list;
            }
            else
            {
                int take = 100000;
                int skip = (pageId - 1) * take;
                UserForAdminViewModel list = new UserForAdminViewModel
                {
              
                Users = result.OrderByDescending(p => p.UserID).Skip(skip).Take(take).ToList(),
                    CurrentPage = pageId,
                    PageCount = result.Count() / take
                };
            return list;

        }
            // Show Item In Page
          

           


        }

    public List<WalletViewModel> GetUserWallets(string username)
    {
        var userId = GetUserIDByUserName(username);

        return _context.Wallets.Where(p => p.UserID == userId).ToList().OrderByDescending(p => p.WalletID).Select(p => new WalletViewModel
        {
            Amount = p.Amount,
            DateTime = p.DateTime,
            Description = p.Description,
            Type = p.TypeID
        }).ToList();
    }

    public Wallet GetWalletByID(long id)
    {
        return _context.Wallets.Find(id);
    }

    public bool IsExistEmail(string email)
    {
        return _context.Users.Any(p => p.Email == email);
    }

    public bool IsExistUserName(string userName)
    {
        return _context.Users.Any(p => p.UserName == userName);
    }

    public User LoginUser(string email, string password)
    {
        var fixedEmail = FixText.FixEmail(email);
        var hashPassword = MyPasswordHasher.EncodePasswordMd5(password);
        var user = _context.Users.SingleOrDefault(p => p.Email == fixedEmail && p.Password == hashPassword);
        return user;
    }

    public long RemainUserWallet(string username)
    {
        var userId = GetUserIDByUserName(username);
        var enterWallet = _context.Wallets.Where(p => p.UserID == userId && p.IsPay && p.TypeID == 1).Select(p => p.Amount).ToList();
        var exitWallet = _context.Wallets.Where(p => p.UserID == userId && p.IsPay && p.TypeID == 2).Select(p => p.Amount).ToList();

        return (enterWallet.Sum() - exitWallet.Sum());
    }

    public void SaveImageInRoot(string newImageName, IFormFile ImageFile, string folderName)
    {
        newImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(ImageFile.FileName);
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/{folderName}", newImageName);
        using (var stream = new FileStream(imagePath, FileMode.Create))
        {
            ImageFile.CopyTo(stream);
        }

    }

    public EditUserForAdminViewModel ShowUserInEditMode(long id)
    {
        return _context.Users.Where(p => p.UserID == id).Select(p => new EditUserForAdminViewModel
        {
            Email = p.Email,
            UserName = p.UserName,
            UserRoles = p.UserInRoles.Select(p => p.RoleID).ToList(),
            Avater = p.UserAvatar,
            UserID = p.UserID
        }).Single();
    }

    public bool UpdateUser(User user)
    {
        try
        {
            _context.Update(user);
            _context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public void UpdateWallet(Wallet wallet)
    {
        _context.Wallets.Update(wallet);
        _context.SaveChanges();
    }
}
}
