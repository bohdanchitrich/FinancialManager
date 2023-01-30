using AutoMapper;
using Domain.Categories;
using Domain.FinancialOperations;
using UI.Models.Categories;
using UI.Models.FinancialOperations;

namespace UI.Mapper;

public class AppMappingProfile : Profile
{

    public AppMappingProfile()
    {
        CreateMap<Category, CategoryUpdateModel>();
        CreateMap<FinancialOperation, FinancialOperationUpdateModel>();
    }

}