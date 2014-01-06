using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotDoge.Entities
{
    /// <summary>
    /// Our domain specific user. Based on identity framework IdentityUser
    /// No additional properties yet
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
    }
}