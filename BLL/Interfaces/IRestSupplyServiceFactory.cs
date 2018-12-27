using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Services;

namespace BLL.Interfaces
{
    interface IRestSupplyServiceFactory
    {
        MenuService GetMenuService();
        // who talks with who? DAL <-> BLL ?? DAL <-> PL ??
    }
}
