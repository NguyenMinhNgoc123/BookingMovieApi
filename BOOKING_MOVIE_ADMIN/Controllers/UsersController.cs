using System.Collections.Generic;
using System.Linq;
using BOOKING_MOVIE_ADMIN.Reponse;
using BOOKING_MOVIE_CORE;
using BOOKING_MOVIE_CORE.Services;
using BOOKING_MOVIE_ENTITY.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BOOKING_MOVIE_ADMIN.Controllers
{
    [Route("[controller]")]
    [ApiController]
    // [Authorize]
    public class UsersController : movieControllerBase
    {
        public UsersController(UserServices userServices) : base(userServices)
        {
            
        }

        [HttpGet]
        public IActionResult GetUser(string keyword = "", string type = "MANAGEMENT")
        {
            var data = _user.GetAll()
                .Where(o => o.Type == type)
                .ToList();  

            return OkList(data);
        }

    }
}