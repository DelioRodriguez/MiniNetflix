using System.ComponentModel.DataAnnotations;

namespace Application.ViewModel;

public class ProducerViewModel
{
    public int? Id { get; set; }
    
    [Required(ErrorMessage = "Ingrese Nombre de la productora, es obligatorio")]
    public string? Name{ get; set; }
}