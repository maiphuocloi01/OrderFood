using FoodAPI.Models.DAO;
using FoodAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FoodAPI.Controllers
{
    public class ProductController : ApiController
    {
        [Route("Api/ProductController/GetAllProduct")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllProduct()
        {
            return Ok(await ProductDAO.Instance.GetAllProduct());
        }
        
        [Route("Api/ProductController/GetPopularProduct")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetPopularProduct()
        {
            return Ok(await ProductDAO.Instance.GetPopularProduct());
        }

        [Route("Api/ProductController/GetProductByID/{ID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetProductByID(int ID)
        {
            return Ok(await ProductDAO.Instance.GetProductByID(ID));
        }

        [Route("Api/ProductController/GetProductByCategoryID/{categoryID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetProductByCategoryID(int categoryID)
        {
            return Ok(await ProductDAO.Instance.GetProductByCategoryID(categoryID));
        }

        [Route("Api/ProductController/AddProduct")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> AddProduct(ProductDTO productDTO)
        {
            return Ok(await ProductDAO.Instance.AddProduct(productDTO));
        }

        [Route("Api/ProductController/UpdateProduct")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateBrand(ProductDTO product)
        {
            return Ok(await ProductDAO.Instance.UpdateProduct(product));
        }

        [Route("Api/ProductController/DeleteProduct/{ID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> DeleteProduct(int ID)
        {
            return Ok(await ProductDAO.Instance.DeleteProduct(ID));
        }
    }
}
