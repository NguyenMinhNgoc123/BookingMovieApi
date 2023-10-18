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
    public class ActorController : movieControllerBase
    {
        private readonly ActorServices _actor;
        private readonly UnitOfWork _unitOfWork;
                
        public ActorController(
            ActorServices actor,
            UnitOfWork unitOfWork,
            UserServices userService) : base(userService)
        {
            _actor = actor;
            _unitOfWork = unitOfWork;
        }
        
        [Authorize("User")]
        [HttpGet]
        public IActionResult GetActor()
        {
            var data = _actor.GetAll().AsNoTracking().ToList();

            return OkList(data);
        }

        [Authorize("User")]
        [HttpPost]
        public IActionResult CreateActor([FromBody] Actor body)
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
                _actor.Add(body);
                transaction.Commit();
            }
            
            return Ok();
        }
        
        [Authorize("User")]
        [HttpPut("{id}")]
        public IActionResult UpdateActor([FromRoute] long id,[FromBody] Actor body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actor = _actor.GetAll()
                .Where(e => e.Id == id)
                .AsNoTracking()
                .FirstOrDefault();

            if (actor == null)
            {
                return BadRequest("actor_NOT_EXIST");
            }
            
            body.Updated = DateTime.Now;
            body.UpdatedBy = CurrentUserEmail;

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _actor.Update(body);
                transaction.Commit();
            }
            
            return Ok();
        }
        
        [Authorize("User")]
        [HttpDelete("{id}")]
        public IActionResult DeleteActor([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actor = _actor.GetAll()
                .Where(e => e.Id == id)
                .FirstOrDefault();

            if (actor == null)
            {
                return BadRequest("actor_NOT_EXIST");
            }
            
            actor.Updated = DateTime.Now;
            actor.UpdatedBy = CurrentUserEmail;
            actor.Status = OBJECT_STATUS.DELETED;
            
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _actor.Update(actor);
                transaction.Commit();
            }
            
            return Ok();
        }
    }
}