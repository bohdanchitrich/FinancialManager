using API.Controllers;
using API.DTOs.Reports;
using API.Services.Reports;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace ApiUnitTests.Controllers
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ReportControllerTests
    {
        private Mock<IReportService> _reportService;
        private ReportController _reportController;


        [TestInitialize]
        public void Initialize()
        {
            _reportService = new Mock<IReportService>();
            _reportController = new ReportController(_reportService.Object);
        }


        [TestCleanup]
        public void Cleanup()
        {
            _reportService = null;
            _reportController = null;
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReportControllerConstructorThrowArgumentNullException()
        {
            //arrange

            //act
            var result = new ReportController(null);
            //assert
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task DailyReportAsyncArgumentNullExceptionTest()
        {
            //arrange
            //act
            await _reportController.DailyReportAsync(null);
            //assert
        }


        [TestMethod]
        public async Task DailyReportAsyncTest()
        {
            //arrange
            _reportService.Setup(obj => obj.GetDailyReportAsync(It.IsAny<GetDailyReportRequest>()));
            //act
            await _reportController.DailyReportAsync(Mock.Of<GetDailyReportRequest>());
            //assert
            _reportService.Verify(obj => obj.GetDailyReportAsync(It.IsAny<GetDailyReportRequest>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task DateReportAsyncArgumentNullExceptionTest()
        {
            //arrange
            //act
            await _reportController.DateReportAsync(null);
            //assert
        }

        [TestMethod]
        public async Task DateReportAsyncTest()
        {
            //arrange
            _reportService.Setup(obj => obj.GetDateReportAsync(It.IsAny<GetDateReportRequest>()));
            //act
            await _reportController.DateReportAsync(Mock.Of<GetDateReportRequest>());
            //assert
            _reportService.Verify(obj => obj.GetDateReportAsync(It.IsAny<GetDateReportRequest>()), Times.Once);
        }

    }
}
