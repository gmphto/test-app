using Microsoft.AspNetCore.Mvc;
using Application.ReadingItems.Queries.GetHelloWorld;

namespace PublicApi.Controllers;

public class ReadingItemsController : ApiControllerBase
{
    [HttpGet("/")]
    public async Task<string> Get([FromQuery] GetHelloWorldQuery query)
    {
        return await Mediator.Send(query);
    }
}