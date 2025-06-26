using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymPlanner.Infrastructure.Persistence.Entities;

[Table("BodyParts")]
public class BodyPartEntity
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; }

    public BodyPartEntity(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    public BodyPartEntity(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}