using API.DTOs.Exceptions;
using API.DTOs.FinancialOperations;
using AutoMapper;
using Domain.Categories;
using Domain.FinancialOperations;
using Domain.Interfaces;
using Domain.Shared;
using System.Data.Entity.Core;

namespace API.Services.FinancialOperations;

public class FinancialOperationService : IFinancialOperationService
{
    private readonly IAsyncRepository<FinancialOperation> _financialOperationRepository;
    private readonly IAsyncRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public FinancialOperationService(IAsyncRepository<FinancialOperation> financialOperationRepository, IAsyncRepository<Category> categoryRepository, IMapper mapper)
    {
        _financialOperationRepository = financialOperationRepository ?? throw new ArgumentNullException(nameof(financialOperationRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    }

    public async Task<AddFinancialOperationResponse> AddNewAsync(AddFinancialOperationRequest addFinancialOperationRequest)
    {
        ArgumentNullException.ThrowIfNull(addFinancialOperationRequest);
        var category = await _categoryRepository.GetAsync(obj => obj.Id == addFinancialOperationRequest.CategoryId);
        if (category == null)
        {
            throw new ItemNotFoundException<Category>();
        }

        if (addFinancialOperationRequest.Amount >= 0f && category.FinancialType == FinancialType.Expense)
        {
            throw new FinancialTypeException();
        }
        if (addFinancialOperationRequest.Amount < 0f && category.FinancialType == FinancialType.Income)
        {
            throw new FinancialTypeException();
        }

        var financialOperation = _mapper.Map<FinancialOperation>(addFinancialOperationRequest);
        var result = await _financialOperationRepository.AddAsync(financialOperation)
                      ?? throw new ObjectNotFoundException();
        return _mapper.Map<AddFinancialOperationResponse>(result);
    }


    public async Task<GetPagedFinancialOperationResponse> GetPagedAsync(int page, int pageSize)
    {
        var financialOperations = await _financialOperationRepository.GetPagedAsync(page, pageSize)
                                 ?? throw new ObjectNotFoundException();
        var allOperations = await _financialOperationRepository.GetAllAsync()
                            ?? throw new ObjectNotFoundException();
        var result = new GetPagedFinancialOperationResponse
        {
            TotalCount = allOperations.Count
        };
        financialOperations.ForEach(obj => result.FinancialOperations.Add(obj));
        return result;
    }


    public async Task DeleteAsync(int id)
    {
        var operationToDelete = await _financialOperationRepository.GetAsync(obj => obj.Id == id)
                                ?? throw new ObjectNotFoundException();
        await _financialOperationRepository.DeleteAsync(operationToDelete);
    }

    public async Task<UpdateFinancialOperationResponse> UpdateAsync(UpdateFinancialOperationRequest updateFinancialOperationRequest)
    {
        ArgumentNullException.ThrowIfNull(updateFinancialOperationRequest);
        var category = await _categoryRepository.GetAsync(obj => obj.Id == updateFinancialOperationRequest.CategoryId);
        if (category == null)
        {
            throw new ItemNotFoundException<Category>();
        }
        if (updateFinancialOperationRequest.Amount >= 0f && category.FinancialType == FinancialType.Expense)
        {
            throw new FinancialTypeException();
        }
        if (updateFinancialOperationRequest.Amount < 0f && category.FinancialType == FinancialType.Income)
        {
            throw new FinancialTypeException();
        }
        var financialOperation = _mapper.Map<FinancialOperation>(updateFinancialOperationRequest);
        var result = await _financialOperationRepository.UpdateAsync(financialOperation)
                     ?? throw new ObjectNotFoundException();
        return _mapper.Map<UpdateFinancialOperationResponse>(result);
    }

    public async Task<GetFinancialOperationResponse> GetByIdAsync(int id)
    {
        var result = await _financialOperationRepository.GetAsync(obj => obj.Id == id)
            ?? throw new ObjectNotFoundException();
        return _mapper.Map<GetFinancialOperationResponse>(result);
    }
}