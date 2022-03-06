using FluentValidation.Results;
using StoreAPI.Exceptions;
using StoreAPI.Infra;

namespace StoreAPI.Validations
{
    /// <summary>
    /// Validations for primitive types, usually parameters
    /// </summary>
    public static class AppCustomValidator
    {
        public static ValidationResult ValidateId(int id, string name = "Id")
        {
            var result = new ValidationResult()
                            .AddValidationResult(GreaterThanOrEqualTo(id, 1, name));

            return result;
        }

        public static ValidationResult GreaterThanOrEqualTo(int value, int comparingTo, string valueName = "Value")
        {
            var result = new ValidationResult();

            if (value < comparingTo)
            {
                result.Errors.Add(new ValidationFailure(valueName, $"{valueName} must be greater than or equal to {comparingTo}", value));

                //throw new AppValidationException()
                //.SetTitle("Validation error")
                //.SetDetail($"{valueName} must be greater than or equal to {comparingTo}");
            }

            return result;
        }


        public static void GreaterThan(int value, int comparingTo, string valueName = "Value")
        {
            var result = new ValidationResult();

            if (value <= comparingTo)
            {
                throw new AppValidationException()
                    .SetTitle("Validation error")
                    .SetDetail($"{valueName} must be greater than {comparingTo}");
            }
        }


        public static void LessThan(int value, int comparingTo, string valueName = "Value")
        {
            if (value > comparingTo)
            {
                throw new AppValidationException()
                    .SetTitle("Validation error")
                    .SetDetail($"{valueName} must be less than {comparingTo}");
            }
        }


        public static void InclusiveBetween(int value, int min, int max, string propertyName = "Value")
        {
            if (value < min || value > max)
            {
                throw new AppValidationException()
                    .SetTitle("Validation error")
                    .SetDetail($"{propertyName} must be between {min} and {max}");
            }
        }


        public static void NotNullOrEmpty(string text, string propertyName = "Property")
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new AppValidationException()
                    .SetTitle("Validation error")
                    .SetDetail($"{propertyName} cannot be null or empty");
            }
        }


        public static void ValidatePassword(string password, string propertyName = "Password", bool nullable = false)
        {
            if (!nullable)
            {
                NotNullOrEmpty(password, propertyName);
            }

            InclusiveBetween(password.Length, AppConstants.Validations.User.PasswordMinLength, AppConstants.Validations.User.PasswordMaxLength, propertyName);
        }


        public static void ValidateRoleName(string roleName, string propertyName = "Role Name")
        {
            NotNullOrEmpty(roleName, propertyName);

            LessThan(roleName.Length, AppConstants.Validations.Role.NameMaxLength, propertyName);
        }


        public static void ValidateUserName(string userName, string propertyName = "Username")
        {
            NotNullOrEmpty(userName, propertyName);

            LessThan(userName.Length, AppConstants.Validations.Role.NameMaxLength, propertyName);
        }
    }
}