using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Infrastructure
{
    //Generating API Paths for each Microservice: 
    //Note: Pic APIs are not generated here because they are generated inside each Catalog Item's PictureUrl 
    public static class ApiPaths
    {
        public static class Catalog
        {
            public static string GetAllTypes(string baseUri)
            {
                return $"{baseUri}/catalogtypes";
            }
            public static string GetAllCatalogItems(string baseUri, int page, int take, int? type)
            {
                var filterQs = string.Empty;
                if (type.HasValue)
                {
                    var typeQs = (type.HasValue) ? type.Value.ToString() : "null";
                    filterQs = $"/type/{typeQs}"; 
                }
                return $"{baseUri}items{filterQs}?pageIndex={page}&pageSize={take}";
            }
        }
    }
}
