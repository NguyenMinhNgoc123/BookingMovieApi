using BOOKING_MOVIE_ADMIN.Reponse;
using BOOKING_MOVIE_CORE.Services;

namespace BOOKING_MOVIE_ADMIN.Controllers
{
    public class InvoiceController : movieControllerBase
    {
        public InvoiceController(UserServices userService) : base(userService)
        {
            
        }
        
        
    }
}