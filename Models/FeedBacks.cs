﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL.BlackList.API.Models
{
    [Table("dt_FeedBacks")]
    public class FeedBacks
    {
        [Key, Column("id")] public int FeedBackId { get; set; }
        [Column("id_driver")] public int DriverId { get; set; }
        [Column("id_taxpool")] public int TaxiPoolId { get; set; }
        [Column("subjest")] public string Subjest { get; set; } = string.Empty;
        [Column("dateadd")] public DateTime AddDate { get; set; }
        [Column("id_city")] public int CityId { get; set; }
        [Column("id_user")] public string UserGuid { get; set; } = string.Empty;

        //[ForeignKey(nameof(CityId))]
        public City? City { get; set; } 
        //[ForeignKey(nameof(TaxiPoolId))] 
        public TaxiPools? TaxiPools { get; set; }
    }
}