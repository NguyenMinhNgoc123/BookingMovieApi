using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BOOKING_MOVIE_CORE.Services;
using BOOKING_MOVIE_ENTITY.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BOOKING_MOVIE_ADMIN.Basis
{
    public class UserValidation : JwtBearerEvents
    {
        public override Task TokenValidated(TokenValidatedContext context)
        {
            var userService = context.HttpContext.RequestServices.GetRequiredService<UserServices>();
            var customerService = context.HttpContext.RequestServices.GetRequiredService<CustomerServices>();

            string userIdStr = context.Principal.FindFirstValue("userId");
            string customerIdStr = context.Principal.FindFirstValue("customerId");
            
            if (!string.IsNullOrEmpty(customerIdStr))
            {
                var customerId = long.Parse(customerIdStr);
                var customer = customerService
                    .GetAll()
                    .AsNoTracking()
                    .Where(e => e.Status == OBJECT_STATUS.ENABLE)
                    .FirstOrDefault(x => x.Id == customerId);

                if (customer == null)
                {
                    context.Fail("CUSTOMER_DELETED");
                    return Task.CompletedTask;
                }

                return Task.CompletedTask;
            }
            
            if (string.IsNullOrEmpty(userIdStr))
            {
                context.Fail("INVALID_ID");
            }

            var userId = long.Parse(userIdStr);
            var user = userService.GetAll().AsNoTracking().FirstOrDefault(x => x.Id == userId);

            if (user == null)
            {
                context.Fail("USER_DELETED");
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}