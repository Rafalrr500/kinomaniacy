using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using projekt.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using projekt.Models;

namespace projekt.Pages.Login
{
    public class LoginStaffModel : UserPageModel
    {
        public LoginStaffModel(UserDB userDB) : base(userDB) { }
        public async Task<IActionResult> OnPost()
        {
            string correctPassword = userDB.GetPassword(staff.userName, "StaffGetPassword");
            if (correctPassword == null)
            {
                errorMessage = "Niepoprawna nazwa u¿ytkownika lub has³o.";
                return Page();
            }
            string hashed = HashPassword(staff.password);
            if (hashed != correctPassword)
            {
                errorMessage = "Niepoprawna nazwa u¿ytkownika lub has³o.";
                return Page();
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, staff.userName),
                new Claim("AdminOrStaff", "true")
            };
            var identity = new ClaimsIdentity(claims, "CookieAuthentication");
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync("CookieAuthentication", principal, new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                IsPersistent = false,
                AllowRefresh = false
            });
            return RedirectToPage("./Index");
        }
    }
}
