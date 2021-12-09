using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
[Table("AccountItems")]
public class AccountItem : AuditableEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public ICollection<MeterReadItem>? MeterReadItems { get; set; }
}
