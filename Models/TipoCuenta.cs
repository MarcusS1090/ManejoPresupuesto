using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ManejoPresupuesto.Validaciones;

namespace ManejoPresupuesto.Models
{
    public class TipoCuenta
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display (Name = "Nombre del tipo de cuenta")]
        [PrimeraLetraMayuscula]

        public string Nombre { get; set; }
        public int UsuarioId { get; set; }
        public int Orden { get; set; }

        
    }
}