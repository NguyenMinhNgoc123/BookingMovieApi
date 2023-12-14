using System;
using System.Linq;
using BOOKING_MOVIE_ADMIN.Reponse;
using BOOKING_MOVIE_ADMIN.Values;
using BOOKING_MOVIE_CORE.Services;
using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BOOKING_MOVIE_ADMIN.Controllers
{
    [Route("[controller]")]
    public class CustomerController : movieControllerBase
    {
        private readonly CustomerServices _customer;
        private readonly AuthServices _auth;
        private readonly UnitOfWork _unitOfWork;
        private readonly PhotoServices _photo;
        public CustomerController(
            CustomerServices customerServices,
            AuthServices authServices,
            UnitOfWork unitOfWork,
            PhotoServices photo,
            UserServices userService) : base(userService)
        {
            _photo = photo;
            _customer = customerServices;
            _auth = authServices;
            _unitOfWork = unitOfWork;
        }
        
        [HttpPost("create")]
        [Authorize("User")]
        public IActionResult CreateUser([FromBody] CustomerDto body)
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
            
            var mailExist = _customer.GetAll()
                .Where(o => o.Email == body.Email)
                .FirstOrDefault();

            if (mailExist != null)
            {
                return BadRequest("EMAIL_EXIST");
            }

            body.CreatedBy = CurrentUserEmail;
            body.Created = DateTime.Now;
            body.Status = OBJECT_STATUS.ENABLE;
            body.PasswordHash = _auth.BCryptPasswordEncoder(body.Password);
            
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _customer.Add(body);

                transaction.Commit();
            }

            return Ok(body);
        }

        [HttpPut("{id}")]
        [Authorize("Customer")]
        public IActionResult UpdateCustomer([FromRoute] long id, [FromBody] CustomerDto body)
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

            body.UpdatedBy = CurrentUserEmail;
            body.Updated = DateTime.Now;
            if (body.Password != null) 
            {
                body.PasswordHash = _auth.BCryptPasswordEncoder(body.Password);
            }
            else
            {
                body.PasswordHash = userExist.PasswordHash;

            }

            var mobileExist = _customer.GetAll()
                .Where(e => e.Mobile == body.Mobile)
                .FirstOrDefault(e => e.Id != id);
            
            if (mobileExist != null)
            {
                return BadRequest("MOBILE_EXIST");
            }
            
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _customer.Update(body);

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

            var email = CurrentUserEmail;
            var userId = CurrentCustomerId;
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
        
        [HttpGet("profileLogin")]
        [Authorize(Policy = "Customer")]
        public IActionResult GetCustomer()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var customer = _customer
                .GetAll()
                .Where(e => e.Id == CurrentCustomerId)
                .FirstOrDefault();

            if (customer == null)
            {
                return NotFound();
            }

            var profilePhoto = _photo.GetAll()
                .Where(e => e.ObjectId == customer.Id)
                .Where(e => e.Type == PHOTO.PROFILE_CUSTOMER)
                .FirstOrDefault();

                if (profilePhoto != null)
                {
                    customer.ProfilePhoto = profilePhoto.url;
                }
            
            return Ok(customer);
        }
        
        [HttpGet("checkOldPassword/{oldPassword}")]
        [Authorize(Policy = "Customer")]
        public IActionResult GetCustomer([FromRoute] string oldPassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var customer = _auth.AuthenticateCustomer(CurrentUserEmail, oldPassword);


            if (customer == null)
            {
                return BadRequest("PASSWORD_INCORRECT");
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
        
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register([FromBody] CustomerDto body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var userExist = _customer.GetAll()
                .Where(o => o.Email == body.Email)
                .FirstOrDefault();

            if (userExist != null)
            {
                return BadRequest("CUSTOMER_EXIST");
            }

            body.CreatedBy = body.Email;
            body.Status = OBJECT_STATUS.ENABLE;
            body.Created = DateTime.Now;
            body.PasswordHash = _auth.BCryptPasswordEncoder(body.Password);
            if (body.Mobile == null)
            {
                body.Mobile = body.Email;
            }
            
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _customer.Add(body);

                transaction.Commit();
            }

            return Ok(body);
        }
        
        [HttpPut("updateImage/{id}")]
        [Authorize("Customer")]
        public IActionResult UpdateImageCustomer([FromRoute] long id, [FromBody] Photo body)
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

            var oldImage = _photo.GetAll()
                .Where(e => e.ObjectId == id)
                .Where(e => e.Type == PHOTO.PROFILE_CUSTOMER)
                .FirstOrDefault();

            body.UpdatedBy = CurrentUserEmail;
            body.Updated = DateTime.Now;
            body.Status = OBJECT_STATUS.ENABLE;
            body.Type = PHOTO.PROFILE_CUSTOMER;
            body.url = body.url;
            body.ObjectId = CurrentCustomerId;
            
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                if (oldImage != null)
                {
                    oldImage.Status = OBJECT_STATUS.DELETED;
                    _photo.Update(oldImage);
                }
                
                _photo.Update(body);

                transaction.Commit();
            }

            return Ok(body);
        }
    }
}