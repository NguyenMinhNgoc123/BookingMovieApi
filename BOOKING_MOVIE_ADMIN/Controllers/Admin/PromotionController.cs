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
    [Route("Admin/Promotion")]
    [ApiController]
    public class PromotionController : movieControllerBase
    {
        private readonly PromotionServices _promotion;
        private readonly UnitOfWork _unitOfWork;

        public PromotionController(
            PromotionServices promotionServices,
            UnitOfWork unitOfWork,
            UserServices userService) : base(userService)
        {
            _unitOfWork = unitOfWork;
            _promotion = promotionServices;
        }

        [Authorize(Policy = "User")]
        [HttpGet]
        public IActionResult GetPromotion()
        {
            var data = _promotion.GetAll().AsNoTracking().ToList();

            return OkList(data);
        }
        
        [Authorize(Policy = "User")]
        [HttpGet("{id}")]
        public IActionResult GetPromotion([FromRoute] long id)
        {
            var data = _promotion.GetAll().AsNoTracking().FirstOrDefault(e => e.Id == id);

            return Ok(data);
        }

        [Authorize(Policy = "User")]
        [HttpPost]
        public IActionResult CreatePromotion([FromBody] Promotion body)
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
                _promotion.Add(body);
                transaction.Commit();
            }

            return Ok(body);
        }

        [Authorize(Policy = "User")]
        [HttpPut("{id}")]
        public IActionResult UpdatePromotion([FromRoute] long id, [FromBody] Promotion body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var promotion = _promotion.GetAll()
                .Where(e => e.Id == id)
                .AsNoTracking()
                .FirstOrDefault();

            if (promotion == null)
            {
                return BadRequest("PROMOTION_NOT_EXIST");
            }

            body.Id = id;
            body.Updated = DateTime.Now;
            body.UpdatedBy = CurrentUserEmail;

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _promotion.Update(body);
                transaction.Commit();
            }

            return Ok();
        }

        [Authorize(Policy = "User")]
        [HttpDelete("{id}")]
        public IActionResult DeletePromotion([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var promotion = _promotion.GetAll()
                .Where(e => e.Id == id)
                .FirstOrDefault();

            if (promotion == null)
            {
                return BadRequest("PROMOTION_NOT_EXIST");
            }

            promotion.Updated = DateTime.Now;
            promotion.UpdatedBy = CurrentUserEmail;
            promotion.Status = OBJECT_STATUS.DELETED;

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _promotion.Update(promotion);
                transaction.Commit();
            }

            return Ok();
        }
    }
}