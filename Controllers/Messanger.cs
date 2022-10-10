using AdventurerOfficialProject.Areas.Identity.Data;
using AdventurerOfficialProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AdventurerOfficialProject.Controllers
{
    public class Messanger : Controller
    {
        private readonly AdventurerDbContext _context;

        public Messanger(AdventurerDbContext context)
        {
            _context = context;
        }

        public IActionResult AllconversationsView()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity; // wczytanie zalogowanego uzytkownika
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);



            var osobyKtoreDoMnieNapisaly = _context.MessageDbSet.Where(iden => iden.Recipient == claims.Value)
                .GroupBy(iden => iden.Sender)
                .Select(fi => fi.First())
                .ToList();

            var osobyDoKtorychJaNapisalem = _context.MessageDbSet.Where(iden => iden.Sender == claims.Value)
                .GroupBy(iden => iden.Recipient)
                .Select(fi => fi.First())
                .ToList();

            var union = osobyKtoreDoMnieNapisaly.Select(iden => iden.Sender)
                .Union(osobyDoKtorychJaNapisalem.Select(iden => iden.Recipient))
                .ToList();

            foreach(var values in union)
            {
                Console.WriteLine("Z kim prowadzilem konwersacje?" + values);
            }

            ViewData["Uzytkownik"] = claims.Value;

            return View(union);
        }


        public IActionResult MessangerView(string? id)
        {

            Message message = new Message();
            message.Recipient = id;

            return View(message);
        }

        [HttpPost]
        public IActionResult MessangerView(Message message)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity; // wczytanie zalogowanego uzytkownika
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            message.AddData = DateTime.Now;

            message.Sender = claims.Value;// id osoby wysylajacej wiadomosc

            _context.Add(message);
            _context.SaveChanges();

            var messageHistory = _context.MessageDbSet.Where(iden => (iden.Sender == message.Sender && iden.Recipient == message.Recipient) ||
                                                                      (iden.Sender == message.Recipient && iden.Recipient == message.Sender))
                  .Select(all => all)
                  .ToList();

            foreach (var values in messageHistory)
            {
                Console.WriteLine("Wiadomosc: " + values.userMessage + " oraz kto wyslal: " + values.Sender);
            }

            ViewData["messages"] = messageHistory;

            return View(message);
        }


    }
}
