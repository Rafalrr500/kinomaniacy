using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using projekt.Data;
using projekt.Models;

namespace projekt.Pages.Login
{
    public class IndexModel : UserPageModel
    {
        public IndexModel(UserDB userDB) : base(userDB) { }
    }
}
