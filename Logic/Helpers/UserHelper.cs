using Logic.IHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Core.Models;
using Core.DB;
using Microsoft.EntityFrameworkCore;
using Core.ViewModels;

namespace Logic.Helpers
{
	public class UserHelper : IUserHelper
	{
		private readonly AppDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        public UserHelper(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.Users.Where(s => s.Email == email)?.FirstOrDefaultAsync();
        }
        public ApplicationUser FindByUserName(string username)
        {
            return _userManager.Users.Where(s => s.UserName == username).FirstOrDefault();
        }
        public int GetAllUser()
        {
            return _userManager.Users.Where(x => !x.IsDeactivated).Count();
        }
        public string GetUserRole(string userId)
        {
            if (userId != null)
            {
                var userRole = _context.UserRoles.Where(x => x.UserId == userId).FirstOrDefault();
                if (userRole != null)
                {
                    var roles = _context.Roles.Where(x => x.Id == userRole.RoleId).FirstOrDefault();
                    if (roles != null)
                    {
                        return roles.Name;
                    }
                }
            }
            return null;
        }
        public string GetUserId(string username)
        {
            return _userManager.Users.Where(s => s.UserName == username)?.FirstOrDefaultAsync().Result.Id?.ToString();
        }
        public ApplicationUser FindById(string Id)
        {
            return _userManager.Users.Where(s => s.Id == Id).FirstOrDefault();
        }
        public string GetCurrentUserId(string username)
        {
            try
            {
                if (username != null)
                {
                    return _userManager.Users.Where(s => s.UserName == username)?.FirstOrDefault()?.Id?.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<bool> CreateCompany(CompanyViewModel companyViewModel, string base64)
        {
            var createUser = await CreateCompanyAdminUser(companyViewModel).ConfigureAwait(false);
            if (createUser != null)
            {
                var company = new Company()
                {
                    CompanyName = companyViewModel.CompanyName,
                    Address = companyViewModel.Address,
                    DateCreated = DateTime.Now,
                    Email = companyViewModel.Email,
                    CompanyPhone = companyViewModel.CompanyPhone,
                    CompanyLogo = base64,
                    Active = true,
                    Deleted = false,
                    CreatedById = createUser.Id,
                };
                await _context.Companies.AddAsync(company).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);

                var updateCompanyId = UpdateUserCompanyId(company.Id, createUser.Id);
                if (updateCompanyId)
                {
                    return true;
                }
            }
            return false;
        }
        public async Task<ApplicationUser> CreateCompanyAdminUser(CompanyViewModel userDetails)
        {
            try
            {
                var user = new ApplicationUser();
                user.UserName = userDetails.CompanyName;
                user.Email = userDetails.Email;
                user.FirstName = userDetails.FirstName;
                user.LastName = userDetails.LastName;
                user.PhoneNumber = userDetails.CompanyPhone;
                user.Address = userDetails.Address;
                user.DateRegistered = DateTime.Now;
                user.IsDeactivated = false;
                user.IsAdmin = true;
                user.StaffPosition = userDetails.StaffPosition;
                var createUser = await _userManager.CreateAsync(user, userDetails.Password).ConfigureAwait(false);
                if (createUser.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin").ConfigureAwait(false);
                    return user;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool UpdateUserCompanyId(Guid companyId, string userId)
        {
            var user = _context.ApplicationUsers.Where(x => x.Id == userId && !x.IsDeactivated).Include(x => x.Company).FirstOrDefault();
            if (user != null)
            {
                user.CompanyId = companyId;
                user.OrganizationName = user?.Company?.CompanyName;
                _context.ApplicationUsers.Update(user);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        

    }
}
