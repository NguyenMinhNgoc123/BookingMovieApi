using System;
using System.Collections.Generic;
using System.Linq;
using BOOKING_MOVIE_ENTITY;
using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class InvoiceServices : ApplicationService<Invoice>
    {
        private readonly PromotionServices _promotion;
        private readonly CustomerPromotionServices _customerPromotion;
        
        public InvoiceServices(
            PromotionServices promotion,
            CustomerPromotionServices customerPromotion,
            GenericDomainService<Invoice> genericDomainService) : base(genericDomainService)
        {
            _promotion = promotion;
            _customerPromotion = customerPromotion;
        }

        public decimal CalculateTotal(decimal totalAllDetailInvoice, decimal discountValue, string discountUnit)
        {
            decimal totalInvoice = 0;
            if (discountUnit != null)
            {
                decimal discountTotal = 0;
                if (discountUnit == "PERCENT")
                {
                    discountTotal = totalAllDetailInvoice / 100 * discountValue;
                    totalInvoice = totalAllDetailInvoice >= discountTotal ? totalAllDetailInvoice - discountTotal : 0;
                }
                else
                {
                    totalInvoice = totalAllDetailInvoice >= discountValue ? totalAllDetailInvoice - discountValue : 0;
                }
            }

            return totalInvoice;
        }

        public void UpdateGiftFollowRevenue(long customerId, string currentUserEmail)
        {
            var invoices = GetAll()
                .Where(e => e.CustomerId == customerId)
                .Where(e => e.Status == PAYMENT_STATUS.PAID)
                .ToList();

            var sum = invoices.Sum(e => e.Total);

            var promotions = _promotion.GetAll()
                .Where(e => e.spendPrice <= sum)
                .Where(e => e.AvailableTo >= DateTime.Now)
                .ToList();

            var promotionIds = promotions.Select(e => e.Id);

            var customerPromotions = _customerPromotion.GetAll()
                .Where(e => e.CustomerId == customerId)
                .Where(e => promotionIds.Contains(e.PromotionId))
                .ToList();

            var oldPromotionIds = customerPromotions.Select(e => e.PromotionId);

            var newCustomerPromotions = new List<CustomerPromotion>();
            foreach (var e in promotions.Where(e => oldPromotionIds.Contains(e.Id)))
            {
                newCustomerPromotions.Add(new CustomerPromotion()
                {
                    CustomerId = customerId,
                    PromotionId = e.Id,
                    Status = OBJECT_STATUS.ENABLE,
                    Created = DateTime.Now,
                    CreatedBy = currentUserEmail
                });
            }

            _customerPromotion.AddRange(newCustomerPromotions);
        }
    }
}