//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Routing;

//namespace SalesAPI.Helpers
//{
//    public class LinkGeneratorHelper
//    {
//        private readonly LinkGenerator _linkGenerator;
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public LinkGeneratorHelper(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
//        {
//            _linkGenerator = linkGenerator;
//            _httpContextAccessor = httpContextAccessor;
//        }

//        public string GetPath(dynamic endpoint, object values)
//        {
//            return _linkGenerator.GetPathByName(nameof(endpoint), values);
//        }
//    }
//}