using Food_Like.Server.Models;
using Food_Like.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Food_Like.Server.Services
{
    public class UserService
    {
        private readonly foodlikeContext _dbContext;
        public UserService(foodlikeContext dbContext)
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
        public bool UserExists(Auth auth)
        {
            return _dbContext.Buyer.ToList().Any(user => user.Email == auth.Email);
        }

        public dynamic GetUser(Buyer buyer)
        {
            return _dbContext.Buyer.ToList().Find(user => user.Email == buyer.Email && user.EncryptedPassword == buyer.EncryptedPassword);
        }
        public dynamic GetUser(LoginRequest buyer)
        {
            return _dbContext.Buyer.ToList().Find(user => user.Email == buyer.Email && user.EncryptedPassword == buyer.EncryptedPassword);
        }
        public AuthState GetUser(Auth auth)
        {
            var user = _dbContext.Buyer.ToList().Find(user => user.Email == auth.Email && user.EncryptedPassword == auth.EncryptedPassword);
            return new AuthState(true, user);
        }

        public bool UserIsSeller(Auth auth)
        {
            return _dbContext.Seller.ToList().Any(user => user.SellerId == auth.Request.SellerId);
        }

        
    }
}
