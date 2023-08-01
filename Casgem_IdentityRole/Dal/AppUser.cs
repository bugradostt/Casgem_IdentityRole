using Microsoft.AspNetCore.Identity;

namespace Casgem_IdentityRole.Dal
{
    public class AppUser : IdentityUser<int>
    {
        public string NameSurname { get; set; }

    }
}
