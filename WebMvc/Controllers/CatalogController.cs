using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMvc.Services;
using WebMvc.ViewModels;

namespace WebMvc.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService _service;
        public CatalogController(ICatalogService service)
        {
            _service = service;
        }
        //The Controller needs to make a service call to get all Catalog items and another service
        //call to Get Types for filtering data
        public async Task<IActionResult> Index(int? page, int? typeFilterApplied)
        {
            var itemsOnPage = 2;
            var catalog = await _service.GetCatalogItemsAsync(page ?? 0, itemsOnPage, typeFilterApplied); // same as "page==null ?? 0 : page"

            var vm = new CatalogIndexViewModel
            {
                CatalogItems = catalog.Data,
                Types = await _service.GetTypesAsync(),
                PaginationInfo = new PaginationInfo
                {
                    ActualPage = page ?? 0,
                    ItemsPerPage = itemsOnPage,
                    TotalItems = catalog.Count,
                    TotalPages = (int)Math.Ceiling((decimal)catalog.Count / itemsOnPage)
                },
                TypesFilterApplied = typeFilterApplied ?? 0
            };
            return View(vm);
        }
    }
}
