using Moq;
using UI.Models.Categories;
using UI.Services;
using UI.Services.Category;

namespace UiUnitTests.Services;
[TestClass]
public class CategoryServiceTests
{
    
    private Mock<IFinancialManagerHttpClient>? _httpClientMock;
    private ICategoryService? _categoryService;

    [TestInitialize]
    public void Initialize()
    {
        _httpClientMock = new Mock<IFinancialManagerHttpClient>().SetupAllProperties();
        _categoryService = new CategoryService(_httpClientMock.Object);
    }


    [TestCleanup]
    public void Cleanup()
    {
        _httpClientMock = null;
        _categoryService = null;
    }



    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public async Task AddAsyncArgumentNullExceptionTest()
    {
        //arrange

        //act
        await _categoryService!.AddAsync(null!);
        //assert
    }

    [TestMethod]
    public async Task AddAsyncTest()
    {
        //arrange
        //act
        await _categoryService!.AddAsync(new CategoryAddModel());
        //assert
        _httpClientMock!.Verify(obj => obj.PostAsync(It.IsAny<Uri>(),It.IsAny<HttpContent>()),Times.Once);
    }


    [TestMethod]
    public async Task DeleteAsyncTest()
    {
        //arrange
        //act
        await _categoryService!.DeleteAsync(1);
        //assert
        _httpClientMock!.Verify(obj => obj.DeleteAsync(It.IsAny<Uri>()),Times.Once);
    }

    [TestMethod]
    public async Task GetAllPagedAsyncTest()
    {
        //arrange

        //act
        await _categoryService!.GetAllAsync(1, 1);
        //assert
        _httpClientMock!.Verify(obj => obj.GetAsync(It.IsAny<Uri>()));
    }


    [TestMethod]
    public async Task GetAllAsyncTest()
    {
        //arrange

        //act
        await _categoryService.GetAllAsync();
        //assert
        _httpClientMock.Verify(obj => obj.GetAsync(It.IsAny<Uri>()));
    }

    [TestMethod]
    public async Task GetByIdAsync()
    {
        //arrange

        //act
        await _categoryService!.GetByIdAsync(1);
        //assert
        _httpClientMock!.Verify(obj => obj.GetAsync(It.IsAny<Uri>()));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public async Task UpdateAsyncArgumentNullExceptionTest()
    {
        //arrange
        //act
        await _categoryService!.UpdateAsync(null!);
        //assert
    }


    [TestMethod]
    public async Task UpdateAsyncTest()
    {
        //arrange
        //act
        await _categoryService!.UpdateAsync(new CategoryUpdateModel());
        //assert
        _httpClientMock!.Verify(obj => obj.PutAsync(It.IsAny<Uri>(),It.IsAny<HttpContent>()));
    }









}