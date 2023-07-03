using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL.BlackList.API.Models
{
    [Table("dt_FeedBacks")]
    public class FeedBacks
    {
        [Key, Column("id")] public int FeedBackId { get; set; }
        [Column("id_driver")] public int DriversId { get; set; }
        [Column("id_taxpool")] public int TaxiPoolsId { get; set; }
        [Column("subjest")] public string Subjest { get; set; } = string.Empty;
        [Column("dateadd")] public DateTime AddDate { get; set; }
        [Column("id_city")] public int CityId { get; set; }
        [Column("id_user")] public string UserGuid { get; set; } = string.Empty;

        public TaxiPools? TaxiPools { get; set; }
        public City? City { get; set; } 
        
    }
}
