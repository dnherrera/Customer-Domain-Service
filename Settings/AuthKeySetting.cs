namespace CustomerAPI.Settings
{
    /// <summary>
    /// Auth Key Setting
    /// </summary>
    public class AuthKeySetting
    {
        /// <summary>
        /// Set up the Section Name
        /// </summary>
        public const string SectionName = "AuthKeySetting";

        /// <summary>
        /// Gets or sets Authentication Key
        /// </summary>
        public string AuthenticationKey { get; set; }
    }
}
