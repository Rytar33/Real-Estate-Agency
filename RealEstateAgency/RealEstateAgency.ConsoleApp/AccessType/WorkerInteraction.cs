using RealEstateAgency.Models;
using RealEstateAgency.Services.Models.ServicesModels;
using RealEstateAgency.Services.Services;

namespace RealEstateAgency.ConsoleApp.AccessType
{
    public class WorkerInteraction
    {
        public Worker worker { get; set; }
        public User User { get; set; }
        public WorkerInteraction(User user) => User = user;
        public User? Interaction()
        {
            worker = new WorkerService().FoundWorker(User)!;
            switch (worker.JobTitle)
            {
                case Enums.EnumWorkerRanked.Realtor:
                    User = InteractionRealtor()!;
                    break;
                case Enums.EnumWorkerRanked.Аccountant:
                    User = InteractionАccountant()!;
                    break;
                case Enums.EnumWorkerRanked.Director:
                    User = InteractionDirector()!;
                    break;
                default:
                    new UserService().Exit(User.IDUser);
                    User = null!;
                    break;
            }
            return User;
        }
        public User? InteractionRealtor()
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
            if(!int.TryParse(Console.ReadLine(), out choise))
            {
                Console.WriteLine($"Ошибка: Вы ввели не числовое значение.");
                return User;
            }
            switch (choise)
            {
                case 1:
                    ShiftInteraction();
                    break;
                case 2:
                    InteractionOrders();
                    break;
                case 3:
                    ServiceInteraction();
                    break;
                case 4:
                    Console.Write("Введите по какому номеру заказа вы бы хотели обработать договор: ");
                    int IDOrder = int.Parse(Console.ReadLine()!);
                    new OrderService().CreateWordFile(IDOrder);
                    break;
                case 5:
                    Console.WriteLine(new WorkerService().GetUserWorker(User.IDUser));
                    break;
                case 6:
                    new UserService().Exit(User.IDUser);
                    User = null!;
                    break;
            }
            return User;
        }
        public User? InteractionDirector()
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
            if (!int.TryParse(Console.ReadLine(), out choise))
            {
                Console.WriteLine($"Ошибка: Вы ввели не числовое значение.");
                return User;
            }
            switch (choise)
            {
                case 1:
                    ShiftInteraction();
                    break;
                case 2:
                    Console.Write(new ModelMenu()
                    {
                        lines = new List<string>()
                        {
                            "Добавить сотрудника",
                            "Уволить сотрудника",
                            "Изменить статусы у сотрудника",
                            "Вывести всех сотрудников",
                            "Назад"
                        },
                        IsChoise = true, ChoiseText = "Выберите какое действие вы хотите совершить"
                    }.GetMenu());
                    int choiseInteraction = 0;
                    if (int.TryParse(Console.ReadLine(), out choiseInteraction))
                    {
                        switch (choiseInteraction)
                        {
                            case 1:
                                Console.Write("Введите индентификатор пользователя которого вы бы хотели добавить: ");
                                int IDUser = int.Parse(Console.ReadLine()!);
                                var response = new WorkerService().AddWorker(IDUser);
                                if (!response.IsSuccess)
                                    Console.WriteLine(response.Message);
                                break;
                            case 2:
                                Console.Write("Введите индентификатор сотрудника которого вы бы хотели уволить: ");
                                int IDWorker = int.Parse(Console.ReadLine()!);
                                var response2 = new WorkerService().FireAnEmployee(IDWorker);
                                if (!response2.IsSuccess)
                                    Console.WriteLine(response2.Message);
                                break;
                            case 3:
                                Console.Write("Введите индентификатор сотрудника: ");
                                break;
                            case 4:
                                new WorkerService().GetList().ForEach(w => Console.WriteLine(new WorkerService().GetUserWorker(w.UserIDUser)));
                                break;
                        }
                    }
                    else Console.WriteLine("Осторожно! Вы ввели не числовое значение!");
                    break;
                case 3:
                    Accountant();
                    break;
                case 4:
                    InteractionOrders();
                    break;
                case 5:
                    ServiceInteraction();
                    break;
                case 6:
                    Console.Write("Введите по какому номеру заказа вы бы хотели обработать договор: ");
                    int IDOrder = int.Parse(Console.ReadLine()!);
                    if (!int.TryParse(Console.ReadLine(), out IDOrder))
                    {
                        Console.WriteLine($"Ошибка: Вы ввели не числовое значение.");
                        return User;
                    }
                    new OrderService().CreateWordFile(IDOrder);
                    break;
                case 7:
                    Console.WriteLine(new WorkerService().GetUserWorker(User.IDUser));
                    break;
                case 8:
                    new UserService().Exit(User.IDUser);
                    User = null!;
                    break;
            }
            return User;
        }
        public User? InteractionАccountant()
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
            if (!int.TryParse(Console.ReadLine(), out choise))
            {
                Console.WriteLine($"Ошибка: Вы ввели не числовое значение.");
                return User;
            }
            else
            {
                Console.Clear();
                switch (choise)
                {
                    case 1:
                        ShiftInteraction();
                        break;
                    case 2:
                        Accountant();
                        break;
                    case 3:
                        ServiceInteraction();
                        break;
                    case 4:
                        Console.WriteLine(new WorkerService().GetUserWorker(User.IDUser));
                        break;
                    case 5:
                        new UserService().Exit(User.IDUser);
                        User = null!;
                        break;
                }
            }
            return User;
        }
        private void ShiftInteraction()
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
            if (!int.TryParse(Console.ReadLine(), out choiseShift))
            {
                Console.WriteLine($"Ошибка: Вы ввели не числовое значение.");
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
            }
        }
        private void ServiceInteraction()
        {
            var services = new ServiceService();
            services.GetListTypeService().ForEach(ts => Console.WriteLine(ts));
            Console.Write("Выберите по какому типу услуг вы бы хотел отсортировать: ");
            string typeService = Console.ReadLine()!;
            services.PrintList(typeService);
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
                if (!int.TryParse(Console.ReadLine(), out choiseService))
                {
                    Console.WriteLine($"Ошибка: Вы ввели не числовое значение.");
                    return;
                }
                switch (choiseService)
                {
                    case 1:
                        Console.Write("Введите название сервиса: ");
                        string name = Console.ReadLine()!;
                        Console.Write("Введите тип сервиса: ");
                        string type = Console.ReadLine()!;
                        Console.Write("Введите описание сервиса: ");
                        string description = Console.ReadLine()!;
                        Console.Write("Введите цену за сервис(можно в процентах от продажи/покупки): ");
                        double price = double.Parse(Console.ReadLine()!);
                        var response = new ServiceService().Create(
                            new ServiceCreateRequest()
                            {
                                NameService = name,
                                TypeService = type,
                                DescriptionService = description,
                                PriceService = price
                            });
                        Console.WriteLine(response.Message);
                        break;
                    case 2:
                        Console.Write("Введите идентификатор сервиса: ");
                        int IDService = int.Parse(Console.ReadLine()!);
                        Console.WriteLine("Если не хотите изменять тот или иной параметр - оставьте строку пустой.");
                        Console.Write("Изменение название: ");
                        string newNameService = Console.ReadLine()!;
                        Console.Write("Изменение типа сервиса: ");
                        string newTypeService = Console.ReadLine()!;
                        Console.Write("Изменение описания: ");
                        string newDescription = Console.ReadLine()!;
                        Console.Write("Изменение цену за услугу: ");
                        string newPrice = Console.ReadLine()!;
                        new ServiceService().Change(IDService, newPrice, newTypeService, newDescription, newNameService);
                        break;
                }
            }
        }
        private void InteractionOrders()
        {
            int sizePageOrders = 5;
            string searchOrders = "";
            bool? isOrderAccepted = null;
            int? IDClient = null;
            int? IDWorker = worker.IDWorker;

            new PagesInteraction(sizePageOrders)
            {
                user = User,
                worker = worker
            }
            .OrdersChekcList(searchOrders, IDClient, IDWorker, isOrderAccepted);
        }
        private void Accountant()
        {
            if (!worker.RightCRUAccountant)
            {
                Console.WriteLine("У вас нет прав на взаимодействие с бухгалтерией.");
                return;
            }
            bool isWork = true;
            do
            {
                Console.Clear();
                Console.Write(new ModelMenu()
                {
                    lines = new List<string>()
                    {
                        "Вывести список заработной платы за месяц сотрудников",
                        "Создать отчёт о сотруднике",
                        "Назад"
                    }, IsChoise = true
                }.GetMenu());
                int choiseService = 0;
                if (!int.TryParse(Console.ReadLine(), out choiseService))
                {
                    Console.WriteLine($"Ошибка: Вы ввели не числовое значение.");
                    continue;
                }
                var listAllWorkers = new WorkerService().GetList();
                switch (choiseService)
                {
                    case 1:
                        foreach (var worker in listAllWorkers)
                        {
                            var salarysWorker = new SalaryWorkerService().GetSalarysWorker(worker.IDWorker);
                            foreach (var workerSalary in salarysWorker)
                            {
                                Console.WriteLine(new SalaryWorkerService().GetSalaryWorkerForMonth(workerSalary));
                            }
                        }
                        break;
                    case 2:
                        Console.Write("\nID`s: ");
                        listAllWorkers.ForEach(w => Console.Write($"{w.IDWorker} "));
                        Console.Write("\nВведите индентификатор сотрудника: ");
                        int idWorker = int.Parse(Console.ReadLine()!);
                        Console.Write("Введите год: ");
                        int year = int.Parse(Console.ReadLine()!);
                        Console.Write("Введите месяц: ");
                        int month = int.Parse(Console.ReadLine()!);
                        Console.Write("Введите фиксированную зп: ");
                        int salary = int.Parse(Console.ReadLine()!);
                        Console.Write("Введите процент премии: ");
                        int premiumPr = int.Parse(Console.ReadLine()!);
                        Console.Write("Введите план по рабочим дням: ");
                        int planDaysWorked = int.Parse(Console.ReadLine()!);
                        Console.Write("Введите план по продажам: ");
                        int planSales = int.Parse(Console.ReadLine()!);
                        Console.Write("Введите процент подоходного налога: ");
                        int incomeTaxPr = int.Parse(Console.ReadLine()!);
                        new SalaryWorkerService().AddSalaryWorkers(idWorker, year, month, salary, planDaysWorked, premiumPr, planSales, incomeTaxPr);
                        break;
                    case 3:
                        isWork = false;
                        break;
                }
                Console.Write("Нажмите чтобы продолжить: ");
                Console.ReadKey();
            } while (isWork);
        }
    }
}
