using API.DTOs.Reports;
using Domain.FinancialOperations;
using Domain.Interfaces;

namespace API.Services.Reports;

public class ReportService : IReportService
{

    private readonly IAsyncRepository<FinancialOperation> _repository;

    public ReportService(IAsyncRepository<FinancialOperation> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<GetDailyReportResponse> GetDailyReportAsync(GetDailyReportRequest dailyReportRequest)
    {
        ArgumentNullException.ThrowIfNull(dailyReportRequest);
        var operation = await _repository.GetAllAsync(obj =>
            obj.Date.Date == dailyReportRequest.DateTime.Date);



        GetDailyReportResponse dailyReportResponse = new GetDailyReportResponse
        {
            DateOnly = DateOnly.FromDateTime(dailyReportRequest.DateTime).ToShortDateString()
        };
        operation.ForEach(obj => dailyReportResponse.Operations.Add(obj));
        return dailyReportResponse;
    }

    public async Task<GetDateReportResponse> GetDateReportAsync(GetDateReportRequest dateReportRequest)
    {
        ArgumentNullException.ThrowIfNull(dateReportRequest);
        var operations = await _repository.GetAllAsync(obj =>
            obj.Date.Date >= dateReportRequest.StartDateTime.Date &&
            obj.Date.Date <= dateReportRequest.EndDateTime.Date);
        GetDateReportResponse dateReportResponse = new GetDateReportResponse
        {
            StartDate = DateOnly.FromDateTime(dateReportRequest.StartDateTime).ToShortDateString(),
            EndDate = DateOnly.FromDateTime(dateReportRequest.EndDateTime).ToShortDateString()
        };
        operations.ForEach(obj => dateReportResponse.Operations.Add(obj));
        return dateReportResponse;
    }



}