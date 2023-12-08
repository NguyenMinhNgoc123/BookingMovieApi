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
    public class ActorController : movieControllerBase
    {
        private readonly ActorServices _actor;
        private readonly UnitOfWork _unitOfWork;
                
        public ActorController(
            ActorServices actor,
            UnitOfWork unitOfWork,
            UserServices userService) : base(userService)
        {
            _actor = actor;
            _unitOfWork = unitOfWork;
        }
        
        [Authorize("User")]
        [HttpGet]
        public IActionResult GetActor()
        {
            var data = _actor.GetAll().AsNoTracking().ToList();

            return OkList(data);
        }
    }
}