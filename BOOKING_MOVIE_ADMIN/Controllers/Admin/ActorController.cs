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
    [Route("Admin/Actor")]
    public class ActorController : movieControllerBase
    {
        private readonly ActorServices _actor;
        private readonly UnitOfWork _unitOfWork;
        private readonly MovieActorServices _movieActor;

        public ActorController(
            ActorServices actor,
            UnitOfWork unitOfWork,
            MovieActorServices movieActor,
            UserServices userService) : base(userService)
        {
            _actor = actor;
            _unitOfWork = unitOfWork;
            _movieActor = movieActor;
        }
        
        [Authorize(Policy = "User")]
        [HttpGet]
        public IActionResult GetActor()
        {
            var data = _actor.GetAll().AsNoTracking().ToList();

            return OkList(data);
        }
        
        [Authorize(Policy = "User")]
        [HttpGet("{id}")]
        public IActionResult GetActor([FromRoute] long id)
        {
            var data = _actor.GetAll().AsNoTracking().FirstOrDefault(e => e.Id == id);

            return Ok(data);
        }
        
        [Authorize(Policy = "User")]
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
        
        [Authorize(Policy = "User")]
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
                return BadRequest("ACTOR_NOT_EXIST");
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
        
        [Authorize(Policy = "User")]
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
                return BadRequest("ACTOR_NOT_EXIST");
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
        
        [Authorize(Policy = "User")]
        [HttpGet("Movie/{id}")]
        public IActionResult GetActorMovie([FromRoute] long id)
        {
            var movieActor = _movieActor.GetAll().Where(e => e.MovieId == id).ToList();
            var actorIds = movieActor.Select(e => e.ActorId).ToList();
            var data = _actor.GetAll().AsNoTracking().Where(e => actorIds.Contains(e.Id)).ToList();

            return OkList(data);
        }
    }
}