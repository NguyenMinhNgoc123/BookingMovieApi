namespace BOOKING_MOVIE_ADMIN.Values
{
    public class ActionResultValue
    {
        public ActionResultMeta Meta { get; set; }
        public object Data { get; set; }
    }

    public class ActionResultValue<T>
    {
        public ActionResultMeta Meta { get; set; }
        public T Data { get; set; }
    }
}