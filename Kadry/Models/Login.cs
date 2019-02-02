
using System.ComponentModel.DataAnnotations;

namespace Kadry.Models
{
    public class Login
    {
        public int Id { get; set; }
        [Display(Name="Nazwa użytkownika")]
        [Required]
        public string Username { get; set; }
        [Display(Name = "Hasło")]
        [Required]
        public string Password { get; set; }
        public bool Admin { get; set; }
    }
}