﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PrimeValLife.Core.Models.Users
{
    public class Address
    {
        public int AddressId { get; set; }
        public string FName {  get; set; }
        public string LName { get; set; }
        public string AddressLine1 {  get; set; }
        public string AddressLine2 { get; set; } 
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public AddressType AddressType { get; set; }

        [DefaultValue("India")]
        public string Country { get; set; } 
        public string Phone { get; set; }
        [ForeignKey("User")]
        public int UserId {  get; set; }

        [JsonIgnore]
        public User User { get; set; } 

    }

}
