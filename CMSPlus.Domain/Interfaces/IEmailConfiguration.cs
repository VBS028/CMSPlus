using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSPlus.Domain.Interfaces
{
    public interface IEmailConfiguration
    {
        public string FromAddress{get;}
        public string Address{get;}
        public int Port{get;}
        public bool UseSsl{get;}
        public string Username{get;}
        public string Password{get;}
    }
}
