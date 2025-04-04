using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuesto.Models
{
    public class OlvideMiPasswordViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo {0} no es un correo electrónico válido.")]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }
    }
}
