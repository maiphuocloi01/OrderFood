using FoodAPI.Models.DAO;
using FoodAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
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

        [Route("Api/UserController/UpdateUser")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateUser(UserDTO userDTO)
        {
            return Ok(await UserDAO.Instance.UpdateUser(userDTO));
        }

        [Route("Api/UserController/GetUserByID/{ID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetUserByID(int ID)
        {
            return Ok(await UserDAO.Instance.GetUserByID(ID));
        }

        [Route("Api/UserController/UploadImage")]
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage UploadImage()
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count < 1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            foreach (string file in httpRequest.Files)
            {
                var postedFile = httpRequest.Files[file];
                var filePath = HttpContext.Current.Server.MapPath("~/Assets/Images/User/" + postedFile.FileName);
                postedFile.SaveAs(filePath);
            }

            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}
