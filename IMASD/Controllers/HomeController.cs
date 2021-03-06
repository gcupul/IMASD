﻿using IMASD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMASD.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new BDNOMINA2020Entities())
            {

                // Return the list of data from the database 
                ListEmpleados list = new ListEmpleados();
                var empleados = list.getListEmpleados();
                ViewBag.countNominas = context.nominas.Count();
                ViewBag.countEmpleados = context.empleadoes.Count();
                ViewBag.countNominasActivas = context.nominas.Count(x => x.status == "Abierto");
                ViewBag.countNominasCompletas = context.nominas.Count(x => x.status == "Completada");
                return View(empleados);
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(user objUser)
        {
            if (ModelState.IsValid)
            {
                using (BDNOMINA2020Entities db = new BDNOMINA2020Entities())
                {
                    var obj = db.users.Where(a => a.userName.Equals(objUser.userName) && a.pass.Equals(objUser.pass)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["userId"] = obj.userId.ToString();
                        Session["userName"] = obj.userName.ToString();
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(objUser);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}