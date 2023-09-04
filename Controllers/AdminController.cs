using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PDBG.CRM.WEB.Models;
using PDBG.CRM.WEB.Models.ViewModels;

namespace PDBG.CRM.WEB.Controllers
{
    [Authorize(Roles = "Admins")]
	public class AdminController : Controller
    {
        private UserManager<AppUser> userManager;
        private IPasswordHasher<AppUser> passworHasher;
        private IPasswordValidator<AppUser> passwordValidator;
        private IUserValidator<AppUser> userValidator;

        public AdminController(
            UserManager<AppUser> userManager, 
            IPasswordHasher<AppUser> passworHasher,
			IPasswordValidator<AppUser> passwordValidator,
			IUserValidator<AppUser> userValidator)
        {
            this.userManager = userManager;
            this.passworHasher = passworHasher;
            this.passwordValidator = passwordValidator;
            this.userValidator = userValidator;
        }

        public ViewResult Index()
        {
            return View(userManager.Users);
        }

        public async Task<IActionResult> Create(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.Name,
                    Email = model.Email
                };
                                    
                IdentityResult result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);

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
                ModelState.AddModelError("", "Пользователь не найден");
            }
            return View("Index", userManager.Users);
        }

        public async Task<IActionResult> Edit(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, string email, string password)
        {
            AppUser user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                user.Email = email;

                IdentityResult validEmail = await userValidator.ValidateAsync(userManager, user);

                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }

                IdentityResult validPass = null;

				if (!String.IsNullOrEmpty(password))
                {
					validPass = await passwordValidator.ValidateAsync(userManager, user, password);

                    if (validPass.Succeeded)
                    {
						user.PasswordHash = passworHasher.HashPassword(user, password);
					}
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
				}

                if ((validEmail.Succeeded && validPass == null) ||
                    (validEmail.Succeeded && password != String.Empty && validPass.Succeeded)) 
                {
					IdentityResult result = await userManager.UpdateAsync(user);

					if (result.Succeeded)
					{
						return RedirectToAction("Index");
					}
					else
					{
						AddErrorsFromResult(result);
					}
				}                                                                          
            }
            else
            {
                ModelState.AddModelError("", "Пользователь не найден");
            }
            return View(user);
		}        

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError ("", error.Description);
            }
        }        
    }
}
