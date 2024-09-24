using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Application.ViewModel
{
    public class SeriesViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "El nombre de la serie es obligatorio")]
        public string? Name { get; set; }
        
        [Required(ErrorMessage = "El enlace del video es obligatorio")]
        [Url(ErrorMessage = "Debe de ser un URL Valido")]
        public string? VideoLink { get; set; }
       
        
        [Required(ErrorMessage = "El enlace de la portada es obligatorio")]
        [Url(ErrorMessage = "Debe de ser un URL Valido")]
        public string? ImgLink { get; set; }
        
        [Required(ErrorMessage = "Debe de seleccionar una poductora")]
        public int ProducerId { get; set; }
        public List<SelectListItem>? Producers { get; set; }
        public string? ProducerName { get; set; }

        [Required(ErrorMessage = "Debe de seleccionar un genero primario")]
        public int PrimaryGenreId { get; set; }
        public List<SelectListItem>? PrimaryGenres { get; set; }
        public string? PrimaryGenreName { get; set; }
        public int? SecondaryGenreId { get; set; }
        public List<SelectListItem>? SecondaryGenres { get; set; }
        public string? SecondaryGenreName { get; set; }


    }
}
