using AdventurerOfficialProject.Areas.Identity.Data;
using AdventurerOfficialProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AdventurerOfficialProject.Controllers
{
    public class ActivitiesBoard : Controller
    {
        private readonly AdventurerDbContext _context;

        public ActivitiesBoard(AdventurerDbContext context)
        {
            _context = context;
        }

        public IActionResult ActivitiesBoardView()
        {
            var activitiesObj = _context.ActivitiesDbSet.Select(all=>all)
                .ToList();
            return View(activitiesObj);
        }

        [HttpPost]
        public IActionResult ActivitiesBoardView(string filterString)
        {
            ViewData["CurrentFilter"] = filterString;

            if (!String.IsNullOrEmpty(filterString))
            {
                var activitiesObjNotEmpty = _context.ActivitiesDbSet.Where(cit => cit.City.ToLower() == filterString.ToLower())
                     .ToList();
                return View(activitiesObjNotEmpty);

            }
            else
            {
                var activitiesObjEmpty = _context.ActivitiesDbSet.Select(all => all)
                     .ToList();
                return View(activitiesObjEmpty);

            }
        }


        //GET
        public IActionResult SpecificActivityView(int? id)
        {
            var specModel = _context.ActivitiesDbSet.Where(iden => iden.Id == id)
                .Select(all => all)
                .FirstOrDefault();

            return View(specModel);
        }

     
    }
}
