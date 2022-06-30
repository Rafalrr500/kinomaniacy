using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using projekt.Data;

namespace projekt.Models
{
    public class UserPageModel : PageModel
    {
        public UserDB userDB { get; private set; }

        [BindProperty(SupportsGet = true)]
        public string returnUrl { get; set; }
        [BindProperty]
        public Client client { get; set; }
        [BindProperty]
        public Staff staff { get; set; }
        [BindProperty]
        public Admin admin { get; set; }
        public string errorMessage { get; set; }
        public UserPageModel(UserDB userDB)
        {
            this.userDB = userDB;
        }
        public void OnGet()
        {
            returnUrl = returnUrl ?? "/";
            errorMessage = "";
        }
        protected string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                    builder.Append(bytes[i].ToString("x2"));
                return builder.ToString();
            }
        }
    }
}
