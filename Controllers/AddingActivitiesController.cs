using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdventurerOfficialProject.Areas.Identity.Data;
using AdventurerOfficialProject.Models;
using System.Security.Claims;

namespace AdventurerOfficialProject.Controllers
{
    public class AddingActivitiesController : Controller
    {
        private readonly AdventurerDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AddingActivitiesController(AdventurerDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: AddingActivities/Create
        public IActionResult Create()
        {
            List<CheckboxOption> checkboxes = new List<CheckboxOption>();

            foreach (var values in Activities.TagsList) // setting default values for all checboxes.
            {
                checkboxes.Add(new CheckboxOption() { IsChecked = false, Value = values });
            }

            Activities activities = new Activities()
            {
                Checkboxes = checkboxes
            };

            return View(activities);
        }

        // POST: AddingActivities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImageName,Image,Title,Decription,Checkboxes,ShortDecription,City,Destination,TypeTags,ChoosedTags,AddData")] Activities activities)
        {

            activities.TypeTags = string.Join(",", activities.ChoosedTags.ToList());

            if (ModelState.IsValid)
            {
                //saving the image to the folder image
                string wwwRoothPath = _hostEnvironment.WebRootPath;
                activities.ImageName = EmailSender.Randomizer.RandomCode() + DateTime.Now.ToString("yymmssfff")+".jpg"; //unikalna nazwa zdjecia
                string path = Path.Combine(wwwRoothPath+"/SavedImages/", activities.ImageName);

                using(var fileStream = new FileStream(path, FileMode.Create))
                {
                    await activities.Image.CopyToAsync(fileStream);
                }

                var claimsIdentity = (ClaimsIdentity)User.Identity; // wczytanie zalogowanego uzytkownika
                var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                var name = _context.UserAtributesDbSet.Where(iden => iden.userLoginAtributesId == claims.Value)
                    .Select(na => na.Name)
                    .FirstOrDefault(); // atributtion name of author post.

                activities.UserName = name;
                activities.AddData = DateTime.Now;
                activities.identityUserId = claims.Value;

                _context.Add(activities);
                await _context.SaveChangesAsync();
                return RedirectToAction("SpecificActivityView", "ActivitiesBoard", activities);
            }
            else
            {
                Console.WriteLine("Brak walidacji");
                return View(activities);

            }
        }
    }
}
