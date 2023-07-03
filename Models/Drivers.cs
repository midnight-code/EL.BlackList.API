using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL.BlackList.API.Models
{
    [Table("dt_Drivers")]
    public class Drivers
    {
        [Key, Column("id")]public int Id { get; set; }
        [Column("firstname")]public string FirstName { get; set; } = string.Empty;
        [Column("lastname")]public string LastName { get; set; } = string.Empty;
        [Column("secondname")]public string SecondName { get; set; } = string.Empty;
        [Column("inn")]public string Inn { get; set; } = string.Empty;
        [Column("id_passport")]public int PasportId { get; set; }
        [Column("blacklist")]public bool AddList { get; set; }
        [Column("avatar")]public int AvatarId { get; set; }
        [Column("datarogden")]public DateTime DateRogden { get; set; }
        [Column("id_taxipool")]public int TaxiPoolsId { get; set; }

        public ICollection<FeedBacks> FeedBacks { get; set; } = new HashSet<FeedBacks>();
        public TaxiPools? TaxiPools { get; set; }
    }
}
