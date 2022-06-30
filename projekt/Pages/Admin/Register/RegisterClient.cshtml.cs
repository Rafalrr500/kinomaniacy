using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using projekt.Data;
using projekt.Models;

namespace projekt.Pages.Admin.Register
{
    [Authorize(Policy = "Admin")]
    public class RegisterClientModel : UserPageModel
    {
        public RegisterClientModel(UserDB userDB) : base(userDB) { }
        public IActionResult OnPost()
        {
            if (userDB.Exists(client.userName, "ClientExists") == true)
            {
                errorMessage = "Klient o podanej nazwie ju¿ istnieje.";
                return Page();
            }
            if (client.password != client.confirmPassword)
            {
                return Page();
            }
            client.password = HashPassword(client.password);
            userDB.AddClient(client);
            return LocalRedirect(returnUrl);
        }
    }
}
