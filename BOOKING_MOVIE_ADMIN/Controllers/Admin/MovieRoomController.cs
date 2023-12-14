using System;
using System.Linq;
using BOOKING_MOVIE_ADMIN.Reponse;
using BOOKING_MOVIE_CORE.Services;
using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BOOKING_MOVIE_ADMIN.Controllers.Admin
{
    [Route("Admin/MovieRoom")]
    [ApiController]
    public class MovieRoomController : movieControllerBase
    {
        private UnitOfWork _unitOfWork;
        private MovieRoomServices _movieRoom;
        
        public MovieRoomController(
            UnitOfWork unitOfWork,
            MovieRoomServices movieRoom,
            UserServices userService) : base(userService)
        {
            _unitOfWork = unitOfWork;
            _movieRoom = movieRoom;
        }
        
        [Authorize(Policy = "User")]
        [HttpDelete("{id}")]
        public IActionResult DeleteMovieDateSetting([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movieRoom = _movieRoom.GetAll()
                .Where(e => e.Id == id)
                .FirstOrDefault();

            if (movieRoom == null)
            {
                return BadRequest("MOVIE_ROOM_NOT_EXIST");
            }

            movieRoom.Updated = DateTime.Now;
            movieRoom.UpdatedBy = CurrentUserEmail;
            movieRoom.Status = OBJECT_STATUS.DELETED;

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _movieRoom.Update(movieRoom);
                transaction.Commit();
            }

            return Ok();
        }
    }
}