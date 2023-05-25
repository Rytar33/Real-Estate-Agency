using Server;
using Server.Models;
using Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.SignInUp
{
    public class LogIn
    {
        public User SetAndCheckData()
        {
            Console.Write("Введите имя пользователя: ");
            string userName = Console.ReadLine();
            Console.Write("Введите пароль: ");
            string password = Console.ReadLine();
            (BaseResponse response, User getUser) = new UserService().LogIn(userName, password);
            Console.WriteLine(response.Message);
            return getUser;
        }
    }
}