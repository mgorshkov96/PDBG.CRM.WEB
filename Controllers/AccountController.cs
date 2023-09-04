using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PDBG.CRM.WEB.Models;
using PDBG.CRM.WEB.Models.ViewModels;

namespace PDBG.CRM.WEB.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private UserManager<AppUser> userManager;
		private SignInManager<AppUser> signInManager;

		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		[AllowAnonymous]
		public IActionResult Login(string returnUrl)
		{
			ViewBag.returnUrl = returnUrl;
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginModel details, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				AppUser user = await userManager.FindByEmailAsync(details.Email);

				if (user != null) 
				{
					await signInManager.SignOutAsync();
					var result = await signInManager.PasswordSignInAsync(
						user, details.Password, false, false);

					if (result.Succeeded)
					{
						return Redirect(returnUrl ?? "/");
					}
				}

				ModelState.AddModelError(nameof(LoginModel.Email), "Invalid user or password");
			}

			return View(details);
		}

		[Authorize]
		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("AgentsOnMap", "Map");
		}

		[AllowAnonymous]
		public IActionResult AccessDenied()
		{
			return View();
		}
	}
}
