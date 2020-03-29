using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Products;
using Domain.Products;
using System;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ICategoryService _catService;
        private readonly IProductService _prodService;

        public ValuesController(ICategoryService catService, IProductService prodService)
        {
            _catService = catService;
            _prodService = prodService;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get(string ProductName = null, string Category = null)
        {
            //var list = await _catService.GetByValueAsync("Electricidad");
            //var list2 =_catService.GetByValueSomeProperties("Sanitarios");

            //var prods = await _prodService.GetAllAsync();
            //var prods_ = await _prodService.GetAllWithCategoryAsync();

            var prods_paging = await _prodService.GetPaginatedAsync(1, 10, ProductName, Category);

            return Ok(prods_paging);

            //return Ok(new { Lista1 = prods, Lista2 = prods_ });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post(string product_name, string product_description, string category_name, decimal price)
        {
            var current_user_id = "8511f061-8722-4958-b7db-a8b63db9f059";

            Product product = new Product()
            {
                ProductName = product_name,
                Description = product_description,
                Price = price,
                Category = new Category()
                {
                    CategoryName = category_name,
                    CreateDate = DateTime.Now,
                }
            };

            await _prodService.InsertAsync(product, current_user_id);

            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var current_user_id = "8511f061-8722-4958-b7db-a8b63db9f059";

            await _prodService.DeleteProductAsync(new Guid(id), current_user_id);

            return Ok($"El producto con Id {id} fue eliminado");
        }
    }
}
