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
    [Route("Genre")]
    public class GenreController : movieControllerBase
    {
        private readonly GenreServices _genre;
        private readonly UnitOfWork _unitOfWork;
                
        public GenreController(
            GenreServices genre,
            UnitOfWork unitOfWork,
            UserServices userService) : base(userService)
        {
            _genre = genre;
            _unitOfWork = unitOfWork;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetGenre()
        {
            var data = _genre.GetAll().AsNoTracking().ToList();

            return OkList(data);
        }
        
        
        [AllowAnonymous]
        [HttpGet("movie/list")]
        public IActionResult GetGenreMovieList()
        {
            var data = _genre.GetAll().AsNoTracking().ToList();

            return OkList(data);
        }
    }
}