using API.DTOs.Categories;
using API.DTOs.FinancialOperations;
using AutoMapper;
using Domain.Categories;
using Domain.FinancialOperations;
using System.Diagnostics.CodeAnalysis;

namespace API.Mapper;
[ExcludeFromCodeCoverage]
public class AppMappingProfile : Profile
{

    public AppMappingProfile()
    {
        CreateMap<Category, GetCategoryResponse>();
        CreateMap<AddCategoryRequest, Category>();
        CreateMap<Category, AddCategoryResponse>();
        CreateMap<UpdateCategoryRequest, Category>();
        CreateMap<Category, UpdateCategoryResponse>()
            .ForMember(destinationMember => destinationMember.FinancialType, memberOptions => memberOptions
                .MapFrom(src => src.FinancialType.ToString()));
        //Financial operation mappers
        CreateMap<AddFinancialOperationRequest, FinancialOperation>();
        CreateMap<FinancialOperation, AddFinancialOperationResponse>();
        CreateMap<FinancialOperation, GetFinancialOperationResponse>();
        CreateMap<UpdateFinancialOperationRequest, FinancialOperation>();
        CreateMap<FinancialOperation, UpdateFinancialOperationResponse>();
    }

}