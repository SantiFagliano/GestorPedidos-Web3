﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Models.Models;
using Service;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace GestorPedidos.Controllers
{
    public class ArticuloController : Controller
    {
        private IArticuloServicio articuloServicio;
        private _20211CTPContext dbContext;

        public ArticuloController(_20211CTPContext ctx)
        {
            dbContext = ctx;
            articuloServicio = new ArticuloServicio(dbContext);
        }

        [HttpGet]
        public IActionResult Articulos()
        {
            string idUsuario = HttpContext.Session.GetString("IdUsuario") != null ? HttpContext.Session.GetString("IdUsuario") : null;
            string admin = HttpContext.Session.GetString("usuarioAdmin") != null ? HttpContext.Session.GetString("usuarioAdmin") : null;
            if (idUsuario == null)
            {
                TempData["Error"] = "Por favor inicie Sesion para poder ingresar a esta seccion";
                return RedirectToAction("Login", "Login");
            }
            if (admin != null && admin != "TRUE")
            {
                TempData["warning"] = "Usted no se encuentra habilitado para ingresar en esta seccion";
                return RedirectToAction("Index", "Home");
            }

            List<Articulo> articulos = articuloServicio.ListarTodos();

            return View(articulos);
        }


        [HttpGet]
        public IActionResult NuevoArticulo()
        {
            string idUsuario = HttpContext.Session.GetString("IdUsuario") != null ? HttpContext.Session.GetString("IdUsuario") : null;
            string admin = HttpContext.Session.GetString("usuarioAdmin") != null ? HttpContext.Session.GetString("usuarioAdmin") : null;
            if (idUsuario == null)
            {
                TempData["Error"] = "Por favor inicie Sesion para poder ingresar a esta seccion";
                return RedirectToAction("Login", "Login");
            }
            if (admin != null && admin != "TRUE")
            {
                TempData["warning"] = "Usted no se encuentra habilitado para ingresar en esta seccion";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult NuevoArticulo(Articulo articulo, string guardar)
        {
            if (!ModelState.IsValid)
            {
                return View(articulo);
            }
            int idUsuario = (int)HttpContext.Session.GetInt32("IdUser");
            articuloServicio.Crear(articulo, idUsuario);

            TempData["art"] = JsonConvert.SerializeObject(articulo);

            if (guardar != null && guardar.ToLower().Equals("guardar"))
            {
                return RedirectToAction(nameof(Articulos));
            }

            return RedirectToAction(nameof(NuevoArticulo));
        }

        [HttpGet]
        public IActionResult EditarArticulo(int IdArticulo)
        {
            string idUsuario = HttpContext.Session.GetString("IdUsuario") != null ? HttpContext.Session.GetString("IdUsuario") : null;
            string admin = HttpContext.Session.GetString("usuarioAdmin") != null ? HttpContext.Session.GetString("usuarioAdmin") : null;
            if (idUsuario == null)
            {
                TempData["Error"] = "Por favor inicie Sesion para poder ingresar a esta seccion";
                return RedirectToAction("Login", "Login");
            }
            if (admin != null && admin != "TRUE")
            {
                TempData["warning"] = "Usted no se encuentra habilitado para ingresar en esta seccion";
                return RedirectToAction("Index", "Home");
            }
            Articulo articulo = articuloServicio.ObtenerPorId(IdArticulo);

            return View(articulo);
        }

        [HttpPost]
        public IActionResult EditarArticulo(Articulo articulo)
        {

            if (!ModelState.IsValid)
            {
                return View(articulo);
            }
            int idUsuario = (int)HttpContext.Session.GetInt32("IdUser");
            articuloServicio.Modificar(articulo,idUsuario);

            return RedirectToAction(nameof(Articulos));
        }


        [HttpGet]
        public IActionResult EliminarArticulo(int IdArticulo)
        {
            int idUsuario = (int)HttpContext.Session.GetInt32("IdUser");
            articuloServicio.BorrarPorId(IdArticulo,idUsuario);

            return RedirectToAction(nameof(Articulos));
        }


    }
}
