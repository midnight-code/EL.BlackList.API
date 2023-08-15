using System.ComponentModel.DataAnnotations.Schema;

namespace EL.BlackList.API.Models
{
    [Table("dt_Users")]
    public class UsersModel
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("UserId")]
        public string UserID { get; set; } = string.Empty;
        [Column("PaymentDate")]
        public DateTime PaymentDate { get; set; }
        [Column("PaymentPeriod")]
        public int PaymentPeriod { get; set; }
        [Column("PaymentAmount")]
        public decimal PaymentAmount { get; set; }
        [Column("PaymentRate")]
        public decimal PaymentRate { get; set; }
    }
}
