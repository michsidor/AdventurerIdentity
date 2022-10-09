using AdventurerOfficialProject.Areas.Identity.Data;
using AdventurerOfficialProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AdventurerOfficialProject.Controllers
{
    public class MyActivitiesController : Controller
    {
        private readonly AdventurerDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public List<Activities> ActivitiesList { get; set; }

        public MyActivitiesController(AdventurerDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult MyActivitiesView()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity; // wczytanie zalogowanego uzytkownika
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var query = _context.ActivitiesDbSet
                 .Where(iden => iden.identityUserId == claims.Value)
                 .Select(all => all)
                 .ToList();

            return View(query);
        }

        public IActionResult EditActivityView(int? id)
        {

            var specModel = _context.ActivitiesDbSet.Where(iden => iden.Id == id)
               .Select(all => all)
               .FirstOrDefault();

            List<CheckboxOption> checkboxes = new List<CheckboxOption>();

            foreach (var values in Activities.TagsList) // setting default values for all checboxes.
            {
                checkboxes.Add(new CheckboxOption() { IsChecked = false, Value = values });
            }

            ActivitiesEdit activitiesEdit = new ActivitiesEdit
            {
                Id = specModel.Id,
                Title = specModel.Title,///
                Decription = specModel.Decription,//
                ShortDecription = specModel.ShortDecription,//
                City = specModel.City,//
                Destination = specModel.Destination,//
                AddData = specModel.AddData,///
                identityUserId = specModel.identityUserId,///
                UserName = specModel.UserName,
                ChoosedTags = specModel.ChoosedTags,
                Checkboxes = checkboxes,
                OldImagePath = specModel.ImageName,
            };

            return View(activitiesEdit);
        }

        [HttpPost]
        public IActionResult EditActivityView([Bind("Id,ImageName,Image,Title,Decription,Checkboxes,ShortDecription,City,Destination,TypeTags,ChoosedTags,AddData,UserName,identityUserId,OldImagePath")] ActivitiesEdit activitiesEdit)
        {
            activitiesEdit.TypeTags = string.Join(",", activitiesEdit.ChoosedTags.ToList());

            Activities updateActivity = new Activities
            {
                Id = activitiesEdit.Id,
                Title = activitiesEdit.Title,
                Decription = activitiesEdit.Decription,
                ShortDecription = activitiesEdit.ShortDecription,
                Destination = activitiesEdit.Destination,
                TypeTags = activitiesEdit.TypeTags,
                AddData = activitiesEdit.AddData,
                City = activitiesEdit.City,
                identityUserId = activitiesEdit.identityUserId,
                UserName = activitiesEdit.UserName
            };


            if (activitiesEdit.Image != null)
            {
                string wwwRoothPath = _hostEnvironment.WebRootPath;
                updateActivity.ImageName = EmailSender.Randomizer.RandomCode() + DateTime.Now.ToString("yymmssfff") + ".jpg"; //unikalna nazwa zdjecia
                string path = Path.Combine(wwwRoothPath + "/SavedImages/", updateActivity.ImageName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    activitiesEdit.Image.CopyToAsync(fileStream);
                }
            }
            else
            {
                updateActivity.ImageName = activitiesEdit.OldImagePath;
            }
            _context.Update(updateActivity);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");

        }
    }
}
