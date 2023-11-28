using System;
using System.Collections.Generic;
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
    [Route("{Controller}")]
    [ApiController]
    public class ComboFoodController : movieControllerBase
    {
        private readonly ComboFoodServices _comboFood;
        private readonly UnitOfWork _unitOfWork;
        private readonly FoodServices _food;
        private readonly ComboServices _combo;

        public ComboFoodController(
            ComboFoodServices comboFood,
            UnitOfWork unitOfWork,
            FoodServices food,
            ComboServices combo,
            UserServices userService) : base(userService)
        {
            _food = food;
            _unitOfWork = unitOfWork;
            _comboFood = comboFood;
            _combo = combo;
        }
        
        [HttpGet]
        [Authorize(Policy = "Customer")]
        public IActionResult GetComboFood()
        {
            var data = _comboFood
                .GetAll()
                .Include(e => e.Combos)
                .ThenInclude(e => e.Food)
                .OrderByDescending(e => e.Created)
                .AsNoTracking()
                .ToList();

            return OkList(data);
        }
        
        [HttpPost]
        [Authorize(Policy = "User")]
        public IActionResult PostComboFood([FromBody] ComboFood body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (body.Combos.Count > 0)
            {
                var foodIds = body.Combos.Select(e => e.FoodId).ToList();

                var foods = _food.GetAll().Where(e => foodIds.Contains(e.Id)).ToList();

                if (foodIds.Count != foods.Count)
                {
                    return BadRequest("FOOD_INVALID");
                }
            }

            var newComboFood = new ComboFood()
            {
                Price = body.Price,
                Name = body.Name,
                Created = DateTime.Now,
                CreatedBy = CurrentUserEmail,
                Status = OBJECT_STATUS.ENABLE,
            };
            
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _comboFood.Add(newComboFood);

                if (body.Combos.Count > 0)
                {
                    var combos = body.Combos.Select(e =>
                    {
                        e.ComboFoodId = newComboFood.Id;
                        e.FoodId = e.FoodId;
                        e.Created = DateTime.Now;
                        e.CreatedBy = CurrentUserEmail;
                        e.Status = OBJECT_STATUS.ENABLE;
                        return e;
                    }).ToList();

                    _combo.AddRange(combos);    
                }
                
                transaction.Commit();
            }
            
            return Ok();
        }
        
        [HttpGet("booking")]
        [Authorize(Policy = "Customer")]
        public IActionResult GetComboFoodBooking()
        {
            var data = _comboFood
                .GetAll()
                .Include(e => e.Combos)
                .OrderByDescending(e => e.Created)
                .AsNoTracking()
                .ToList();

            return OkList(data);
        }
    }
}