using Name_Here.Cosmos.ModelBuilding;
using Name_Here.Models;
using Name_Here.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Name_Here.Cosmos
{
    public class CosmosRepo : IRepository
    {
        readonly IConfiguration config;
        readonly AppDBContext ctx;

        public CosmosRepo(IConfiguration config, AppDBContext repo)
        {
            this.ctx = repo;
            this.config = config;
        }
        
        public bool AddUser(AppUser user)
        {
            return true;
        }

        public AppUser GetUser(string email)
        {
           return ctx 
                .AppUsers
                .Where(e => e.Email == "email")
                .FirstOrDefault(); 
        }

        public bool UpdateUser(AppUser user)
        {
            return true;
        }
    }
}
