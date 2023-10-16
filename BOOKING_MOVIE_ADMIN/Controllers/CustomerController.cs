using System;
using System.Linq;
using BOOKING_MOVIE_ADMIN.Reponse;
using BOOKING_MOVIE_ADMIN.Values;
using BOOKING_MOVIE_CORE.Services;
using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BOOKING_MOVIE_ADMIN.Controllers
{
    [Route("[controller]")]
    public class CustomerController : movieControllerBase
    {
        public readonly CustomerServices _customer;
        public readonly AuthServices _auth;
        public readonly UnitOfWork _unitOfWork;
        public CustomerController(
            CustomerServices customerServices,
            AuthServices authServices,
            UnitOfWork unitOfWork,
            UserServices userService) : base(userService)
        {
            _customer = customerServices;
            _auth = authServices;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register([FromBody] CustomerDto body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var userExist = _customer.GetAll()
                .Where(o => o.Mobile == body.Mobile)
                .FirstOrDefault();

            if (userExist != null)
            {
                return BadRequest("CUSTOMER_EXIST");
            }

            body.CreatedBy = "";
            body.Status = OBJECT_STATUS.ENABLE;
            body.PasswordHash = _auth.BCryptPasswordEncoder(body.Password);
            
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _customer.Add(body);

                transaction.Commit();
            }

            return Ok(body);
        }
        
        [HttpGet("{id}")]
        [Authorize(Policy = "Customer")]
        public IActionResult GetCustomer([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var customer = _customer
                .GetAll()
                .Where(e => e.Id == id)
                .FirstOrDefault();

            if (customer == null)
            {
                return NotFound();
            }
            
            return Ok(customer);
        }
        
        [HttpDelete("{id}")]
        [Authorize(Policy = "Customer")]
        public IActionResult DeleteCustomer([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var customer = _customer
                .GetAll()
                .Where(e => e.Id == id)
                .FirstOrDefault();

            if (customer == null)
            {
                return NotFound();
            }

            customer.Status = OBJECT_STATUS.DELETED;
            customer.Updated = DateTime.Now;
            customer.UpdatedBy = CurrentUserEmail;
            
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _customer.Update(customer);
                transaction.Commit();
            }
            
            return Ok(customer);
        }
    }
}