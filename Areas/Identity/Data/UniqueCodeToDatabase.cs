using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventurerOfficialProject.Areas.Identity.Data
{
    public class UniqueCodeToDatabase : IdentityUser
    {
        public string UniqueCode { get; set; }

        [NotMapped]
        public string ConfirmUniqueCode { get; set; }

    }
}
