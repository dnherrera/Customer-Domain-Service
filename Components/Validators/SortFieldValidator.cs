using System.Reflection;
using Customer.Components.Enums;
using Customer.Components.Validators.Models;

namespace Customer.Components.Validators
{
    /// <summary>
    /// The sort field validator.
    /// </summary>
    public static class SortFieldValidator
    {
        /// <summary>
        /// Validates sorting field is belong with T or not.
        /// </summary>
        /// <typeparam name="T">The database model.</typeparam>
        /// <param name="sortingField">The sorting field.</param>
        /// <param name="validSortingField">The valid sorting field.</param>
        /// <returns>The error info object.</returns>
        public static ErrorInfo Validate<T>(string sortingField, out string validSortingField) where T : class
        {
            sortingField = string.IsNullOrWhiteSpace(sortingField) ? "Id" : sortingField;

            validSortingField = null;

            var e = new ErrorInfo();

            var propertyInfo = typeof(T).GetProperty(sortingField, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo is null)
            {
                e.ErrorCode = ErrorTypes.InvalidSortField;
                e.ErrorMessage = $"Sort field '{sortingField}' is invalid.";
                return e;
            }

            validSortingField = propertyInfo.Name;

            return e;
        }
    }
}
