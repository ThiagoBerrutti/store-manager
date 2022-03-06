using FluentValidation.Results;

namespace StoreAPI.Validations
{
    public static class TryAddValidationResultErrorsExtension
    {
        /// <summary>
        /// If a ValidationResult is invalid, adds every failure from it
        /// </summary>
        /// <param name="result">ValidationResult where the errors will be add</param>
        /// <param name="validationResult">Validation result that can be get the failures from</param>
        /// <returns></returns>
        public static ValidationResult AddValidationFailuresFrom(this ValidationResult result, ValidationResult validationResult)
        {
            if (!validationResult.IsValid)
            {
                foreach (var f in validationResult.Errors)
                {
                    result.Errors.Add(f);
                }
            }

            return result;
        }
    }
}