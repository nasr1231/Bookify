﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bookify.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Category Mapper
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<CategoryFormViewModel, Category>().ReverseMap();
            CreateMap<Category, SelectListItem>()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.CategoryName));

            // Author Mapper
            CreateMap<Author, AuthorViewModel>().ReverseMap();
            CreateMap<AuthorFormViewModel, Author>().ReverseMap();
            CreateMap<Author, SelectListItem>()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));

            // Books Mapper
            CreateMap<BookFormViewModel, Book>().ReverseMap();
        }
    }
}
