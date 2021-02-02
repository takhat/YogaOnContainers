using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogAPI.Data;
using CatalogAPI.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //from URI: /api/pic/1
    //from Query: /api/catalog/items?pageIndex=0&pageSize=6
    //from Body:
    public class CatalogController : ControllerBase
    {
        private readonly CatalogContext _context;
        private readonly IConfiguration _config;

        public CatalogController(CatalogContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        //ActionResult = Output 
        [HttpGet("[action]")]
        public async Task<IActionResult> Items(
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 6)
        //async needs to be wrapped with Task
        {
            var itemsCount = await _context.CatalogItems.LongCountAsync();

            var items = await _context.CatalogItems
                .OrderBy(c => c.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            items = ChangePictureUrl(items);

            var model = new ViewModels.PaginatedItemsViewModel<CatalogItem>
            {
                PageIndex = pageIndex,
                PageSize = items.Count,
                Count = itemsCount,
                Data = items
            };
            return Ok(model);
        }

        private List<CatalogItem> ChangePictureUrl(List<CatalogItem> items)
        {
            items.ForEach(i =>
            i.PictureUrl = i.PictureUrl.Replace(
                "http://externalcatalogbaseurltobereplaced",
                _config["ExternalCatalogBaseUrl"]));
            return items;
        }

        //We will need below methods to filter by type
        [HttpGet("[action]")]
        public async Task<IActionResult> CatalogTypes()
        {
            var types = await _context.CatalogTypes.ToListAsync();
            return Ok(types);
        }

        [HttpGet("[action]/type/{catalogTypeId}")]
        public async Task<IActionResult> Items(
            int? catalogTypeId,
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 6
            )
        {
            var query = (IQueryable<CatalogItem>)_context.CatalogItems;
            if (catalogTypeId.HasValue) 
            {
                query = query.Where(c => c.CatalogTypeId == catalogTypeId);
            }
            var itemsCount = await query.LongCountAsync();

            var items = await query
                .OrderBy(c => c.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            items = ChangePictureUrl(items);

            var model = new ViewModels.PaginatedItemsViewModel<CatalogItem>
            {
                PageIndex = pageIndex,
                PageSize = items.Count,
                Count = itemsCount,
                Data = items
            };
            return Ok(model);
        }
    }
}
