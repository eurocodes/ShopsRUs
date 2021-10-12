using AutoMapper;
using Domain.DTO;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsRUs.Mapper
{
    public class Profiles: Profile
    {
        public Profiles()
        {
            CreateMap<CustomerDto, Customer>().ReverseMap();
            CreateMap<UserTypeDto, UserType>();
            CreateMap<DiscountDto, Discount>();
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<Invoice, InvoiceReturnDto>();
        }
    }
}
