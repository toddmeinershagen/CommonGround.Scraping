using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommonGround.Scraping.Service.Models
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ValidateObjectAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            var items = value as Array;
            var compositeResults = new CompositeValidationResult($"{validationContext.DisplayName} field is not valid.");

            if (items != null)
            {
                foreach (var item in items)
                {
                    var context = new ValidationContext(item, null, null);
                    Validator.TryValidateObject(item, context, results, true);

                    if (results.Count != 0)
                    {
                        results.ForEach(compositeResults.AddResult);
                    }
                }

                if (results.Count != 0)
                {
                    return compositeResults;
                }
            }
            else if (value != null)
            {
                var context = new ValidationContext(value, null, null);
                Validator.TryValidateObject(value, context, results, true);

                if (results.Count != 0)
                {
                    results.ForEach(compositeResults.AddResult);
                    return compositeResults;
                }
            }

            return ValidationResult.Success;
        }
    }
}