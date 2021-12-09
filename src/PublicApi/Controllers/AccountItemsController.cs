using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application.AccountItems.Queries.GetAccountItems;

namespace PublicApi.Controllers;

public class AccountItemsController : ApiControllerBase
{
    [HttpGet("/accounts")]
    public async Task<List<AccountItem>> Get([FromQuery] GetAllAccountItemsQuery query)
    {
        return await Mediator.Send(query);
    }
}