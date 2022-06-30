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
    public class RegisterAdminModel : UserPageModel
    {
        public RegisterAdminModel(UserDB userDB) : base(userDB) { }
        public IActionResult OnPost()
        {
            if (userDB.Exists(admin.userName, "AdminExists") == true)
            {
                errorMessage = "Admin o podanej nazwie ju¿ istnieje.";
                return Page();
            }
            if (admin.password != admin.confirmPassword)
            {
                return Page();
            }
            admin.password = HashPassword(admin.password);
            userDB.AddAdmin(admin);
            return LocalRedirect(returnUrl);
        }
    }
}
