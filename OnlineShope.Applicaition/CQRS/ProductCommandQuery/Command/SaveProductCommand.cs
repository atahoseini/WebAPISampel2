﻿using MediatR;
using OnlineShope.Core;
using OnlineShope.Core.Entities;
using OnlineShope.Core.IRepositories;
using OnlineShope.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShope.Applicaition.CQRS.ProductCommandQuery.Command;

public class SaveProductCommand : IRequest<SaveProductCommandResponse>
{
    public string ProductName { get; set; }
    public int CategoryId { get; set; }
    public long Price { get; set; }
    public string Description { get; set; }

}

public class SaveProductCommandResponse
{
    public int ProductId { get; set; }
}

public class SaveProductCommandHandler : IRequestHandler<SaveProductCommand, SaveProductCommandResponse>
{
    private readonly IProductRepository productRepository;

    //private readonly OnlineShopDbContext onlineShopDbContext;

    //public SaveProductCommandHandler(OnlineShopDbContext onlineShopDbContext)
    //{
    //    this.onlineShopDbContext=onlineShopDbContext;
    //}


    public SaveProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        this.productRepository=productRepository;
        UnitOfWork=unitOfWork;
    }

    public IUnitOfWork UnitOfWork { get; }

    public async Task<SaveProductCommandResponse> Handle(SaveProductCommand request, CancellationToken cancellationToken)
    {
        //بیزنیس تازه اینجا مینویسیم- سرویس طوری مینویسیم
        var product = new Product
        {
            ProductName=request.ProductName,
            Price=request.Price,   
        };

        //await onlineShopDbContext.Products.AddAsync(product);
        //await onlineShopDbContext.SaveChangesAsync();

        await productRepository.InsertAsync(product);
        await UnitOfWork.SaveChangesAsync();

        var response = new SaveProductCommandResponse
        {
            ProductId=product.Id
        };
        return response;
    }
}
