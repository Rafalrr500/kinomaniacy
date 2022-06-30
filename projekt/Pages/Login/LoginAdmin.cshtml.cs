using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using projekt.Data;
using projekt.Models;

namespace projekt.Pages.Login
{
    public class LoginAdminModel : UserPageModel
    {
        public LoginAdminModel(UserDB userDB) : base(userDB) { }
        public async Task<IActionResult> OnPost()
        {
            string correctPassword = userDB.GetPassword(admin.userName, "AdminGetPassword");
            if (correctPassword == null)
            {
                errorMessage = "Niepoprawna nazwa u¿ytkownika lub has³o.";
                return Page();
            }
            string hashed = HashPassword(admin.password);
            if (hashed != correctPassword)
            {
                errorMessage = "Niepoprawna nazwa u¿ytkownika lub has³o.";
                return Page();
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, admin.userName),
                new Claim("AdminOrStaff", "true"),
                new Claim("Admin", "true")
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
