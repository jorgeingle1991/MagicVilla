using Microsoft.AspNetCore.Identity;

namespace MagicVilla_VillaAPI.Models.Authentication
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }

    }
}
