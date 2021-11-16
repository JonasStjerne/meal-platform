using Food_Like.Server.Models;
using Food_Like.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Food_Like.Server.Services
{
    public class UserService
    {
        private readonly FoodLikeContext _dbContext;
        public UserService(FoodLikeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Buyer> GetAll()
        {
            return _dbContext.Buyer.ToList();
        }

        public bool UserExists(Buyer buyer)
        {
            return _dbContext.Buyer.ToList().Any(user => user.Email == buyer.Email);
        }

        public dynamic GetUser(Buyer buyer)
        {
            return _dbContext.Buyer.ToList().Find(user => user.Email == buyer.Email && user.Password == buyer.Password);
        }
        public dynamic GetUser(LoginRequest buyer)
        {
            return _dbContext.Buyer.ToList().Find(user => user.Email == buyer.Email && user.Password == buyer.Password);
        }
    }
}
