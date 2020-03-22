using FriendsForever_App.Data;
using FriendsForever_App.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsForever_App.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext dbContext;

        public AccountRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<string> GetCountries()
        {
            var liCountries = dbContext.CountryMaster.Select(e => e.CountryName).ToList();
            return liCountries;
        }
    }
}
