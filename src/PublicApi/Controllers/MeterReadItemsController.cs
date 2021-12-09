using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using System.Data;
using Application.Common.Interfaces;
using Application.MeterReadItems.Commands.CreateMeterReadItems;
using Application.MeterReadItems.Queries.GetMeterReadItems;

namespace PublicApi.Controllers;

public class MeterReadItemsController : ApiControllerBase
{

    private readonly IExcelReader _excelReader;

    public MeterReadItemsController(IExcelReader excelReader)
    {
        // NOTE: Could be moved
        _excelReader = excelReader;
    }

    [HttpGet("/meter-reading")]
    public async Task<List<MeterReadItem>> Get()
    {
        return await Mediator.Send(new GetMeterReadItemsQuery());
    }

    [HttpPost("/meter-reading-uploads")]
    public async Task<MeterReadItemsDto> UploadMeterReadItems(IFormFile file)
    {

        var items = _excelReader.ReadExcelDocument(file.OpenReadStream()).AsEnumerable().Select(row => new MeterReadItem
        {
            Id = Convert.ToInt32(row.Field<double>("AccountId")),
            Date = row.Field<DateTime>("MeterReadingDateTime"),
            Value = row["MeterReadValue"].ToString()
        }).ToList(); ;

        return await Mediator.Send(new CreateMeterReadItemsCommand { Items = items });
    }
}