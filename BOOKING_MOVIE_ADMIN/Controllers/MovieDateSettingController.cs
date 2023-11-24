using System;
using System.Globalization;
using System.Linq;
using BOOKING_MOVIE_ADMIN.Reponse;
using BOOKING_MOVIE_CORE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BOOKING_MOVIE_ADMIN.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieDateSettingController : movieControllerBase
    {
        private readonly MovieDateSettingServices _movieDateSetting;
        public MovieDateSettingController(
            MovieDateSettingServices movieDateSetting,
            UserServices userService) : base(userService)
        {
            _movieDateSetting = movieDateSetting;
        }

        [Authorize(Policy = "Customer")]
        [HttpGet]
        public IActionResult GetMovieDateSettings([FromQuery] string date, [FromQuery] long? movieId)
        {
            var data = _movieDateSetting.GetAll().AsNoTracking();

            if (date != null)
            {
                string inputFormat = "dd/MM/yyyy";
                DateTime parsedDate = DateTime.ParseExact(date, inputFormat, CultureInfo.InvariantCulture);

                data = data.Where(e => e.Time == parsedDate);
            }

            if (movieId != null)
            {
                data = data.Where(e => e.MovieId == movieId);
            }

            var movieDateSetting = data.Include(e => e.Movie).ToList();
            
            return OkList(movieDateSetting);
        }
    }
}