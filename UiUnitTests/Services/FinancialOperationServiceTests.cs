using Moq;
using UI.Models.FinancialOperations;
using UI.Services;
using UI.Services.FinancialOperation;

namespace UiUnitTests.Services;
[TestClass]
public class FinancialOperationServiceTests
{
    
    private Mock<IFinancialManagerHttpClient>? _httpClientMock;
    private IFinancialOperationService? _financialOperationService;

    [TestInitialize]
    public void Initialize()
    {
        _httpClientMock = new Mock<IFinancialManagerHttpClient>().SetupAllProperties();
        _financialOperationService = new FinancialOperationService(_httpClientMock.Object);
    }



    [TestCleanup]
    public void Cleanup()
    {
        _httpClientMock = null;
        _financialOperationService = null;
    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public async Task AddAsyncArgumentNullExceptionTest()
    {
        //arrange
        //act
        await  _financialOperationService!.AddAsync(null!);
        //assert
    }


    [TestMethod]
    public async Task AddAsyncTest()
    {
        //arrange
        //act
        await _financialOperationService!.AddAsync(new FinancialOperationAddModel());
        //assert
        _httpClientMock!.Verify(obj => obj.PostAsync(It.IsAny<Uri>(),It.IsAny<HttpContent>()));
    }

    [TestMethod]
    public async Task DeleteAsyncTest()
    {
        //arrange

        //act
        await _financialOperationService!.DeleteAsync(1);
        //assert
        _httpClientMock!.Verify(obj => obj.DeleteAsync(It.IsAny<Uri>()));
    }


    [TestMethod]
    public async Task GetAllAsyncTest()
    {
        //arrange
        //act
        await _financialOperationService!.GetAllAsync(1, 1);
        //assert
        _httpClientMock!.Verify(obj => obj.GetAsync(It.IsAny<Uri>()));
    }

    [TestMethod]
    public async Task GetByIdAsyncTest()
    {
        //arrange

        //act
        await _financialOperationService!.GetByIdAsync(1);
        //assert
        _httpClientMock!.Verify(obj => obj.GetAsync(It.IsAny<Uri>()));
    }


    [TestMethod]
    public async Task UpdateAsyncTest()
    {
        //arrange

        //act
        await _financialOperationService!.UpdateAsync(new FinancialOperationUpdateModel());
        //assert
        _httpClientMock!.Verify(obj => obj.PutAsync(It.IsAny<Uri>(),It.IsAny<HttpContent>()));

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

}