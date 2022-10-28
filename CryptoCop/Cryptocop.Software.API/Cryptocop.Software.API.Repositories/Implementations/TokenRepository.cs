using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.Entities;
using Cryptocop.Software.API.Repositories.Data;
using Cryptocop.Software.API.Repositories.Interfaces;
using System.Linq;

namespace Cryptocop.Software.API.Repositories.Implementations
{
    public class TokenRepository : ITokenRepository
    {
        private readonly CryptoDbContext _dbContext;

        public TokenRepository(CryptoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public JwtTokenDto CreateNewToken()
        {
            var entity = new JwtToken();
            _dbContext.Add(entity);
            _dbContext.SaveChanges();

            return new JwtTokenDto
            {
                Id = entity.Id,
                Blacklisted = entity.Blacklisted
            };

        }

        public bool IsTokenBlacklisted(int tokenId)
        {
            var token = _dbContext.JwtTokens.FirstOrDefault(t => t.Id == tokenId);
            if (token == null) { return true; } // TODO: Implement exception

            return token.Blacklisted;
        }

        public void VoidToken(int tokenId)
        {
            var token = _dbContext.JwtTokens.FirstOrDefault(t => t.Id == tokenId);
            if (token == null) { return; }
            token.Blacklisted = true;
            _dbContext.SaveChanges();
        }
    }
}