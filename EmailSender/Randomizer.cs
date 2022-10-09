
namespace AdventurerOfficialProject.EmailSender
{
    public abstract class Randomizer /// Robie ta klase, poniewaz SOLID mowi o tym, ze klasa powinna byc odpowiedzialna tylko za jej funkcjonalnosc.
                                     /// Randomize nie ma nic zwiazanego z logowaniem uzytkownika. Na dodatek robie ja jako abstrakcyjna, poniewaz
                                     /// nie chce zeby ktokolwiek robil jej instacje. Ona ma tylko przechowywac ta metode.
    {
        private static Random random = new Random();

        public static string RandomCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
