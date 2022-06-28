namespace Domain.Models
{
    public class DataRequest
    {
        public string StringParam { get; set; }
        public long LongParam{ get; set; }
        public long PageSize { get; set; }
        public long PageIndex { get; set; }
        public long IdBase { get; set; }
    }
}
