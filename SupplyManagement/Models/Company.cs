using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SupplyManagement.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsApproved { get; set; }
    }
}