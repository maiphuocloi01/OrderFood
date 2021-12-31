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
    public class UserController : ApiController
    {
        [Route("Api/UserController/Login")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> Login(UserDTO userDTO)
        {
            return Ok(await UserDAO.Instance.Login(userDTO));
        }

        [Route("Api/UserController/Register")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> Register(UserDTO userDTO)
        {
            return Ok(await UserDAO.Instance.Register(userDTO));
        }

        [Route("Api/CustomerController/UpdateUser")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateUser(UserDTO userDTO)
        {
            return Ok(await UserDAO.Instance.UpdateUser(userDTO));
        }
    }
}
