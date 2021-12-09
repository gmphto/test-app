
namespace Domain.Entities;

public class ReadingItem : AuditableEntity
{
    public int ReadingId { get; set; }
    public DateTime? Date { get; set; }
    public int Value { get; set; }
    public AccountItem? Account { get; set; }
}

public class ReadingItemExcel
{
    public string? Id { get; set; }
    public string? Date { get; set; }
    public string? Value { get; set; }
}