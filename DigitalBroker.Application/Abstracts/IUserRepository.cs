using DigitalBrooker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBroker.Application.Abstracts
{
    public interface IUserRepository
    {
        Task<User?> GetUserByTokenAsync(string token);
    }
}
