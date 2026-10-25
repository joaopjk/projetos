using System.ComponentModel.DataAnnotations.Schema;

namespace MyRecipeBook.Domain.Entities;

public abstract class BaseEntity
{
    public long Id { get; set; }
    public bool Active { get; set; } = true;
    [Column("created_on")]
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}