using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Case.CoreLayer.Entities.Identity
{
    public class AppUserRoles: IdentityUserRole<int>
    {
        public const string User = "User";
    }
}
