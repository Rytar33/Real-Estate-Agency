using Microsoft.IdentityModel.Tokens;
using Server.Context;
using Server.Models;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    public class UserService
    {
        public BaseResponse SignUp(User userRequest)
        {
            if (userRequest.UserName.Length < 4 || userRequest.UserName.Length > 30)
                return new BaseResponse() { IsSuccess = false, Message = "Имя пользователя должно быть в диапозоне от 4-х до 30 символов." };
            if (userRequest.Password.Length < 5 ||  userRequest.Password.Length > 25)
                return new BaseResponse() { IsSuccess = false, Message = "Пароль должен быть в диапозоне от 5-ти до 25 символов." };
            if (userRequest.FirstName.Length < 2 || userRequest.FirstName.Length > 50)
                return new BaseResponse() { IsSuccess = false, Message = "Имя должно быть в диапозоне от 2-х до 50 символов." };
            if (userRequest.LastName.Length < 2 || userRequest.LastName.Length > 50)
                return new BaseResponse() { IsSuccess = false, Message = "Фамилия должна быть в диапозоне от 2-х до 50 символов." };
            if (!string.IsNullOrWhiteSpace(userRequest.SecondName) && (userRequest.SecondName!.Length < 2 || userRequest.SecondName.Length > 50))
                return new BaseResponse() { IsSuccess = false, Message = "Отчество должно быть в диапозоне от 2-х до 50 символов." };

            using var db = new DataBaseContext();
            userRequest.Password = userRequest.Password.GetSha256();
            db.User.Add(userRequest);
            db.SaveChanges();
            return new BaseResponse() { IsSuccess = true, Message = "Поздравляем! Вы успешно зарегестрировались." };
        }
        public (BaseResponse, User?) LogIn(string name, string password)
        {
            using var db = new DataBaseContext();
            var userFind = db.User.FirstOrDefault(u => u.UserName == name && u.Password == password.GetSha256());
            if(userFind == null)
                return (new BaseResponse() { IsSuccess = false, Message = "Неверное имя пользователя или пароль." }, null);
            if (userFind.Status == EnumStatus.Blocked)
                return (new BaseResponse() { IsSuccess = false, Message = "Ваш аккаунт заблокирован." }, null);
            userFind.Status = EnumStatus.Online;
            db.SaveChanges();
            return (new BaseResponse() { IsSuccess = true, Message = "Вы успешно вошли в аккаунт." }, userFind);
        }
        public void Exit(int IDUser)
        {
            using var db = new DataBaseContext();
            var userFind = db.User.FirstOrDefault(u => u.IDUser == IDUser);
            userFind!.Status = EnumStatus.Offline;
            db.SaveChanges();
        }
        
    }
}
