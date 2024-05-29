using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class CompanyViewModel
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string? CompanyLogo { get; set; }
        public string CompanyPhone { get; set; }
        public string? CompanyAccountNumber { get; set; }
        public string? CompanyAccountName { get; set; }
        public string? Bank { get; set; }
        public string? NIN { get; set; }
        public string? BVN { get; set; }
        public string? StaffPosition { get; set; }
        public DateTime DateCreated { get; set; }
        public string? CompanyName { get; set; }
        public bool Deleted { get; set; }
        public bool Active { get; set; }
        public string? CreatedById { get; set; }
        public virtual ApplicationUser? CreatedBy { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
