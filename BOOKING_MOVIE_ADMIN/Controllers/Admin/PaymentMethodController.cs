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
    [Route("Admin/PaymentMethod")]
    [ApiController]
    public class PaymentMethodController : movieControllerBase
    {
        private readonly PaymentMethodServices _paymentMethod;
        private readonly UnitOfWork _unitOfWork;

        public PaymentMethodController(
            PaymentMethodServices payment,
            UserServices userService,
            UnitOfWork unitOfWork
        ) : base(userService)
        {
            _paymentMethod = payment;
            _unitOfWork = unitOfWork;
        }

        [Authorize(Policy = "User")]
        [HttpGet]
        public IActionResult GetPaymentMethod()
        {
            var data = _paymentMethod.GetAll().AsNoTracking().ToList();

            return OkList(data);
        }
        
        [Authorize(Policy = "User")]
        [HttpGet("{id}")]
        public IActionResult GetPaymentMethodDetail([FromRoute] long id)
        {
            var data = _paymentMethod.GetAll().AsNoTracking().FirstOrDefault(e => e.Id == id);

            return Ok(data);
        }
        
        [Authorize(Policy = "User")]
        [HttpPost]
        public IActionResult CreatePaymentMethod([FromBody] PaymentMethod body)
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
                _paymentMethod.Add(body);
                transaction.Commit();
            }
            
            return Ok();
        }
        
        [Authorize(Policy = "User")]
        [HttpPut("{id}")]
        public IActionResult UpdatePaymentMethod([FromRoute] long id,[FromBody] PaymentMethod body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var director = _paymentMethod.GetAll()
                .Where(e => e.Id == id)
                .AsNoTracking()
                .FirstOrDefault();

            if (director == null)
            {
                return BadRequest("DIRECTOR_NOT_EXIST");
            }
            
            body.Updated = DateTime.Now;
            body.UpdatedBy = CurrentUserEmail;

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _paymentMethod.Update(body);
                transaction.Commit();
            }
            
            return Ok();
        }
        
        [Authorize(Policy = "User")]
        [HttpDelete("{id}")]
        public IActionResult DeletePaymentMethod([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var director = _paymentMethod.GetAll()
                .Where(e => e.Id == id)
                .FirstOrDefault();

            if (director == null)
            {
                return BadRequest("PAYMENT_NOT_EXIST");
            }
            
            director.Updated = DateTime.Now;
            director.UpdatedBy = CurrentUserEmail;
            director.Status = OBJECT_STATUS.DELETED;
            
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _paymentMethod.Update(director);
                transaction.Commit();
            }
            
            return Ok();
        }
    }
}