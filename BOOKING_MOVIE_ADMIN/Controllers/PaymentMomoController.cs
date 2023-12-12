using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BOOKING_MOVIE_ADMIN.Helper;
using BOOKING_MOVIE_ADMIN.Reponse;
using BOOKING_MOVIE_ADMIN.Values;
using BOOKING_MOVIE_CORE.Services;
using BOOKING_MOVIE_CORE.Values;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BOOKING_MOVIE_ADMIN.Controllers
{
    [Route("Payment/momo")]
    [ApiController]
    public class PaymentMomoController : movieControllerBase
    {
        private readonly MomoConfig _momoConfig;

        public PaymentMomoController(
            IOptions<MomoConfig> momoConfig,
            UserServices userService) : base(userService)
        {
            _momoConfig = momoConfig.Value;
        }

        [AllowAnonymous]
        [HttpGet("/ipn")]
        public async Task<IActionResult> IpnInvoicePaymentMomo()
        {
            
            return Ok();
        }
    }
}