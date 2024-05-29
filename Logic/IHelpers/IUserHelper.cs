using Core.Models;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.IHelpers
{
    public interface IUserHelper
    {
        Task<bool> CreateCompany(CompanyViewModel companyViewModel, string base64);
        Task<ApplicationUser> FindByEmailAsync(string email);
        ApplicationUser FindById(string Id);
        ApplicationUser FindByUserName(string username);
        string GetCurrentUserId(string username);
        string GetUserId(string username);
        string GetUserRole(string userId);
    }
}
