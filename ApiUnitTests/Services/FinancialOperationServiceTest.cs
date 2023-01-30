using API.DTOs.Exceptions;
using API.DTOs.FinancialOperations;
using API.Mapper;
using API.Services.FinancialOperations;
using AutoMapper;
using Domain.Categories;
using Domain.FinancialOperations;
using Domain.Interfaces;
using Domain.Shared;
using Moq;
using System.Data.Entity.Core;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace ApiUnitTests.Services;

[TestClass]
[ExcludeFromCodeCoverage]
public class FinancialOperationServiceTest
{
    private Mock<IAsyncRepository<FinancialOperation>>? _operationRepository;
    private IFinancialOperationService? _financialOperationService;
    private Mock<IAsyncRepository<Category>>? _categoryRepositoryMock;
    private IMapper? _mapper;
    [TestInitialize]
    public void Initialize()
    {
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AppMappingProfile>();
        }).CreateMapper();

        _operationRepository = new Mock<IAsyncRepository<FinancialOperation>>();
        _categoryRepositoryMock = new Mock<IAsyncRepository<Category>>();
        _financialOperationService = new FinancialOperationService(_operationRepository.Object, _categoryRepositoryMock.Object, _mapper);
    }


    [TestCleanup]
    public void Cleanup()
    {
        _operationRepository = null;
        _financialOperationService = null;
        _categoryRepositoryMock = null;
    }



    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void FinancialOperationServiceConstructorThrowArgumentNullExceptionNullOperationRepositoryTest()
    {

        //arrange

        //act
        var result = new FinancialOperationService(null!, _categoryRepositoryMock!.Object, Mock.Of<IMapper>());
        //assert
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void FinancialOperationServiceConstructorThrowArgumentNullExceptionNullCategoryRepositoryTest()
    {

        //arrange

        //act
        var result = new FinancialOperationService(_operationRepository!.Object, null!, Mock.Of<IMapper>());
        //assert
    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void FinancialOperationServiceConstructorThrowArgumentNullExceptionNullMapperTest()
    {

        //arrange

        //act
        var result = new FinancialOperationService(_operationRepository!.Object, _categoryRepositoryMock!.Object, null!);
        //assert
    }






    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public async Task AddNewAsyncArgumentNullExceptionTest()
    {
        //arrange
        //act
        await _financialOperationService!.AddNewAsync(null!);
        //assert
    }

    [TestMethod]
    [ExpectedException(typeof(ObjectNotFoundException))]
    public async Task AddNewAsyncObjectNotFoundExceptionTest()
    {
        //arrange
        AddFinancialOperationRequest request = new AddFinancialOperationRequest();
        _categoryRepositoryMock!.Setup(obj => obj.GetAsync(It.IsAny<Expression<Func<Category, bool>>>()).Result)
            .Returns(new Category());
        _operationRepository!.Setup(obj => obj.AddAsync(It.IsAny<FinancialOperation>()).Result)
            .Returns(() => null!);
        //act
        await _financialOperationService!.AddNewAsync(request);
        //assert
    }

    [TestMethod]
    [ExpectedException(typeof(ItemNotFoundException<Category>))]
    public async Task AddNewAsyncCategoryNotFoundExceptionTest()
    {
        //arrange
        AddFinancialOperationRequest request = new AddFinancialOperationRequest();
        _categoryRepositoryMock!.Setup(obj => obj.GetAsync(It.IsAny<Expression<Func<Category, bool>>>()).Result)
            .Returns(() => null);
        _operationRepository!.Setup(obj => obj.AddAsync(It.IsAny<FinancialOperation>()).Result)
            .Returns(() => new FinancialOperation());
        //act
        await _financialOperationService!.AddNewAsync(request);
        //assert
    }


    [TestMethod]
    [ExpectedException(typeof(FinancialTypeException))]
    public async Task AddNewAsyncCategoryFinancialTypeExceptionTest()
    {
        //arrange
        _categoryRepositoryMock!.Setup(obj => obj.GetAsync(It.IsAny<Expression<Func<Category, bool>>>()).Result)
            .Returns(() => new Category() { FinancialType = FinancialType.Income });
        _operationRepository!.Setup(obj => obj.AddAsync(It.IsAny<FinancialOperation>()).Result)
            .Returns(() => new FinancialOperation());
        AddFinancialOperationRequest addFinancialOperationRequest = new()
        {
            Amount = -1f
        };
        //act
        await _financialOperationService!.AddNewAsync(addFinancialOperationRequest);
        //assert
    }

    [TestMethod]
    public async Task AddNewAsyncTest()
    {
        //arrange
        AddFinancialOperationRequest request = new AddFinancialOperationRequest();
        _categoryRepositoryMock!.Setup(obj => obj.GetAsync(It.IsAny<Expression<Func<Category, bool>>>()).Result)
            .Returns(new Category());
        _operationRepository!.Setup(obj => obj.AddAsync(It.IsAny<FinancialOperation>()).Result)
            .Returns(() => new FinancialOperation());
        //act
        await _financialOperationService!.AddNewAsync(request);
        //assert
        _operationRepository.Verify(obj => obj.AddAsync(It.IsAny<FinancialOperation>()), Times.Once());
    }


    [TestMethod]
    [ExpectedException(typeof(ObjectNotFoundException))]
    public async Task GetAllAsyncObjectNotFoundExceptionFirstTest()
    {
        //arrange
        _operationRepository!.Setup(obj => obj.GetPagedAsync(It.IsAny<int>(), It.IsAny<int>()).Result)
            .Returns(() => null!);
        _operationRepository.Setup(obj => obj.GetAllAsync().Result)
            .Returns(() => new List<FinancialOperation>());
        //act
        await _financialOperationService!.GetPagedAsync(1, 1);
        //assert
    }
    [TestMethod]
    [ExpectedException(typeof(ObjectNotFoundException))]
    public async Task GetAllAsyncObjectNotFoundExceptionSecondTest()
    {
        //arrange
        _operationRepository!.Setup(obj => obj.GetPagedAsync(It.IsAny<int>(), It.IsAny<int>()).Result)
            .Returns(() => new List<FinancialOperation>());
        _operationRepository.Setup(obj => obj.GetAllAsync().Result)
            .Returns(() => null!);
        //act
        await _financialOperationService!.GetPagedAsync(1, 1);
        //assert
    }
    [TestMethod]
    public async Task GetAllAsyncTest()
    {
        //arrange
        _operationRepository!.Setup(obj => obj.GetPagedAsync(It.IsAny<int>(), It.IsAny<int>()).Result)
            .Returns(new List<FinancialOperation>());
        _operationRepository.Setup(obj => obj.GetAllAsync().Result)
            .Returns(() => new List<FinancialOperation>());
        //act
        await _financialOperationService!.GetPagedAsync(1, 1);
        //assert
        _operationRepository.Verify(obj => obj.GetPagedAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
    }





    [TestMethod]
    [ExpectedException(typeof(ObjectNotFoundException))]
    public async Task GetByIdAsyncObjectNotFoundExceptionTest()
    {
        //arrange
        _operationRepository!.Setup(obj => obj.GetAsync(It.IsAny<Expression<Func<FinancialOperation, bool>>>()).Result)
            .Returns(() => null);
        //act
        await _financialOperationService!.GetByIdAsync(1);
        //assert
    }
    [TestMethod]
    public async Task GetByIdAsyncTest()
    {
        //arrange
        _operationRepository!.Setup(obj => obj.GetAsync(It.IsAny<Expression<Func<FinancialOperation, bool>>>()).Result)
            .Returns(new FinancialOperation());
        //act
        await _financialOperationService!.GetByIdAsync(1);
        //assert
        _operationRepository.Verify(obj => obj.GetAsync(It.IsAny<Expression<Func<FinancialOperation, bool>>>()), Times.Once);
    }





    [TestMethod]
    [ExpectedException(typeof(ObjectNotFoundException))]
    public async Task DeleteAsyncObjectNotFoundExceptionTest()
    {
        //arrange
        _operationRepository!.Setup(obj => obj.GetAsync(It.IsAny<Expression<Func<FinancialOperation, bool>>>()).Result)
            .Returns(() => null);
        //act
        await _financialOperationService!.DeleteAsync(1);
        //assert
    }


    [TestMethod]
    public async Task DeleteAsyncTest()
    {
        //arrange
        _operationRepository!.Setup(obj => obj.GetAsync(It.IsAny<Expression<Func<FinancialOperation, bool>>>()).Result)
            .Returns(new FinancialOperation());
        //act
        await _financialOperationService!.DeleteAsync(1);
        //assert
        _operationRepository.Verify(obj => obj.DeleteAsync(It.IsAny<FinancialOperation>()), Times.Once);
    }



    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public async Task UpdateAsyncArgumentNullExceptionTest()
    {
        //arrange
        //act
        await _financialOperationService!.UpdateAsync(null!);
        //assert
    }


    [TestMethod]
    [ExpectedException(typeof(ObjectNotFoundException))]
    public async Task UpdateAsyncObjectNotFoundExceptionTest()
    {
        //arrange
        UpdateFinancialOperationRequest financialOperationRequest = new UpdateFinancialOperationRequest();
        _categoryRepositoryMock!.Setup(obj => obj.GetAsync(It.IsAny<Expression<Func<Category, bool>>>()).Result)
            .Returns(new Category());
        _operationRepository!.Setup(obj => obj.UpdateAsync(It.IsAny<FinancialOperation>()).Result)
            .Returns(() => null!);
        //act
        await _financialOperationService!.UpdateAsync(financialOperationRequest);
        //assert
    }

    [TestMethod]
    [ExpectedException(typeof(FinancialTypeException))]
    public async Task UpdateAsyncFinancialTypeExceptionTest()
    {
        //arrange
        _categoryRepositoryMock!.Setup(obj => obj.GetAsync(It.IsAny<Expression<Func<Category, bool>>>()).Result)
            .Returns(new Category() { FinancialType = FinancialType.Income });
        _operationRepository!.Setup(obj => obj.UpdateAsync(It.IsAny<FinancialOperation>()).Result)
            .Returns(() => new FinancialOperation());
        UpdateFinancialOperationRequest financialOperationRequest = new()
        {
            Amount = -1f
        };
        //act
        await _financialOperationService!.UpdateAsync(financialOperationRequest);
        //assert
    }


    [TestMethod]
    [ExpectedException(typeof(ItemNotFoundException<Category>))]
    public async Task UpdateAsyncCategoryNotFoundExceptionTest()
    {
        //arrange
        UpdateFinancialOperationRequest financialOperationRequest = new UpdateFinancialOperationRequest();
        _categoryRepositoryMock!.Setup(obj => obj.GetAsync(It.IsAny<Expression<Func<Category, bool>>>()).Result)
            .Returns(() => null);
        _operationRepository!.Setup(obj => obj.UpdateAsync(It.IsAny<FinancialOperation>()).Result)
            .Returns(() => new FinancialOperation());
        //act
        await _financialOperationService!.UpdateAsync(financialOperationRequest);
        //assert
    }

    [TestMethod]
    public async Task UpdateAsyncTest()
    {
        //arrange
        UpdateFinancialOperationRequest financialOperationRequest = new UpdateFinancialOperationRequest();
        _categoryRepositoryMock!.Setup(obj => obj.GetAsync(It.IsAny<Expression<Func<Category, bool>>>()).Result)
            .Returns(new Category());
        _operationRepository!.Setup(obj => obj.UpdateAsync(It.IsAny<FinancialOperation>()).Result)
            .Returns(new FinancialOperation());
        //act
        await _financialOperationService!.UpdateAsync(financialOperationRequest);
        //assert
        _operationRepository.Verify(obj => obj.UpdateAsync(It.IsAny<FinancialOperation>()), Times.Once);
    }
}