using StoreAPI.Infra;

namespace StoreAPI.Enums
{
    /// <summary>
    /// Role Enum
    /// </summary>
    public enum RolesEnum
    {
        /// <summary>
        /// Admin
        /// </summary>
        Administrator = AppConstants.Roles.Admin.Id,
        /// <summary>
        /// manager
        /// </summary>
        Manager = AppConstants.Roles.Manager.Id,
        /// <summary>
        /// stock
        /// </summary>
        Stock = AppConstants.Roles.Stock.Id,
        /// <summary>
        /// sell
        /// </summary>
        Seller = AppConstants.Roles.Seller.Id,
            Test = 42069
    }
}