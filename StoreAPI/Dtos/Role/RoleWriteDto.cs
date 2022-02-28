namespace StoreAPI.Dtos
{
    public class RoleWriteDto
    {
        /// <summary>
        /// Role name, must be unique
        /// </summary>
        /// <example>trainee</example>
        public string Name { get; set; }
    }
}