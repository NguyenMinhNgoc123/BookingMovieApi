namespace BOOKING_MOVIE_ENTITY.Entities
{
    public static class OBJECT_STATUS
    {
        public const string ENABLE = "ENABLE";
        public const string DELETED = "DELETED";
        public const string DISABLE = "DISABLE";
    }
    
    public static class MOBILE
    {
        public const string DEFAULT = "01234";
    }
    public static class CLAIMUSER
    {
        public const string SUB = "sub";
        public const string ROLE = "role";
        public const string NAME = "name";
        public const string EMAILADDRESS = "emailAddress";
        public const string SALONID = "salonId";
        public const string EXP = "exp";
        public const string ISS = "iss";
        public const string AUD = "aud";
        public const string NAMEIDENTIFIER = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        public const string CURRENT_USER_ID = "userId";
        public const string CURRENT_CUSTOMER_ID = "customerId";
        public const string IS_ADMIN_USER = "isAdminUser";
    }
    
    public static class PAYMENT_STATUS
    {
        public const string PAID = "PAID";
        public const string UNPAID = "UNPAID";
        public const string WAITING_10_MINUTE = "WAITING_10_MINUTE";
    }
    public static class PAYMENT_METHOD
    {
        public const string MOMO = "MOMO";
        public const string ATM = "ATM";
    }
    public static class OBJECT_NAME_MOVIE
    {
        public const string SEAT = "SEAT";
        public const string FOOD = "FOOD";
    }
    
    public static class VALIDATE_TEXT
    {
        public const string SUCCESS = "SUCCESS";
    }
    public static class PHOTO
    {
        public const string POSTER_MOVIE = "POSTER_MOVIE";
        public const string BACKDROP_MOVIE = "BACKDROP_MOVIE";
        public const string PROFILE_ACTOR = "PROFILE_ACTOR";
        public const string PROFILE_CUSTOMER = "PROFILE_CUSTOMER";
    }
    
    public static class VIDEO
    {
        public const string MOVIE = "MOVIE";
    }
    
    public static class DISCOUNT_UNIT
    {
        public const string PERCENT = "PERCENT";
        public const string MONEY = "MONEY";
    }
    
    
    public static class SORT_BY
    {
        public const string POPULARITY = "POPULARITY";
        public const string RATING = "RATING";
        public const string MOST_RECENT = "MOST_RECENT";
    }
    
    public static class MOVIE_STATUS
    {
        public const string COMING_SOON = "COMING_SOON";
        public const string PREMIERING = "PREMIERING";
        public const string EXPIRED = "EXPIRED";
    }
}