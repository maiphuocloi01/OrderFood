using FoodAPI.Models.DAO;
using FoodAPI.Models.DTO;
using FoodAPI.Assets.Contain;
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
    public class OrderController : ApiController
    {
        FoodAppDbEntities db = new FoodAppDbEntities();

        [Route("Api/OrderController/PlaceOrder")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> PlaceOrder(OrderDTO orderDTO)
        {
            return Ok(await OrderDAO.Instance.PlaceOrder(orderDTO));
        }

        [Route("Api/OrderController/GetOrdersByUserID/{ID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetOrdersByUserID(int ID)
        {
            return Ok(await OrderDAO.Instance.GetOrdersByUserID(ID));
        }

        //[Route("Api/OrderController/GetOrderDetailByID/{ID}")]
        //[AllowAnonymous]
        //[HttpGet]
        //public async Task<IHttpActionResult> GetOrderDetailByID(int ID)
        //{
        //    return Ok(await OrderDetailDAO.Instance.GetOrderDetailByID(ID));
        //}

        [Route("Api/OrderController/GetOrderDetailByID/{ID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetOrderDetailByID(int ID)
        {
            //var orders = db.Orders.Where(order => order.Id == ID)
            //       .Include(order => order.OrderDetails)
            //       .ThenInclude(product => product.Product);

            //return Ok(orders);
            var orders = from s in db.OrderDetails.Where(order => order.OrderId == ID)
                                    join p in db.Products on s.ProductId equals p.Id

                                    select new
                                    {
                                        Id = s.Id,
                                        ProductId = p.Id,
                                        OrderId = s.OrderId,
                                        Price = s.Price,
                                        TotalAmount = s.TotalAmount,
                                        Qty = s.Qty,
                                        ProductName = p.Name,
                                        Image = Const.ProductImagePath + p.ImageUrl,
                                        Sumary = p.Detail
                                    };
            
            return Ok(orders);
        }
    }
}
