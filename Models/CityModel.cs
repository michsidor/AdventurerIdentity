using System.ComponentModel.DataAnnotations;

namespace AdventurerOfficialProject.Models
{
    public class CityModel
    {
        [Key]
        public int Id { get; set; } 
        public string CityName { get; set; }
        public int CountryId { get; set; }  
    }
}
