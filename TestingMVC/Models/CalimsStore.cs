using System.Collections.Generic;
using System.Security.Claims;

namespace TestingMVC.Models
{
    public static class CalimsStore
    {
        public static List<Claim> AllClaims =new List<Claim>
        {
            new Claim("Create Role","Create Role"),
            new Claim("Edit Role","Edit Role"),
            new Claim("Delete Role","Createelete Role")
        };
    }
}
