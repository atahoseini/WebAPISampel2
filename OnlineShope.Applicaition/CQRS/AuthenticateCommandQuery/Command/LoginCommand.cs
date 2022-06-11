using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShope.Core;
using OnlineShope.Core.Utilitiy;

namespace OnlineShope.Applicaition.CQRS.AuthenticateCommandQuery.Command
{

    public class LoginCommand : IRequest<LoginCommandResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandResponse
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public int ExpireTime { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
    {
        private readonly OnlineShopDbContext onlineShopDbContext;
        private readonly EncryptionUtility encryptionUtility;

        public LoginCommandHandler(OnlineShopDbContext onlineShopDbContext, EncryptionUtility encryptionUtility)
        {
            this.onlineShopDbContext=onlineShopDbContext;
            this.encryptionUtility=encryptionUtility;
        }
        public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await onlineShopDbContext.Users.SingleOrDefaultAsync(q => q.UserName == request.UserName);
            if (user == null) throw new Exception();
            
            var hashPassword = encryptionUtility.GetSH256(request.Password, user.PasswordSalt);
            if (user.Password != hashPassword) throw new Exception();

            var token= encryptionUtility.GetNewToken(user.Id);
            var response = new LoginCommandResponse
            {
                UserName = request.UserName,
                Token=token
            };
            return response;
        }
    }

}
