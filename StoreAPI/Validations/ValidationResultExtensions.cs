using FluentValidation.Results;

namespace StoreAPI.Validations
{
    public static class TryAddValidationResultErrorsExtension
    {
        /// <summary>
        /// If a ValidationResult is invalid, adds every failure from it
        /// </summary>
        /// <param name="source">ValidationResult where the errors will be add</param>
        /// <param name="validationResult">Validation result that can be get the failures from</param>
        /// <returns></returns>
        public static ValidationResult AddFailuresFrom(this ValidationResult source, ValidationResult validationResult)
        {
            //source. = validationResult.

            if (!validationResult.IsValid)
            {
                foreach (var f in validationResult.Errors)
                {
                    source.Errors.Add(f);
                }
            }

            return source;
        }
    }
}