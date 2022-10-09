using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventurerOfficialProject.Models
{
    public class Activities
    {
        [Key]
        public int Id { get; set; }

        public string? ImageName { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile Image { get; set; }

        public string? Title { get; set; }

        public string? Decription { get; set; }

        public string? ShortDecription { get; set; }

        public string? City { get; set; }
        public string? Destination { get; set; }

        public DateTime? AddData { get; set; }

        public IdentityUser? identityUser { get; set; }

        [ForeignKey("identityUser")]
        public string? identityUserId { get; set; }

        public string? UserName { get; set; }

        public string? TypeTags { get; set; }

        [NotMapped]
        public static List<string>? TagsList { get; set; } = Enum.GetValues(typeof(Hobbys)) // Downloading all hobbys values from enum class. I want to have hobbys in another file.
                              .Cast<Hobbys>() // operator wlaczenia podstawowych zapytan dla jakis dziwnych wartosci
                              .Select(v => v.ToString().Replace('_', ' '))
                              .OrderBy(sort => sort)
                              .ToList();
                            

        [NotMapped]
        public List<CheckboxOption>? Checkboxes { get; set; } // Options of checkbox

        [NotMapped]
        public List<string>? ChoosedTags { get; set; } // All checked tags. Its NotMapped, because i will convert it to string and then save to database.
    }
}
