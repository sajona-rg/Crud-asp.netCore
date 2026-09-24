using System.ComponentModel.DataAnnotations;

namespace CrudNet8MVC.Models
{
    public class Libro
    {
        [Key] // siendo explicitos con que Id es la llave
        public int Id { get; set; }

        [Required(ErrorMessage = "El ISBN es obligatorio.")]
        [RegularExpression(@"^(?:\d{10}|\d{13})$", ErrorMessage = "El ISBN debe tener 10 o 13 dígitos.")]
        [Display(Name = "ISBN")]
        public string Isbn { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(200, ErrorMessage = "El título no puede tener más de 200 caracteres.")]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El autor es obligatorio.")]
        [StringLength(100, ErrorMessage = "El autor no puede tener más de 100 caracteres.")]
        [Display(Name = "Autor")]
        public string Autor { get; set; }

        [Required(ErrorMessage = "La editorial es obligatoria.")]
        [StringLength(100, ErrorMessage = "La editorial no puede tener más de 100 caracteres.")]
        [Display(Name = "Editorial")]
        public string Editorial { get; set; }

        [Required(ErrorMessage = "El género es obligatorio.")]
        [StringLength(50, ErrorMessage = "El género no puede tener más de 50 caracteres.")]
        [Display(Name = "Género")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "El año es obligatorio.")]
        [Range(1450, 2100, ErrorMessage = "El año debe estar entre 1450 y 2100.")]
        [Display(Name = "Año")]
        public int Anio { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad no puede ser negativa.")]
        [Display(Name = "Cantidad")]
        public int Cantidad { get; set; }
    }
}