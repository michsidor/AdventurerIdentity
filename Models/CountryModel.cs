using System.ComponentModel.DataAnnotations;

namespace AdventurerOfficialProject.Models
{
    public class CountryModel
    {
        [Key]
        public int Id { get; set; }
        public string CountryName { get; set; }
    }
}
