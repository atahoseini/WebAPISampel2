using AutoMapper;
using MediatR;
using OnlineShope.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShope.Applicaition.CQRS.ProductCommandQuery.Query
{
    public class GetProductQuery: IRequest<GetProductQueryResponse>
    {
        public int Id { get; set; }
    }

    public class GetProductQueryResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PriceWithComma { get; set; }
    }

    public class ProductQueryHandler //: IRequestHandler<GetProductQuery, GetProductQueryResponse>
    {
        //private readonly IProductRepository productRepository;

        //public ProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        //{
        //    this.productRepository=productRepository;
        //    Mapper=mapper;
        //}


        ////public Task<GetProductQueryResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
        ////{

        ////}
    }
}
