using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models;

namespace WebMvc.Services
{
    public interface ICatalogService
    {
        Task<Catalog> GetCatalogItemsAsync(int page, int size, int? type);
        Task<IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>> GetTypesAsync();
    }
}
