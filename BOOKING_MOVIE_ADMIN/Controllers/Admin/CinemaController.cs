using System;
using System.Linq;
using BOOKING_MOVIE_ADMIN.Reponse;
using BOOKING_MOVIE_CORE.Services;
using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BOOKING_MOVIE_ADMIN.Controllers.Admin
{
    [Route("Admin/Cinema")]
    public class CinemaController : movieControllerBase
    {
        private readonly CinemaServices _cinema;
        private readonly UnitOfWork _unitOfWork;

        public CinemaController(
            CinemaServices cinema,
            UnitOfWork unitOfWork,
            UserServices userService) : base(userService)
        {
            _cinema = cinema;
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        [Authorize(Policy = "User")]
        public IActionResult GetCinemaCustomer()
        {
            var data = _cinema.GetAll().AsNoTracking().ToList();

            return OkList(data);
        }
        
        [Authorize(Policy = "User")]
        [HttpPost]
        public IActionResult CreateCinema([FromBody] Cinema body)
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
                _cinema.Add(body);
                transaction.Commit();
            }

            return Ok();
        }
        
        [HttpGet("{id}")]
        [Authorize(Policy = "User")]
        public IActionResult GetCinemaCustomer([FromRoute] long id)
        {
            var data = _cinema.GetAll().AsNoTracking().FirstOrDefault(e => e.Id == id);

            return Ok(data);
        }
        
        [Authorize(Policy = "User")]
        [HttpDelete("{id}")]
        public IActionResult DeleteCinema([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cinema = _cinema.GetAll()
                .Where(e => e.Id == id)
                .FirstOrDefault();

            if (cinema == null)
            {
                return BadRequest("ROOM_NOT_EXIST");
            }

            cinema.Updated = DateTime.Now;
            cinema.UpdatedBy = CurrentUserEmail;
            cinema.Status = OBJECT_STATUS.DELETED;

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _cinema.Update(cinema);
                transaction.Commit();
            }

            return Ok();
        }
    }
}