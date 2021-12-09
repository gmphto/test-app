using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Data;

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using System.Linq;
using Application.Common.Interfaces;

namespace Application.ReadingItems.Commands.UploadReadings;

public class UploadReadingsCommand : IRequest<List<ReadingItemExcel>>
{
    public IFormFile File { get; set; }

}

public class CreateReadingItemCommandHandler : IRequestHandler<UploadReadingsCommand, List<ReadingItemExcel>>
{

    private readonly IExcelReader _excelReader;

    public CreateReadingItemCommandHandler(IExcelReader excelReader)
    {
        _excelReader = excelReader;
    }
    public Task<List<ReadingItemExcel>> Handle(UploadReadingsCommand request, CancellationToken cancellationToken)
    {
        if (request.File == null)   throw new ArgumentNullException(nameof(request.File));

        var dt = _excelReader.ReadExcelDocument(request.File.OpenReadStream());

        var item = dt.AsEnumerable().Select(row => new ReadingItemExcel
        {
            Id = row.Field<string>("AccountId"),
            Value = row.Field<string>("MeterReadValue"),
            Date = row.Field<string>("MeterReadingDateTime"),
        }).ToList();

        return Task.FromResult(item);
    }
}
