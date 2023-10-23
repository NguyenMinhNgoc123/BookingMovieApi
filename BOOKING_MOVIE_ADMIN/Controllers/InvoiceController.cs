using System.Linq;
using BOOKING_MOVIE_ADMIN.Reponse;
using BOOKING_MOVIE_CORE.Services;
using BOOKING_MOVIE_ENTITY.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BOOKING_MOVIE_ADMIN.Controllers
{
    [Route("{Controller}")]
    [ApiController]
    public class InvoiceController : movieControllerBase
    {
        private readonly InvoiceServices _invoice;
        
        public InvoiceController(
            InvoiceServices invoice,
            UserServices userService) : base(userService)
        {
            _invoice = invoice;
        }

        [Authorize("User")]
        [HttpGet]
        public IActionResult GetUser(string keyword = "", string type = "MANAGEMENT")
        {
            var data = _invoice.GetAll()
                .ToList();

            return OkList(data);
        }
        
        [Authorize("User")]
        [HttpPost]
        public IActionResult CreateInvoice([FromBody] Invoice body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}