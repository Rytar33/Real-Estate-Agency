using RealEstateAgency.Models;
using RealEstateAgency.Services.Models;
using RealEstateAgency.Services.Models.Users;

namespace RealEstateAgency.Services.Validators
{
    public static class ValidatorUserCreateRequest
    {
        public static BaseResponse CheckValidation(this UserCreateRequest user)
        {
            var checkUserName = CheckValidationUserName(user);
            if (!checkUserName.IsSuccess) return checkUserName;
            var checkPassword = CheckValidationPassword(user);
            if (!checkPassword.IsSuccess) return checkPassword;
            var checkFirstName = CheckValidationFirstName(user);
            if (!checkFirstName.IsSuccess) return checkFirstName;
            var checkLastName = CheckValidationLastName(user);
            if (!checkLastName.IsSuccess) return checkLastName;
            var checkSecondName = CheckValidationSecondName(user);
            if (!checkSecondName.IsSuccess) return checkSecondName;
            var checkValidationEMail = CheckValidationEMail(user);
            if(!checkValidationEMail.IsSuccess) return checkValidationEMail;

            return new BaseResponse { Message = "Проверка прошла успешно", IsSuccess = true };
        }
        private static BaseResponse CheckValidationUserName(UserCreateRequest user)
        {
            if (string.IsNullOrWhiteSpace(user.Name))
                return new BaseResponse()
                {
                    Message = "Имя пользователя не должно быть пустым!",
                    IsSuccess = false
                };
            else if (user.Name.Length < 4 || user.Name.Length > 30)
                return new BaseResponse()
                {
                    Message = "Имя пользователя должно быть в диапозоне от 4-х до 30 символов.",
                    IsSuccess = false
                };

            return new BaseResponse() { IsSuccess = true };
        }
        private static BaseResponse CheckValidationPassword(UserCreateRequest user)
        {
            if (string.IsNullOrWhiteSpace(user.Password))
                return new BaseResponse()
                {
                    Message = "Пароль не может быть пустым!",
                    IsSuccess = false
                };
            else if (user.Password.Length < 5 || user.Password.Length > 25)
                return new BaseResponse()
                {
                    Message = "Пароль должен быть в диапозоне от 5-ти до 25 символов.",
                    IsSuccess = false
                };

            return new BaseResponse() { IsSuccess = true };
        }
        private static BaseResponse CheckValidationFirstName(UserCreateRequest user)
        {
            if (string.IsNullOrWhiteSpace(user.FirstName))
                return new BaseResponse()
                {
                    Message = "Имя не может быть пустым!",
                    IsSuccess = false
                };
            else if (user.FirstName.Length < 2 || user.FirstName.Length > 50)
                return new BaseResponse()
                {
                    Message = "Имя должно быть в диапозоне от 2-х до 50 символов.",
                    IsSuccess = false
                };

            return new BaseResponse() { IsSuccess = true };
        }
        private static BaseResponse CheckValidationLastName(UserCreateRequest user)
        {
            if (string.IsNullOrWhiteSpace(user.LastName))
                return new BaseResponse()
                {
                    Message = "Фамилия не может быть пусто, IsSuccess = false ",
                    IsSuccess = false
                };
            else if (user.LastName.Length < 2 || user.LastName.Length > 50)
                return new BaseResponse()
                {
                    Message = "Фамилия должна быть в диапозоне от 2-х до 50 символов.",
                    IsSuccess = false
                };
            return new BaseResponse() { IsSuccess = true };
        }
        private static BaseResponse CheckValidationSecondName(UserCreateRequest user)
        {
            if (!string.IsNullOrWhiteSpace(user.SecondName) &&
                (user.SecondName!.Length < 2 || user.SecondName.Length > 50))
                return new BaseResponse()
                {
                    Message = "Отчество должно быть в диапозоне от 2-х до 50 символов.",
                    IsSuccess = false
                };

            return new BaseResponse() { IsSuccess = true };
        }
        public static BaseResponse CheckValidationEMail(UserCreateRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
                return new BaseResponse()
                {
                    IsSuccess = false,
                    Message = "Почта не может быть пустой."
                };
            else if (!request.Email.Contains('@') || !request.Email.Contains('.'))
                return new BaseResponse()
                {
                    IsSuccess = false,
                    Message = "Неверный формат записи. Почта должна содержать знаки '@' и '.'"
                };
            else if (request.Email.Length < 15)
                return new BaseResponse()
                {
                    IsSuccess = false,
                    Message = "Почта не может быть менее 15 символов!"
                };
            else if (request.Email.Length > 311)
                return new BaseResponse()
                {
                    IsSuccess = false,
                    Message = "Почта не может превышать 311 символов!"
                };
            return new BaseResponse() { IsSuccess = true };
        }
    }
}
