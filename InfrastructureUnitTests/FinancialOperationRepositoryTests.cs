using Domain.Categories;
using Domain.FinancialOperations;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace InfrastructureUnitTests;

[TestClass]
[ExcludeFromCodeCoverage]
public class FinancialOperationRepositoryTests
{

    private ApplicationDbContext _context;
    private FinancialOperationRepository _repository;


    [TestInitialize]
    public void Initialize()
    {
        var _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("Base")
            .Options;
        _context = new ApplicationDbContext(_dbContextOptions);
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
        _context.Categories.Add(new Category() { Name = "TestCategory", Description = "Test" });
        _context.FinancialOperations.AddRange(
            new FinancialOperation()
            {
                CategoryId = 1
            },
            new FinancialOperation()
            {
                CategoryId = 1
            }
        );
        _context.SaveChanges();

        _repository = new FinancialOperationRepository(_context);
    }


    [TestCleanup]
    public void Cleanup()
    {
        _context = null;
        _repository = null;
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public async Task AddAsyncExceptionTest()
    {
        //arrange
        //act
        var result = await _repository.AddAsync(null);
        //assert

    }


    [TestMethod]
    public async Task AddAsyncTest()
    {
        //arrange
        FinancialOperation financialOperation = new FinancialOperation()
        {
            CategoryId = 1
        };
        var expected = _context.FinancialOperations.Count() + 1;
        //act
        await _repository.AddAsync(financialOperation);
        var actually = _context.FinancialOperations.Count();
        //assert
        Assert.AreEqual(expected, actually);
    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public async Task DeleteAsyncExceptionTest()
    {
        //arrange

        //act
        await _repository.DeleteAsync(null);
        //assert
    }


    [TestMethod]
    public async Task DeleteAsyncTest()
    {
        //arrange
        var operationToDelete = _context.FinancialOperations.First();
        var expected = _context.FinancialOperations.Count() - 1;
        //act
        await _repository.DeleteAsync(operationToDelete);
        var actually = _context.FinancialOperations.Count();
        //assert
        Assert.AreEqual(expected, actually);
    }



    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public async Task GetAllAsyncWithExpressionParameterExceptionTest()
    {
        //arrange

        //act
        await _repository.GetAllAsync(null);
        //assert
    }


    [TestMethod]
    public async Task GetAllAsyncWithExpressionParameterTest()
    {
        //arrange
        var expected = await _context.FinancialOperations.ToListAsync();
        //act
        var actually = await _repository.GetAllAsync(obj => obj.Id > 0);
        //assert
        CollectionAssert.AreEqual(expected, actually);
    }


    [TestMethod]
    public async Task GetAllWithoutParametersTest()
    {
        //arrange
        var expected = await _context.FinancialOperations.ToListAsync();
        //act
        var actually = await _repository.GetAllAsync();
        //assert
        CollectionAssert.AreEqual(expected, actually);
    }

    [TestMethod]
    public async Task GetAllWithPageParametersTest()
    {
        //arrange
        var expected = await _context.FinancialOperations.FirstAsync();
        //act
        var actually = await _repository.GetPagedAsync(1, 1);
        //assert
        Assert.IsTrue(actually.Count == 1);
        Assert.AreEqual(expected, actually.First());
    }







    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public async Task GetAsyncExceptionTest()
    {
        //arrange

        //act
        await _repository.GetAsync(null); ;
        //assert
    }


    [TestMethod]
    public async Task GetAsyncTest()
    {
        //arrange
        var expected = _context.FinancialOperations.First();
        //act
        var actually = await _repository.GetAsync(obj => obj.Id == expected.Id);
        //assert
        Assert.AreEqual(expected, actually);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public async Task UpdateAsyncExceptionTest()
    {
        //arrange

        //act
        await _repository.UpdateAsync(null);
        //assert
    }

    [TestMethod]
    public async Task UpdateAsyncTest()//!
    {
        //arrange
        var expectedAmount = _context.FinancialOperations.First().Amount;
        var operationExpected = _context.FinancialOperations.First();
        operationExpected.Amount = -120;
        //act
        await _repository.UpdateAsync(operationExpected);
        var actually = _context.FinancialOperations.First();
        //assert
        Assert.AreNotEqual(expectedAmount, actually.Amount);

    }

}