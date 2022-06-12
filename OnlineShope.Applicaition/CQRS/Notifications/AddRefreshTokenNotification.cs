using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShope.Core;
using OnlineShope.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShope.Applicaition.CQRS.Notifications;

public class AddRefreshTokenNotification : INotification
{
    public string RefreshToken { get; set; }
    public Guid UserId { get; set; }
    public int RefreshTokenTimeOut { get; set; }
}


public class AddRefreshTokenNotificaitionHandler : INotificationHandler<AddRefreshTokenNotification>
{
    private readonly OnlineShopDbContext onlineShopDbContext;
    private readonly IMapper mapper;

    public AddRefreshTokenNotificaitionHandler(OnlineShopDbContext onlineShopDbContext, IMapper mapper)
    {
        this.onlineShopDbContext=onlineShopDbContext;
        this.mapper=mapper;
    }
    public async Task Handle(AddRefreshTokenNotification notification, CancellationToken cancellationToken)
    {
        var userRefreshToken = mapper.Map<UserRefreshToken>(notification);


        var currentRefreshToken = await onlineShopDbContext.UserRefreshTokens
                .SingleOrDefaultAsync(q => q.UserId == notification.UserId);
        //insert or update
        if (currentRefreshToken==null)
        {
            await onlineShopDbContext.AddAsync(userRefreshToken);
        }
        else
        {
            currentRefreshToken.RefreshToken= userRefreshToken.RefreshToken;
            currentRefreshToken.RefreshTokenTimeout=userRefreshToken.RefreshTokenTimeout;
            currentRefreshToken.CreateDate= userRefreshToken.CreateDate;
            currentRefreshToken.IsValid=true;

        }
        await onlineShopDbContext.SaveChangesAsync();

    }
}
