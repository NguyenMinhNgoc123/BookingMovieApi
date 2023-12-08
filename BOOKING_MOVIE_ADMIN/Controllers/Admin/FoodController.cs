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
    [Route("Admin/Food")]
    [ApiController]
    public class FoodController : movieControllerBase
    {
        private readonly FoodServices _food;
        private readonly UnitOfWork _unitOfWork;

        public FoodController(
            FoodServices food,
            UnitOfWork unitOfWork,
            UserServices userService) : base(userService)
        {
            _food = food;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize(Policy = "User")]
        public IActionResult GetFood()
        {
            var data = _food.GetAll().AsNoTracking().ToList();

            return OkList(data);
        }
        
        
        [HttpGet("{id}")]
        public IActionResult GetFoodCustomerDetail([FromRoute] long id)
        {
            var data = _food.GetAll().AsNoTracking().FirstOrDefault(e => e.Id == id);

            return Ok(data);
        }

        [HttpPost]
        [Authorize(Policy = "User")]
        public IActionResult PostFood([FromBody] Food body)
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
                _food.Add(body);
                transaction.Commit();
            }

            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "User")]
        public IActionResult PutFood([FromRoute] long id, [FromBody] Food body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var oldFood = _food.GetAll().FirstOrDefault(e => e.Id == id);
            if (oldFood == null)
            {
                return BadRequest();
            }

            oldFood.Updated = DateTime.Now;
            oldFood.UpdatedBy = CurrentUserEmail;
            oldFood.Price = body.Price;
            oldFood.Name = body.Name;

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _food.Update(oldFood);
                transaction.Commit();
            }

            return Ok();
        }
        
        [Authorize(Policy = "User")]
        [HttpDelete("{id}")]
        public IActionResult DeleteFood([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genre = _food.GetAll()
                .Where(e => e.Id == id)
                .FirstOrDefault();

            if (genre == null)
            {
                return BadRequest("FOOD_NOT_EXIST");
            }
            
            genre.Updated = DateTime.Now;
            genre.UpdatedBy = CurrentUserEmail;
            genre.Status = OBJECT_STATUS.DELETED;
            
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _food.Update(genre);
                transaction.Commit();
            }
            
            return Ok();
        }
    }
}