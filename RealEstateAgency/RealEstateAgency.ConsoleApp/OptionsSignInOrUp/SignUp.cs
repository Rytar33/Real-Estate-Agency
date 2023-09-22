using RealEstateAgency.Services.Models.Users;
using RealEstateAgency.Services.Services;

namespace RealEstateAgency.ConsoleApp.SignInUp
{
    public class SignUp
    {
        public void CreateUser()
        {
            UserCreateRequest user = new UserCreateRequest();
            Console.WriteLine("* - Обязательное поле");
            Console.Write("Имя пользователя*: ");
            user.Name = Console.ReadLine()!;
            Console.Write("Пароль пользователя*: ");
            user.Password = Console.ReadLine()!;
            Console.Write("Ваша почта*: ");
            user.Email = Console.ReadLine()!;
            Console.Write("Ваше имя*: ");
            user.FirstName = Console.ReadLine()!;
            Console.Write("Ваша фамилия*: ");
            user.LastName = Console.ReadLine()!;
            Console.Write("Вашe отчество: ");
            user.SecondName = Console.ReadLine();

            var userService = new UserService();
            var responseFromServer = userService.Create(user);
            if (responseFromServer.IsSuccess) 
                new ClientService()
                    .CreateClient(userService.GetUser(user.Email)!);
            Console.WriteLine(responseFromServer.Message);
        }
    }
}
