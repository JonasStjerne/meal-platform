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
        public bool UserExists(dynamic auth)
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
        public AuthState GetUser(dynamic auth)
        {
            var user = _dbContext.Buyer.Include(e => e.Seller).ToList().Find(user => user.Email == auth.Email && user.EncryptedPassword == auth.EncryptedPassword);
            if (user != null)
            {
                return new AuthState(true, user);
            } else
            {
                return new AuthState(false);
            }
            
        }

        public bool UserIsSeller(Auth<Seller> auth)
        {
            return _dbContext.Seller.Any(seller => seller.SellerId == GetUser(auth).User.BuyerId);
        }

        public bool UserIsSeller(AuthState authState)
        {
            return _dbContext.Seller.Any(seller => seller.SellerId == authState.User.BuyerId);
        }

        public bool BoughtMeal(AuthState authState, int mealId)
        {
            return _dbContext.Mealorder.Any(order => order.BuyerId == authState.User.BuyerId && order.MealId == mealId);
        }

        public bool HasMadeReview(AuthState authState, int mealId)
        {
            return _dbContext.Review.Any(review => review.BuyerId == authState.User.BuyerId && review.MealId == mealId);
        }
    }
}
