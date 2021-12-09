using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.AccountItems.Queries.GetAccountItems;

public class GetAllAccountItemsQuery : IRequest<List<AccountItem>>
{
}


public class GetAllAccountItemsQueryHandler : IRequestHandler<GetAllAccountItemsQuery, List<AccountItem>>
{

    private readonly IApplicationDbContext _context;

    public GetAllAccountItemsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<AccountItem>> Handle(GetAllAccountItemsQuery request, CancellationToken cancellationToken)
    {
        return await _context.AccountItems
             .ToListAsync(cancellationToken);
    }
}