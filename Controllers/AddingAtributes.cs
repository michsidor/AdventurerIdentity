using AdventurerOfficialProject.Areas.Identity.Data;
using AdventurerOfficialProject.EmailSender;
using AdventurerOfficialProject.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdventurerOfficialProject.Controllers
{
    public class AddingAtributes : Controller
    {

        private readonly AdventurerDbContext _db; // creating a context to saving data
        public AddingAtributes(AdventurerDbContext db)
        {
            _db = db;
        }

        //GET ATRIBUTES
        public IActionResult AddingAtributesView() // to musi byc bo bez tego strona nie dziala(Oczywiscie, ze to musi byc bo to odpowida za odpalanie strony
                                                         // po kliknieciu Create account[Odpowiada za odpalenie pustej strony z riderect to action np.
        {
            List<CheckboxOption> checkboxes = new List<CheckboxOption>();

            foreach (var values in UserAtributes.HobbyList) // setting default values for all checboxes.
            {
                checkboxes.Add(new CheckboxOption() { IsChecked = false, Value = values });
            }

            UserAtributes userAtributes = new UserAtributes()
            {
                Checkboxes = checkboxes
            };

            return View(userAtributes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddingAtributesView(UserAtributes userAtributes, IdentityUser identityUser) // to musi byc bo bez tego strona nie dziala
        {
            string _code = Randomizer.RandomCode();
            SendingEmails sendingEmails = new SendingEmails()
            {
                Subject = "Creating account in Adventurer",
                Body = $"This message was generated automatically \n Please pass code below to confirm you account settings \n Your code: \n" +
               $"{_code}",
                RecepientEmails = identityUser.Email
            };
            sendingEmails.EmailSender();

            var identification = identityUser.Id;
            var user = _db.Users.Where(iden => iden.Id == identification).Select(all => all).FirstOrDefault();

            userAtributes.UniqueCode = _code;
            userAtributes.HobbysSavedInDatabase = string.Join(",", userAtributes.ChoosedHobbys.ToList());
            userAtributes.userLoginAtributes = user;

            _db.Add(userAtributes);
            _db.SaveChanges();
            return RedirectToAction("ConfirmingAccountView", "AddingAtributes", userAtributes);
        }

        public IActionResult ConfirmingAccountView()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmingAccountView(UserAtributes userAtributes)
        {
            if (string.Equals(userAtributes.UniqueCode, userAtributes.UniqueCodeConfirm))
            {
                _db.Database.EnsureCreated();
                _db.Database.ExecuteSqlRaw($"UPDATE [AspNetUsers] SET [EmailConfirmed] = 1 WHERE Id='{userAtributes.userLoginAtributesId}'");
                _db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            else
            {
                TempData["Alert"] = "You have entered the wrong code. Please try again!";
                Console.WriteLine("Inne");
                return View(userAtributes);
            }
        }
    }
}
