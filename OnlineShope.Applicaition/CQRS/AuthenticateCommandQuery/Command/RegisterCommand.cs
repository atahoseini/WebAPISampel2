using MediatR;
using OnlineShope.Core;
using OnlineShope.Core.Entities;
using OnlineShope.Core.Utilitiy;

namespace OnlineShope.Applicaition.CQRS.AuthenticateCommandQuery.Command
{
    public class RegisterCommand : IRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
    {
        private readonly OnlineShopDbContext onlineShopDbContext;
        private readonly EncryptionUtility encryptionUtility;

        public RegisterCommandHandler(OnlineShopDbContext onlineShopDbContext, EncryptionUtility encryptionUtility)
        {
            this.onlineShopDbContext=onlineShopDbContext;
            this.encryptionUtility=encryptionUtility;
        }
        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var salt = encryptionUtility.GetNewSalt();
            var hashPassword = encryptionUtility.GetSH256(request.Password, salt);

            var User = new User
            {
                Id=Guid.NewGuid(),
                Password=hashPassword,
                PasswordSalt=salt,
                RegisterDate=DateTime.Now,
                UserName=request.UserName
            };

            await onlineShopDbContext.Users.AddAsync(User);
            await onlineShopDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
