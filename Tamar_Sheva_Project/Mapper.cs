using AutoMapper;
using DTOs;
using Entities;


namespace Tamar_Sheva_Project;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();

        CreateMap<User, UserDto>().ReverseMap();

        CreateMap<Order, OrderReturnDto>().ReverseMap();

        CreateMap<OrderDto, Order>()        
            .ForMember(dest => dest.OrderDate,
             opts => opts.MapFrom(src => DateOnly.FromDateTime(DateTime.UtcNow)))
            .ForMember(dest => dest.OrderItems,
             opts => opts.MapFrom(src => src.OrderItemsDto)).ReverseMap();
    
        CreateMap<Product, ProductDto>().ForMember
            (dest => dest.CategoryName,
            opts => opts.MapFrom(src => src.Category.CategoryName));
 
    }
}