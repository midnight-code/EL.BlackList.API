using System.ComponentModel.DataAnnotations.Schema;

namespace EL.BlackList.API.Models;

[Table("dt_Documents")]
public class Documents
{
    public int ID { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public int DriverID { get;set; }
    public string ImgType { get; set; } = string.Empty;
}
