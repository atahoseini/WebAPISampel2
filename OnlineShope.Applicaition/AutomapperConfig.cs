using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OnlineShope.Applicaition.Models;
using OnlineShope.Core.Entities;

namespace OnlineShope.Applicaition
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.ProductName, opt => opt.MapFrom(src => string.Format("{0:#,#}", src)));

        }
    }
}
