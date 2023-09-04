using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using PDBG.CRM.WEB.Models;

namespace PDBG.CRM.WEB.Infrastructure
{
	[HtmlTargetElement("td", Attributes = "identity-role")]
	public class RoleUsersTagHalper : TagHelper
	{
		private UserManager<AppUser> userManager;
		private RoleManager<IdentityRole> roleManager;

		public RoleUsersTagHalper(UserManager<AppUser> usermgr,
								  RoleManager<IdentityRole> rolemgr)
		{
			userManager = usermgr;
			roleManager = rolemgr;
		}

		[HtmlAttributeName("identity-role")]
		public string Role { get; set; }

		public override async Task ProcessAsync(TagHelperContext context,
				TagHelperOutput output)
		{

			List<string> names = new List<string>();

			IdentityRole role = await roleManager.FindByIdAsync(Role);

			if (role != null)
			{
				foreach (var user in userManager.Users.ToList())
				{
					if (user != null
						&& await userManager.IsInRoleAsync(user, role.Name))
					{
						names.Add(user.UserName);
					}
				}
			}

			output.Content.SetContent(names.Count == 0 ?
				"Нету пользователей" : string.Join(", ", names));
		}
	}
}
