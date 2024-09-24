using Data.Common;

namespace Data.Entities;

public class Producer : EntityBase
{
    public ICollection<Series>? Series { get; set; }
}