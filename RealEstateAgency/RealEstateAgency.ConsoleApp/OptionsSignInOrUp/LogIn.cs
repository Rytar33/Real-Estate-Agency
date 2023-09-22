using RealEstateAgency.Models;
using RealEstateAgency.Services.Models;
using RealEstateAgency.Services.Services;

namespace RealEstateAgency.ConsoleApp.SignInUp
{
    public class LogIn
    {
        public User? SetAndCheckData()
        {
            Console.Write("Введите имя пользователя: ");
            string userName = Console.ReadLine()!;
            Console.Write("Введите пароль: ");
            string password = Console.ReadLine()!;
            (BaseResponse response, User? getUser) = new UserService().LogIn(userName, password);
            Console.WriteLine(response.Message);
            return getUser;
        }
    }
}