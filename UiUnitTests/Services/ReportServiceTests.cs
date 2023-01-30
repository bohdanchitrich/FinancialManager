using Moq;
using UI.Models.Reports.DailyReports;
using UI.Models.Reports.DateReports;
using UI.Services;
using UI.Services.Report;

namespace UiUnitTests.Services;
[TestClass]
public class ReportServiceTests
{
    private Mock<IFinancialManagerHttpClient>? _httpClientMock;
    private IReportService? _reportService;

    [TestInitialize]
    public void Initialize()
    {
        _httpClientMock = new Mock<IFinancialManagerHttpClient>().SetupAllProperties();
        _reportService = new ReportService(_httpClientMock.Object);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _httpClientMock = null;
        _reportService = null;
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public async Task GetDailyReportAsyncArgumentNullExceptionTest()
    {
        //arrange

        //act
        await _reportService!.GetDailyReportAsync(null);
        //assert
    }

    [TestMethod]
    public async Task GetDailyReportAsyncTest()
    {
        //arrange

        //act
        await _reportService!.GetDailyReportAsync(new DailyReportGetModel());
        //assert
        _httpClientMock!.Verify(obj => obj.PostAsync(It.IsAny<Uri>(),It.IsAny<HttpContent>()));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public async Task GetDateReportAsyncArgumentNullExceptionTest()
    {
        //arrange

        //act
        await _reportService!.GetDateReportAsync(null!);
        //assert
    }


    [TestMethod]
    public async Task GetDateReportAsyncTest()
    {
        //arrange

        //act
        await _reportService!.GetDateReportAsync(new DateReportGetModel());
        //assert
        _httpClientMock!.Verify(obj => obj.PostAsync(It.IsAny<Uri>(),It.IsAny<HttpContent>()));
    }
}