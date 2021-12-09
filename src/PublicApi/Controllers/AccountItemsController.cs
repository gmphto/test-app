using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application.AccountItems.Queries.GetAccountItems;

namespace PublicApi.Controllers;

public class AccountItemsController : ApiControllerBase
{
    [HttpGet("/accounts")]
    public async Task<List<AccountItem>> Get([FromQuery] GetAccountItemsQuery query)
    {
        return await Mediator.Send(query);
    }
}