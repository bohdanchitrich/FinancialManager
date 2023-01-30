using API.Controllers;
using API.DTOs.FinancialOperations;
using API.Services.FinancialOperations;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace ApiUnitTests.Controllers
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class FinancialOperationControllerTests
    {
        private Mock<IFinancialOperationService> _financialOperationService;
        private FinancialOperationController _controller;


        [TestInitialize]
        public void Initialize()
        {
            _financialOperationService = new Mock<IFinancialOperationService>();
            _controller = new FinancialOperationController(_financialOperationService.Object);
        }


        [TestCleanup]
        public void Cleanup()
        {
            _financialOperationService = null;
            _controller = null;
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FinancialOperationConstructorThrowArgumentNullException()
        {
            //arrange

            //assert
            var result = new FinancialOperationController(null);
            //act
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
            AddFinancialOperationRequest addFinancialOperationRequest = new AddFinancialOperationRequest();
            _financialOperationService.Setup(obj => obj.AddNewAsync(It.IsAny<AddFinancialOperationRequest>()));
            //act
            await _controller.AddAsync(addFinancialOperationRequest);
            //assert
            _financialOperationService.Verify(obj => obj.AddNewAsync(It.IsAny<AddFinancialOperationRequest>()), Times.Once);
        }


        [TestMethod]
        public async Task GetAsyncTest()
        {
            //arrange
            _financialOperationService.Setup(obj => obj.GetByIdAsync(It.IsAny<int>()));
            //act
            await _controller.GetAsync(1);
            //assert
            _financialOperationService.Verify(obj => obj.GetByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public async Task GetPagedAsyncTest()
        {
            //arrange
            _financialOperationService.Setup(obj => obj.GetPagedAsync(It.IsAny<int>(), It.IsAny<int>()));
            //act
            await _controller.GetPagedAsync(1, 1);
            //assert
            _financialOperationService.Verify(obj => obj.GetPagedAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsyncTest()
        {
            //arrange
            _financialOperationService.Setup(obj => obj.DeleteAsync(It.IsAny<int>()));
            //act
            await _controller.DeleteAsync(1);
            //assert
            _financialOperationService.Verify(obj => obj.DeleteAsync(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task UpdateArgumentNullException()
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
            UpdateFinancialOperationRequest financialOperationRequest = new UpdateFinancialOperationRequest();
            _financialOperationService.Setup(obj => obj.UpdateAsync(It.IsAny<UpdateFinancialOperationRequest>()));
            //act
            await _controller.UpdateAsync(financialOperationRequest);
            //assert
            _financialOperationService.Verify(obj => obj.UpdateAsync(It.IsAny<UpdateFinancialOperationRequest>()), Times.Once);
        }
    }
}
