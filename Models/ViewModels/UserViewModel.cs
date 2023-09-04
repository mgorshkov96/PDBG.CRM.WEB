﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PDBG.CRM.WEB.Models.ViewModels
{	
	public class CreateModel
	{
		[Required]
		public string Name { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }
	}

	public class LoginModel
	{
		[Required]
		[UIHint("email")]
		public string Email { get; set; }

		[Required]
		[UIHint("password")]
		public string Password { get; set; }
	}

	public class RoleEditModel
	{
		public IdentityRole Role { get; set; }
		public IEnumerable<AppUser> Members { get; set; }
		public IEnumerable<AppUser> NonMembers { get; set; }
	}

	public class RoleModificationModel
	{				
		public string RoleId { get; set; }
		public string[]? IdsToAdd { get; set; }
		public string[]? IdsToDelete { get; set; }

		[Required]
		public string RoleName { get; set; }
	}
}
