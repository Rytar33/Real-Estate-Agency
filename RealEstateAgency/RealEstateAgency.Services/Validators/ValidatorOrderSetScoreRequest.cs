using Azure.Core;
using RealEstateAgency.Services.Models;
using RealEstateAgency.Services.Models.Orders;

namespace RealEstateAgency.Services.Validators
{
    public static class ValidatorOrderSetScoreRequest
    {
        public static BaseResponse CheckValidation(this OrderSetScoreRequest order)
        {
            if (order.Score < 1 || order.Score > 10) 
                return new BaseResponse()
                {
                    IsSuccess = false,
                    Message = "Оценка должна быть в диапозоне от 1 до 10 баллов."
                };

            return new BaseResponse() { IsSuccess = true };
        }
    }
}
