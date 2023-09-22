using RealEstateAgency.DBMigrations;
using RealEstateAgency.Enums;
using RealEstateAgency.Models;
using RealEstateAgency.Services.Extensions;
using RealEstateAgency.Services.Models;
using RealEstateAgency.Services.Models.Users;
using RealEstateAgency.Services.Validators;

namespace RealEstateAgency.Services.Services
{
    public class UserService
    {
        public BaseResponse Create(UserCreateRequest userRequest)
        {
            var checkValidation = userRequest.CheckValidation();
            if (!checkValidation.IsSuccess) return checkValidation;

            using var db = new DataBaseContext();

            db.User.Add(new User()
            {
                UserName = userRequest.Name,
                Password = userRequest.Password.GetSha256(),
                Email = userRequest.Email,
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,
                SecondName = userRequest.SecondName,
                TypeAccount = EnumUserRanked.Client,
                Status = EnumUserStatus.Offline,
                DateRegistration = DateTime.Now
            });
            db.SaveChanges();
            return new BaseResponse() { IsSuccess = true, Message = "Поздравляем! Вы успешно зарегестрировались." };
        }
        public (BaseResponse, User?) LogIn(string name, string password)
        {
            using var db = new DataBaseContext();
            var userFind = db.User.FirstOrDefault(u => u.UserName == name && u.Password == password.GetSha256());
            if (userFind == null)
                return (new BaseResponse() { IsSuccess = false, Message = "Неверное имя пользователя или пароль." }, null);
            if (userFind.Status == EnumUserStatus.Blocked)
                return (new BaseResponse() { IsSuccess = false, Message = "Ваш аккаунт заблокирован." }, null);
            userFind.Status = EnumUserStatus.Online;
            db.SaveChanges();
            return (new BaseResponse() { IsSuccess = true, Message = "Вы успешно вошли в аккаунт." }, userFind);
        }
        public BaseResponse BlockUser(int IDUser, bool isUnBlock)
        {
            using var db = new DataBaseContext();
            var userFind = db.User.FirstOrDefault(u => u.IDUser == IDUser);
            if (userFind == null)
                return new BaseResponse { IsSuccess = false, Message = "Данный пользователь не был найден." };
            string? message = null;
            if (isUnBlock)
            {
                userFind.Status = EnumUserStatus.Offline;
                message = "Пользователь успешно разблокирован.";
            }
            else
            {
                userFind.Status = EnumUserStatus.Blocked;
                message = "Пользователь успешно заблокирован.";
            }

            db.SaveChanges();
            return new BaseResponse() { IsSuccess = true, Message = message };
        }
        public UserDetailResponse? GetUserDetail(int userId)
        {
            using var db = new DataBaseContext();

            var user = db.User.FirstOrDefault(x => x.IDUser == userId);

            if (user == null) return null;

            return new UserDetailResponse()
            {
                IDUser = userId,
                UserName = user.UserName,
                Email = user.Email,
                TypeAccount = user.TypeAccount,
                FirstName = user.FirstName,
                LastName = user.LastName,
                SecondName = user.SecondName,
                DateRegistration = user.DateRegistration,
                Status = user.Status
            };
        }
        public User? GetUser(string eMail)
        {
            using var db = new DataBaseContext();
            var user = db.User.FirstOrDefault(u => u.Email == eMail);
            return user;
        }
        public void Exit(int IDUser)
        {
            using var db = new DataBaseContext();
            var userFind = db.User.FirstOrDefault(u => u.IDUser == IDUser);
            userFind!.Status = EnumUserStatus.Offline;
            db.SaveChanges();
        }
    }
}
