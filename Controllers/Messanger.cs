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

            var unionId = osobyKtoreDoMnieNapisaly.Select(iden => iden.Sender)
                .Union(osobyDoKtorychJaNapisalem.Select(iden => iden.Recipient))
                .ToList();

            var unionName = osobyKtoreDoMnieNapisaly.Select(name => name.SenderName)
                .Union(osobyDoKtorychJaNapisalem.Select(name => name.RecipientName))
                .ToList();

            Dictionary<string, string> namesAndIds =
                new Dictionary<string, string>();

            for(int i = 0; i < unionName.Count(); i++)
            {
                namesAndIds.Add(unionName[i], unionId[i]);
            }

            return View(namesAndIds);
        }


        public IActionResult MessangerView(string? id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity; // wczytanie zalogowanego uzytkownika
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            Message message = new Message();

            message.Recipient = id;
            message.Sender = claims.Value;// id osoby wysylajacej wiadomosc

            message.RecipientName = _context.UserAtributesDbSet.Where(iden => iden.userLoginAtributesId == id)
                .Select(name => name.Name)
                .FirstOrDefault();

            message.SenderName = _context.UserAtributesDbSet.Where(iden => iden.userLoginAtributesId == claims.Value)
                .Select(name => name.Name)
                .FirstOrDefault();

            var messageHistory = _context.MessageDbSet.Where(iden => (iden.Sender == message.Sender && iden.Recipient == message.Recipient) ||
                                                                      (iden.Sender == message.Recipient && iden.Recipient == message.Sender))
                  .Select(all => all)
                  .ToList();

            ViewData["messages"] = messageHistory;

            return View(message);
        }

        [HttpPost]
        public IActionResult MessangerView(Message message)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity; // wczytanie zalogowanego uzytkownika
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            message.SenderName = message.SenderName;
            message.RecipientName = message.RecipientName;
            message.AddData = DateTime.Now;
            message.Recipient = message.Recipient;
            message.Sender = claims.Value;// id osoby wysylajacej wiadomosc

            _context.Add(message);
            _context.SaveChanges();

            var messageHistory = _context.MessageDbSet.Where(iden => (iden.Sender == message.Sender && iden.Recipient == message.Recipient) ||
                                                          (iden.Sender == message.Recipient && iden.Recipient == message.Sender))
                .Select(all => all)
                .ToList();

            ViewData["messages"] = messageHistory;

            return View(message);
        }


    }
}
