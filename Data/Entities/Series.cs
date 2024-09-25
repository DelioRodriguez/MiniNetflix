using Data.Common;

namespace Data.Entities;

public class Series : EntityBase
{
    public required string? VideoLink { get; set; }
    public required string? ImgLink { get; set; } 
    public int ProducerId { get; set; }
    public Producer Producer { get; set; } = null!;

    public int PrimaryGenreId { get; set; }
    public Genre PrimaryGenre { get; set; } = null!;

    public int? SecondaryGenreId { get; set; }
    public Genre? SecondaryGenre { get; set; }
    
    
    
}