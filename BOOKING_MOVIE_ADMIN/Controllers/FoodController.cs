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
        [Authorize(Policy = "Customer")]
        public IActionResult GetFood()
        {
            var data = _food.GetAll().AsNoTracking().ToList();

            return OkList(data);
        }
        
        [HttpPost]
        [Authorize(Policy = "Customer")]
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
        [Authorize(Policy = "Customer")]
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
                _food.Add(oldFood);
                transaction.Commit();
            }
            
            return Ok();
        }
    }
}