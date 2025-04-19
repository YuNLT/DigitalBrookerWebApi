using DigitalBrooker.Domain.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBroker.Application.Abstracts
{
    public interface IPostService
    {
        Task<List<Post>> GetAllPostAsync();
    }
}
