using System;
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

        public RoomController(
            RoomServices room,
            UnitOfWork unitOfWork,
            MovieRoomServices movieRoom,
            UserServices userService) : base(userService)
        {
            _room = room;
            _unitOfWork = unitOfWork;
            _movieRoom = movieRoom;
        }

        [Authorize(Policy = "Customer")]
        [HttpGet]
        public IActionResult GetRoom([FromQuery] long? cinemaId, [FromQuery] long? movieId, [FromQuery] string time)
        {
            var data = _movieRoom.GetAll().AsNoTracking();

            if (cinemaId != null)
            {
                data = data.Where(e => e.MovieCinema.CinemaId == cinemaId);
            }

            if (time != null)
            {
                data = data.Where(e => e.MovieTimeSettings.Any(o => o.Time == time));
            }
            
            if (movieId != null)
            {
                data = data.Where(e => e.MovieCinema.MovieDateSetting.MovieId == movieId);
            }

            var room = data
                .Include(e => e.Room)
                .Include(e => e.MovieCinema)
                .ThenInclude(e => e.Cinema)
                .ToList();
            
            return OkList(room);
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