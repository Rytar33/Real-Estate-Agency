using RealEstateAgency.ConsoleApp.SignInUp;
using RealEstateAgency.Enums;
using RealEstateAgency.Models;

namespace RealEstateAgency.ConsoleApp;

public class Program
{
    static void Main()
    {
        do
        {
            User? user = null!;
            do
            {
                Console.Clear();
                Console.Write(new ModelMenu()
                {
                    lines = new List<string>() {
                        "Войти в аккаунт",
                        "Зарегестрироваться",
                        "Забыли пароль?"
                    },
                    IsChoise = true,
                    ChoiseText = "Выберите действие"
                }.GetMenu());
                int choise = 0;

                if(!int.TryParse(Console.ReadLine(), out choise))
                    Console.WriteLine("Осторожно! Вы ввели не числовое значение.");
                else
                {
                    switch (choise)
                    {
                        case 1:
                            user = new LogIn().SetAndCheckData();
                            break;
                        case 2:
                            new SignUp().CreateUser();
                            break;
                        case 3:

                            break;
                        default:
                            Console.WriteLine("Вы выбрали не существующую опцию.");
                            break;
                    }
                }
                Console.Write("Нажмите чтобы продолжить: ");
                Console.ReadKey();
            } while (user == null || user.Status != EnumUserStatus.Online);

            do
            {
                Console.Clear();
                Console.WriteLine($"Здраствуйте {user.FullName}, что бы вы хотели сделать?");
                user = new TransitionInteraction(user).WrapperInteraction()!;
                Console.Write("Нажмите чтобы продолжить: ");
                Console.ReadKey();
            } while (user != null && user.Status == EnumUserStatus.Online);
        } while (true);
    }
}