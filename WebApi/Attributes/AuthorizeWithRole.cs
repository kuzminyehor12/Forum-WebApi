using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace WebApi.Attributes
{
    public class AuthorizeWithRole : AuthorizeAttribute
    {
        public AuthorizeWithRole(params Roles[] roles) : base()
        {
            Roles = String.Join(",", roles);
        }
    }
}
