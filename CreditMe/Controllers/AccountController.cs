using Core.DB;
using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Policy;

namespace CreditMe.Controllers
{
	public class AccountController : Controller
	{
		private readonly AppDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserHelper _userHelper;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context, IUserHelper userHelper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _userHelper = userHelper;
        }
        public IActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public IActionResult CompanyRegistration()
		{
			return View();
		}
        [HttpPost]
        public async Task<JsonResult> CompanyRegistration(string companyDetails, string base64)
        {
            if (companyDetails != null)
            {
                var companyViewModel = JsonConvert.DeserializeObject<CompanyViewModel>(companyDetails);
                if (companyViewModel != null)
                {
                    var checkForCompanyName = _context.Companies.Where(x => x.CompanyName == companyViewModel.CompanyName && !x.Deleted && x.Active).FirstOrDefault();
                    if (checkForCompanyName != null)
                    {
                        return Json(new { isError = true, msg = "Company Name belongs to another Company" });
                    }
                    var checkForEmail = await _userHelper.FindByEmailAsync(companyViewModel.Email).ConfigureAwait(false);
                    if (checkForEmail != null)
                    {
                        return Json(new { isError = true, msg = "Email belongs to another Company" });
                    }
                    if (companyViewModel.Password != companyViewModel.ConfirmPassword)
                    {
                        return Json(new { isError = true, msg = "Password and Confirm password must match" });
                    }
                    var createCompany = await _userHelper.CreateCompany(companyViewModel, base64).ConfigureAwait(false);
                    if (createCompany)
                    {
                        return Json(new { isError = false, msg = "Company registered successfully. Please, login to continue" });
                    }
                    return Json(new { isError = true, msg = "Unable to create Company" });
                }
            }
            return Json(new { isError = true, msg = " An error occured, please try again. Please contact admin if issue persists.." });
        }
        [HttpGet]
        public IActionResult Login()
		{
			return View();
		}
        [HttpPost]
        public async Task<JsonResult> Login(string email, string password)
        {
            if (email != null && password != null)
            {
                var filterSpace = email.Replace(" ", "");
                var existingUser = _userHelper.FindByEmailAsync(filterSpace).Result;
                if (existingUser != null)
                {
                    var PasswordSignIn = await _signInManager.PasswordSignInAsync(existingUser, password, true, true).ConfigureAwait(false);

                    if (PasswordSignIn.Succeeded)
                    {
                        var url = "";
                        var userRole = await _userManager.GetRolesAsync(existingUser).ConfigureAwait(false);
                        if (userRole.FirstOrDefault().ToLower().Contains("superadmin"))
                        {
                            url = "/SuperAdmin/Index";
                        }
                        else
                        {
                            url = "/Admin/Index";
                        }
                        return Json(new { isError = false, dashboard = url });
                    }
                    return Json(new { isError = true, msg = "Password is not correct" });
                }
                return Json(new { isError = true, msg = "Account does not exist,Contact your Admin" });
            }
            return Json(new { isError = true, msg = "Username and Password Required" });
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Login", "Account");
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }


    }
}
