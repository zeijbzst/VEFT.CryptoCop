﻿using System;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Interfaces;
using Cryptocop.Software.API.Repositories.Data;
using Cryptocop.Software.API.Models.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using System.Linq;

namespace Cryptocop.Software.API.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly CryptoDbContext _dbContext;
        private readonly ITokenRepository _tokenRepository;

        private string _salt = "00209b47-08d7-475d-a0fb-20abf0872ba0";

        public UserRepository(CryptoDbContext dbContext, ITokenRepository tokenRepository)
        {
            _dbContext = dbContext;
            _tokenRepository = tokenRepository;
        }

        public UserDto CreateUser(RegisterInputModel inputModel)
        {
            var entity = new User
            {
                FullName = inputModel.FullName,
                Email = inputModel.Email,
                HashedPassword = HashPassword(inputModel.Password)
            };

            _dbContext.Add(entity);
            _dbContext.SaveChanges();

            var token = _tokenRepository.CreateNewToken();

            return new UserDto
            {
                Id = entity.Id,
                FullName = entity.FullName,
                Email = entity.Email,
                TokenId = token.Id
            };

        }

        public UserDto AuthenticateUser(LoginInputModel loginInputModel)
        {
            var user = _dbContext.Users.FirstOrDefault(u =>
                u.Email == loginInputModel.Email &&
                u.HashedPassword == HashPassword(loginInputModel.Password));
            if (user == null) { return null; }

            var token = _tokenRepository.CreateNewToken();

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                TokenId = token.Id
            };
        }

        private string HashPassword(string password)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: CreateSalt(),
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
        }

        private byte[] CreateSalt() =>
            Convert.FromBase64String(Convert.ToBase64String(Encoding.UTF8.GetBytes(_salt)));
    }
}