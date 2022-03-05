namespace StoreAPI.Swagger
{
    public class Descriptions
    {
        public const string XPaginationDescription = "Pagination data on JSON format. " +
            "<ul>" +
            "<li><b>TotalCount</b>: total number of items</li>" +
            "<li><b>PageSize</b>: item quantity per page</li>" +
            "<li><b>CurrentPage</b>: page number</li>" +
            "<li><b>TotalPages</b>: total number of pages</b></li>" +
            "<li><b>HasNext</b>: true if the result has a next page</li>" +
            "<li><b>HasPrevious</b>: true if the result has a previous page</li>" +
            "</ul>" +
            "Example:\n\n" +
            "<example>x-pagination: {\"TotalCount\":30,\"PageSize\":10,\"CurrentPage\":1,\"TotalPages\":3,\"HasNext\":true,\"HasPrevious\":false} </example>";
    }
}