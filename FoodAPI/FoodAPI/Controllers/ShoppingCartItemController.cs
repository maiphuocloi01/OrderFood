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
    public class ShoppingCartItemController : ApiController
    {
        [Route("Api/ShoppingCartItemController/GetAllShoppingCartItems/{ID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllShoppingCartItems(int ID)
        {
            return Ok(await ShoppingCartItemDAO.Instance.GetAllShoppingCartItems(ID));
        }

        [Route("Api/ShoppingCartItemController/SubTotal/{ID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> SubTotal(int ID)
        {
            return Ok(await ShoppingCartItemDAO.Instance.SubTotal(ID));
        }

        [Route("Api/ShoppingCartItemController/TotalItems/{ID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> TotalItems(int ID)
        {
            return Ok(await ShoppingCartItemDAO.Instance.TotalItems(ID));
        }

        [Route("Api/ShoppingCartItemController/AddShoppingCartItems")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> AddShoppingCartItems(ShoppingCartItemDTO ShoppingCartItemDTO)
        {
            return Ok(await ShoppingCartItemDAO.Instance.AddShoppingCartItems(ShoppingCartItemDTO));
        }

        [Route("Api/ShoppingCartItemController/DeleteCartItem/{ID}")]
        [AllowAnonymous]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCartItem(int ID)
        {
            return Ok(await ShoppingCartItemDAO.Instance.DeleteCartItem(ID));
        }
    }
}
