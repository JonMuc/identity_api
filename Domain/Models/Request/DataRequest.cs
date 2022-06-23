namespace Domain.Models
{
    public class DataRequest
    {
        public string Filter { get; set; }
        public long PageSize { get; set; }
        public long PageIndex { get; set; }
        public long IdBase { get; set; }
    }
}
