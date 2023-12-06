using System;
using BOOKING_MOVIE_ADMIN.Models;
using BOOKING_MOVIE_CORE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BOOKING_MOVIE_ADMIN.Controllers
{
    [Route("api")]
    public class AccountController : Controller
    {
        public readonly AuthServices _auth;
        public AccountController(AuthServices authServices)
        {
            _auth = authServices;
        }
        
        [AllowAnonymous]
        [HttpPost("create-managers-token")]
        public IActionResult CreateManagersToken([FromBody] LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var user = _auth.Authenticate(login.Email, login.Password);

            if (user != null)
            {
                var tokenString = _auth.RequestToken(user);
                return Ok(new
                {
                    token = tokenString.Token,
                    expTokenDate = (int)TimeSpan.FromDays(999).TotalDays,
                    expRefreshTokenDate = (int)TimeSpan.FromDays(999).TotalDays,
                    refreshToken = tokenString.RefreshToken,
                    user = user
                });
            }

            return Ok(new
            {
                Message = "FAILED_TO_LOGIN"
            });
        }
        
        [AllowAnonymous]
        [HttpPost("create-managers-token-customer")]
        public IActionResult CreateManagersTokenCustomer([FromBody] LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var customer = _auth.AuthenticateCustomer(login.Email, login.Password);

            if (customer != null)
            {
                var tokenString = _auth.RequestTokenCustomer(customer);
                return Ok(new
                {
                    token = tokenString.Token,
                    expTokenDate = (int)TimeSpan.FromDays(999).TotalDays,
                    expRefreshTokenDate = (int)TimeSpan.FromDays(999).TotalDays,
                    refreshToken = tokenString.RefreshToken
                });
            }

            return Ok(new
            {
                Message = "FAILED_TO_LOGIN"
            });
        }
    }
}