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
    public class OrderController : ApiController
    {
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

        [Route("Api/OrderController/GetOrderDetailByID/{ID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetOrderDetailByID(int ID)
        {
            return Ok(await OrderDetailDAO.Instance.GetOrderDetailByID(ID));
        }
    }
}
