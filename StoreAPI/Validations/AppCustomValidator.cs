using FluentValidation.Results;
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
                            .AddValidationFailuresFrom(GreaterThanOrEqualTo(id, 1, name));

            return result;
        }

        public static ValidationResult GreaterThanOrEqualTo(int value, int comparingTo, string valueName = "Value")
        {
            var result = new ValidationResult();

            if (value < comparingTo)
            {
                var failure = new ValidationFailure(valueName, $"{valueName} must be greater than or equal to {comparingTo}", value);
                result.Errors.Add(failure);
            }

            return result;
        }


        public static ValidationResult GreaterThan(int value, int comparingTo, string valueName = "Value")
        {
            var result = new ValidationResult();

            if (value <= comparingTo)
            {
                var failure = new ValidationFailure(valueName, $"{valueName} must be greater than {comparingTo}", value);
                result.Errors.Add(failure);
            }

            return result;
        }


        public static ValidationResult LessThan(int value, int comparingTo, string valueName = "Value")
        {
            var result = new ValidationResult();

            if (value > comparingTo)
            {
                var failure = new ValidationFailure(valueName, $"{valueName} must be less than {comparingTo}", value);
                result.Errors.Add(failure);
            }

            return result;
        }


        public static ValidationResult InclusiveBetween(int value, int min, int max, string propertyName = "Value")
        {
            var result = new ValidationResult();

            if (value < min || value > max)
            {
                var failure = new ValidationFailure(propertyName, $"{propertyName} must be between {min} and {max}", value);
                result.Errors.Add(failure);
            }

            return result;
        }


        public static ValidationResult NotNullOrEmpty(string text, string propertyName = "Property")
        {
            var result = new ValidationResult();

            if (string.IsNullOrEmpty(text))
            {
                var failure = new ValidationFailure(propertyName, $"{propertyName} cannot be null or empty");
                result.Errors.Add(failure);
            }


            return result;
        }


        public static ValidationResult ValidatePassword(string password, string propertyName = "Password", bool nullable = false)
        {
            var result = new ValidationResult();
            var nullResult = new ValidationResult();

            if (!nullable)
            {
                nullResult = NotNullOrEmpty(password, propertyName);
            }

            var rangeResult = InclusiveBetween(password.Length, AppConstants.Validations.User.PasswordMinLength, AppConstants.Validations.User.PasswordMaxLength, propertyName);

            result.AddValidationFailuresFrom(nullResult);
            result.AddValidationFailuresFrom(rangeResult);

            return result;
        }


        public static ValidationResult ValidateRoleName(string roleName, string propertyName = "Role Name")
        {
            var result = new ValidationResult();

            var nullResult = NotNullOrEmpty(roleName, propertyName);
            var lessResult = LessThan(roleName.Length, AppConstants.Validations.Role.NameMaxLength, propertyName);

            result.AddValidationFailuresFrom(nullResult);
            result.AddValidationFailuresFrom(lessResult);

            return result;
        }


        public static ValidationResult ValidateUserName(string userName, string propertyName = "Username")
        {
            var result = new ValidationResult();

            var nullResult = NotNullOrEmpty(userName, propertyName);
            var lessResult = LessThan(userName.Length, AppConstants.Validations.Role.NameMaxLength, propertyName);

            result.AddValidationFailuresFrom(nullResult);
            result.AddValidationFailuresFrom(lessResult);

            return result;
        }
    }
}