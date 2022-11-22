using System;
using Domain.Attributes;
using Microsoft.AspNetCore.Identity;

namespace Domain.Users
{
    [Audtable]
    public class User : IdentityUser
    {
        public string FullName { get; set; }    
    }
}