using BOOKING_MOVIE_ENTITY.Entities;

namespace BOOKING_MOVIE_CORE.Services
{
    public class CategoryServices : ApplicationService<Category>
    {
        public CategoryServices(GenericDomainService<Category> domainService) : base(domainService)
        {
        }
    }
}