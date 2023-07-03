using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL.BlackList.API.Models
{
    [Table("dt_TaxiPool")]
    public class TaxiPools
    {
        [Key, Column("id")] public int TaxiPoolId { get; set; }
        [Column("name")] public string Name { get; set; } = string.Empty;
    }
}
