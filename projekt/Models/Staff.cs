using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projekt.Models
{
    public class Staff
    {
        [Display(Name = "Nazwa użytkownika")]
        [Required]
        public string userName { get; set; }
        [Display(Name = "Stanowisko")]
        [Required]
        public string jobPosition { get; set; }
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
