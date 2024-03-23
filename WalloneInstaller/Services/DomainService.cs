using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalloneInstaller.Models;

namespace WalloneInstaller.Services
{
    class DomainService
    {
        private static readonly Domain domain = new Domain();
        public static string Get()
        {
            return domain.Url;
        }
    }
}
