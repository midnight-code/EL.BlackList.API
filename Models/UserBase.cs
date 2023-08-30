using Microsoft.EntityFrameworkCore;

namespace EL.BlackList.API.Models
{
    public class UserBase
    {
        public int Id { get; set; }
        public string UserID { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string SecondName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string PhoneNumberPublic { get; set; } = string.Empty;
        public string NameCompPublic { get; set; } = string.Empty;

        public int PinCode { get; set; }
        public DateTime DateStart { get; set; }


        public int CityID { get; set; }
        public string CityName { get; set; } = string.Empty;
        public int TaxiPoolID { get; set; }
        public string UserName { get; set; } = string.Empty;
        //eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiY3Q3OEBiay5ydSIsImp0aSI6ImY1YTY2NGYyLTdiNDUtNDJlNy05YTBlLTU1ODQ0OTIyMDNkNiIsImV4cCI6MTY5NTk2Mjk0OSwiaXNzIjoiaHR0cHM6Ly9pdGlzcy5ydSIsImF1ZCI6Imh0dHBzOi8vaXRpc3MucnUifQ.rFdsjNc9_DxoQ_Tv7ajwAgNmwDF9tMEFEhxHuKS9N9c
    }
}
