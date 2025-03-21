﻿using AutoMapper;
using Basket.Api.Entities;
using EventBus.Message.Events;


namespace Basket.Api.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BasketCheckout,BasketCheckoutEvent>().ReverseMap();
        }
    }
}
