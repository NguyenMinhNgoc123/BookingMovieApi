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
    public class MovieTimeSettingController : movieControllerBase
    {
        private readonly MovieTimeSettingServices _movieTimeSetting;

        public MovieTimeSettingController(
            UserServices userService, 
            MovieTimeSettingServices movieTimeSetting) : base(userService)
        {
            _movieTimeSetting = movieTimeSetting;
        }
        
        [Authorize(Policy = "Customer")]
        [HttpGet]
        public IActionResult GetMovieDateTimeSetting(
            [FromQuery] string time, 
            [FromQuery] long? movieId, 
            [FromQuery] long? roomId,
            [FromQuery] long? cinemaId,
            [FromQuery] long? movieDateSettingId
            )
        {
            var data = _movieTimeSetting
                .GetAll()
                .Where(e => e.Time == time)
                .AsNoTracking();
            
            if (movieDateSettingId != null)
            {
                data = data.Where(e => e.MovieRoom.MovieCinema.MovieDateSetting.Movie.Id == movieId);
            }
            
            if (roomId != null)
            {
                data = data.Where(e => e.MovieRoom.RoomId == roomId);
            }
            
            if (cinemaId != null)
            {
                data = data.Where(e => e.MovieRoom.MovieCinema.CinemaId == cinemaId);
            }
            
            if (movieId != null)
            {
                data = data.Where(e => e.MovieRoom.MovieCinema.MovieDateSetting.Id == movieDateSettingId);
            }
            
            var movieTime = data
                .OrderBy(e => e.Time)
                .FirstOrDefault();
            
            return Ok(movieTime);
        }
        
        [Authorize(Policy = "Customer")]
        [HttpGet("bookingTime")]
        public IActionResult GetMovieDateTimeSettings([FromQuery] string date, [FromQuery] long movieId)
        {
            var data = _movieTimeSetting
                .GetAll()
                .AsNoTracking();
            
            if (date != null)
            {
                string inputFormat = "dd/MM/yyyy";
                DateTime parsedDate = DateTime.ParseExact(date, inputFormat, CultureInfo.InvariantCulture);

                data = data.Where(e => e.MovieRoom.MovieCinema.MovieDateSetting.Time == parsedDate);
            }

            if (movieId != null)
            {
                data = data.Where(e => e.MovieRoom.MovieCinema.MovieDateSetting.Movie.Id == movieId);
            }
            
            var movieTime = data
                .OrderBy(e => e.Time)
                .ToList();
            
            return OkList(movieTime);
        }
    }
}