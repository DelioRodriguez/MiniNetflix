using Data.Common;

namespace Data.Entities;

public class Genre : EntityBase
{
    public  ICollection<Series?> PrimarySeries { get; set; } = null!;
    public ICollection<Series?> SecondarySeries { get; set; } = null!;
}