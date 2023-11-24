using BOOKING_MOVIE_ADMIN.Reponse;
using BOOKING_MOVIE_CORE.Services;

namespace BOOKING_MOVIE_ADMIN.Controllers
{
    public class ComboFoodController : movieControllerBase
    {
        public ComboFoodController(UserServices userService) : base(userService)
        {
            
        }
    }
}