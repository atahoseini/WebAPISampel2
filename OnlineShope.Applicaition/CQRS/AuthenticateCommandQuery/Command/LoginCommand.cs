using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineShope.Applicaition.CQRS.Notifications;
using OnlineShope.Core;
using OnlineShope.Core.Utilitiy;
using OnlineShope.Infrastructure.Model;

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
        public string RefreshToken { get; set; }
        public int ExpireTime { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
    {
        private readonly OnlineShopDbContext onlineShopDbContext;
        private readonly EncryptionUtility encryptionUtility;
        private readonly IMediator mediator;
        private readonly Configs configs;

        public LoginCommandHandler(OnlineShopDbContext onlineShopDbContext, 
            EncryptionUtility encryptionUtility,IMediator mediator,
            IOptions<Configs> options
            )
        {
            this.onlineShopDbContext=onlineShopDbContext;
            this.encryptionUtility=encryptionUtility;
            this.mediator=mediator;
            this.configs=options.Value;
        }
        public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await onlineShopDbContext.Users.SingleOrDefaultAsync(q => q.UserName == request.UserName);
            if (user == null) throw new Exception();
            
            var hashPassword = encryptionUtility.GetSH256(request.Password, user.PasswordSalt);
            if (user.Password != hashPassword) throw new Exception();

            var token= encryptionUtility.GetNewToken(user.Id);
            var refreshToken= encryptionUtility.GetnewRefreshToken();

            //insert or update refresh token in db
            // Inotification Imediator
            // send sms login
            var refreshTokenNotofication = new AddRefreshTokenNotification
            {
                RefreshToken=refreshToken,
                RefreshTokenTimeOut= configs.RefreshTokenTimout,//read from configs
                UserId=user.Id,

            };
            await mediator.Publish(refreshTokenNotofication);

            var response = new LoginCommandResponse
            {
                UserName = request.UserName,
                Token=token,
                RefreshToken=refreshToken,
                
            };
            return response;
        }
    }

}
