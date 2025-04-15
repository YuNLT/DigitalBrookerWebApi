using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBrooker.Domain.Constants
{
    public class SmtpInfo
    {
        public string smtpEmail {get; set;} = default!;
        public string smtpPassword { get; set; } = default!;
    }
}
