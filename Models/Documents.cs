namespace EL.BlackList.API.Models
{
    public class Documents
    {
        public int ID { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long FileSize { get; set; }
    }
}
