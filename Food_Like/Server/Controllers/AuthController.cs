using Food_Like.Server.Models;
using Food_Like.Server.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Food_Like.Shared;
using Microsoft.EntityFrameworkCore;

namespace Food_Like.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        [HttpPost("login")]
        public LoginResponse Login(LoginRequest request)
        {
            using (var context = new foodlikeContext())
            {
                var user = new UserService(context).GetUser(request);
                if (user != null)
                {
                    return new LoginResponse
                    {
                        Sucess = true,
                        Token = String.Format("{0}-.-{1}", user.Email, user.EncryptedPassword),
                        User = user
                    };
                } 
                else
                {
                    return new LoginResponse
                    {
                        Sucess = false
                    };
                }
            }
                
        }

        [HttpPost("create")]
        public LoginResponse Create(Buyer request)
        {
            using (var context = new foodlikeContext())
            {
                if (!new UserService(context).UserExists(request))
                {
                    try
                    {
                        context.Buyer.Add(request);
                        context.SaveChanges();
                        return new LoginResponse
                        {
                            Sucess = true,
                            Token = String.Format("{0}-.-{1}", request.Email, request.EncryptedPassword),
                            User = request
                        };
                    } 
                    catch
                    {
                        return new LoginResponse
                        {
                            Sucess = false,
                        };
                    }
                    
                } else
                {
                    return new LoginResponse
                    {
                        Sucess = false,
                    };
                }
                
                
            }

        }

        [HttpPost("edit")]
        public async Task<ActionResult<Buyer>> Edit([FromHeader] string Auth, Buyer newInformation)
        {
            using (var context = new foodlikeContext())
            {
                string token;
                //Standard check for authorized access
                var userService = new UserService(context);
                var authState = userService.GetUser(Auth);

                if (authState.FoundUser == false)
                {
                    return Unauthorized();
                }

                try
                {
                    //Map relevant data to only allowed relevant data change
                    if (userService.UserIsSeller(authState))
                    {
                        context.Remove(context.Buyer.Include(e => e.Seller).ThenInclude(e => e.Address).Single(e => e.BuyerId == authState.User.BuyerId));
                        Buyer newUserInformation = new Buyer()
                        {
                            BuyerId = authState.User.BuyerId,
                            FirstName = newInformation.FirstName,
                            LastName = newInformation.LastName,
                            Email = newInformation.Email,
                            PhoneNumber = newInformation.PhoneNumber,
                            ProfilePicture = newInformation.ProfilePicture,
                            EncryptedPassword = newInformation.EncryptedPassword,
                            Seller = new Seller()
                            {
                                KitchenPicture = newInformation.Seller.KitchenPicture,
                                Address = new Address()
                                {
                                    Line1 = newInformation.Seller.Address.Line1,
                                    City = newInformation.Seller.Address.City
                                }
                            }
                        };
                        token = newInformation.Email + "-.-" + newInformation.EncryptedPassword;
                        context.Buyer.Add(newUserInformation);
                    } else
                    {
                        context.Remove(context.Buyer.Single(e => e.BuyerId == authState.User.BuyerId));
                        Buyer newUserInformation = new Buyer()
                        {
                            BuyerId = authState.User.BuyerId,
                            FirstName = newInformation.FirstName,
                            LastName = newInformation.LastName,
                            Email = newInformation.Email,
                            PhoneNumber = newInformation.PhoneNumber,
                            ProfilePicture = newInformation.ProfilePicture,
                            EncryptedPassword = newInformation.EncryptedPassword,
                        };
                        token = newInformation.Email + "-.-" + newInformation.EncryptedPassword;

                        context.Buyer.Add(newUserInformation);
                    }
                    context.SaveChanges();
                    return Ok(token);
                }
                catch (Exception exp)
                {
                    return BadRequest(exp);
                }

            }
        }
    }
}
