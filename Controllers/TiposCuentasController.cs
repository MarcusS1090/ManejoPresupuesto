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
            var YaExisteTipoCuenta = 
                await repositorioTiposCuentas.Existe(tipoCuenta.Nombre, tipoCuenta.UsuarioId);

            await repositorioTiposCuentas.Crear(tipoCuenta);

            if (YaExisteTipoCuenta) 
            {
                ModelState.AddModelError(nameof (tipoCuenta.Nombre)
                    , $"El Tipo de cuenta {tipoCuenta.Nombre} ya existe");

                return View(tipoCuenta);

            }

            await repositorioTiposCuentas.Crear(tipoCuenta);

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> VerificarExisteTipoCuenta(string nombre)
        {
            var usuarioId = 1;
            var yaExisteTipoCuenta = await repositorioTiposCuentas.Existe(nombre, usuarioId);

            if (yaExisteTipoCuenta)
            {
                return Json($"El Tipo de cuenta {nombre} ya existe");
            }
        return Json(true);
        }
       }


}