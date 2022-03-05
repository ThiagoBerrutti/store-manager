namespace StoreAPI.Dtos
{
    /// <summary>
    /// User data to authenticate
    /// </summary>
    public class UserLoginDto
    {
        /// <summary>
        /// User username
        /// </summary>
        /// <example>user123</example>
        public string UserName { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        /// <example>mysecretpw01</example>
        public string Password { get; set; }
    }
}