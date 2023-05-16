using Server;
using Server.Models;
using Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Worker
    {
        public Server.Models.Worker worker { get; set; }
        public User User { get; set; }
        public Worker(User user) => User = user;
        public User Interaction()
        {
            worker = new WorkerService().FoundWorker(User);
            switch (worker.JobTitle)
            {
                case "Realtor":
                    User = InteractionRealtor();
                    break;
                case "Accountant":
                    User = InteractionАccountant();
                    break;
                case "Director":
                    User = InteractionDirector();
                    break;
                default:
                    new UserService().Exit(User.IDUser);
                    User = null;
                    break;
            }
            return User;
        }
        public User InteractionRealtor()
        {
            Console.Write(new ModelMenu() 
            {
                lines = new List<string>() 
                {
                    "Смена",
                    "Лист заказов",
                    "Список услуг",
                    "Обработать договор на печать",
                    "Просмотр информации о себе",
                    "Выход"
                },
                IsChoise = true 
            }.GetMenu());
            int choise = 0;
            try
            {
                choise = int.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return User;
            }
            switch (choise)
            {
                case 1:
                    ShiftInteraction();
                    break;
                case 2:
                    break;
                case 3:
                    ServiceInteraction();
                    break;
                case 4:
                    break;
                case 5:
                    Console.WriteLine(new WorkerService().GetUserWorker(User.IDUser));
                    break;
                case 6:
                    new UserService().Exit(User.IDUser);
                    User = null;
                    break;
                default:
                    break;
            }
            return User;
        }
        public User InteractionDirector()
        {
            Console.Write(new ModelMenu()
            {
                lines = new List<string>()
                {
                    "Смена",
                    "Взаимодействие с сотрудниками",
                    "Бухгалтерия",
                    "Список заказов",
                    "Список услуг",
                    "Обработать договор на печать",
                    "Просмотр информации о себе",
                    "Выход"
                },
                IsChoise = true
            }.GetMenu());
            int choise = 0;
            try
            {
                choise = int.Parse(Console.ReadLine());
            } catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return User;
            }
            switch (choise)
            {
                case 1:
                    ShiftInteraction();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    ServiceInteraction();
                    break;
                case 6:
                    break;
                case 7:
                    Console.WriteLine(new WorkerService().GetUserWorker(User.IDUser));
                    break;
                case 8:
                    new UserService().Exit(User.IDUser);
                    User = null;
                    break;
                default:
                    break;
            }
            return User;
        }
        public User InteractionАccountant()
        {
            Console.Write(new ModelMenu()
            {
                lines = new List<string>()
                {
                    "Смена",
                    "Бухгалтерия",
                    "Список услуг",
                    "Просмотр информации о себе",
                    "Выход"
                },
                IsChoise = true
            }.GetMenu());
            int choise = 0;
            try
            {
                choise = int.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return User;
            }
            switch (choise)
            {
                case 1:
                    ShiftInteraction();
                    break;
                case 2:
                    break;
                case 3:
                    ServiceInteraction();
                    break;
                case 4:
                    Console.WriteLine(new WorkerService().GetUserWorker(User.IDUser));
                    break;
                case 5:
                    new UserService().Exit(User.IDUser);
                    User = null;
                    break;
                default:
                    break;
            }
            return User;
        }
        public void ShiftInteraction()
        {
            Console.Write(new ModelMenu()
            {
                lines = new List<string>()
                {
                    "Начать смену",
                    "Закончить смену",
                    "Назад"
                },
                IsChoise = true,
                ChoiseText = "Выберите действие"
            }.GetMenu());
            int choiseShift = 0;
            try
            {
                choiseShift = int.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return;
            }
            switch (choiseShift)
            {
                case 1:
                    new ShiftService(worker.IDWorker).StartShift();
                    break;
                case 2:
                    new ShiftService(worker.IDWorker).StopShift();
                    break;
                case 3:
                default:
                    break;
            }
        }
        public void ServiceInteraction()
        {
            var services = new ServiceService();
            services.PrintList(null);
            if (worker.RightCRUAccountant)
            {
                Console.Write(new ModelMenu()
                {
                    lines = new List<string>()
                        {
                            "Создать",
                            "Изменить",
                            "Назад"
                        },
                    IsChoise = true,
                    ChoiseText = "Что бы вы хотели сделать с сервисами"
                }.GetMenu());
                int choiseService = 0;
                try
                {
                    choiseService = int.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                    return;
                }
                switch (choiseService)
                {
                    case 1:
                        var newService = new Service();
                        Console.Write("Введите название сервиса: ");
                        newService.NameService = Console.ReadLine();
                        Console.Write("Введите тип сервиса: ");
                        newService.TypeService = Console.ReadLine();
                        Console.Write("Введите описание сервиса: ");
                        newService.DescriptionService = Console.ReadLine();
                        Console.Write("Введите цену за сервис(можно в процентах от продажи/покупки): ");
                        newService.PriceService = double.Parse(Console.ReadLine());
                        new ServiceService().Create(newService);
                        break;
                    case 2:
                        Console.Write("Введите идентификатор сервиса: ");
                        int IDService = int.Parse(Console.ReadLine());
                        Console.WriteLine("Если не хотите изменять тот или иной параметр - оставьте строку пустой.");
                        Console.Write("Изменение название: ");
                        string newNameService = Console.ReadLine();
                        Console.Write("Изменение типа сервиса: ");
                        string newTypeService = Console.ReadLine();
                        Console.Write("Изменение описания: ");
                        string newDescription = Console.ReadLine();
                        Console.Write("Изменение типа сервиса: ");
                        string newPrice = Console.ReadLine();
                        new ServiceService().Change(IDService, newPrice, newTypeService, newDescription, newNameService);
                        break;
                    case 3:
                    default:
                        break;
                }
            }
        }
    }
}
