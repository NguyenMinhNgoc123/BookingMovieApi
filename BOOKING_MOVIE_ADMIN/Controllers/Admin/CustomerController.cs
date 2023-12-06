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

namespace BOOKING_MOVIE_ADMIN.Controllers.Admin
{
    [Route("Admin/Customer")]
    [ApiController]
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
        
        [HttpGet]
        [Authorize(Policy = "User")]
        public IActionResult GetCustomer()
        {
            var customers = _customer
                .GetAll()
                .AsNoTracking()
                .OrderByDescending(e => e.Created)
                .ToList();

            if (customers.Count > 0)
            {
                var movieIds = customers.Select(e => e.Id);
                var profilePhoto = _photo.GetAll()
                    .Where(e => movieIds.Contains(e.ObjectId))
                    .Where(e => e.Type == PHOTO.PROFILE_CUSTOMER)
                    .ToList();

                if (profilePhoto.Count > 0)
                {
                    foreach (var elm in customers)
                    {
                        elm.ProfilePhoto = profilePhoto.FirstOrDefault(o => o.ObjectId == elm.Id)?.url;
                    }
                }
            }
            
            return OkList(customers);
        }
        
        [HttpGet("{id}")]
        [Authorize(Policy = "User")]
        public IActionResult GetCustomer([FromRoute] long id)
        {
            var customer = _customer
                .GetAll()
                .AsNoTracking()
                .Where(e => e.Id == id)
                .FirstOrDefault();

            if (customer != null)
            {
                var profilePhoto = _photo.GetAll()
                    .Where(e => customer.Id == e.ObjectId)
                    .Where(e => e.Type == PHOTO.PROFILE_CUSTOMER)
                    .FirstOrDefault();

                customer.ProfilePhoto = profilePhoto?.url;
            }
            
            return Ok(customer);
        }
        
        [HttpPost]
        [Authorize(Policy = "User")]
        public IActionResult CreateCustomer([FromBody] CustomerDto body)
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
                return BadRequest("CUSTOMER_EMAIL_EXIST");
            }

            if (body.Mobile != null)
            {
                var userMobileExist = _customer.GetAll()
                    .Where(o => o.Mobile == body.Mobile)
                    .FirstOrDefault();
                
                if (userMobileExist != null)
                {
                    return BadRequest("CUSTOMER_MOBILE_EXIST");
                }
            }

            body.CreatedBy = CurrentUserEmail;
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
        [Authorize(Policy = "User")]
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
            
            body.Status = OBJECT_STATUS.ENABLE;
            body.Type = PHOTO.PROFILE_CUSTOMER;
            body.url = body.url;
            body.ObjectId = id;
            
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                if (oldImage != null)
                {
                    body.UpdatedBy = CurrentUserEmail;
                    body.Updated = DateTime.Now;
                    oldImage.Status = OBJECT_STATUS.DELETED;
                    _photo.Update(oldImage);
                }   
                else
                {
                    body.CreatedBy = CurrentUserEmail;
                    body.Created = DateTime.Now;
                }
                
                _photo.Update(body);

                transaction.Commit();
            }

            return Ok(body);
        }
        
        [HttpPut("{id}")]
        [Authorize(Policy = "User")]
        public IActionResult UpdateCustomer([FromRoute] long id, [FromBody] CustomerDto body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var userExist = _customer.GetAll()
                .Where(o => o.Id == id)
                .FirstOrDefault();

            if (userExist == null)
            {
                return BadRequest("CUSTOMER_NOT_EXIST");
            }

            if (userExist.Email != body.Email)
            {
                var emailExist = _customer.GetAll()
                    .Where(o => o.Email == body.Email)
                    .Where(o => o.Id != id)
                    .FirstOrDefault();

                if (emailExist != null)
                {
                    return BadRequest("CUSTOMER_EMAIL_EXIST");
                }
                
                userExist.Email = body.Email;
            }
            
            if (userExist.Mobile != body.Mobile)
            {
                var phoneExist = _customer.GetAll()
                    .Where(o => o.Mobile == body.Mobile)
                    .Where(o => o.Id != id)
                    .FirstOrDefault();

                if (phoneExist != null)
                {
                    return BadRequest("CUSTOMER_MOBILE_EXIST");
                }
                
                userExist.Email = body.Email;
            }

            if (body.Name == null)
            {
                return BadRequest("CUSTOMER_NAME_NOT_NULL");
            }
            
            userExist.UpdatedBy = CurrentUserEmail;
            userExist.Updated = body.Updated;
            userExist.Address = body.Address;
            userExist.Note = body.Note;
            userExist.Sex = body.Sex;
            userExist.Code = body.Code;
            userExist.Name = body.Name;

            if (body.Password != null) 
            {
                userExist.PasswordHash = _auth.BCryptPasswordEncoder(body.Password);
            }
            
            
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _customer.Update(userExist);

                transaction.Commit();
            }

            return Ok(body);
        }
        
        [HttpDelete("{id}")]
        [Authorize(Policy = "User")]
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