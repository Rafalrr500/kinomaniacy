using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace projekt.Models
{
    public class Client
    {
        [Display(Name = "Imię")]
        [Required]
        public string firstName { get; set; }
        [Display(Name = "Nazwisko")]
        [Required]
        public string lastName { get; set; }
        [Display(Name = "Nazwa użytkownika")]
        [Required]
        public string userName { get; set; }
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Pole 'Hasło' jest wymagane.")]
        public string password { get; set; }
        [Display(Name = "Potwierdzenie Hasła")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Potwierdzenie hasła jest wymagane")]
        [Compare("password", ErrorMessage = "Podane hasła nie są zgodne.")]
        public string confirmPassword { get; set; }
    }
}
