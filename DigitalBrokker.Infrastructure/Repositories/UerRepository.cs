using DigitalBroker.Application.Abstracts;
using DigitalBrooker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBrokker.Infrastructure.Repositories
{
    public class UerRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public UerRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        //Method to get user by refresh token
        public async Task<User?> GetUserByTokenAsync(string token)
        {
            var user = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.RefreshToken == token);
            return user;
        }
    }
}
