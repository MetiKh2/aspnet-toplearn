using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs.UserInAdminViewModels;
using TopLearn.Core.DTOs.UserPanelViewModel;
using TopLearn.Core.DTOs.UsersInAdminViewModel;
using TopLearn.Core.DTOs.WalletViewModels;
using TopLearn.DataLayer.Entities.User;
using TopLearn.DataLayer.Entities.Wallet;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IUserService
    {
        bool IsExistEmail(string email);
        void SaveImageInRoot(string newImageName, IFormFile ImageFile, string folderName);
        void DeleteImageFromRoot(string ImagePathInRoot);
        bool IsExistUserName(string userName);
        long AddUser(User user);
        User LoginUser(string email, string password);
        bool ActiveUser(string ActivationCode);
        User GetUserByEmail(string email);
        User GetUserByUserName(string username);
        User GetUserByActiveCode(string activeCode);
        bool UpdateUser(User user);
        long GetUserIDByUserName(string username);
        User GetUserByID(long userId);

        #region Panel
        InformationUserViewModel GetInformation(string username);
        InformationUserViewModel GetInformation(long id);
        GetUserPanelSideBarViewModel GetSideBarViewModel(string username);

        EditProfileViewModel GetDataForEditProfile(string username);

        void EditProfile(string username, EditProfileViewModel profile);
        bool CompareCurrentPassword(string username, string currentPassword);
        void ChangeUserPassword(string username,string newPassword);

        #endregion

        #region Wallet
        long RemainUserWallet(string username);
        List<WalletViewModel> GetUserWallets(string username);
        long ChargeWallet(string username, long amount, string description, bool isPay = false);
        long AddWallet(Wallet wallet);
        Wallet GetWalletByID(long id);
        void UpdateWallet(Wallet wallet);
        #endregion

        #region AdminPanel

        UserForAdminViewModel GetUsers(int pageId=1,string filterUserName="",string filterEmail="");
        UserForAdminViewModel GetRemovedUsers(int pageId = 1, string filterUserName = "", string filterEmail = "");
        long AddUserForAdmin(AddUserViewModel user);
        EditUserForAdminViewModel ShowUserInEditMode(long id);
        void EditUserForAdmin( EditUserForAdminViewModel editUser);

        void DeleteUser(long userId);

        #endregion
    }
}
