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
    [Route("Admin/Genre")]
    [ApiController]
    public class GenreController : movieControllerBase
    {
        private readonly GenreServices _genre;
        private readonly UnitOfWork _unitOfWork;

        public GenreController(
            GenreServices genre,
            UnitOfWork unitOfWork,
            UserServices userService) : base(userService)
        {
            _genre = genre;
            _unitOfWork = unitOfWork;
        }

        [Authorize(Policy = "User")]
        [HttpGet]
        public IActionResult GetGenre()
        {
            var data = _genre.GetAll().AsNoTracking().ToList();

            return OkList(data);
        }
        
        [Authorize(Policy = "User")]
        [HttpGet("{id}")]
        public IActionResult GetGenreDetail([FromRoute] long id)
        {
            var data = _genre.GetAll().AsNoTracking().FirstOrDefault(e => e.Id == id);

            return Ok(data);
        }
        
        [Authorize("User")]
        [HttpPost]
        public IActionResult CreateGenre([FromBody] Genre body)
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
                _genre.Add(body);
                transaction.Commit();
            }
            
            return Ok();
        }
        
        [Authorize("User")]
        [HttpPut("{id}")]
        public IActionResult UpdateGenre([FromRoute] long id,[FromBody] Genre body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genre = _genre.GetAll()
                .Where(e => e.Id == id)
                .AsNoTracking()
                .FirstOrDefault();

            if (genre == null)
            {
                return BadRequest("genre_NOT_EXIST");
            }
            
            body.Updated = DateTime.Now;
            body.UpdatedBy = CurrentUserEmail;

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _genre.Update(body);
                transaction.Commit();
            }
            
            return Ok();
        }
        
        [Authorize("User")]
        [HttpDelete("{id}")]
        public IActionResult DeleteGenre([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genre = _genre.GetAll()
                .Where(e => e.Id == id)
                .FirstOrDefault();

            if (genre == null)
            {
                return BadRequest("genre_NOT_EXIST");
            }
            
            genre.Updated = DateTime.Now;
            genre.UpdatedBy = CurrentUserEmail;
            genre.Status = OBJECT_STATUS.DELETED;
            
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _genre.Update(genre);
                transaction.Commit();
            }
            
            return Ok();
        }
    }
}