using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.MeterReadItems.Queries.GetMeterReadItems;

public class GetMeterReadItemsQuery : IRequest<List<MeterReadItem>>
{
}

public class GetMeterReadItemsQueryHandler : IRequestHandler<GetMeterReadItemsQuery, List<MeterReadItem>>
{

    private readonly IApplicationDbContext _context;

    public GetMeterReadItemsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<MeterReadItem>> Handle(GetMeterReadItemsQuery request, CancellationToken cancellationToken)
    {
        return _context.MeterReadItems.ToList();
    }
}