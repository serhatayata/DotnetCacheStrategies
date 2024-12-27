using System.ComponentModel.DataAnnotations;

namespace DotnetCacheStrategies.CacheAside.Entities;

public record Person
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
}
