namespace SalesAPI.Models
{
    public class ProductStock
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }

        public ProductStock()
        {
        }

        public ProductStock(Product product, int count)
        {
            Product = product;
            Count = count;
        }


        //public override bool Equals(object obj)
        //{
        //    if (ReferenceEquals(null, obj)) return false;
        //    if (ReferenceEquals(this, obj)) return true;

        //    if (obj is ProductStock compareTo)
        //    {
        //        return Product.Equals(compareTo);
        //    }

        //    return false;
        //}
    }
}