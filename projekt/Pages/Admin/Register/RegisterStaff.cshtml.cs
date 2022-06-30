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
    public class RegisterStaffModel : UserPageModel
    {
        public RegisterStaffModel(UserDB userDB) : base(userDB) { }
        public IActionResult OnPost()
        {
            if (userDB.Exists(staff.userName, "StaffExists") == true)
            {
                errorMessage = "Pracownik o podanej nazwie ju¿ istnieje.";
                return Page();
            }
            if (staff.password != staff.confirmPassword)
            {
                return Page();
            }
            staff.password = HashPassword(staff.password);
            userDB.AddStaff(staff);
            return LocalRedirect(returnUrl);
        }
    }
}
