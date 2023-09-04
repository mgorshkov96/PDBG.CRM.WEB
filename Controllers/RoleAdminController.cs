using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PDBG.CRM.WEB.Models;
using PDBG.CRM.WEB.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace PDBG.CRM.WEB.Controllers
{
	[Authorize(Roles = "Admins")]
	public class RoleAdminController : Controller
	{
		private RoleManager<IdentityRole> roleManager;
		private UserManager<AppUser> userManager;

		public RoleAdminController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
		{
			this.roleManager = roleManager;
			this.userManager = userManager;
		}

		public ViewResult Index()		
		{
			return View(roleManager.Roles.ToList());
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create([Required] string name)
		{
			if (ModelState.IsValid)
			{
				var result = await roleManager.CreateAsync(new IdentityRole(name));
				if (result.Succeeded)
				{
					return RedirectToAction("Index");
				}
				else
				{
					AddErrorsFromResult(result);
				}
			}
			return View(name);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(string id)
		{
			var role = await roleManager.FindByIdAsync(id);

			if (role != null)
			{
				var result = await roleManager.DeleteAsync(role);

				if (result.Succeeded)
				{
					return RedirectToAction("Index");
				}
				else
				{
					AddErrorsFromResult(result);
				}
			}
			else
			{
				ModelState.AddModelError("", "Не найдено ни одной роли");
			}

			return View("Index", roleManager.Roles);
		}

		public async Task<IActionResult> Edit(string id)
		{
			var role = await roleManager.FindByIdAsync(id);
			List<AppUser> members = new List<AppUser>();
			List<AppUser> nonMembers = new List<AppUser>();

			foreach (var user in userManager.Users.ToList())
			{
				var list = await userManager.IsInRoleAsync(user, role.Name)
					? members : nonMembers;
				list.Add(user);
			}

			return View(new RoleEditModel
			{
				Role = role,
				Members = members,
				NonMembers = nonMembers
			});
		}

		[HttpPost]
		public async Task<IActionResult> Edit(RoleModificationModel model)
		{
			IdentityResult result;
			if (ModelState.IsValid)
			{
				foreach (string userId in model.IdsToAdd ?? new string[] {})
				{
					var user = await userManager.FindByIdAsync(userId);

					if (user != null)
					{
						result = await userManager.AddToRoleAsync(user, model.RoleName);

						if (!result.Succeeded)
						{
							AddErrorsFromResult(result);
						}
					}
				}

				foreach (string userId in model.IdsToDelete ?? new string[] { })
				{
					var user = await userManager.FindByIdAsync(userId);

					if (user != null)
					{
						result = await userManager.RemoveFromRoleAsync(user, model.RoleName);

						if (!result.Succeeded)
						{
							AddErrorsFromResult(result);
						}
					}
				}
			}

			if (ModelState.IsValid)
			{
				return RedirectToAction(nameof(Index));
			}
			else
			{
				return await Edit(model.RoleId);
			}
		}

		private void AddErrorsFromResult(IdentityResult result)
		{
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError("", error.Description);
			}
		}
	}
}
