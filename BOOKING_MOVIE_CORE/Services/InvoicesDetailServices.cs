using System;
using System.Collections.Generic;
using System.Linq;
using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;
using Microsoft.EntityFrameworkCore;

namespace BOOKING_MOVIE_CORE.Services
{
    public class InvoicesDetailServices : ApplicationService<InvoiceDetails>
    {
        private readonly MovieServices _movie;
        public InvoicesDetailServices(
            MovieServices movie,
            GenericDomainService<InvoiceDetails> genericDomainService) : base(genericDomainService)
        {
            _movie = movie;
        }

        public decimal TotalInvoice (List<InvoiceDetails> invoiceDetails)
        {
            decimal total = 0;
            
            foreach (var invoiceDetail in invoiceDetails)
            {
                var totalDetail = invoiceDetail.ObjectPrice * invoiceDetail.Quantity;
                if (invoiceDetail.PromotionId != null)
                {
                    decimal discountTotal = totalDetail / 100 * invoiceDetail.DiscountValue ?? 0;
                    totalDetail -= discountTotal;
                }
                
                total += totalDetail;
            }
            
            return total;
        }

        public decimal TotalInvoiceDetail(
            decimal objectPrice,
            decimal quantity,
            long? promotionId,
            decimal discountValue
            )
        {
            decimal total = 0;

            var totalDetail = objectPrice * quantity;
            if (promotionId != null)
            {
                decimal discountTotal = totalDetail / 100 * discountValue;
                totalDetail -= discountTotal;
            }

            total = totalDetail;

            return total;
        }
        
        public List<string> CheckSeatBooked(List<InvoiceDetails> invoiceDetails)
        {
            List<string> seatBooked = new List<string>();

            var seatMovie = invoiceDetails
                .Where(e => e.ObjectName == OBJECT_NAME_MOVIE.SEAT)
                .Select(e => e.ObjectCode).ToList();
            var movieTimeSettingIds = invoiceDetails
                .Where(e => e.ObjectName == OBJECT_NAME_MOVIE.SEAT)
                .Select(e => e.MovieTimeSettingId).ToList();

            var checkSeatInvoiceDetail = GetAll()
                .Where(e => e.Invoice.Status == OBJECT_STATUS.ENABLE)
                .Where(e => e.ObjectName == OBJECT_NAME_MOVIE.SEAT)
                .Where(e => seatMovie.Contains(e.ObjectCode))
                .Where(e => movieTimeSettingIds.Contains(e.MovieTimeSettingId))
                .ToList();

            if (checkSeatInvoiceDetail.Count > 0)
            {
                foreach (var e in invoiceDetails)
                {
                    var checkSeatTimeSetting = checkSeatInvoiceDetail
                        .Where(o => o.MovieTimeSettingId == e.MovieTimeSettingId)
                        .Any(o => o.ObjectCode == e.ObjectCode);
                    
                    if (checkSeatTimeSetting)
                    {
                        seatBooked.Add(e.ObjectCode);  
                    }
                }
            }

            return seatBooked;
        }

        public string CheckMovieExist(List<InvoiceDetails> invoiceDetails)
        {
            var movieIds = invoiceDetails
                .Where(e => e.ObjectName == OBJECT_NAME_MOVIE.SEAT)
                .Select(e => e.MovieId)
                .ToList();

            var movies = _movie.GetAll()
                .Where(e => movieIds.Contains(e.Id))
                .Include(e => e.MovieDateSettings)
                .ThenInclude(e => e.MovieCinemas)
                .ThenInclude(e => e.MovieRooms)
                .ThenInclude(e => e.MovieTimeSettings)
                .ToList();

            if (movies.Count > 0)
            {
                foreach (var invoiceDetail in invoiceDetails)
                {
                    var movie = movies.FirstOrDefault(e => e.Id == invoiceDetail.MovieId);
                    if (movie == null)
                    {
                        return $"MOVIE_NOT_EXIST_ID_{invoiceDetail.MovieId}";
                    }
                    
                    var movieDateSetting =
                        movie?.MovieDateSettings.FirstOrDefault(e => e.Id == invoiceDetail.MovieDateSettingId);
                    if (movieDateSetting == null)
                    {
                        return $"MOVIE_DATE_SETTING_NOT_EXIST_ID_{invoiceDetail.MovieDateSettingId}";
                    }

                    var movieCinema = movieDateSetting?.MovieCinemas.FirstOrDefault(e => e.CinemaId == invoiceDetail.CinemaId);
                    if (movieCinema == null)
                    {
                        return $"MOVIE_CINEMA_NOT_EXIST_ID_{invoiceDetail.CinemaId}";
                    }
                    
                    var movieRoom = movieCinema?.MovieRooms.FirstOrDefault(e => e.RoomId == invoiceDetail.RoomId);
                    if (movieRoom == null)
                    {
                        return $"MOVIE_ROOM_NOT_EXIST_ID_{invoiceDetail.RoomId}";
                    }

                    var movieTimeSettings =
                        movieRoom?.MovieTimeSettings.FirstOrDefault(e => e.Id == invoiceDetail.MovieTimeSettingId);
                    if (movieTimeSettings == null)
                    {
                        return $"MOVIE_TIME_SETTING_NOT_EXIST_ID_{invoiceDetail.MovieTimeSettingId}";
                    }
                }
            }

            return "SUCCESS";
        }
    }
}