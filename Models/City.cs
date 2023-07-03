using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL.BlackList.API.Models
{
    [Table("dt_CityName")]
    public class City
    {
        [Key, Column("id")] public int CityId { get; set; }
        [Column("name")] public string Name { get; set; } = string.Empty;
        [Column("region")] public string Region { get; set; } = string.Empty;
    }
}
