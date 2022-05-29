using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OnlineShope.Applicaition.CQRS.ProductCommandQuery.Query;
using OnlineShope.Applicaition.Models;
using OnlineShope.Core.Entities;

namespace OnlineShope.Applicaition
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            //CreateMap<Source, Destination>().ReverseMap();

            CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.PriceWithComma, opt => opt.MapFrom(src => String.Format("{0:n0}", src.Price)))
            .ReverseMap();

            //CreateMap<Product,GetProductQueryResponse>()
            //    .ForMember(des=>des.PriceWithComma,)

        }
    }
}
