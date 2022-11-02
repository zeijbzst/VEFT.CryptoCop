using System.Collections.Generic;
using System.Linq;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.Entities;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Data;
using Cryptocop.Software.API.Repositories.Helpers;
using Cryptocop.Software.API.Repositories.Interfaces;

namespace Cryptocop.Software.API.Repositories.Implementations
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly CryptoDbContext _dbContext;

        public PaymentRepository(CryptoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddPaymentCard(string email, PaymentCardInputModel paymentCard)
        {
            var userId = _dbContext
                .Users
                .Where(u => u.Email == email)
                .Select(u => u.Id)
                .FirstOrDefault();

            var paymentEntity = new PaymentCard
            {
                UserId = userId,
                CardHolderName = paymentCard.CardholderName,
                CardNumber = paymentCard.CardNumber,
                Month = paymentCard.Month,
                Year = paymentCard.Year
            };

            _dbContext.Add(paymentEntity);
            _dbContext.SaveChanges();

        }

        public IEnumerable<PaymentCardDto> GetStoredPaymentCards(string email)
        {
            return _dbContext
                .Paymentcards
                .Where(p => p.User.Email == email)
                .Select(p => new PaymentCardDto
                {
                    Id = p.Id,
                    CardholderName = p.CardHolderName,
                    CardNumber = p.CardNumber,
                    Month = p.Month,
                    Year = p.Year
                });
        }
    }
}