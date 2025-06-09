namespace RealEstate.Models
{
    public class ErrorViewModel
    {
        public string ExceptionMessage { get; set; }
        public string ExceptionType { get; set; }
        public DateTime ExceptionTime { get; set; }
        public string Path { get; set; }
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
