using DigitalBroker.Application.Abstracts;
using DigitalBrokker.Infrastructure.DbContext;
using DigitalBrooker.Domain.Entities.Models;
using DigitalBrooker.Domain.Entities.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBrokker.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<User> _userManager;
        public PropertyRepository(ApplicationDbContext applicationDbContext, UserManager<User> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }
        public async Task<List<Property>> GetAllPostAsync()
        {
            List<Property> posts = await _applicationDbContext.Properties
                .Where(p=>p.IsVerify==true).ToListAsync();
            return posts;
        }
        public async Task<List<Property>> GetPostByTownship(string township)
        {
            List<Property> posts = await _applicationDbContext.Properties
                .Where(p => p.IsVerify == true && p.IsActive== true && p.Township == township).ToListAsync();
            return posts;
        }
        public async Task<List<Property>> GetPostByUserId(Guid userId)
        {
            List<Property> posts = await _applicationDbContext.Properties
                .Where(p => p.IsVerify == true && p.IsActive == true && p.UserId == userId).ToListAsync();
            return posts;
        }

    }
}
