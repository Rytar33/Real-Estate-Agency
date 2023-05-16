using Microsoft.IdentityModel.Tokens;
using Server.Context;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    public class ServiceService
    {
        public BaseResponse Create(Service service)
        {
            if (service.NameService.Length < 4 || service.NameService.Length > 60)
                return new BaseResponse() { IsSuccess = false, Message = "Размер имени услуги должна быть в диапозоне от 4-х до 60 символов." };
            if (service.TypeService.Length < 3 || service.TypeService.Length < 50)
                return new BaseResponse() { IsSuccess = false, Message = "Размер типа услуги должна быть в диапозоне от 3-х до 50 символов." };
            if (service.DescriptionService.Length > 500)
                return new BaseResponse() { IsSuccess = false, Message = "Описание не может превышать 500 символов. Сократите название и повторите попытку." };
            if (service.PriceService < 0)
                return new BaseResponse() { IsSuccess = false, Message = "Невозможно ставить цену в отрицательном ключе." };
            
            using var db = new DataBaseContext();
            db.Service.Add(service);
            db.SaveChanges();
            return new BaseResponse() { IsSuccess = true, Message = "Услуга была успешно создана." };
        }
        public BaseResponse Change(int IDService, string? priceChange, string? typeChange, string? descriptionChange, string? nameChange) 
        {
            using var db = new DataBaseContext();
            var service = db.Service.FirstOrDefault(s => s.IDService == IDService);
            if(service != null)
            {
                if(priceChange != null)
                {
                    if(priceChange.IsNullOrEmpty())
                        return new BaseResponse() { IsSuccess = false, Message = "Невозможно ставить цену в отрицательном ключе." };
                    service.PriceService = Convert.ToDouble(priceChange);
                }
                if(!typeChange.IsNullOrEmpty())
                    service.TypeService = typeChange;
                if (!descriptionChange.IsNullOrEmpty())
                {
                    if (descriptionChange.Length > 500)
                        return new BaseResponse() { IsSuccess = false, Message = "Описание не может превышать 500 символов. Сократите название и повторите попытку." };
                    service.DescriptionService = descriptionChange;
                }
                if (!nameChange.IsNullOrEmpty())
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
        public BaseResponse PrintList(string? typeService)
        {
            using var db = new DataBaseContext();
            var serviceСonditions = db.Service.AsQueryable();
            if (typeService != null)
            {
                if(serviceСonditions.Count() == 0)
                    return new BaseResponse() { IsSuccess = false, Message = $"Услуг '{typeService}' не было найдено. Повторите попытку." };
                serviceСonditions = serviceСonditions.Where(s => s.TypeService == typeService);
            }
            serviceСonditions.ToList()
                    .ForEach(s => Console.WriteLine(
                       "=================================\n"
                    + $"Услуга №{s.IDService} \n"
                    + $"Название: {s.NameService} \n"
                    + $"Тип: {s.TypeService} \n"
                    + $"Описание: {s.DescriptionService}\n"
                    + $"Цена за услугу: {s.PriceService}\n"
                    + "================================="));
            if(!typeService.IsNullOrEmpty())
                return new BaseResponse() { IsSuccess = true, Message = $"Услуг '{typeService}' по вашему запросу было найденно {serviceСonditions.Count()} штук." };
            return new BaseResponse() { IsSuccess = true, Message = $"Всего услуг было найденно {serviceСonditions.Count()} штук." };
        }
    }
}
