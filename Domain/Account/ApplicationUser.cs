using Microsoft.AspNetCore.Identity;

namespace Domain.Account
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
