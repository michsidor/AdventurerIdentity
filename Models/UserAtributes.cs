using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AdventurerOfficialProject.Models
{
    public class UserAtributes
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "You have to input your name")]
        public string Name { get; set; }    
        public string? Country { get; set; }

        public string? City { get; set; }

        public string? Gender { get; set; }

        public IdentityUser userLoginAtributes { get; set; }

        [ForeignKey("userLoginAtributes")]
        public string userLoginAtributesId { get; set; }

        public string? HobbysSavedInDatabase { get; set; }

        [NotMapped]
        public static List<string> HobbyList { get; set; } = Enum.GetValues(typeof(Hobbys)) // Downloading all hobbys values from enum class. I want to have hobbys in another file.
                              .Cast<Hobbys>() // operator wlaczenia podstawowych zapytan dla jakis dziwnych wartosci
                              .Select(v => v.ToString().Replace('_', ' '))
                              .OrderBy(sort => sort)
                              .ToList();



        [NotMapped]
        public List<string> ChoosedHobbys { get; set; } // All checked hobbys. Its NotMapped, because i will convert it to string and then save to database.

        [NotMapped]
        public List<CheckboxOption> Checkboxes { get; set; } // Options of checkbox

        [NotMapped]
        public List<SelectListItem> GenderList { get; } = new List<SelectListItem>() // Iam declareiting it there, because it would not change.
                                                                                            // I do not have to take it const or readonly, because prop has only getter.
        { new SelectListItem { Text = "Man"},
          new SelectListItem { Text = "Woman"},
          new SelectListItem { Text = "Other"}
        };

        public string UniqueCode { get; set; }


        [NotMapped]
        public string UniqueCodeConfirm { get; set; }
        


    }
}
