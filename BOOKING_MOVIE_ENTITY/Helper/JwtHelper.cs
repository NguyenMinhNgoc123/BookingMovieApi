using System;
using System.Linq;
using System.Security.Claims;

namespace BOOKING_MOVIE_ENTITY.Helper
{
    public class JwtHelper
    {
        public static string GetCurrentInformation(ClaimsPrincipal User, Func<Claim, bool> func)
        {
            Claim claim = User.Claims.Where(func).FirstOrDefault();
            if (claim != null)
            {
                return claim.Value;
            }
            
            return "";
        }
    }
}