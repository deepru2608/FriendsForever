using FriendsForever_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsForever_App.Repository
{
    public interface IAccountRepository
    {
        IEnumerable<string> GetCountries();
    }
}
