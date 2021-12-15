using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreIdentityExample.Models.Authentication
{
    public class User : IdentityUser
    {
        [PersonalData]
        public string Firstname { get; set; }
        [PersonalData]
        public string LastName { get; set; }
    }
}
