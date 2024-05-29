using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? Address { get; set; }
        public string? OrganizationName { get; set; }
        public DateTime DateRegistered { get; set; }
        public int? GenderId { get; set; }
        public string? GenderName { get; set; }
        public virtual CommonDropdown? Gender { get; set; }
        public Guid? CompanyId { get; set; }
        public bool IsDeactivated { get; set; }
        public bool IsAdmin { get; set; }
        public virtual Company? Company { get; set; }
        
    }
}
