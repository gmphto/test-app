using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities;
[Table("MeterReadItems")]
public class MeterReadItem : AuditableEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public DateTime? Date { get; set; }
    public string? Value { get; set; }
    public AccountItem? Account { get; set; }
}