using Microsoft.AspNetCore.Mvc;
using Application.ReadingItems.Queries.GetHelloWorld;
using Domain.Entities;
using System.Data;
using Application.Common.Interfaces;
using Application.MeterReadItems.Commands.CreateMeterReadItems;

namespace PublicApi.Controllers;

public class MeterReadItemsController : ApiControllerBase
{

    private readonly IExcelReader _excelReader;

    public MeterReadItemsController(IExcelReader excelReader)
    {
        _excelReader = excelReader;
    }

    [HttpGet("/")]
    public async Task<string> Get([FromQuery] GetHelloWorldQuery query)
    {
        return await Mediator.Send(query);
    }

    //[HttpPost("/meter-reading-uploads")]
    //public async Task<List<MeterReadItem>> UploadMeterReadings([FromForm] CreateMeterReadItemCommand command)
    //{
    //    return await Mediator.Send(command);
    //}

    [HttpPost("/meter-reading-upload")]
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