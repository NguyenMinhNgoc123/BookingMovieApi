using System;
using System.Collections.Generic;
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
    [Route("{Controller}")]
    [ApiController]
    public class InvoiceController : movieControllerBase
    {
        private readonly InvoiceServices _invoice;
        private readonly CustomerServices _customer;
        private readonly UnitOfWork _unitOfWork;
        private readonly InvoicesDetailServices _invoicesDetail;
        private readonly PromotionServices _promotion;
        private readonly InvoicePaymentServices _invoicePayment;
        
        public InvoiceController(
            CustomerServices customer,
            InvoiceServices invoice,
            InvoicesDetailServices invoicesDetail,
            UnitOfWork unitOfWork,
            PromotionServices promotion,
            InvoicePaymentServices invoicePayment,
            UserServices userService) : base(userService)
        {
            _promotion = promotion;
            _invoicesDetail = invoicesDetail;
            _unitOfWork = unitOfWork;
            _customer = customer;
            _invoice = invoice;
            _invoicePayment = invoicePayment;
        }

        [Authorize("User")]
        [HttpGet]
        public IActionResult GetUser(string keyword = "", string type = "MANAGEMENT")
        {
            var data = _invoice.GetAll()
                .ToList();

            return OkList(data);
        }
        
        [Authorize("UserOrCustomer")]
        [HttpPost]
        public IActionResult CreateInvoice([FromBody] InvoiceCreateDto body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (body.CustomerId != null)
            {
                var customer = _customer.GetAll().Where(e => e.Id == body.CustomerId).FirstOrDefault();

                if (customer == null)
                {
                    return BadRequest("CUSTOMER_NOT_EXIST");
                }
            }

            var currentDateTimeNow = DateTime.Now;
            var promotionInvoiceDetailIds = body.InvoiceDetails.Select(e => e.PromotionId).ToList();

            var promotions = new List<Promotion>();
            if (promotionInvoiceDetailIds.Count > 0)
            {
                promotions = _promotion
                    .GetAll()
                    .Where(e => e.AvailableFrom <= currentDateTimeNow)
                    .Where(e => e.AvailableTo >= currentDateTimeNow)
                    .Where(e => promotionInvoiceDetailIds.Contains(e.Id)).ToList();

                if (promotionInvoiceDetailIds.Count() > promotions.Count())
                {
                    return BadRequest("PROMOTION_EXPIRES_OR_DELETED");
                }
            }
            
            var createInvoiceDetails = body.InvoiceDetails.Select(e =>
            {
                var promotion = promotions.FirstOrDefault(o => o.Id == e.PromotionId);
                var total = _invoicesDetail.TotalInvoiceDetail(e.ObjectPrice, e.Quantity, e.PromotionId, e.DiscountValue ?? 0);
                
                return new InvoiceDetails()
                {
                    MovieId = e.MovieId,
                    RoomId = e.RoomId,
                    CinemaId = e.CinemaId,
                    MovieDateSettingId = e.MovieDateSettingId,
                    MovieTimeSettingId = e.MovieTimeSettingId,
                    ObjectId = e.ObjectId,
                    ObjectName = e.ObjectName,
                    ObjectCode = e.ObjectCode,
                    ObjectPrice = e.ObjectPrice,
                    DiscountUnit = promotion?.DiscountUnit,
                    DiscountValue = promotion?.DiscountValue,
                    Total = total,
                    Quantity = e.Quantity,
                    PromotionId = e.PromotionId,
                    Created = DateTime.Now,
                    CreatedBy = CurrentUserEmail,
                    Status = OBJECT_STATUS.ENABLE
                };
            }).ToList();

            var validationResult = _invoicesDetail.CheckMovieExist(createInvoiceDetails);
            if (validationResult != VALIDATE_TEXT.SUCCESS)
            {
                return BadRequest(validationResult);
            }
            
            List<string> checkSeatBooked = _invoicesDetail.CheckSeatBooked(createInvoiceDetails);
            if (checkSeatBooked.Count > 0)
            {
                return BadRequest($"{string.Join("_", checkSeatBooked) + "_SEAT_BOOKED"}");
            }

            decimal totalAllDetailInvoice = _invoicesDetail.TotalInvoice(createInvoiceDetails);
            decimal totalInvoice = _invoice.CalculateTotal(totalAllDetailInvoice, body.DiscountValue ?? 0, body.DiscountUnit);

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                var discountTotal = totalAllDetailInvoice * body.DiscountValue / 100;
                
                var createInvoice = new Invoice()
                {
                    Code = body.Code,
                    CustomerId = body.CustomerId,
                    DiscountUnit = body.DiscountUnit,
                    DiscountValue = body.DiscountValue,
                    IsDisplay = true,
                    Note = body.Note,
                    Total = totalInvoice,
                    NotePayment = body.NotePayment,
                    TotalDetails = totalAllDetailInvoice,
                    PaymentStatus = PAYMENT_STATUS.PAID,
                    PaidTotal = body.InvoicePayment.Sum(e => e.Total),
                    DiscountTotal = discountTotal,
                    PaidAt = DateTime.Now,
                    PromotionId = body.PromotionId,
                    Created = DateTime.Now,
                    CreatedBy = CurrentUserEmail,
                    Status = OBJECT_STATUS.ENABLE
                };

                _invoice.Add(createInvoice);

                
                createInvoiceDetails = createInvoiceDetails.Select(detail =>
                {
                    detail.InvoiceId = createInvoice.Id;
                    return detail;
                }).ToList();
                
                _invoicesDetail.AddRange(createInvoiceDetails);
                
                var createInvoicePayment = body.InvoicePayment.Select(e => new InvoicePayment()
                {
                    InvoiceMethodId = e.InvoiceMethodId,
                    Total = e.Total,
                    Created = DateTime.Now,
                    CreatedBy = CurrentUserEmail,
                    InvoiceId = createInvoice.Id
                }).ToList();

                _invoicePayment.AddRange(createInvoicePayment);
                
                transaction.Commit();
            }
            
            return Ok();
        }
    }
}