namespace StoreAPI.Dtos
{
    /// <summary>
    /// Data to change passwords
    /// </summary>
    public class ChangePasswordDto
    {
        /// <summary>
        /// User's current password
        /// </summary>
        /// <example>12345</example>
        public string CurrentPassword { get; set; }

        /// <summary>
        /// The new password that will overwrite the current password
        /// </summary>
        /// <example>12345678</example>
        public string NewPassword { get; set; }
    }
}