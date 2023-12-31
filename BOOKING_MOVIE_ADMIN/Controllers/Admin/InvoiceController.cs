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
    [Route("Admin/Invoice")]
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

        [Authorize(Policy = "User")]
        [HttpGet]
        public IActionResult GetInvoice()
        {
            var data = _invoice.GetAll()
                .Include(e => e.InvoiceDetails)
                .Include(e => e.InvoicePayment)
                .Include(e => e.Customer)
                .Include(e => e.Promotion)
                .OrderByDescending(e => e.Created)
                .ToList();

            return OkList(data);
        }
        
        [Authorize(Policy = "User")]
        [HttpGet("Sum")]
        public SumInvoiceDto GetSumInvoice()
        {
            var data = _invoice.GetAll()
                .Include(e => e.InvoiceDetails)
                .Include(e => e.InvoicePayment)
                .Include(e => e.Customer)
                .Include(e => e.Promotion)
                .ToList();

            var sumCustomer = _customer.GetAll().ToList().Count();
            var newSumInvoice = new SumInvoiceDto()
            {
                CountInvoice = data.ToList().Count,
                RevenueInvoice = data.ToList().Sum(e => e.Total ?? 0),
                RevenueInvoicePaid = data.ToList().Sum(e => e.PaidTotal ?? 0),
                CountCustomer = data.ToList().Select(e => e.CustomerId).Distinct().Count(),
                SumCustomer = sumCustomer
            };

            return newSumInvoice;
        }


        [Authorize(Policy = "User")]
        [HttpGet("{id}")]
        public IActionResult GetUser([FromRoute] long id)
        {
            var data = _invoice.GetAll()
                .AsNoTracking()
                .Where(e => e.Id == id)
                .Include(e => e.InvoiceDetails)
                .ThenInclude(e => e.Movie)
                .Include(e => e.InvoiceDetails)
                .ThenInclude(e => e.Promotion)
                .Include(e => e.InvoiceDetails)
                .ThenInclude(e => e.Room)
                .Include(e => e.InvoiceDetails)
                .ThenInclude(e => e.Cinema)
                .Include(e => e.InvoiceDetails)
                .ThenInclude(e => e.MovieDateSetting)
                .Include(e => e.InvoiceDetails)
                .ThenInclude(e => e.MovieTimeSetting)
                .Include(e => e.InvoicePayment)
                .Include(e => e.Customer)
                .Include(e => e.Promotion)
                .FirstOrDefault();

            return Ok(data);
        }
        
        [HttpDelete("{id}")]
        [Authorize(Policy = "User")]
        public IActionResult DeleteInvoice([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var invoice = _invoice
                .GetAll()
                .Where(e => e.Id == id)
                .FirstOrDefault();

            if (invoice == null)
            {
                return NotFound();
            }

            invoice.Status = OBJECT_STATUS.DELETED;
            invoice.Updated = DateTime.Now;
            invoice.UpdatedBy = CurrentUserEmail;
            
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _invoice.Update(invoice);
                transaction.Commit();
            }
            
            return Ok(invoice);
        }
    }
}