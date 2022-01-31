using Customer.Components.Enums;
using Customer.Components.Validators.Models;

namespace Customer.Components.Validators
{
    public static class StateValidator
    {
        /// <summary>
        /// Validates the State
        /// </summary>
        /// <param name="state">The State.</param>
        /// <param name="validValue">The valid value.</param>
        /// <returns></returns>
        public static ErrorInfo Validate(string state, out string validValue)
        {
            validValue = null;

            var e = new ErrorInfo();

            if (string.IsNullOrWhiteSpace(state))
            {
                e.ErrorCode = ErrorTypes.InvalidState;
                e.ErrorMessage = "State is null or empty.";
                return e;
            }

            state = state.Trim();

            if (!string.IsNullOrWhiteSpace(state) && state.Length > 50)
            {
                e.ErrorCode = ErrorTypes.InvalidState;
                e.ErrorMessage = "State must not exceed to 50 characters";
                return e;
            }

            validValue = state;

            return e;
        }
    }
}
