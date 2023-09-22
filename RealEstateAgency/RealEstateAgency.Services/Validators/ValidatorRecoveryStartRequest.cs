using RealEstateAgency.Services.Models;
using RealEstateAgency.Services.Models.Recoverys;

namespace RealEstateAgency.Services.Validators
{
    public static class ValidatorRecoveryStartRequest
    {
        public static BaseResponse CheckValidation(this RecoveryStartRequest recoveryStart)
        {

            return new BaseResponse { IsSuccess = true };
        }
    }
}
