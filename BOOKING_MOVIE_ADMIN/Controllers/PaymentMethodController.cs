using System;
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
    [Route("[controller]")]
    [ApiController]
    public class PaymentMethodController : movieControllerBase
    {
        private readonly PaymentMethodServices _paymentMethod;
        private readonly UnitOfWork _unitOfWork;
        private readonly MomoConfig _momoConfig;

        public PaymentMethodController(
            PaymentMethodServices paymentMethodServices,
            UnitOfWork unitOfWork,
            UserServices userService,           
            IOptions<MomoConfig> momoConfig
        ) : base(userService)
        {
            _unitOfWork = unitOfWork;
            _paymentMethod = paymentMethodServices;
            _momoConfig = momoConfig.Value;

        }

        [Authorize(Policy = "User")]
        [HttpGet("ipn")]
        public async Task<IActionResult> IpnInvoicePaymentMomo()
        {
            // using HttpClient client = new HttpClient();
            //
            // var momoRequest = new MomoRequestDto()
            // {
            //     partnerCode = _momoConfig.PartnerCode,
            //     partnerName = "Test",
            //     storeId  = "MoMoTestStore",
            //     requestType = "onDelivery",
            //     ipnUrl = _momoConfig.IpnUrl,
            //     redirectUrl = _momoConfig.ReturnUrl,
            //     orderId = "1232ABC",
            //     amount = 140000,
            //     lang =  "en",
            //     orderInfo = "SDKteam",
            //     requestId = "MM15404562472575",
            //     extraData = "eyJ1c2VybmFtZSI6ICJtb221vIn0=",
            // };
            //
            // var rawSignature = "accessKey=" + _momoConfig.AccessKey +
            //                    "&amount=" + momoRequest.amount +
            //                    "&extraData=" + momoRequest.extraData +
            //                    "&ipnUrl=" + momoRequest.ipnUrl +
            //                    "&orderId=" + momoRequest.orderId +
            //                    "&orderInfo=" + momoRequest.orderInfo +
            //                    "&partnerCode=" + momoRequest.partnerCode +
            //                    "&redirectUrl=" + momoRequest.redirectUrl +
            //                    "&requestId=" + momoRequest.requestId +
            //                    "&requestType=" + momoRequest.requestType;
            //
            // momoRequest.signature = HashHelper.CreateSHA256(rawSignature, _momoConfig.SecretKey);
            //
            // string jsonContent = JsonConvert.SerializeObject(momoRequest);
            //
            // HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            //
            // var createPaymentLinkRes = await client.PostAsync(_momoConfig.PaymentUrl, content);
            //
            // if (!createPaymentLinkRes.IsSuccessStatusCode)
            // {
            //     var errorContent = await createPaymentLinkRes.Content.ReadAsStringAsync();
            //     Console.WriteLine($"Error: {errorContent}");
            // }
            //
            // if (createPaymentLinkRes.IsSuccessStatusCode)
            // {
            //     var responseContent = createPaymentLinkRes.Content.ReadAsStringAsync().Result;
            //     var responseData = JsonConvert.DeserializeObject<MomoResponseDto>(responseContent);
            //     if (responseData.ResultCode == "0")
            //     {
            //         return Ok(responseData.PayUrl);
            //     }
            //     
            //     return Ok(responseData.Message);
            // }
            //
            return Ok();
        }
        
        [HttpGet]
        [Authorize(Policy = "Customer")]
        public IActionResult GetPaymentMethod()
        {
            var data = _paymentMethod
                .GetAll()
                .AsNoTracking()
                .OrderByDescending(e => e.Created)
                .ToList();

            return OkList(data);
        }
        
        [HttpGet("{id}")]
        [Authorize(Policy = "User")]
        public IActionResult GetPaymentMethodDetail([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var data = _paymentMethod
                .GetAll()
                .AsNoTracking()
                .Where(e => e.Id == id)
                .OrderByDescending(e => e.Created)
                .FirstOrDefault();

            return Ok(data);
        }
        
        [HttpPost]
        [Authorize(Policy = "User")]
        public IActionResult Create([FromBody] PaymentMethod body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var paymentMethod = _paymentMethod
                .GetAll()
                .AsNoTracking()
                .Where(e => e.Code == body.Code)
                .FirstOrDefault();

            if (paymentMethod != null)
            {
                return BadRequest("PAYMENT_EXIST");
            }

            body.Status = OBJECT_STATUS.ENABLE;
            body.Created = DateTime.Now;
            body.CreatedBy = CurrentUserEmail;
            
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _paymentMethod.Add(body);
                transaction.Commit();
            }
            
            return Ok();
        }
        
        [HttpPut("{id}")]
        [Authorize(Policy = "User")]
        public IActionResult Update([FromRoute] long id,[FromBody] PaymentMethod body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var paymentMethod = _paymentMethod
                .GetAll()
                .AsNoTracking()
                .Where(e => e.Id == id)
                .FirstOrDefault();

            if (paymentMethod == null)
            {
                return BadRequest("PAYMENT_NOT_EXIST");
            }

            var paymentMethodCheckCode = _paymentMethod
                .GetAll()
                .AsNoTracking()
                .Where(e => e.Id != id)
                .Where(e => e.Code == body.Code)
                .FirstOrDefault();

            if (paymentMethodCheckCode != null)
            {
                return BadRequest("PAYMENT_SAME_CODE_EXIST");
            }
            
            body.Updated = DateTime.Now;
            body.UpdatedBy = CurrentUserEmail;
            
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _paymentMethod.Update(body);
                transaction.Commit();
            }
            
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var paymentMethod = _paymentMethod
                .GetAll()
                .Where(e => e.Id == id)
                .FirstOrDefault();

            if (paymentMethod == null)
            {
                return BadRequest("PAYMENT_NOT_EXIST");
            }

            paymentMethod.Updated = DateTime.Now;
            paymentMethod.UpdatedBy = CurrentUserEmail;
            paymentMethod.Status = OBJECT_STATUS.DELETED;
            
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _paymentMethod.Update(paymentMethod);
                transaction.Commit();
            }
            
            return Ok();
        }
    }
}