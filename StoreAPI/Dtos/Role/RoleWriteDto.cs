namespace StoreAPI.Dtos
{
    /// <summary>
    /// Role data to be persisted
    /// </summary>
    public class RoleWriteDto
    {
        /// <summary>
        /// Role name, must be unique
        /// </summary>
        /// <example>seller</example>
        public string Name { get; set; }
    }
}