// Ignore Spelling: Crear

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ManejoPresupuesto.Models;
using ManejoPresupuesto.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Controllers
{
    public class TiposCuentasController: Controller
    {
        private readonly IRepositorioTiposCuentas repositorioTiposCuentas;

        public TiposCuentasController(IRepositorioTiposCuentas repositorioTiposCuentas) 
        { 
            this.repositorioTiposCuentas = repositorioTiposCuentas;
        }

        [HttpGet]
        public IActionResult Crear() 
        {

            return View();
        }


        [HttpPost]
        public async Task <IActionResult> Crear(TipoCuenta tipoCuenta) 
        {

            if (!ModelState.IsValid) {

                return View(tipoCuenta);
            }

            tipoCuenta.UsuarioId = 1;   
            await repositorioTiposCuentas.Crear(tipoCuenta);



            return View();
        }
    }


}