namespace StoreAPI.Dtos
{
    public class RoleReadDto
    {
        /// <summary>
        /// Role Id
        /// </summary>
        /// <example>12</example>
        public int Id { get; set; }

        /// <summary>
        /// Role name, should be unique
        /// </summary>
        /// <example>trainee</example>
        public string Name { get; set; }
    }
}