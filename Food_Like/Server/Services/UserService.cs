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
    }
}
