using System.ComponentModel.DataAnnotations.Schema;

namespace EL.BlackList.API.Models
{
    [Table("dt_UserRegister")]
    public class UsersRegisterModel
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("UserID")]
        public string UserID { get; set; } = string.Empty;
        [Column("FirstName")]
        public string FirstName { get; set; } = string.Empty;
        [Column("LastName")]
        public string LastName { get; set; } = String.Empty;
        [Column("SecondName")]
        public string SecondName { get; set; }= string.Empty;
        [Column("PhoneNumber")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Column("PhoneNumberPublic")]
        public string PhoneNumberPublic { get; set; } = string.Empty;
        [Column("NameCompPublic")]
        public string NameCompPublic { get; set; } = string.Empty;
        [Column("CityID")]
        public int CityID { get; set; }
        [Column("CityName")]
        public string CityName { get; set; } = string.Empty;
        [Column("TaxiPoolID")]
        public int TaxiPoolID { get; set; }
    }
}
