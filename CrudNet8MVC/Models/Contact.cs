using System.ComponentModel.DataAnnotations;

namespace CrudNet8MVC.Models
{
    public class Contact
    {
        [Key]// siendo explicitos con que Id es la llave
        public int Id { get; set; }

        [Required (ErrorMessage = "Name is required")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ]+(?:\s[A-Za-zÁÉÍÓÚáéíóúÑñ]+)*$", ErrorMessage = "El nombre solo puede contener letras.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        [Display(Name ="Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "HomePhone number is required")]
        [RegularExpression(@"^\d{7,10}$", ErrorMessage = "El número debe tener entre 7 y 10 dígitos (con fijo de su zona).")]
        [Display(Name = "Telefono")]
        public string HomePhone { get; set; }

        [Required(ErrorMessage = "CellPhone number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El número debe tener exactamente 10 dígitos.")]
        [Display(Name = "Celular")]
        public string CellPhone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
        [Display(Name = "Correo")]
        public string Email { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }

    }
}
