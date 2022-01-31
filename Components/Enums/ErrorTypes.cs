namespace Customer.Components.Enums
{
    /// <summary>
    /// The error type.
    /// </summary>
    public enum ErrorTypes
    {
        /// <summary>
        /// The ok
        /// </summary>
        OK = 0,

        #region 1-99 range Generic Validator

        /// <summary>
        /// The invalid paging
        /// </summary>
        InvalidPaging = 1,

        /// <summary>
        /// The invalid sort field name
        /// </summary>
        InvalidSortField = 2,

        /// <summary>
        /// The invalid date time
        /// </summary>
        InvalidDate = 3,

        /// <summary>
        /// The invalid enum name
        /// </summary>
        InvalidEnumName = 4,

        /// <summary>
        /// The identifier mismatch
        /// </summary>
        IdentifierMismatch = 5,

        #endregion

        #region 100-199 range Customer Validator

        /// <summary>
        /// The User Not Found
        /// </summary>
        CustomerNotFound = 100,

        /// <summary>
        /// The name is invalid
        /// </summary>
        InvalidName = 101,

        /// <summary>
        /// The password is invalid
        /// </summary>
        InvalidPassword = 102,

        /// <summary>
        /// The id is invalid
        /// </summary>
        InvalidIdentifier = 103,

        /// <summary>
        /// The invalid address line
        /// </summary>
        InvalidAddressLine = 104,

        /// <summary>
        /// The invalid city
        /// </summary>
        InvalidCity = 105,

        /// <summary>
        /// The invalid state
        /// </summary>
        InvalidState = 106,

        /// <summary>
        /// Cannot Delete User
        /// </summary>
        CannotDeleteUser = 107


        #endregion
    }
}
