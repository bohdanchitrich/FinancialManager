using Domain.Categories;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;

using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace InfrastructureUnitTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CategoryRepositoryTests
    {


        private ApplicationDbContext _context;
        private CategoryRepository _repository;


        [TestInitialize]
        public void Initialize()
        {
            var _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Base")
                .Options;
            _context = new ApplicationDbContext(_dbContextOptions);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            _context.Categories.AddRange(
                new Category()
                {
                    Name = "Category1Name",
                    Description = "Category1Desc"
                },
                new Category()
                {
                    Name = "Category2Name",
                    Description = "Category2Desc"
                }
            );
            _context.SaveChanges();

            _repository = new CategoryRepository(_context);
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
            Category category = new Category()
            {
                Name = "NameTest",
                Description = "DescTest"
            };
            var expected = _context.Categories.Count() + 1;
            //act
            await _repository.AddAsync(category);
            var actually = _context.Categories.Count();
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
            var categoryToDelete = _context.Categories.First();
            //act
            await _repository.DeleteAsync(categoryToDelete);
            var actually = _context.Categories.First();
            //assert
            Assert.AreNotEqual(actually, categoryToDelete);
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
            var expected = await _context.Categories.ToListAsync();
            //act
            var actually = await _repository.GetAllAsync(obj => obj.Id > 0);
            //assert
            CollectionAssert.AreEqual(expected, actually);
        }

        [TestMethod]
        public async Task GetAllAsyncWithoutParametersTest()
        {
            //arrange
            var expected = await _context.Categories.ToListAsync();
            //act
            var actually = await _repository.GetAllAsync();
            //assert
            CollectionAssert.AreEqual(expected, actually);
        }



        [TestMethod]
        public async Task GetPagedAsyncTest()
        {
            //arrange
            var expected = await _context.Categories.FirstAsync();
            //act
            var actual = await _repository.GetPagedAsync(1, 1);
            //assert
            Assert.IsTrue(actual.Count == 1);
            Assert.AreEqual(expected, actual.First());
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetAsyncExceptionTest()
        {
            //arrange

            //act
            await _repository.GetAsync(null);
            //assert
        }


        [TestMethod]
        public async Task GetAsyncTest()
        {
            //arrange
            var expected = _context.Categories.First();
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
        public async Task UpdateAsyncTest()
        {
            //arrange
            string expectedName = _context.Categories.First().Name;
            var categoryExpected = _context.Categories.First();
            categoryExpected.Name = "Update";
            //act
            await _repository.UpdateAsync(categoryExpected);
            var actually = _context.Categories.First();
            //assert
            Assert.AreNotEqual(expectedName, actually.Name);
        }
    }
}