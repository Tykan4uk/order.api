namespace OrderApi.Models.Requests
{
    public class GetByPageRequest
    {
        public string UserId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
