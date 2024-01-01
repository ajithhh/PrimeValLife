namespace PrimeValLife.Core.Models.Users
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore.Metadata.Conventions;
    using PrimeValLife.Core.Models.Orders;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Address> Address { get; set; }

        public AspNetUsers UserIdentity { get; set;}

    }

    public class AspNetUsers:IdentityUser
    {
        //Class created to customize mappings to another db context
    }
}
