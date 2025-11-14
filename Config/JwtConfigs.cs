namespace ToDoList.Config
{
    /// <summary>
    /// JWT configuration settings.
    /// </summary>
    public class JwtConfigs
    {
        /// <summary>
        /// Secret key used for signing the JWT.
        /// </summary>
        public string Secret { get; set; } = string.Empty;
        /// <summary> 
        /// Expiration time in hours for the JWT.
        /// </summary>
        public int ExpirationInHour { get; set; }
        /// <summary>
        /// Issuer of the JWT.
        /// </summary>
        public string Issuer { get; set; } = string.Empty;
        /// <summary>
        /// Audience for the JWT.
        /// </summary>
        public string Audience { get; set; } = string.Empty;
    }
}