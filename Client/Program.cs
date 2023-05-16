using Client.SignInUp;
using Server;
using Server.Models;
using System.Runtime.Intrinsics.Arm;

namespace Client
{
    public class Program
    {
        static void Main()
        {
            do
            {
                User user = null;
                do
                {
                    Console.Clear();
                    Console.Write(new ModelMenu()
                    {
                        lines = new List<string>() { "Войти в аккаунт", "Зарегестрироваться" },
                        IsChoise = true,
                        ChoiseText = "Выберите действие"
                    }.GetMenu());
                    int choise = 0;
                    try
                    {
                        choise = int.Parse(Console.ReadLine());
                        Console.Clear();
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    switch (choise)
                    {
                        case 1:
                            user = new LogIn().SetAndCheckData();
                            break;
                        case 2:
                            new SignUp().CreateUser();
                            break;
                    }
                    Console.Write("Нажмите чтобы продолжить: ");
                    Console.ReadKey();
                } while (user == null || user.EnumStatus != EnumStatus.Online);

                do
                {
                    Console.Clear();
                    user = new MainInteraction(user).WrapperInteraction();
                    Console.Write("Нажмите чтобы продолжить: ");
                    Console.ReadKey();
                } while (user != null && user.EnumStatus == EnumStatus.Online);
            } while (true);
        }
    }
}