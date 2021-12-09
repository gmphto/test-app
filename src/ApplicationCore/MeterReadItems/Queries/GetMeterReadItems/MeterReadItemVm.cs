using Domain.Entities;
namespace Application.MeterReadItems.Queries.GetMeterReadItems;

public class MeterReadItemVm
{
    public MeterReadItem MeterRead { get; set; }
    public AccountItem Account { get; set; }
}
