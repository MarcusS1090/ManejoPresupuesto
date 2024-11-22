using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManejoPresupuesto.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManejoPresupuesto.Controllers
{
    public class TiposCuentasController: Controller
    {
        [HttpGet]
        public IActionResult Crear() {
            return View();
        }
        [HttpPost]
        public IActionResult Crear(TipoCuenta tipoCuenta) {
            if (!ModelState.IsValid) {

                return View(tipoCuenta);
            }
            return View();
        }
    }
}