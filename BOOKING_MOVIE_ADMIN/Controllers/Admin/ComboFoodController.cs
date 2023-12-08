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

namespace BOOKING_MOVIE_ADMIN.Controllers.Admin
{
    [Route("Admin/ComboFood")]
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
        [Authorize(Policy = "User")]
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
        
        [HttpGet("{id}")]
        [Authorize(Policy = "User")]
        public IActionResult GetComboFoodDetail([FromRoute] long id)
        {
            var data = _comboFood
                .GetAll()
                .Where(e => e.Id == id)
                .Include(e => e.Combos)
                .ThenInclude(e => e.Food)
                .OrderByDescending(e => e.Created)
                .AsNoTracking()
                .FirstOrDefault();

            return Ok(data);
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
        
        [HttpPut("{id}")]
        [Authorize(Policy = "User")]
        public IActionResult PutComboFood([FromRoute] long id, [FromBody] ComboFood body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comboFood = _comboFood.GetAll()
                .Include(e => e.Combos)
                .FirstOrDefault(e => e.Id == id);

            if (comboFood == null)
            {
                return BadRequest("FOOD_NOT_EXIST");
            }

            var oldCombo = new List<Combo>();
            if (body.Combos.Count > 0)
            {
                var foodIds = body.Combos.Select(e => e.FoodId).ToList();

                var foods = _food.GetAll().Where(e => foodIds.Contains(e.Id)).ToList();

                if (foodIds.Count != foods.Count)
                {
                    return BadRequest("FOOD_INVALID");
                }
                
                oldCombo = comboFood.Combos.ToList();
            }

            var newComboFood = new ComboFood()
            {
                Id = comboFood.Id,
                Price = body.Price,
                Name = body.Name,
                Created = DateTime.Now,
                CreatedBy = CurrentUserEmail,
                Status = OBJECT_STATUS.ENABLE,
            };

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _comboFood.Update(newComboFood);

                if (oldCombo.Count > 0)
                {
                    _combo.RemoveRange_HARD(oldCombo);
                }
                
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
        
        [Authorize(Policy = "User")]
        [HttpDelete("{id}")]
        public IActionResult DeleteComboFood([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comboFood = _comboFood.GetAll()
                .Where(e => e.Id == id)
                .Include(e => e.Combos)
                .FirstOrDefault();

            if (comboFood == null)
            {
                return BadRequest("COMBO_FOOD_NOT_EXIST");
            }
            
            comboFood.Updated = DateTime.Now;
            comboFood.UpdatedBy = CurrentUserEmail;
            comboFood.Status = OBJECT_STATUS.DELETED;
            
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _comboFood.Update(comboFood);

                if (comboFood.Combos.ToList().Count > 0)
                {
                    _combo.RemoveRange_HARD(comboFood.Combos.ToList());
                }
                transaction.Commit();
            }
            
            return Ok();
        }
    }
}