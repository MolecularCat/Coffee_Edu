using Microsoft.AspNetCore.Identity;

namespace Coffee.Models
{
    public class User : IdentityUser
    {
        public DateTime Created {  get; set; }
        public string UserName {  get; set; }
        public string Email { get; set; }
    }
}
