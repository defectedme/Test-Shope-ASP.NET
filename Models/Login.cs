using System.ComponentModel.DataAnnotations;

namespace Test_Shope_ASP.NET.Models
{
    public class Login
    {

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
