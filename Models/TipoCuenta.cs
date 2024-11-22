using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManejoPresupuesto.Models
{
    public class TipoCuenta
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50, MinimumLength = 3,
        ErrorMessage = "La longitud del campo {0} debe ser minimo de {2} y maximo de {1}")]
        [Display (Name = "Nombre del tipo de cuenta")]
        public string Nombre { get; set; }
        public int UsuarioId { get; set; }
        public int Orden { get; set; }

        /*Creando mas Validaciones y modelos*/
        [Required(ErrorMessage ="El campo {0} es requerido")]
        [EmailAddress(ErrorMessage ="El correo electronico debe ser valido")]
        public string Email { get; set; }

        [Range(minimum:18, maximum:130, ErrorMessage ="la edad debe ser entre {1} y {2} ")]
        public int Edad { get; set; }

        [Url(ErrorMessage ="La URL debe ser valida")]
        public string Url { get; set; }

        [CreditCard(ErrorMessage ="La tarjeta de credito debe no es valida")]
        public int TarjetaDeCredito { get; set; }
    }
}