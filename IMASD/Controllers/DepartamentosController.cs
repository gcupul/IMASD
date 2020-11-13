using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMASD.Controllers
{
    public class DepartamentosController : Controller
    {
        // GET: Departamentos
        public ActionResult Index()
        {
            using (var context = new BDNOMINA2020Entities())
            {

                // Return the list of data from the database 
                var data = context.departamentoes.ToList();
                return View(data);
            }
        }

        // GET: Departamentos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Departamentos/Create
        [HttpPost]
        public ActionResult Create(departamento objDepartamento)
        {
            try
            {
                // To open a connection to the database 
                using (var context = new BDNOMINA2020Entities())
                {
                    // Add data to the particular table 
                    objDepartamento.create_at = DateTime.Now;
                    objDepartamento.update_at = DateTime.Now;
                    context.departamentoes.Add(objDepartamento);

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

        // GET: Departamentos/Edit/5
        public ActionResult Edit(int id)
        {
            using (var context = new BDNOMINA2020Entities())
            {
                var data = context.departamentoes.Where(x => x.departamentoId == id).SingleOrDefault();
                return View(data);
            }
        }

        // POST: Departamentos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, departamento objDepartamento)
        {
            try
            {
                // To open a connection to the database 
                using (var context = new BDNOMINA2020Entities())
                {
                    // particular record from a database 
                    var data = context.departamentoes.FirstOrDefault(x => x.departamentoId == id);
                    if (data != null)
                    {
                        // save the changes 
                        data.nombre = objDepartamento.nombre;
                        data.descripcion = objDepartamento.descripcion;
                        data.update_at = DateTime.Now;
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

        // GET: Departamentos/Delete/5
        public ActionResult Delete(int id)
        {
            using (var context = new BDNOMINA2020Entities())
            {
                var data = context.departamentoes.Where(x => x.departamentoId == id).SingleOrDefault();
                return View(data);
            }
        }

        // POST: Departamentos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (var context = new BDNOMINA2020Entities())
                {
                    var data = context.departamentoes.FirstOrDefault(x => x.departamentoId == id);
                    if (data != null)
                    {
                        context.departamentoes.Remove(data);
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
    }
}
