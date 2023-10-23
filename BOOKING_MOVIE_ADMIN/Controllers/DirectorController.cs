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
    [ApiController]
    public class DirectorController : movieControllerBase
    {
        private readonly DirectorServices _director;
        private readonly UnitOfWork _unitOfWork;
        
        public DirectorController(
            DirectorServices director,
            UserServices userService,
            UnitOfWork unitOfWork
            ) : base(userService)
        {
            _unitOfWork = unitOfWork;
            _director = director;
        }

        [Authorize("User")]
        [HttpGet]
        public IActionResult GetDirector()
        {
            var data = _director.GetAll().AsNoTracking().ToList();

            return OkList(data);
        }

        [Authorize(Policy = "User")]
        [HttpPost]
        public IActionResult CreateDirector([FromBody] Director body)
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
                _director.Add(body);
                transaction.Commit();
            }
            
            return Ok();
        }
        
        [Authorize("User")]
        [HttpPut("{id}")]
        public IActionResult UpdateDirector([FromRoute] long id,[FromBody] Director body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var director = _director.GetAll()
                .Where(e => e.Id == id)
                .AsNoTracking()
                .FirstOrDefault();

            if (director == null)
            {
                return BadRequest("DIRECTOR_NOT_EXIST");
            }
            
            body.Updated = DateTime.Now;
            body.UpdatedBy = CurrentUserEmail;

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _director.Update(body);
                transaction.Commit();
            }
            
            return Ok();
        }
        
        [Authorize("User")]
        [HttpDelete("{id}")]
        public IActionResult DeleteDirector([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var director = _director.GetAll()
                .Where(e => e.Id == id)
                .FirstOrDefault();

            if (director == null)
            {
                return BadRequest("DIRECTOR_NOT_EXIST");
            }
            
            director.Updated = DateTime.Now;
            director.UpdatedBy = CurrentUserEmail;
            director.Status = OBJECT_STATUS.DELETED;
            
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _director.Update(director);
                transaction.Commit();
            }
            
            return Ok();
        }
    }
}