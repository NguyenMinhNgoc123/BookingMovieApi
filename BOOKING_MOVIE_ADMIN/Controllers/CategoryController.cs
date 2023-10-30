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
    public class CategoryController : movieControllerBase
    {
        private readonly CategoryServices _category;
        private readonly UnitOfWork _unitOfWork;
                
        public CategoryController(
            CategoryServices category,
            UnitOfWork unitOfWork,
            UserServices userService) : base(userService)
        {
            _category = category;
            _unitOfWork = unitOfWork;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetCategory()
        {
            var data = _category.GetAll().AsNoTracking().ToList();

            return OkList(data);
        }

        [Authorize("User")]
        [HttpPost]
        public IActionResult CreateCategory([FromBody] Category body)
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
                _category.Add(body);
                transaction.Commit();
            }
            
            return Ok();
        }
        
        [Authorize("User")]
        [HttpPut("{id}")]
        public IActionResult UpdateCategory([FromRoute] long id,[FromBody] Category body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = _category.GetAll()
                .Where(e => e.Id == id)
                .AsNoTracking()
                .FirstOrDefault();

            if (category == null)
            {
                return BadRequest("category_NOT_EXIST");
            }
            
            body.Updated = DateTime.Now;
            body.UpdatedBy = CurrentUserEmail;

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _category.Update(body);
                transaction.Commit();
            }
            
            return Ok();
        }
        
        [Authorize("User")]
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = _category.GetAll()
                .Where(e => e.Id == id)
                .FirstOrDefault();

            if (category == null)
            {
                return BadRequest("category_NOT_EXIST");
            }
            
            category.Updated = DateTime.Now;
            category.UpdatedBy = CurrentUserEmail;
            category.Status = OBJECT_STATUS.DELETED;
            
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _category.Update(category);
                transaction.Commit();
            }
            
            return Ok();
        }
    }
}