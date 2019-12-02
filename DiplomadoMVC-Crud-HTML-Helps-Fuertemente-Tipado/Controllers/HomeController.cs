﻿using DiplomadoMVC_Crud_HTML_Helps_NoTipados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiplomadoMVC_Crud_HTML_Helps_NoTipados.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection coleccion)
        {
            MantenimientoProducto mp = new MantenimientoProducto();
            Productos prod = mp.Recuperar(int.Parse(coleccion["codigo"].ToLower()));
            if (prod != null)
                return View("ModificacionProducto", prod);
            else
                return View("ProductoNoExistente");
        }

        [HttpPost]
        public ActionResult ModificacionProducto(FormCollection coleccion)
        {
            MantenimientoProducto mp = new MantenimientoProducto();
            Productos prod = new Productos
            {
                Codigo = int.Parse(coleccion["Codigo"].ToString()),
                Descripcion = coleccion["Descripcion"].ToString(),
                Precio = float.Parse(coleccion["Precio"].ToString())
            };
            mp.Modificar(prod);
            return RedirectToAction("Index");
        }

        public ActionResult Grabar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Grabar(FormCollection collection)
        {
            MantenimientoProducto mp = new MantenimientoProducto();
            Productos prod = new Productos
            {
                Descripcion = collection["Descripcion"],
                Precio = float.Parse(collection["Precio"].ToString())
            };
            mp.Agregar(prod);
            return RedirectToAction("Index");

        }
        

    }
}