using System.ComponentModel.DataAnnotations;

namespace Application.ViewModel;

public class GenreViewModel
{
    public int? Id { get; set; }
    
    [Required(ErrorMessage = "Debes de asignarle un nombre. es obligatorio")]
    public string? Name { get; set; }
}