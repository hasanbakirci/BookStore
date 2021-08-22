using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using WebApi.Application.TokenOperations;
using WebApi.Application.TokenOperations.Models;
using WebApi.DBOperations;

namespace WebApi.Application.UserOperations.Commands.RefreshTokenCommand
{
    public class RefreshTokenCommand
    {
        public string refreshToken {get; set;}
        private readonly IBookStoreDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public RefreshTokenCommand(IBookStoreDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public Token Handle(){
            var user = _dbContext.Users.FirstOrDefault(x => x.RefreshToken == refreshToken && x.RefreshTokenExpireDate > DateTime.Now);
            if(user is not null){
                TokenHandler handler = new TokenHandler(_configuration); 
                Token token = handler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _dbContext.SaveChanges();
                return token;
            }
            else
                throw new InvalidOperationException("Valid bir Refresh Token Bulunamadı!");

        }
    }
}