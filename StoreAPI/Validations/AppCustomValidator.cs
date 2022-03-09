using FluentValidation.Results;
using StoreAPI.Infra;

namespace StoreAPI.Validations
{
    /// <summary>
    /// Validations for primitive types, usually parameters
    /// </summary>
    public static class AppCustomValidator
    {
        public static ValidationResult ValidateId(this ValidationResult source, int id, string name = "Id")
        {
            source.GreaterThanOrEqualTo(id, 1, name);

            return source;
        }

        public static ValidationResult GreaterThanOrEqualTo(this ValidationResult source, int value, int comparingTo, string valueName = "Value")
        {
            if (value < comparingTo)
            {
                var failure = new ValidationFailure(valueName, $"{valueName} must be greater than or equal to {comparingTo}", value);
                source.Errors.Add(failure);
            }

            return source;
        }


        public static ValidationResult GreaterThan(this ValidationResult source, int value, int comparingTo, string valueName = "Value")
        {
            if (value <= comparingTo)
            {
                var failure = new ValidationFailure(valueName, $"{valueName} must be greater than {comparingTo}", value);
                source.Errors.Add(failure);
            }

            return source;
        }


        public static ValidationResult LessThan(this ValidationResult source, int value, int comparingTo, string valueName = "Value")
        {
            if (value > comparingTo)
            {
                var failure = new ValidationFailure(valueName, $"{valueName} must be less than {comparingTo}", value);
                source.Errors.Add(failure);
            }
            return source;
        }


        public static ValidationResult InclusiveBetween(this ValidationResult source, int value, int min, int max, string propertyName = "Value")
        {
            if (value < min || value > max)
            {
                var failure = new ValidationFailure(propertyName, $"{propertyName} must be between {min} and {max}", value);
                source.Errors.Add(failure);
            }

            return source;
        }


        public static ValidationResult NotNullOrEmpty(this ValidationResult source, string text, string propertyName = "Property")
        {
            if (string.IsNullOrEmpty(text))
            {
                var failure = new ValidationFailure(propertyName, $"{propertyName} cannot be null or empty");
                source.Errors.Add(failure);
            }

            return source;
        }


        public static ValidationResult ValidatePassword(this ValidationResult source, string password, string propertyName = "Password", bool nullable = false)
        {
            source.InclusiveBetween(password.Length, AppConstants.Validations.User.PasswordMinLength, AppConstants.Validations.User.PasswordMaxLength, propertyName);

            if (!nullable)
            {
                source.NotNullOrEmpty(password, propertyName);
            }

            return source;
        }


        public static ValidationResult ValidateRoleName(this ValidationResult source, string roleName, string propertyName = "Role Name")
        {
            source
                .NotNullOrEmpty(roleName, propertyName)
                .LessThan(roleName.Length, AppConstants.Validations.Role.NameMaxLength, propertyName);

            return source;
        }


        public static ValidationResult ValidateUserName(this ValidationResult source, string userName, string propertyName = "Username")
        {
            source
                .NotNullOrEmpty(userName, propertyName)
                .LessThan(userName.Length, AppConstants.Validations.Role.NameMaxLength, propertyName);

            return source;
        }
    }
}