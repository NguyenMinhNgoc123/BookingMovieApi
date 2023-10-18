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

        public RoomController(
            RoomServices room,
            UnitOfWork unitOfWork,
            UserServices userService) : base(userService)
        {
            _room = room;
            _unitOfWork = unitOfWork;
        }

        [Authorize("User")]
        [HttpGet]
        public IActionResult GetRoom()
        {
            var data = _room.GetAll().AsNoTracking().ToList();

            return OkList(data);
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

        [Authorize("User")]
        [HttpPut("{id}")]
        public IActionResult UpdateRoom([FromRoute] long id, [FromBody] Room body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var room = _room.GetAll()
                .Where(e => e.Id == id)
                .AsNoTracking()
                .FirstOrDefault();

            if (room == null)
            {
                return BadRequest("ROOM_NOT_EXIST");
            }

            body.Updated = DateTime.Now;
            body.UpdatedBy = CurrentUserEmail;

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _room.Update(body);
                transaction.Commit();
            }

            return Ok();
        }

        [Authorize("User")]
        [HttpDelete("{id}")]
        public IActionResult DeleteRoom([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var room = _room.GetAll()
                .Where(e => e.Id == id)
                .FirstOrDefault();

            if (room == null)
            {
                return BadRequest("ROOM_NOT_EXIST");
            }

            room.Updated = DateTime.Now;
            room.UpdatedBy = CurrentUserEmail;
            room.Status = OBJECT_STATUS.DELETED;

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _room.Update(room);
                transaction.Commit();
            }

            return Ok();
        }
    }
}