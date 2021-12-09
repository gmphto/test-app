using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class AccountItem : AuditableEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int AccountId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public ICollection<ReadingItem>? ReadingItems { get; set; }
}
