using System.ComponentModel.DataAnnotations.Schema;

namespace EL.BlackList.API.Models
{
    [Table("dt_Token")]
    public class TokenModel
    {
        [Column("id")]
        public int ID { get; set; }
        [Column("token")]
        public string Token { get; set; } = string.Empty;
        [Column("dateadd")]
        public DateTime DateAdd { get; set; }
        public string Users { get; set; } = string.Empty;
    }
}
