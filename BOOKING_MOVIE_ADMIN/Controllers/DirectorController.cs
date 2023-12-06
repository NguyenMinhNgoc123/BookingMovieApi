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
    public class DirectorController : movieControllerBase
    {
        private readonly DirectorServices _director;
        private readonly UnitOfWork _unitOfWork;
        
        public DirectorController(
            DirectorServices director,
            UserServices userService,
            UnitOfWork unitOfWork
            ) : base(userService)
        {
            _unitOfWork = unitOfWork;
            _director = director;
        }
        
    }
}