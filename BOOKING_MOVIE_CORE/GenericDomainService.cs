using BOOKING_MOVIE_ENTITY;

namespace BOOKING_MOVIE_CORE
{
    public class GenericDomainService<T> : DomainService<T> where T : class
    {
        public GenericDomainService(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}