using System.Collections.Generic;
using System.Linq;
using BOOKING_MOVIE_ADMIN.Values;
using BOOKING_MOVIE_CORE;
using BOOKING_MOVIE_CORE.Services;
using BOOKING_MOVIE_ENTITY.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BOOKING_MOVIE_ADMIN.Reponse
{
    public class movieControllerBase : ControllerBase
    {
        private readonly int DEFAULT_ROW_PER_PAGE = 20;

        private User _currentUser;
        public readonly UserServices _user;

        public movieControllerBase(UserServices userService)
        {
            _user = userService;
        }

        public OkObjectResult OkList<T>(List<T> rs, int? count = null)
        {
            var response = new ActionResultValue()
            {
                Data = rs,
                Meta = new ActionResultMeta
                {
                    TotalItem = count != null ? count.Value : rs.Count(),
                    CurrentPage = Page,
                    RowPerPage = RowPerPage,
                }
            };

            return Ok(response);
        }
        public int Page
        {
            get
            {
                int.TryParse(HttpContext.Request.Query["page"].ToString(), out int page);
                return page > 0 ? page : 1;
            }
        }

        public int RowPerPage
        {
            get
            {
                int.TryParse(HttpContext.Request.Query["rowPerPage"].ToString(), out int rowPerPage);
                return rowPerPage == 0 ? DEFAULT_ROW_PER_PAGE : rowPerPage;
            }
        }
    }
}