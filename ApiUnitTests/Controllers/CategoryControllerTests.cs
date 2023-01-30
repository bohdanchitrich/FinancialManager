using API.Controllers;
using API.DTOs.Categories;
using API.Services.Categories;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace ApiUnitTests.Controllers
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CategoryControllerTests
    {
        private Mock<ICategoryService>? _categoryService;
        private CategoryController? _controller;


        [TestInitialize]
        public void Initialize()
        {
            _categoryService = new Mock<ICategoryService>();
            _controller = new CategoryController(_categoryService.Object);
        }


        [TestCleanup]
        public void Cleanup()
        {
            _categoryService = null;
            _controller = null;
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CategoryControllerConstructorThrowsArgumentNullException()
        {
            //arrange

            //act
            var result = new CategoryController(null);
            //assert
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task AddAsyncArgumentNullExceptionTest()
        {
            //arrange
            //act
            await _controller.AddAsync(null);
            //assert
        }


        [TestMethod]
        public async Task AddAsyncTest()
        {
            //arrange
            _categoryService.Setup(obj => obj.AddNewAsync(It.IsAny<AddCategoryRequest>()).Result);
            //act
            await _controller.AddAsync(Mock.Of<AddCategoryRequest>());
            //assert
            _categoryService.Verify(obj => obj.AddNewAsync(It.IsAny<AddCategoryRequest>()), Times.Once);
        }


        [TestMethod]
        public async Task GetPagedAsyncTest()
        {
            //arrange
            _categoryService.Setup(obj => obj.GetPagedAsync(It.IsAny<int>(), It.IsAny<int>()));
            //act
            await _controller.GetPagedAsync(1, 1);
            //assert
            _categoryService.Verify(obj => obj.GetPagedAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);

        }



        [TestMethod]
        public async Task GetAllAsyncTest()
        {
            //arrange
            _categoryService.Setup(obj => obj.GetAllCategoriesAsync().Result);
            //act
            await _controller.GetAllAsync();
            //assert
            _categoryService.Verify(obj => obj.GetAllCategoriesAsync(), Times.Once);
        }

        [TestMethod]
        public async Task GetAsyncTest()
        {
            //arrange
            _categoryService.Setup(obj => obj.GetByIdAsync(It.IsAny<int>()));
            //act
            await _controller.GetAsync(1);
            //assert
            _categoryService.Verify(obj => obj.GetByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsyncTest()
        {
            //arrange
            _categoryService.Setup(obj => obj.DeleteAsync(It.IsAny<int>()));
            //act
            await _controller.DeleteAsync(1);
            //assert
            _categoryService.Verify(obj => obj.DeleteAsync(It.IsAny<int>()), Times.Once);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task UpdateAsyncArgumentNullExceptionTest()
        {
            //arrange
            //act
            await _controller.UpdateAsync(null);
            //assert
        }


        [TestMethod]
        public async Task UpdateAsyncTest()
        {
            //arrange
            UpdateCategoryRequest updateCategoryRequest = new UpdateCategoryRequest();
            _categoryService.Setup(obj => obj.UpdateAsync(It.IsAny<UpdateCategoryRequest>()));
            //act
            await _controller.UpdateAsync(updateCategoryRequest);
            //assert
            _categoryService.Verify(obj => obj.UpdateAsync(It.IsAny<UpdateCategoryRequest>()), Times.Once);
        }
    }
}
