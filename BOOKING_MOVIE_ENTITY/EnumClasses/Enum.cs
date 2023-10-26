namespace BOOKING_MOVIE_ENTITY.Entities
{
    public static class OBJECT_STATUS
    {
        public const string ENABLE = "ENABLE";
        public const string DELETED = "DELETED";
        public const string DISABLE = "DISABLE";
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
        public const string IS_ADMIN_USER = "isAdminUser";
    }
    
    public static class PAYMENT_STATUS
    {
        public const string PAID = "PAID";
        public const string UNPAID = "UNPAID";
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
    
}