using System.Security.Policy;
using BOOKING_MOVIE_ADMIN.Helper;
using Microsoft.AspNetCore.Mvc;

namespace BOOKING_MOVIE_ADMIN.Values
{
    [BindProperties]
    public class MomoDto
    {
        public string PartnerCode { get; set; }
        public string OrderId { get; set; }
        public string RequestId { get; set; }
        public string Amount { get; set; }
        public string OrderInfo { get; set; }
        public string OrderType { get; set; }
        public string TransId { get; set; }
        public string ResultCode { get; set; }
        public string PayType { get; set; }
        public string ResponseTime { get; set; }
        public string ExtraData { get; set; }
        public string Signature { get; set; }
        public string Message { get; set; }

        public bool MakeSignature(string accessKey, string secretKey)
        {
            var rawHash = "accessKey=" + accessKey +
                          "&amount=" + this.Amount +
                          "&extraData=" + this.ExtraData +
                          "&message=" + this.Message +
                          "&orderId=" + this.OrderId +
                          "&orderInfo=" + this.OrderInfo +
                          "&orderType=" + this.OrderType +
                          "&partnerCode=" + this.PartnerCode +
                          "&payType=" + this.PayType +
                          "&requestId=" + this.RequestId +
                          "&responseTime=" + this.ResponseTime +
                          "&resultCode=" + this.ResultCode +
                          "&transId=" + this.TransId;

            var checkSignature = HashHelper.CreateSHA256(rawHash, secretKey);
            return this.Signature.Equals(checkSignature);
        }
    }
}