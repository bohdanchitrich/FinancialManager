using API.DTOs.Reports;
using API.Services.Reports;
using Domain.FinancialOperations;
using Domain.Interfaces;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace ApiUnitTests.Services;
[ExcludeFromCodeCoverage]
[TestClass]
public class ReportServiceTests
{
    private Mock<IAsyncRepository<FinancialOperation>> _financialOperationRepositoryMock;
    private IReportService _reportService;

    [TestInitialize]
    public void Initialize()
    {
        _financialOperationRepositoryMock = new Mock<IAsyncRepository<FinancialOperation>>();
        _reportService = new ReportService(_financialOperationRepositoryMock.Object);
    }


    [TestCleanup]
    public void Cleanup()
    {
        _financialOperationRepositoryMock = null;
        _reportService = null;
    }



    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ReportServiceConstructorThrowArgumentNullExceptionTest()
    {

        //arrange

        //act
        var result = new ReportService(null);
        //assert
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public async Task GetDailyReportAsyncArgumentNullExceptionTest()
    {
        //arrange
        //act
        await _reportService.GetDailyReportAsync(null);
        //assert
    }

    [TestMethod]
    public async Task GetDailyReportAsyncTest()
    {
        //arrange
        GetDailyReportRequest request = new GetDailyReportRequest();
        var operationExpected = new List<FinancialOperation>()
        {
            new FinancialOperation()
            {
                Amount = 100
            }
        };
        _financialOperationRepositoryMock.Setup(obj =>
                obj.GetAllAsync(It.IsAny<Expression<Func<FinancialOperation, bool>>>()).Result)
            .Returns(operationExpected);
        //act
        var actually = await _reportService.GetDailyReportAsync(request);
        //assert
        _financialOperationRepositoryMock.Verify(obj => obj.GetAllAsync(It.IsAny<Expression<Func<FinancialOperation, bool>>>()), Times.Once);
        CollectionAssert.AreEqual(operationExpected, actually.Operations.ToList());
    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public async Task GetDateReportAsyncArgumentNullExceptionTest()
    {
        //arrange
        //act
        await _reportService.GetDateReportAsync(null);
        //assert
    }


    [TestMethod]
    public async Task GetDateAsyncTest()
    {
        //arrange
        GetDateReportRequest getDateReportRequest = new GetDateReportRequest();
        var operationExpected = new List<FinancialOperation>()
        {
            new FinancialOperation()
            {
                Amount = 100
            }
        };
        _financialOperationRepositoryMock.Setup(obj =>
                obj.GetAllAsync(It.IsAny<Expression<Func<FinancialOperation, bool>>>()).Result)
            .Returns(operationExpected);
        //act
        var actually = await _reportService.GetDateReportAsync(getDateReportRequest);
        //assert
        _financialOperationRepositoryMock.Verify(obj => obj.GetAllAsync(It.IsAny<Expression<Func<FinancialOperation, bool>>>()), Times.Once);
        CollectionAssert.AreEqual(operationExpected, actually.Operations.ToList());
    }
}
