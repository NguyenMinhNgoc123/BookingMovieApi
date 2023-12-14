using System.Net.Http;
using System.Text;
using BOOKING_MOVIE_ADMIN.Helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BOOKING_MOVIE_ADMIN.Values
{
    public class MomoRequestDto
    {
        public string partnerCode { get; set; }
        
        public string partnerName { get; set; }
        
        public string storeId { get; set; }
        
        public string requestType { get; set; }
        
        public string ipnUrl { get; set; }
        
        public string redirectUrl { get; set; }
        public string orderId { get; set; }
        public string orderInfo { get; set; }
        public long amount { get; set; }
        
        public string lang { get; set; }
        public string requestId { get; set; }
        public string extraData { get; set; }
        public string signature { get; set; }

        // public void MakeSignature(string accessKey, string secretKey)
        // {
        //     var rawHash = "accessKey=" + accessKey +
        //                   "&amount=" + this.Amount +
        //                   "&extraData=" + this.ExtraData +
        //                   "&ipnUrl=" + this.IpnUrl +
        //                   "&lang=" + this.Lang +
        //                   "&orderId=" + this.OrderId +
        //                   "&orderInfo="+ this.OrderInfo+
        //                   "&partnerCode=" + this.PartnerCode +
        //                   "&partnerName=" + this.PartnerName +
        //                   "&requestType=" + this.RequestType +
        //                   "&redirectUrl=" + this.RedirectUrl +
        //                   "&requestId=" + this.RequestId +
        //                   "&storeId=" + this.StoreId+
        //                   "&signature=" + this.Signature;
        //     this.Signature = HashHelper.CreateSHA256(rawHash, secretKey);
        // }

        public (bool, string) GetLink(string paymentUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                var requestData = JsonConvert.SerializeObject(this, new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Formatting = Formatting.Indented,
                });

                var requestContent = new StringContent(requestData, Encoding.UTF8, "application/json");
                var createPaymentLinkRes = client.PostAsync(paymentUrl, requestContent).Result;
        
                if (createPaymentLinkRes.IsSuccessStatusCode)
                {
                    var responseContent = createPaymentLinkRes.Content.ReadAsStringAsync().Result;
                    var responseData = JsonConvert.DeserializeObject<MomoResponseDto>(responseContent);
                    if (responseData.ResultCode == "0")
                    {
                        return (true, responseData.PayUrl);
                    }
                
                    return (false, responseData.Message);
                }   
                
                return (false, createPaymentLinkRes.ReasonPhrase);
            }
        }
    }
}