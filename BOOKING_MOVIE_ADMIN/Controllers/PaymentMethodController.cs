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
    public class PaymentMethodController : movieControllerBase
    {
        private readonly PaymentMethodServices _paymentMethod;
        private readonly UnitOfWork _unitOfWork;
        
        public PaymentMethodController(
            PaymentMethodServices paymentMethodServices,
            UnitOfWork unitOfWork,
            UserServices userService
            ) : base(userService)
        {
            _unitOfWork = unitOfWork;
            _paymentMethod = paymentMethodServices;
        }

        [HttpGet]
        [Authorize(Policy = "User")]
        public IActionResult GetPaymentMethod()
        {
            var data = _paymentMethod
                .GetAll()
                .AsNoTracking()
                .OrderByDescending(e => e.Created)
                .ToList();

            return OkList(data);
        }
        
        [HttpGet("{id}")]
        [Authorize(Policy = "User")]
        public IActionResult GetPaymentMethodDetail([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var data = _paymentMethod
                .GetAll()
                .AsNoTracking()
                .Where(e => e.Id == id)
                .OrderByDescending(e => e.Created)
                .FirstOrDefault();

            return Ok(data);
        }
        
        [HttpPost]
        [Authorize(Policy = "User")]
        public IActionResult Create([FromBody] PaymentMethod body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var paymentMethod = _paymentMethod
                .GetAll()
                .AsNoTracking()
                .Where(e => e.Code == body.Code)
                .FirstOrDefault();

            if (paymentMethod != null)
            {
                return BadRequest("PAYMENT_EXIST");
            }

            body.Status = OBJECT_STATUS.ENABLE;
            body.Created = DateTime.Now;
            body.CreatedBy = CurrentUserEmail;
            
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _paymentMethod.Add(body);
                transaction.Commit();
            }
            
            return Ok();
        }
        
        [HttpPut("{id}")]
        [Authorize(Policy = "User")]
        public IActionResult Update([FromRoute] long id,[FromBody] PaymentMethod body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var paymentMethod = _paymentMethod
                .GetAll()
                .AsNoTracking()
                .Where(e => e.Id == id)
                .FirstOrDefault();

            if (paymentMethod == null)
            {
                return BadRequest("PAYMENT_NOT_EXIST");
            }

            var paymentMethodCheckCode = _paymentMethod
                .GetAll()
                .AsNoTracking()
                .Where(e => e.Id != id)
                .Where(e => e.Code == body.Code)
                .FirstOrDefault();

            if (paymentMethodCheckCode != null)
            {
                return BadRequest("PAYMENT_SAME_CODE_EXIST");
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
        
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var paymentMethod = _paymentMethod
                .GetAll()
                .Where(e => e.Id == id)
                .FirstOrDefault();

            if (paymentMethod == null)
            {
                return BadRequest("PAYMENT_NOT_EXIST");
            }

            paymentMethod.Updated = DateTime.Now;
            paymentMethod.UpdatedBy = CurrentUserEmail;
            paymentMethod.Status = OBJECT_STATUS.DELETED;
            
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _paymentMethod.Update(paymentMethod);
                transaction.Commit();
            }
            
            return Ok();
        }
    }
}