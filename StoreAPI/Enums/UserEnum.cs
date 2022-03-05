using StoreAPI.Infra;

namespace StoreAPI.Enums
{
    /// <summary>
    /// Test user to authenticate to
    /// </summary>
    public enum UserEnum
    {
        /// <summary>
        ///     - Username: admin
        ///     - Password: admin
        /// </summary>
        Administrator = AppConstants.Users.Admin.Id,

        /// <summary>
        ///     - Username: manager
        ///     - Password: manager
        /// </summary>
        Manager = AppConstants.Users.Manager.Id,
                
        /// <summary>
        ///     - Username: stock
        ///     - Password: stock
        /// </summary>
        Stock = AppConstants.Users.Stock.Id,

        /// <summary>
        ///     - Username: seller
        ///     - Password: seller
        /// </summary>
        Seller = AppConstants.Users.Seller.Id,

        /// <summary>
        ///     - Username: public
        ///     - Password: public
        /// </summary>
        Public = AppConstants.Users.Public.Id
    }
}