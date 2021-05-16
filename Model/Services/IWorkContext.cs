using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services
{
   public interface IWorkContext
    {
        Task<User> GetCurrentUser();
    }
}
