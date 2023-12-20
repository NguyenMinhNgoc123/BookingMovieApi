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
    public class PromotionController : movieControllerBase
    {
        private readonly PromotionServices _promotion;
        private readonly UnitOfWork _unitOfWork;
        private readonly CustomerPromotionServices _customerPromotion;
        
        public PromotionController(
            PromotionServices promotionServices,
            UnitOfWork unitOfWork,
            CustomerPromotionServices customerPromotion,
            UserServices userService) : base(userService)
        {
            _unitOfWork = unitOfWork;
            _promotion = promotionServices;
            _customerPromotion = customerPromotion;
        }

        [Authorize(Policy = "Customer")]
        [HttpGet]
        public IActionResult GetMovieDateTimeSetting()
        {
            var promotionIds = _customerPromotion
                .GetAll()
                .AsNoTracking()
                .Where(e => e.CustomerId == CurrentCustomerId)
                .Select(e => e.PromotionId)
                .ToList();

            var promotions = new List<Promotion>();
            if (promotionIds.Count >= 0)
            {
                promotions = _promotion.GetAll()
                    .Where(e => promotionIds.Contains(e.Id))
                    .AsNoTracking()
                    .Where(e => e.AvailableFrom <= DateTime.Now)
                    .Where(e => e.AvailableTo >= DateTime.Now)
                    .ToList();
            }

            return OkList(promotions);
        }
    }
}