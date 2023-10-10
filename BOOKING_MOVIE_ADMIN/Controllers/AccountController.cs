// using System;
// using BOOKING_MOVIE_ADMIN.Models;
// using BOOKING_MOVIE_CORE.Services;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
//
// namespace BOOKING_MOVIE_ADMIN.Controllers
// {
//     [Route("api")]
//     public class AccountController : Controller
//     {
//         public readonly AuthServices _auth;
//         public AccountController()
//         {
//             
//         }
//         
//         [AllowAnonymous]
//         [HttpPost("create-managers-token")]
//         public IActionResult CreateManagersToken([FromBody] LoginViewModel login)
//         {
//             if (!ModelState.IsValid)
//             {
//                 return BadRequest(ModelState);
//             }
//             
//             var user = _authService.Authenticate(login.Username, login.Password);
//
//             if (user != null)
//             {
//                 if (user.Type != "MANAGEMENT")
//                 {
//                     return Ok(new
//                     {
//                         Message = "CAN_NOT_USE_THIS_USER_IT_HERE"
//                     });
//                 }
//
//                 var tokenString = _authService.RequestToken(user);
//                 return Ok(new
//                 {
//                     token = tokenString.Token,
//                     expTokenDate = (int)TimeSpan.FromDays(999).TotalDays,
//                     expRefreshTokenDate = (int)TimeSpan.FromDays(999).TotalDays,
//                     refreshToken = tokenString.RefreshToken
//                 });
//             }
//
//             return Ok(new
//             {
//                 Message = "FAILD_TO_LOGIN"
//             });
//         }
//     }
// }