using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL.BlackList.API.Models
{
    [Table("dt_TaxiPool")]
    public class TaxiPools
    {
        [Key, Column("id")] public int TaxiPoolsId { get; set; }
        [Column("name")] public string Name { get; set; } = string.Empty;
        [Column("id_region")] public int CityId { get; set; }

        public City? City { get; set; }
    }
}
