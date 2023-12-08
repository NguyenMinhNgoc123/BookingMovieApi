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
    [Route("{Controller}")]
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
        
    }
}