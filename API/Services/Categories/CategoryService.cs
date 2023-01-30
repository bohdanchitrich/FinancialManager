using API.DTOs.Categories;
using API.DTOs.Exceptions;
using AutoMapper;
using Domain.Categories;
using Domain.FinancialOperations;
using Domain.Interfaces;
using System.Data.Entity.Core;

namespace API.Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly IAsyncRepository<Category> _categoryRepository;
    private readonly IAsyncRepository<FinancialOperation> _financialOperationRepository;
    private readonly IMapper _mapper;

    public CategoryService(IAsyncRepository<Category> categoryRepository, IAsyncRepository<FinancialOperation> financialOperationRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _financialOperationRepository = financialOperationRepository ??
                                        throw new ArgumentNullException(nameof(financialOperationRepository));
    }

    public async Task<AddCategoryResponse> AddNewAsync(AddCategoryRequest categoryRequest)
    {
        ArgumentNullException.ThrowIfNull(categoryRequest);
        var category = _mapper.Map<Category>(categoryRequest);
        var result = await _categoryRepository.AddAsync(category)
                     ?? throw new ObjectNotFoundException();
        return _mapper.Map<AddCategoryResponse>(result);
    }


    public async Task DeleteAsync(int id)
    {
        var categoryToDelete = await _categoryRepository.GetAsync(obj => obj.Id == id)
                               ?? throw new ObjectNotFoundException();
        var financialOperationsWithCategories = await _financialOperationRepository.GetAllAsync(obj => obj.CategoryId == id);
        if (financialOperationsWithCategories.Any())
        {
            throw new CategoryDeleteException($"It is impossible to delete a category from an ID: {id} if financial operation are assigned to it");
        }
        await _categoryRepository.DeleteAsync(categoryToDelete);
    }

    public async Task<UpdateCategoryResponse> UpdateAsync(UpdateCategoryRequest categoryRequest)
    {
        ArgumentNullException.ThrowIfNull(categoryRequest);
        var category = await _categoryRepository.UpdateAsync(_mapper.Map<Category>(categoryRequest))
                       ?? throw new ObjectNotFoundException();
        var response = _mapper.Map<UpdateCategoryResponse>(category);
        return response;
    }

    public async Task<GetPagedCategoryResponse> GetPagedAsync(int page, int pageSize)
    {
        var categoriesResponse = await _categoryRepository.GetPagedAsync(page, pageSize)
                                 ?? throw new ObjectNotFoundException();
        var allCategories = await _categoryRepository.GetAllAsync()
                            ?? throw new ObjectNotFoundException();
        var result = new GetPagedCategoryResponse
        {
            TotalCount = allCategories.Count
        };
        categoriesResponse.ForEach(obj => result.Categories.Add(obj));
        return result;
    }

    public async Task<GetCategoryResponse> GetByIdAsync(int id)
    {
        var result = await _categoryRepository.GetAsync(obj => obj.Id == id)
                     ?? throw new ObjectNotFoundException();
        return _mapper.Map<GetCategoryResponse>(result);
    }

    public async Task<GetAllCategoryResponse> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        var result = new GetAllCategoryResponse();
        categories.ForEach(obj => result.Categories.Add(obj));
        return result;
    }
}