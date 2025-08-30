// 代码生成时间: 2025-08-31 06:59:14
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DataValidation
{
    /// <summary>
    /// Represents a form data validator that uses Entity Framework data annotations for validation.
    /// </summary>
    public class FormDataValidator
    {
        /// <summary>
        /// Validates the form data model using the Entity Framework data annotations.
        /// </summary>
        /// <typeparam name="T">The type of the form data model.</typeparam>
        /// <param name="model">The form data model to be validated.</param>
        /// <returns>A ValidationResult indicating the success or failure of the validation.</returns>
        public ValidationResult ValidateFormData<T>(T model) where T : class, new()
        {
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            // Perform validation
            bool isValid = Validator.TryValidateObject(model, context, results, true);

            // If validation fails, return the first error message
            if (!isValid)
            {
                return new ValidationResult(results.First().ErrorMessage);
            }

            // If validation passes, return a success result
            return ValidationResult.Success;
        }
    }
}
