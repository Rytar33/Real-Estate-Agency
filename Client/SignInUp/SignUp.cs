using Server.Models;
using Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.SignInUp
{
    public class SignUp
    {
        public void CreateUser(Server.Models.Worker worker = null)
        {
            User user = new User();
            Console.Write("Имя пользователя: ");
            user.UserName = Console.ReadLine()!;
            Console.Write("Пароль пользователя: ");
            user.Password = Console.ReadLine()!;
            Console.Write("Ваше имя: ");
            user.FirstName = Console.ReadLine()!;
            Console.Write("Ваша фамилия: ");
            user.LastName = Console.ReadLine()!;
            Console.Write("Ваша отчество(не обязательно): ");
            user.SecondName = Console.ReadLine();
            if (worker != null && worker.RightChangeWorkers)
                user.TypeAccount = "Worker";
            else user.TypeAccount = "Client";

            var responseFromServer = new UserService().SignUp(user);
            if (user.TypeAccount == "Client")
                new ClientService().CreateClient(user);
            else if (user.TypeAccount == "Worker")
                new WorkerService().CreateWorker(user);
            Console.WriteLine(responseFromServer.Message);
        }
    }
}
