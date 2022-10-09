using AdventurerOfficialProject.Areas.Identity.Data;
using AdventurerOfficialProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventurerOfficialProject.Areas.Identity.Pages.Account.Manage
{
    public class EditingAccount : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<PersonalDataModel> _logger;
        private readonly AdventurerDbContext _context;



        public EditingAccount(
            UserManager<IdentityUser> userManager,
            ILogger<PersonalDataModel> logger,
            AdventurerDbContext context)
        {
            _userManager = userManager;
            _logger = logger;
            _context = context;
        }

        [BindProperty] // to odpowiada za zwracanie modelu do OnPost metody
        public string Name { get; set; }

        [BindProperty]
        public string City { get; set; }

        [BindProperty]
        public string Country { get; set; }

        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public string Gender { get; set; }

        [BindProperty]
        public string userLoginId { get; set; }

        [BindProperty]
        public string UniqueCode { get; set; }

        [BindProperty]
        public string OldHobbysSave { get; set; }

        public string? HobbysSave { get; set; }

        [BindProperty]
        public static List<string> HobbyList { get; set; } = Enum.GetValues(typeof(Hobbys)) // Downloading all hobbys values from enum class. I want to have hobbys in another file.
                              .Cast<Hobbys>() // operator wlaczenia podstawowych zapytan dla jakis dziwnych wartosci
                              .Select(v => v.ToString().Replace('_', ' '))
                              .OrderBy(sort => sort)
                              .ToList();


        [BindProperty]
        public List<string> ChoosedHobbys { get; set; } // All checked hobbys. Its NotMapped, because i will convert it to string and then save to database.

        [BindProperty]
        public List<CheckboxOption> Checkboxes { get; set; } // Options of checkbox


        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            var modelObject = _context.UserAtributesDbSet.Where(iden => iden.userLoginAtributesId == user.Id)
                .Select(all => all)
                .FirstOrDefault();

            List<CheckboxOption> checkboxes = new List<CheckboxOption>();

            foreach (var values in EditingAccount.HobbyList) // setting default values for all checboxes.
            {
                checkboxes.Add(new CheckboxOption() { IsChecked = false, Value = values });
            }

            Name = modelObject.Name;
            City = modelObject.City;
            Country = modelObject.Country;
            UniqueCode = modelObject.UniqueCode;
            Gender = modelObject.Gender;
            OldHobbysSave = modelObject.HobbysSavedInDatabase;
            Checkboxes = checkboxes;
            userLoginId = modelObject.userLoginAtributesId;
            Id = modelObject.Id;

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            UserAtributes userAtributes = new UserAtributes()
            {
                Id = Id,
                Name = Name,
                Country = Country,
                City = City,
                Gender = Gender,
                userLoginAtributesId = userLoginId,
                UniqueCode = UniqueCode
            };

            HobbysSave = string.Join(",", ChoosedHobbys.ToList());

            if(string.IsNullOrEmpty(HobbysSave))
            {
                userAtributes.HobbysSavedInDatabase = OldHobbysSave;
            }
            else
            {
                userAtributes.HobbysSavedInDatabase = HobbysSave;
            }

            _context.Update(userAtributes);
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}
