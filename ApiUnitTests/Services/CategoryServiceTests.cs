using API.DTOs.Categories;
using API.DTOs.Exceptions;
using API.Mapper;
using API.Services.Categories;
using AutoMapper;
using Domain.Categories;
using Domain.FinancialOperations;
using Domain.Interfaces;
using Moq;
using System.Data.Entity.Core;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace ApiUnitTests.Services
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CategoryServiceTests
    {

        private Mock<IAsyncRepository<Category>> _categoryRepository;
        private Mock<IAsyncRepository<FinancialOperation>> _operationRepository;
        private ICategoryService _categoryService;
        private Category _validCategory;
        private IMapper _mapper;
        [TestInitialize]
        public void Initialize()
        {
            _validCategory = new Category()
            {
                Name = "Name",
                Description = "Desc"
            };
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AppMappingProfile>();
            }).CreateMapper();
            _categoryRepository = new Mock<IAsyncRepository<Category>>();
            _operationRepository = new Mock<IAsyncRepository<FinancialOperation>>();
            _categoryService = new CategoryService(_categoryRepository.Object, _operationRepository.Object, _mapper);
        }


        [TestCleanup]
        public void Cleanup()
        {
            _categoryRepository = null;
            _operationRepository = null;
            _categoryService = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CategoryServiceConstructorThrowArgumentNullExceptionNullCategoryRepositoryTest()
        {

            //arrange

            //act
            var result = new CategoryService(null, Mock.Of<IAsyncRepository<FinancialOperation>>(), Mock.Of<IMapper>());
            //assert
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CategoryServiceConstructorThrowArgumentNullExceptionNullOperationRepositoryTest()
        {

            //arrange

            //act
            var result = new CategoryService(Mock.Of<IAsyncRepository<Category>>(), null, Mock.Of<IMapper>());
            //assert
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CategoryServiceConstructorThrowArgumentNullExceptionNullCategoryMapperTest()
        {

            //arrange

            //act
            var result = new CategoryService(Mock.Of<IAsyncRepository<Category>>(), Mock.Of<IAsyncRepository<FinancialOperation>>(), null);
            //assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task AddNewAsyncArgumentNullExceptionTest()
        {
            //arrange
            //act
            await _categoryService.AddNewAsync(null);
            //assert
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException))]
        public async Task AddNewAsyncObjectNotFoundExceptionTest()
        {
            //arrange
            _categoryRepository.Setup(obj => obj.AddAsync(It.IsAny<Category>()).Result).Returns(() => null);
            AddCategoryRequest categoryRequest = new AddCategoryRequest();
            //act
            await _categoryService.AddNewAsync(categoryRequest);
            //assert
        }

        [TestMethod]
        public async Task AddNewAsyncTest()
        {
            //arrange
            AddCategoryRequest categoryRequest = new AddCategoryRequest();
            _categoryRepository.Setup(obj => obj.AddAsync(It.IsAny<Category>()).Result).Returns(_validCategory);
            //act
            await _categoryService.AddNewAsync(categoryRequest);
            //assert
            _categoryRepository.Verify(obj => obj.AddAsync(It.IsAny<Category>()), Times.Once);
        }


        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException))]
        public async Task GetPagedAsyncObjectNotFoundExceptionFirstTest()
        {
            //arrange
            _categoryRepository.Setup(obj => obj.GetPagedAsync(It.IsAny<int>(), It.IsAny<int>()).Result).Returns(() => null);
            _categoryRepository.Setup(obj => obj.GetAllAsync().Result).Returns(() => new List<Category>());
            //assert
            await _categoryService.GetPagedAsync(1, 1);
            //act
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException))]
        public async Task GetPagedAsyncObjectNotFoundExceptionSecondTest()
        {
            //arrange
            _categoryRepository.Setup(obj => obj.GetPagedAsync(It.IsAny<int>(), It.IsAny<int>()).Result).Returns(() => new List<Category>());
            _categoryRepository.Setup(obj => obj.GetAllAsync().Result).Returns(() => null);
            //assert
            await _categoryService.GetPagedAsync(1, 1);
            //act
        }

        [TestMethod]
        public async Task GetPagedAsyncTest()
        {
            //arrange
            _categoryRepository.Setup(obj => obj.GetPagedAsync(It.IsAny<int>(), It.IsAny<int>()).Result).Returns(new List<Category>());
            _categoryRepository.Setup(obj => obj.GetAllAsync().Result).Returns(() => new List<Category>());

            //act
            await _categoryService.GetPagedAsync(1, 1);
            //assert
            _categoryRepository.Verify(obj => obj.GetPagedAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }


        [TestMethod]
        public async Task GetAllAsyncTest()
        {
            //arrange

            _categoryRepository.Setup(obj => obj.GetAllAsync().Result)
                .Returns(() => new List<Category>());
            //act
             await _categoryService.GetAllCategoriesAsync();
            //assert
            _categoryRepository.Verify(obj => obj.GetAllAsync(),Times.Once);
        }





        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException))]
        public async Task GetByIdAsyncObjectNotFoundExceptionTest()
        {
            //arrange
            _categoryRepository.Setup(obj => obj.GetAsync(It.IsAny<Expression<Func<Category, bool>>>()).Result).Returns(() => null);
            //act
            await _categoryService.GetByIdAsync(1);
            //assert
        }


        [TestMethod]
        public async Task GetByIdAsyncTest()
        {
            //arrange
            _categoryRepository.Setup(obj => obj.GetAsync(It.IsAny<Expression<Func<Category, bool>>>()).Result).Returns(() => _validCategory);
            //act
            await _categoryService.GetByIdAsync(1);
            //assert
            _categoryRepository.Verify(obj => obj.GetAsync(It.IsAny<Expression<Func<Category, bool>>>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException))]
        public async Task DeleteAsyncObjectNotFoundExceptionTest()
        {
            //arrange
            _categoryRepository.Setup(obj => obj.GetAsync(It.IsAny<Expression<Func<Category, bool>>>()).Result)
                .Returns(() => null);
            //act
            await _categoryService.DeleteAsync(1);
            //assert
        }


        [TestMethod]
        [ExpectedException(typeof(CategoryDeleteException))]
        public async Task DeleteAsyncCategoryDeleteExceptionTest()
        {
            //arrange
            _categoryRepository.Setup(obj => obj.GetAsync(It.IsAny<Expression<Func<Category, bool>>>()).Result)
                .Returns(new Category());
            _operationRepository
                .Setup(obj => obj.GetAllAsync(It.IsAny<Expression<Func<FinancialOperation, bool>>>()).Result)
                .Returns(new List<FinancialOperation>() { new FinancialOperation() });
            //act
            await _categoryService.DeleteAsync(1);
            //assert
        }


        [TestMethod]
        public async Task DeleteAsyncTest()
        {
            //arrange
            _categoryRepository.Setup(obj => obj.GetAsync(It.IsAny<Expression<Func<Category, bool>>>()).Result)
                .Returns(new Category());
            _operationRepository
                .Setup(obj => obj.GetAllAsync(It.IsAny<Expression<Func<FinancialOperation, bool>>>()).Result)
                .Returns(new List<FinancialOperation>());
            //act
            await _categoryService.DeleteAsync(1);
            //assert
            _categoryRepository.Verify(obj => obj.DeleteAsync(It.IsAny<Category>()), Times.Once);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task UpdateAsyncArgumentNullExceptionTest()
        {
            //arrange

            //act
            await _categoryService.UpdateAsync(null);
            //assert
        }


        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException))]
        public async Task UpdateAsyncObjectNotFoundExceptionTest()
        {
            //arrange
            UpdateCategoryRequest request = new UpdateCategoryRequest();
            _categoryRepository.Setup(obj => obj.UpdateAsync(It.IsAny<Category>()).Result)
                .Returns(() => null);
            //act
            await _categoryService.UpdateAsync(request);
            //assert
        }


        [TestMethod]
        public async Task UpdateAsyncTest()
        {
            //arrange
            UpdateCategoryRequest request = new UpdateCategoryRequest();
            _categoryRepository.Setup(obj => obj.UpdateAsync(It.IsAny<Category>()).Result)
                .Returns(_validCategory);
            //act
            await _categoryService.UpdateAsync(request);
            //assert
            _categoryRepository.Verify(obj => obj.UpdateAsync(It.IsAny<Category>()), Times.Once());
        }
    }
}