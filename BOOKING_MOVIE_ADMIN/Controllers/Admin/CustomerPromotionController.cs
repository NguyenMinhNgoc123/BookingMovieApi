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
    [Route("Admin/CustomerPromotion")]
    [ApiController]
    public class CustomerPromotionController : movieControllerBase
    {
        private readonly PromotionServices _promotion;
        private readonly UnitOfWork _unitOfWork;
        private readonly CustomerPromotionServices _customerPromotion;
        private readonly CustomerServices _customer;

        public CustomerPromotionController(
            PromotionServices promotionServices,
            UnitOfWork unitOfWork,
            CustomerPromotionServices customerPromotion,
            CustomerServices customer,
            UserServices userService) : base(userService)
        {
            _unitOfWork = unitOfWork;
            _promotion = promotionServices;
            _customerPromotion = customerPromotion;
            _customer = customer;
        }
        
        [Authorize(Policy = "User")]
        [HttpPut("{id}")]
        public IActionResult UpdatePromotion([FromRoute] long id, [FromBody] List<CustomerPromotion> body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userExist = _customer.GetAll()
                .AsNoTracking()
                .Where(o => o.Id == id)
                .FirstOrDefault();

            if (userExist == null)
            {
                return BadRequest("CUSTOMER_NOT_EXIST");
            }

            var oldCustomerPromtion = _customerPromotion.GetAll()
                .Where(e => e.CustomerId == id)
                .ToList();

            foreach (var e in body)
            {
                e.Status = OBJECT_STATUS.ENABLE;
                e.Updated = DateTime.Now;
                e.UpdatedBy = CurrentUserEmail;   
            }

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                if (oldCustomerPromtion.Count >= 0)
                {
                    foreach (var e in oldCustomerPromtion)
                    {
                        e.Status = OBJECT_STATUS.DELETED;
                        e.Updated = DateTime.Now;
                        e.UpdatedBy = CurrentUserEmail;   
                    }
                    
                    _customerPromotion.UpdateRange(oldCustomerPromtion);
                }
                
                _customerPromotion.UpdateRange(body);
                transaction.Commit();
            }

            return Ok();
        }
    }
}