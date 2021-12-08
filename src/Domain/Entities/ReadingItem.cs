namespace Domain.Entities;
public class ReadingItem : AuditableEntity
{
    public int Id { get; set; }
    public DateTime? ReadingDate { get; set; }
    public int ReadingValue { get; set; }
}

public class ReadingItemExcel
{
    public string? Id { get; set; }
    public string? ReadingDate { get; set; }
    public string? ReadingValue { get; set; }
}