using IMASD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMASD.Controllers
{
    public class NominaController : Controller
    {
        // GET: Nomina
        public ActionResult Index()
        {
            using (var context = new BDNOMINA2020Entities())
            {

                // Return the list of data from the database 
                var data = context.nominas.ToList();
                return View(data);
            }
        }

        // GET: Nomina/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nomina/Create
        [HttpPost]
        public ActionResult Create(nomina collection)
        {
            try
            {
                // To open a connection to the database 
                using (var context = new BDNOMINA2020Entities())
                {
                    // Add data to the particular table 
                    collection.status = "Abierto";
                    context.nominas.Add(collection);

                    // save the changes 
                    context.SaveChanges();
                }
                string message = "Created the record successfully";

                // To display the message on the screen 
                // after the record is created successfully 
                ViewBag.Message = message;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Nomina/Edit/5
        public ActionResult Edit(int id)
        {
            using (var context = new BDNOMINA2020Entities())
            {
                var data = context.nominas.Where(x => x.nominaId == id).SingleOrDefault();
                return View(data);
            }
        }

        // POST: Nomina/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, nomina collection)
        {
            try
            {
                // To open a connection to the database 
                using (var context = new BDNOMINA2020Entities())
                {
                    // particular record from a database 
                    var data = context.nominas.FirstOrDefault(x => x.nominaId == id);
                    if (data != null)
                    {
                        // save the changes 
                        data.descripcion = collection.descripcion;
                        data.inicio_n = collection.inicio_n;
                        data.fin_n = collection.fin_n;
                        context.SaveChanges();
                    }

                }
                string message = "Updated the record successfully";

                // To display the message on the screen 
                // after the record is created successfully 
                ViewBag.Message = message;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Nomina/Delete/5
        public ActionResult Delete(int id)
        {
            using (var context = new BDNOMINA2020Entities())
            {
                var data = context.nominas.Where(x => x.nominaId == id).SingleOrDefault();
                return View(data);
            }
        }

        // POST: Nomina/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, nomina collection)
        {
            try
            {
                // To open a connection to the database 
                using (var context = new BDNOMINA2020Entities())
                {
                    // particular record from a database 
                    var data = context.nominas.FirstOrDefault(x => x.nominaId == id);
                    if (data != null)
                    {
                        // save the changes 
                        context.nominas.Remove(data);
                        context.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    else
                        return View();

                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Nomina/Pago/5
        public ActionResult Pago(int id)
        {
            using (var context = new BDNOMINA2020Entities())
            {
                //var empleados = context.tabuladors.ToList(); 
                TabuladoEmpleado tabEmpleado = new TabuladoEmpleado();
                var empleados = tabEmpleado.GetTabEmpleados(id);
                var nominas = context.nominas.Where(x => x.nominaId == id).SingleOrDefault();
                ViewData["nominaId"] = nominas.nominaId;
                ViewData["descripcion"] = nominas.descripcion;
                ViewData["inicio"] = nominas.inicio_n;
                ViewData["fin"] = nominas.fin_n;
                var data = context.departamentoes.ToList();
                var _departamentosList = new List<SelectListItem>();
                foreach (var item in data)
                {
                    _departamentosList.Add(new SelectListItem
                    {
                        Text = item.nombre,
                        Value = item.departamentoId.ToString()
                    });
                }
                ViewBag.departamentoId = _departamentosList;
                return View(empleados);
            }
        }

        // GET: Nomina/Pago/5
        public ActionResult TableEmpleados(int id, int iddep)
        {
            using (var context = new BDNOMINA2020Entities())
            {
                //var empleados = context.tabuladors.ToList(); 
                TabuladoEmpleado tabEmpleado = new TabuladoEmpleado();
                var empleados = tabEmpleado.GetTabEmpleados(id);
                if (iddep > 0)
                {
                    empleados = empleados.Where(x => x.departamentoId == iddep).ToList();
                }
                return PartialView("TableEmpleados", empleados);
            }
        }

        // POST: Nomina/Delete/5
        [HttpPost]
        public ActionResult Pago(int id, nominaEmpleado collection)
        {
            try
            {
                using (var context = new BDNOMINA2020Entities())
                {
                    // particular record from a database 
                    var data = context.nominaEmpleadoes.FirstOrDefault(x => x.nominaEmpleadoId == collection.nominaEmpleadoId);
                    if (data != null)
                    {
                        // save the changes 
                        data.nominaId = collection.nominaId;
                        data.numEmpleado = collection.numEmpleado;
                        data.diasTrabajado = collection.diasTrabajado;
                        data.sueldo = collection.sueldo;
                        data.vales = collection.vales;
                        data.status = collection.status;
                        context.SaveChanges();
                    }
                    else
                    {
                        collection.status = "En proceso";
                        collection.create_at = DateTime.Now;
                        context.nominaEmpleadoes.Add(collection);
                        context.SaveChanges();
                    }

                    var pagados = context.nominaEmpleadoes.Count(x => x.nominaId == collection.nominaId && x.status == "Pagado");
                    var pagos = context.nominaEmpleadoes.Count(x => x.nominaId == collection.nominaId);
                    var nomina = context.nominas.FirstOrDefault(x => x.nominaId == collection.nominaId);

                    if (pagados == pagos)
                    {
                        nomina.status = "Completada";
                        context.SaveChanges();
                    }
                    collection.nomina = null;

                }
                string message = "Cambios guardados exitosamente";

                // To display the message on the screen 
                // after the record is created successfully 
                ViewBag.Message = message;

                return Json(collection);
            }
            catch
            {
                return View();
            }
        }
    }
}
