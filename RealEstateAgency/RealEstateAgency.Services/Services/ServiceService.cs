using RealEstateAgency.DBMigrations;
using RealEstateAgency.Models;
using RealEstateAgency.Services.Models;
using RealEstateAgency.Services.Models.Pages;
using RealEstateAgency.Services.Models.ServicesModels;
using RealEstateAgency.Services.Validators;

namespace RealEstateAgency.Services.Services
{
    public class ServiceService
    {
        public BaseResponse Create(ServiceCreateRequest service)
        {
            var serviceValidation = service.CheckValidation();
            if (serviceValidation.IsSuccess) return serviceValidation;

            using var db = new DataBaseContext();
            db.Service.Add(
                new Service()
                {
                    NameService = service.NameService,
                    DescriptionService = service.DescriptionService,
                    TypeService = service.TypeService,
                    PriceService = service.PriceService
                });
            db.SaveChanges();
            return new BaseResponse() { IsSuccess = true, Message = "Услуга была успешно создана." };
        }
        public BaseResponse Change(int IDService, string? priceChange, string? typeChange, string? descriptionChange, string? nameChange)
        {
            using var db = new DataBaseContext();
            var service = db.Service.FirstOrDefault(s => s.IDService == IDService);
            if (service != null)
            {
                if (priceChange != null)
                {
                    if (string.IsNullOrWhiteSpace(priceChange))
                        return new BaseResponse() { IsSuccess = false, Message = "Невозможно ставить цену в отрицательном ключе." };
                    service.PriceService = Convert.ToDouble(priceChange);
                }
                if (!string.IsNullOrWhiteSpace(typeChange))
                    service.TypeService = typeChange;
                if (!string.IsNullOrWhiteSpace(descriptionChange))
                {
                    if (descriptionChange.Length > 500)
                        return new BaseResponse() { IsSuccess = false, Message = "Описание не может превышать 500 символов. Сократите название и повторите попытку." };
                    service.DescriptionService = descriptionChange;
                }
                if (!string.IsNullOrWhiteSpace(nameChange))
                {
                    if (nameChange.Length < 4 || nameChange.Length > 60)
                        return new BaseResponse() { IsSuccess = false, Message = "Размер имени услуги должна быть в диапозоне от 4-х до 60 символов." };
                    service.NameService = nameChange;
                }
                db.Service.Update(service);
                db.SaveChanges();
                return new BaseResponse() { IsSuccess = true, Message = "Услуга была успешно изменена." };
            }
            return new BaseResponse() { IsSuccess = false, Message = "Услуга не была найдена." };
        }
        public BaseResponse PrintList(string? typeService = null)
        {
            using var db = new DataBaseContext();
            var serviceСonditions = db.Service.AsQueryable();
            if (!string.IsNullOrWhiteSpace(typeService))
            {
                serviceСonditions = serviceСonditions.Where(s => s.TypeService == typeService);
                if (serviceСonditions.Count() == 0)
                    return new BaseResponse() { IsSuccess = false, Message = $"Услуг '{typeService}' не было найдено. Повторите попытку." };
            }
            serviceСonditions.ToList()
                    .ForEach(s => Console.WriteLine(GetService(s)));
            if (!string.IsNullOrWhiteSpace(typeService))
                return new BaseResponse() { IsSuccess = true, Message = $"Услуг '{typeService}' по вашему запросу было найденно {serviceСonditions.Count()} штук." };
            return new BaseResponse() { IsSuccess = true, Message = $"Всего услуг было найденно {serviceСonditions.Count()} штук." };
        }
        public List<string> GetListTypeService()
        {
            using var db = new DataBaseContext();
            var services = db.Service.ToList();
            var listServices = new List<string>();
            services.ForEach(s => listServices.Add(s.TypeService));
            return listServices;
        }
        public ServiceListResponse GetList(ServiceListRequest request)
        {
            using var db = new DataBaseContext();

            var servicesForConditions = db.Service.AsQueryable();

            if(!string.IsNullOrWhiteSpace(request.Search))
                servicesForConditions = servicesForConditions
                    .Where(s => s.NameService.Contains(request.Search)
                    || s.DescriptionService != null && s.DescriptionService.Contains(request.Search)
                    || s.TypeService.Contains(request.Search));
            if (request.PricesFrom != null)
                servicesForConditions = servicesForConditions
                    .Where(s => s.PriceService > request.PricesFrom);
            if(request.PricesTo != null)
                servicesForConditions = servicesForConditions
                    .Where(s => s.PriceService < request.PricesTo);

            int countServices = servicesForConditions.Count();

            servicesForConditions = servicesForConditions
                .Skip((request.Page!.Page!.Value - 1) * request.Page!.PageSize!.Value)
                .Take(request.Page!.PageSize!.Value);

            var services = servicesForConditions.Select(s => 
                new ServiceListItem()
                {
                    IDService = s.IDService,
                    NameService = s.NameService,
                    DescriptionService = s.DescriptionService,
                    TypeService = s.TypeService,
                    PriceService = s.PriceService
                }
            ).ToList();

            return new ServiceListResponse()
            {
                Items = services,
                Page = new PageResponse() 
                {
                    Page = request.Page!.Page!.Value,
                    PageSize = request.Page!.PageSize!.Value,
                    Count = countServices
                },
                IsSuccess = true
            };
        }
        public string GetService(Service service)
            => "=================================\n"
            + $"Услуга №{service.IDService} \n"
            + $"Название: {service.NameService} \n"
            + $"Тип: {service.TypeService} \n"
            + $"Описание: {service.DescriptionService}\n"
            + $"Цена за услугу: {service.PriceService}\n"
            + "=================================";
    }
}
