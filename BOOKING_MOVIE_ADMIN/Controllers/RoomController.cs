using System;
using System.Collections.Generic;
using System.Linq;
using BOOKING_MOVIE_ADMIN.Reponse;
using BOOKING_MOVIE_CORE.Services;
using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BOOKING_MOVIE_ADMIN.Controllers
{
    [Route("[controller]")]
    public class RoomController : movieControllerBase
    {
        private readonly RoomServices _room;
        private readonly UnitOfWork _unitOfWork;
        private readonly MovieRoomServices _movieRoom;
        private readonly MovieDateSettingServices _movieDateSetting;

        public RoomController(
            RoomServices room,
            UnitOfWork unitOfWork,
            MovieRoomServices movieRoom,
            MovieDateSettingServices movieDateSetting,
            UserServices userService) : base(userService)
        {
            _room = room;
            _unitOfWork = unitOfWork;
            _movieRoom = movieRoom;
            _movieDateSetting = movieDateSetting;
        }

        [Authorize(Policy = "Customer")]
        [HttpGet]
        public IActionResult GetRoom([FromQuery] long? cinemaId, [FromQuery] long? movieId, [FromQuery] string time, [FromQuery] long? movieDateSettingId, [FromQuery] long? movieTimeSettingId)
        {
            var movie = _movieDateSetting.GetAll().AsNoTracking();
            
            if (movieDateSettingId != null)
            {
                movie = movie.Where(e => e.Id == movieDateSettingId);
            }
            
            if (movieId != null)
            {
                movie = movie.Where(e => e.MovieId == movieId);
            }
            
            if (cinemaId != null)
            {
                movie = movie.Where(e => e.MovieCinemas.Any(o => o.CinemaId == cinemaId));
            }

            var data = movie.Include(e => e.MovieCinemas)
                .ThenInclude(e => e.MovieRooms).FirstOrDefault();


            var movieRoomIds = new List<long>();
            
            if (cinemaId != null)
            {
                movieRoomIds = data.MovieCinemas.Where(e => e.CinemaId == cinemaId).FirstOrDefault().MovieRooms.Select(e => e.Id).ToList();
            }

            var room = _movieRoom.GetAll()
                .Where(e => movieRoomIds.Contains(e.Id));

            if (time != null)
            {
                room = room.Where(elm => elm.MovieTimeSettings.Any(e => e.Time == time));
            }
            
            var rooms = room.Include(e => e.Room)
                .Include(e => e.MovieCinema)
                .ThenInclude(e => e.Cinema)
                .ToList();
            
            return OkList(rooms);
        }

        [Authorize("User")]
        [HttpPost]
        public IActionResult CreateRoom([FromBody] Room body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            body.Created = DateTime.Now;
            body.CreatedBy = CurrentUserEmail;
            body.Status = OBJECT_STATUS.ENABLE;

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _room.Add(body);
                transaction.Commit();
            }

            return Ok();
        }
    }
}