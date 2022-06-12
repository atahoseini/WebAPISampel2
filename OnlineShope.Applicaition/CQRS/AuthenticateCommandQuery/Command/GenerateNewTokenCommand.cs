using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineShope.Applicaition.CQRS.Notifications;
using OnlineShope.Core;
using OnlineShope.Core.Utilitiy;
using OnlineShope.Infrastructure.Model;

namespace OnlineShope.Applicaition.CQRS.AuthenticateCommandQuery.Command;

public class GenerateNewTokenCommand : IRequest<GenerateNewTokenCommandResponse>
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}


public class GenerateNewTokenCommandResponse
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}

public class GenerateNewTokenCommandHandler : IRequestHandler<GenerateNewTokenCommand, GenerateNewTokenCommandResponse>
{
    private readonly OnlineShopDbContext onlineShopDbContext;
    private readonly IMapper mapper;
    private readonly IMediator mediator;
    private readonly EncryptionUtility encryptionUtility;
    private readonly Configs configs;

    public GenerateNewTokenCommandHandler(OnlineShopDbContext onlineShopDbContext, IMapper mapper, IMediator mediator,
        EncryptionUtility encryptionUtility, IOptions<Configs> options)
    {
        this.onlineShopDbContext=onlineShopDbContext;
        this.mapper=mapper;
        this.mediator=mediator;
        this.encryptionUtility=encryptionUtility;
        this.configs=options.Value;
    }
    public async Task<GenerateNewTokenCommandResponse> Handle(GenerateNewTokenCommand request, CancellationToken cancellationToken)
    {
        var userRefreshToken = await onlineShopDbContext.UserRefreshTokens
                .SingleOrDefaultAsync(q => q.RefreshToken== request.RefreshToken);
        if (userRefreshToken==null) throw new Exception();

        var token = encryptionUtility.GetNewToken(userRefreshToken.UserId);
        var refreshToken = encryptionUtility.GetnewRefreshToken();

        //insert or update refresh token in db
        // Inotification Imediator
        // send sms login
        var refreshTokenNotofication = new AddRefreshTokenNotification
        {
            RefreshToken=refreshToken,
            RefreshTokenTimeOut= configs.RefreshTokenTimout,//read from configs
            UserId=userRefreshToken.UserId,

        };
        await mediator.Publish(refreshTokenNotofication);

        var response = new GenerateNewTokenCommandResponse
        {
            RefreshToken = refreshToken,
            Token=token,
        };

        return response;
    }
}
