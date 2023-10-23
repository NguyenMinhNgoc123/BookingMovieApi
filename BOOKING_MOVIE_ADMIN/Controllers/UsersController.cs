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
    [ApiController]
    public class UsersController : movieControllerBase
    {
        public readonly UnitOfWork _unitOfWork;
        public readonly AuthServices _auth;

        public UsersController(
            UnitOfWork unitOfWork,
            UserServices userServices,
            AuthServices auth
            ) : base(userServices)
        {
            _auth = auth;
            _unitOfWork = unitOfWork;
        }
        
        [HttpGet]
        public IActionResult GetUser(string keyword = "", string type = "MANAGEMENT")
        {
            var data = _user.GetAll()
                .ToList();  

            return OkList(data);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserDetail([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var data = _user.GetAll()
                .Where(e => e.Id == id)
                .FirstOrDefault();

            if (data == null)
            {
                return BadRequest("USER_NOT_EXIST");
            }
            
            return Ok(data);
        }
        
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserPasswordValue body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var userExist = _user.GetAll()
                .Where(o => o.Email == body.Email)
                .FirstOrDefault();

            if (userExist != null)
            {
                return BadRequest("USER_EXIST");
            }

            body.CreatedBy = "";
            body.Status = OBJECT_STATUS.ENABLE;
            body.PasswordHash = _auth.BCryptPasswordEncoder(body.Password);
            
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _user.Add(body);

                transaction.Commit();
            }

            return Ok(body);
        }
        
        [HttpPut("{id}")]
        public IActionResult UpdateUser([FromRoute] long id, [FromBody] UserPasswordValue body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var userExist = _user.GetAll()
                .Where(o => o.Id == id)
                .Where(o => o.Email == body.Email)
                .FirstOrDefault();

            if (userExist == null)
            {
                return BadRequest("USER_NOT_EXIST");
            }

            body.Email = userExist.Email;
            body.CreatedBy = "";
            body.Status = OBJECT_STATUS.ENABLE;
            
            if (!string.IsNullOrEmpty(body.Password))
            {
                body.PasswordHash = _auth.BCryptPasswordEncoder(body.Password);
            }
            
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _user.Update(body);

                transaction.Commit();
            }

            return Ok(body);
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteUser([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userExist = _user.GetAll().Where(e => e.Id == id).FirstOrDefault();

            if (userExist == null)
            {
                return NotFound();
            }

            userExist.Status = OBJECT_STATUS.DELETED;
            userExist.Updated = DateTime.Now;
            userExist.UpdatedBy = "";

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _user.Update(userExist);
                transaction.Commit();
            }

            return Ok();
        }
    }
}