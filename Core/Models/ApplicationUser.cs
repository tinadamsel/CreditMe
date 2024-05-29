using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Address { get; set; }
		public Guid? CompanyId { get; set; }
		[ForeignKey("CompanyId")]
		public virtual Company? Company { get; set; }
		public int? GenderId { get; set; }
		[ForeignKey("GenderId")]
		public virtual CommonDropdown? Gender { get; set; }
		public DateTime DateRegistered { get; set; }
		public bool IsAdmin { get; set; }
		public bool IsDeactivated { get; set; }
		public string? OrganizationName { get; set; }
		public string? StaffPosition { get; set; }
		
	}
}
