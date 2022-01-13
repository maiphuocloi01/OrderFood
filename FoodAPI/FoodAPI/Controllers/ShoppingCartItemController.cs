using FoodAPI.Assets.Contain;
using FoodAPI.Models.DAO;
using FoodAPI.Models.DTO;
using FoodAPI.Models.EF;
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
        FoodAppDbEntities db = new FoodAppDbEntities();

        [Route("Api/ShoppingCartItemController/GetAllShoppingCartItems/{ID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllShoppingCartItems(int ID)
        {
            var user = db.ShoppingCartItems.Where(s => s.CustomerId == ID);
            if (user == null)
            {
                return NotFound();
            }
            var shoppingCartItems = from s in db.ShoppingCartItems.Where(s => s.CustomerId == ID)
                                    join p in db.Products on s.ProductId equals p.Id

                                    select new
                                    {
                                        Id = s.Id,
                                        Price = s.Price,
                                        TotalAmount = s.TotalAmount,
                                        Qty = s.Qty,
                                        ProductId = p.Id,
                                        ProductName = p.Name,
                                        Image = Const.ProductImagePath + p.ImageUrl,
                                        Sumary = p.Detail
                                    };
            //return Ok(await ShoppingCartItemDAO.Instance.GetAllShoppingCartItems(ID));
            return Ok(shoppingCartItems);
        }

        [Route("Api/ShoppingCartItemController/SubTotal/{ID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> SubTotal(int ID)
        {
            return Ok(new { SubTotal = await ShoppingCartItemDAO.Instance.SubTotal(ID) });
        }

        [Route("Api/ShoppingCartItemController/TotalItems/{ID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> TotalItems(int ID)
        {
            return Ok(new { TotalItems = await ShoppingCartItemDAO.Instance.TotalItems(ID) });
        }

        [Route("Api/ShoppingCartItemController/AddShoppingCartItems")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> AddShoppingCartItems(ShoppingCartItemDTO ShoppingCartItemDTO)
        {
            return Ok(await ShoppingCartItemDAO.Instance.AddShoppingCartItems(ShoppingCartItemDTO));
        }

        [Route("Api/ShoppingCartItemController/UpdateQuantity/{ID}/{quantity}")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateQuantity(int ID, int quantity)
        {
            return Ok(await ShoppingCartItemDAO.Instance.UpdateQuantity(ID, quantity));
        }

        [Route("Api/ShoppingCartItemController/DeleteCartItem/{ID}")]
        [AllowAnonymous]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCartItem(int ID)
        {
            return Ok(await ShoppingCartItemDAO.Instance.DeleteCartItem(ID));
        }

        [Route("Api/ShoppingCartItemController/DeleteCartItemByID/{ID}")]
        [AllowAnonymous]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCartItemByID(int ID)
        {
            return Ok(await ShoppingCartItemDAO.Instance.DeleteCartItemByID(ID));
        }
    }
}
