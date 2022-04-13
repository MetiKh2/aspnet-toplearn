using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Site.Pages.Admin.Roles
{
    [PermissionChecker(6)]
    public class IndexModel : PageModel
    {
       private readonly IPermessionService _permessionService;
        public IndexModel(IPermessionService permessionService)
        {
            _permessionService = permessionService;
        }

        public List<Role> Roles { get; set; }

        public void OnGet()
        {
            Roles = _permessionService.GetRoles();
        }
    }
}
