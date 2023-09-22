using RealEstateAgency.Models;
using RealEstateAgency.Services.Models;
using RealEstateAgency.Services.Models.ServicesModels;

namespace RealEstateAgency.Services.Validators
{
    public static class ValidatorServiceCreateRequest
    {
        public static BaseResponse CheckValidation(this ServiceCreateRequest service)
        {
            var checkNameService = CheckValidationNameService(service);
            if (!checkNameService.IsSuccess) return checkNameService;
            var checkTypeService = CheckValidationTypeService(service);
            if (!checkTypeService.IsSuccess) return checkTypeService;
            var checkDescriptionService = CheckValidationDescriptionService(service);
            if (!checkDescriptionService.IsSuccess) return checkDescriptionService;
            var checkPriceService = CheckValidationPriceService(service);
            if (!checkPriceService.IsSuccess) return checkPriceService;

            return new BaseResponse { Message = "Проверка прошла успешно", IsSuccess = true };
        }
        private static BaseResponse CheckValidationNameService(ServiceCreateRequest service)
        {
            if (string.IsNullOrWhiteSpace(service.NameService))
                return new BaseResponse()
                {
                    Message = "Название услуги не может быть пустым!",
                    IsSuccess = false
                };
            else if (service.NameService.Length < 4 || service.NameService.Length > 60)
                return new BaseResponse()
                {
                    Message = "Размер имени услуги должна быть в диапозоне от 4-х до 60 символов.",
                    IsSuccess = false
                };

            return new BaseResponse() { IsSuccess = true };
        }
        private static BaseResponse CheckValidationTypeService(ServiceCreateRequest service)
        {
            if (string.IsNullOrWhiteSpace(service.TypeService))
                return new BaseResponse()
                {
                    Message = "Тип услуги не может быть пустым",
                    IsSuccess = false
                };
            else if (service.TypeService.Length < 3 || service.TypeService.Length > 50)
                return new BaseResponse()
                {
                    Message = "Размер типа услуги должна быть в диапозоне от 3-х до 50 символов.",
                    IsSuccess = false
                };
            return new BaseResponse() { IsSuccess = true };
        }
        private static BaseResponse CheckValidationDescriptionService(ServiceCreateRequest service)
        {
            if (!string.IsNullOrWhiteSpace(service.DescriptionService) &&
                service.DescriptionService.Length > 500)
                return new BaseResponse()
                {
                    Message = "Описание не может превышать 500 символов. Сократите название и повторите попытку.",
                    IsSuccess = false
                };
            return new BaseResponse() { IsSuccess = true };
        }
        private static BaseResponse CheckValidationPriceService(ServiceCreateRequest service)
        {
            if (service.PriceService < 0)
                return new BaseResponse()
                {
                    Message = "Невозможно ставить цену в отрицательном ключе.",
                    IsSuccess = false
                };
            else if (service.PriceService > double.MaxValue || service.PriceService < double.MinValue)
                return new BaseResponse()
                {
                    Message = "Вы вышли за предел диапозона 32-х битного размера числа",
                    IsSuccess = false
                };
            return new BaseResponse() { IsSuccess = true };
        }
    }
}
