using Microsoft.AspNetCore.Mvc;
using Application.ReadingItems.Queries.GetHelloWorld;
using Application.ReadingItems.Commands.UploadReadings;
using Domain.Entities;
using System.Data;

namespace PublicApi.Controllers;

public class ReadingItemsController : ApiControllerBase
{
    [HttpGet("/")]
    public async Task<string> Get([FromQuery] GetHelloWorldQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost("/meter-reading-uploads")]
    public async Task<List<ReadingItemExcel>> UploadMeterReadings([FromForm] UploadReadingsCommand command)
    {
        return await Mediator.Send(command);
    }
}