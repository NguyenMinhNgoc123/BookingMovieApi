using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BOOKING_MOVIE_ADMIN.Helper;
using BOOKING_MOVIE_ADMIN.Reponse;
using BOOKING_MOVIE_ADMIN.Values;
using BOOKING_MOVIE_CORE.Services;
using BOOKING_MOVIE_CORE.Values;
using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

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
        private readonly MomoConfig _momoConfig;
        private readonly PaymentMethodServices _paymentMethod;
        private readonly ComboFoodServices _comboFood;
        private readonly FoodServices _food;

        public InvoiceController(
            CustomerServices customer,
            InvoiceServices invoice,
            InvoicesDetailServices invoicesDetail,
            UnitOfWork unitOfWork,
            PromotionServices promotion,
            InvoicePaymentServices invoicePayment,
            IOptions<MomoConfig> momoConfig,
            PaymentMethodServices paymentMethod,
            ComboFoodServices comboFood,
            FoodServices food,
            UserServices userService) : base(userService)
        {
            _promotion = promotion;
            _invoicesDetail = invoicesDetail;
            _unitOfWork = unitOfWork;
            _customer = customer;
            _invoice = invoice;
            _invoicePayment = invoicePayment;
            _paymentMethod = paymentMethod;
            _comboFood = comboFood;
            _food = food;
            _momoConfig = momoConfig.Value;
        }

        [HttpGet]
        public IActionResult GetUser(string keyword = "", string type = "MANAGEMENT")
        {
            var data = _invoice.GetAll()
                .ToList();

            return OkList(data);
        }
        
        [Authorize(Policy = "Customer")]
        [HttpGet("code/{code}")]
        public IActionResult GetInvoiceCode([FromRoute] string code)
        {
            var data = _invoice.GetAll()
                .AsNoTracking()
                .Where(e => e.Code == code)
                .Where(e => e.CustomerId == CurrentCustomerId)
                .Include(e => e.InvoiceDetails)
                .Include(e => e.InvoicePayment)
                .ToList();

            return OkList(data);
        }
        
        [Authorize(Policy = "Customer")]
        [HttpGet("/return/{code}")]
        public IActionResult ReturnInvoicePaymentMomo([FromRoute] string code)
        {
            var data = _invoice.GetAll()
                .Where(e => e.Code == code)
                .Where(e => e.CustomerId == CurrentCustomerId)
                .Include(e => e.InvoiceDetails)
                .Include(e => e.InvoicePayment)
                .FirstOrDefault();

            if (data != null)
            {
                data.PaymentStatus = PAYMENT_STATUS.PAID;

                using (var transaction = _unitOfWork.BeginTransaction())
                {
                    _invoice.Update(data);
                    transaction.Commit();
                }   
            }

            return Ok(data);
        }
        
        [Authorize(Policy = "Customer")]
        [HttpGet("Histories")]
        public IActionResult GetInvoiceHistory()
        {
            var data = _invoice.GetAll()
                .AsNoTracking()
                .Where(e => e.CustomerId == CurrentCustomerId)
                .Include(e => e.InvoiceDetails)
                .ThenInclude(e => e.Movie)
                .Include(e => e.InvoiceDetails)
                .ThenInclude(e => e.MovieDateSetting)
                .Include(e => e.InvoiceDetails)
                .ThenInclude(e => e.MovieTimeSetting)
                .Include(e => e.InvoiceDetails)
                .ThenInclude(e => e.Cinema)
                .Include(e => e.InvoiceDetails)
                .ThenInclude(e => e.Room)
                .Include(e => e.InvoicePayment)
                .OrderByDescending(e => e.Created)
                .ToList();

            if (data.Count > 0)
            {
                var comboFoodIds = data
                    .Where(e => e.InvoiceDetails.Any(o => o.ObjectName == "COMBO_FOOD"))
                    .SelectMany(e => e.InvoiceDetails.Select(o => o.ObjectId))
                    .ToList();

                var foodIds = data
                    .Where(e => e.InvoiceDetails.Any(o => o.ObjectName == "FOOD"))
                    .SelectMany(e => e.InvoiceDetails.Select(o => o.ObjectId))
                    .ToList();

                var comboFoods = _comboFood.GetAll()
                    .Where(e => comboFoodIds.Contains(e.Id))
                    .ToList();

                var foods = _food.GetAll()
                    .Where(e => foodIds.Contains(e.Id))
                    .ToList();

                foreach (var elm in data)
                {
                    foreach (var e in elm.InvoiceDetails)
                    {
                        if (e.ObjectName == "COMBO_FOOD" && comboFoods.Count > 0)
                        {
                            e.ComboFood = comboFoods.Where(o => o.Id == e.ObjectId).FirstOrDefault();
                        }

                        if (e.ObjectName == "FOOD" && foods.Count > 0)
                        {
                            e.Food = foods.Where(o => o.Id == e.ObjectId).FirstOrDefault();
                        }
                    }
                }
            }
            
            return OkList(data);
        }
        
        [Authorize(Policy = "Customer")]
        [HttpPut("Payment/momo/{code}")]
        public IActionResult CreateInvoicePaymentMomoCode([FromRoute] string code)
        {
            var invoice = _invoice.GetAll()
                .Where(e => e.Code == code)
                .Where(e => e.CustomerId == CurrentCustomerId)
                .Where(e => e.InvoicePayment.Any(o => o.InvoiceMethod.Code == PAYMENT_METHOD.MOMO))
                .Include(e => e.InvoiceDetails)
                .FirstOrDefault();
            
            if (invoice == null)
            {
                return BadRequest();
            }
            var seatsSelectSold = new List<string>();
            foreach (var e in invoice.InvoiceDetails)
            {
                if (e.ObjectName == OBJECT_NAME_MOVIE.SEAT)
                {
                    seatsSelectSold.Add(e.ObjectCode);
                }
            };
            
            var momoRequest = new MomoRequestDto()
            {
                partnerCode = _momoConfig.PartnerCode,
                partnerName = "Test",
                storeId = "MoMoTestStore",
                requestType = "onDelivery",
                ipnUrl = _momoConfig.IpnUrl,
                redirectUrl = _momoConfig.ReturnUrl,
                orderId = invoice.Code,
                amount = (long)invoice.Total,
                lang = "en",
                orderInfo = string.Join(',', seatsSelectSold),
                requestId = invoice.Code,
                extraData = "eyJ1c2VybmFtZSI6ICJtb221vIn0=",
            };

            var rawSignature = "accessKey=" + _momoConfig.AccessKey +
                               "&amount=" + momoRequest.amount +
                               "&extraData=" + momoRequest.extraData +
                               "&ipnUrl=" + momoRequest.ipnUrl +
                               "&orderId=" + momoRequest.orderId +
                               "&orderInfo=" + momoRequest.orderInfo +
                               "&partnerCode=" + momoRequest.partnerCode +
                               "&redirectUrl=" + momoRequest.redirectUrl +
                               "&requestId=" + momoRequest.requestId +
                               "&requestType=" + momoRequest.requestType;

            momoRequest.signature = HashHelper.CreateSHA256(rawSignature, _momoConfig.SecretKey);

            (bool createMomoLinkResult, string createMessage) = momoRequest.GetLink(_momoConfig.PaymentUrl);
            return Ok(createMessage);
        }
        
        [Authorize(Policy = "Customer")]
        [HttpGet("Sum")]
        public SumInvoiceDto GetSumInvoice()
        {
            var data = _invoice.GetAll()
                .Where(e => e.CustomerId == CurrentCustomerId)
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
        
        [Authorize(Policy = "Customer")]
        [HttpPut("Chart/PaymentStatus")]
        public IActionResult CreateInvoicePaymentMomo()
        {
            var invoices = _invoice.GetAll()
                .Where(e => e.PaymentStatus == PAYMENT_STATUS.WAITING_10_MINUTE)
                .ToList();

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                if (invoices.Count > 0)
                {
                    foreach (var e in invoices)
                    {
                        if (e.Created != null)
                        {
                            var waitingTenMinute = e.Created.Value.AddMinutes(10);
                            if (waitingTenMinute <= DateTime.Now)
                            {
                                e.Status = OBJECT_STATUS.DELETED;
                            }   
                        }
                    }
                    
                    _invoice.UpdateRange(invoices);
                }

                transaction.Commit();
            }
            return Ok();
        }
        
        [Authorize(Policy = "Customer")]
        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] InvoiceCreateDto body)
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
            var promotionInvoiceDetailIds = body.InvoiceDetails
                .Where(e => e.PromotionId != null)
                .Select(e => e.PromotionId)
                .ToList();

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

            var seatsSelectSold = new List<string>();
            var createInvoiceDetails = body.InvoiceDetails.Select(e =>
            {
                var promotion = promotions.FirstOrDefault(o => o.Id == e.PromotionId);
                var total = _invoicesDetail.TotalInvoiceDetail(e.ObjectPrice, e.Quantity, e.PromotionId, e.DiscountValue ?? 0);

                if (e.ObjectName == OBJECT_NAME_MOVIE.SEAT)
                {
                    seatsSelectSold.Add(e.ObjectCode);
                }
                
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
                    DiscountUnit = promotion?.DiscountUnit != null ? promotion?.DiscountUnit : DISCOUNT_UNIT.PERCENT ,
                    DiscountValue = promotion?.DiscountValue ?? 0,
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
            if (body.PromotionId != null)
            {
                var promotion = _promotion.GetAll()
                    .Where(e => e.Id == body.PromotionId)
                    .Where(e => e.AvailableFrom <= DateTime.Now)
                    .Where(e => e.AvailableTo >= DateTime.Now)
                    .FirstOrDefault();

                if (promotion != null)
                {
                    body.DiscountValue = promotion.DiscountValue;
                    body.DiscountUnit = promotion.DiscountUnit;
                }
            }
            decimal totalInvoice = _invoice.CalculateTotal(totalAllDetailInvoice, body.DiscountValue ?? 0, body.DiscountUnit);

            var invoicePaymentMethodIds = body.InvoicePayment.Select(e => e.InvoiceMethodId).ToList();
            var paymentMethodMomo = _paymentMethod.GetAll().Where(e => e.Code == PAYMENT_METHOD.MOMO).Where(e => invoicePaymentMethodIds.Contains(e.Id)).ToList();
            
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
                PaymentStatus = paymentMethodMomo.Count == 0 ? PAYMENT_STATUS.PAID : PAYMENT_STATUS.WAITING_10_MINUTE,
                PaidTotal = body.InvoicePayment.Sum(e => e.Total),
                DiscountTotal = discountTotal,
                PaidAt = DateTime.Now,
                PromotionId = body.PromotionId,
                Created = DateTime.Now,
                CreatedBy = CurrentUserEmail,
                Status = OBJECT_STATUS.ENABLE,
            };
            
            using (var transaction = _unitOfWork.BeginTransaction())
            {
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
                    InvoiceId = createInvoice.Id,
                    NameAtm = e.NameAtm,
                    NumberAtm = e.NumberAtm,
                    NameShortCutAtm = e.NameShortCutAtm,
                    NotePayment = e.NotePayment
                }).ToList();

                _invoicePayment.AddRange(createInvoicePayment);

                transaction.Commit();
            }

            if (paymentMethodMomo.Count > 0)
            {
                var momoRequest = new MomoRequestDto()
                {
                    partnerCode = _momoConfig.PartnerCode,
                    partnerName = "Test",
                    storeId = "MoMoTestStore",
                    requestType = "onDelivery",
                    ipnUrl = _momoConfig.IpnUrl,
                    redirectUrl = _momoConfig.ReturnUrl,
                    orderId = body.Code,
                    amount = (long)totalInvoice,
                    lang = "en",
                    orderInfo = string.Join(',', seatsSelectSold),
                    requestId = body.Code,
                    extraData = "eyJ1c2VybmFtZSI6ICJtb221vIn0=",
                };

                var rawSignature = "accessKey=" + _momoConfig.AccessKey +
                                   "&amount=" + momoRequest.amount +
                                   "&extraData=" + momoRequest.extraData +
                                   "&ipnUrl=" + momoRequest.ipnUrl +
                                   "&orderId=" + momoRequest.orderId +
                                   "&orderInfo=" + momoRequest.orderInfo +
                                   "&partnerCode=" + momoRequest.partnerCode +
                                   "&redirectUrl=" + momoRequest.redirectUrl +
                                   "&requestId=" + momoRequest.requestId +
                                   "&requestType=" + momoRequest.requestType;

                momoRequest.signature = HashHelper.CreateSHA256(rawSignature, _momoConfig.SecretKey);

                (bool createMomoLinkResult, string createMessage) = momoRequest.GetLink(_momoConfig.PaymentUrl);
                return Ok(createMessage);
            }

            return Ok();
        }
        
    }
}