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
    [Route("Admin/MovieRoom")]
    [ApiController]
    public class MovieRoomController : movieControllerBase
    {
        private UnitOfWork _unitOfWork;
        private MovieRoomServices _movieRoom;
        private InvoiceServices _invoice;
        
        public MovieRoomController(
            UnitOfWork unitOfWork,
            MovieRoomServices movieRoom,
            InvoiceServices invoice,
            UserServices userService) : base(userService)
        {
            _unitOfWork = unitOfWork;
            _movieRoom = movieRoom;
            _invoice = invoice;
        }
        
        [Authorize(Policy = "User")]
        [HttpDelete("{id}")]
        public IActionResult DeleteMovieDateSetting([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movieRoom = _movieRoom.GetAll()
                .Include(e => e.MovieTimeSettings)
                .Where(e => e.Id == id)
                .FirstOrDefault();

            if (movieRoom == null)
            {
                return BadRequest("MOVIE_ROOM_NOT_EXIST");
            }

            var movieTimeSettingIds = movieRoom.MovieTimeSettings.Select(e => id).ToList();
            if (movieTimeSettingIds.Count > 0)
            {
                var invoice = _invoice
                    .GetAll()
                    .Where(e => e.InvoiceDetails.Any(o => movieTimeSettingIds.Contains(o.MovieTimeSettingId.Value)))
                    .Where(e => e.PaymentStatus == PAYMENT_STATUS.PAID)
                    .FirstOrDefault();
            
                if (invoice != null)
                {
                    return BadRequest("ROOM_INVOICE_EXIST");
                }   
            }

            movieRoom.Updated = DateTime.Now;
            movieRoom.UpdatedBy = CurrentUserEmail;
            movieRoom.Status = OBJECT_STATUS.DELETED;

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _movieRoom.Update(movieRoom);
                transaction.Commit();
            }

            return Ok();
        }
    }
}