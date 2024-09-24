using Data.Common;

namespace Data.Entities;

public class Genre : EntityBase
{
    public  ICollection<Series> PrimarySeries { get; set; }
    public ICollection<Series> SecondarySeries { get; set; }
}