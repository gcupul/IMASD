using IMASD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMASD.Controllers
{
    public class EmpleadosController : Controller
    {
        // GET: Empleados
        public ActionResult Index()
        {
            using (var context = new BDNOMINA2020Entities())
            {

                // Return the list of data from the database 
                ListEmpleados list = new ListEmpleados();
                var empleados = list.getListEmpleados();

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

        public ActionResult TableEmpleados(int iddep)
        {
            using (var context = new BDNOMINA2020Entities())
            {

                // Return the list of data from the database 
                ListEmpleados list = new ListEmpleados();
                var empleados = list.getListEmpleados();
                if (iddep > 0)
                {
                    empleados = empleados.Where(x => x.departamentoId == iddep).ToList();
                }

                return PartialView("TableEmpleados", empleados);
            }
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {
            using (var context = new BDNOMINA2020Entities())
            {

                // Return the list of data from the database 
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
                return View();
            }
        }

        // POST: Empleados/Create
        [HttpPost]
        public ActionResult Create(empleado collection)
        {
            try
            {
                // To open a connection to the database 
                using (var context = new BDNOMINA2020Entities())
                {
                    collection.create_at = DateTime.Now;
                    collection.update_at = DateTime.Now;
                    collection.inicio = DateTime.Now;
                    collection.status = "Activo";
                    context.empleadoes.Add(collection);

                    // save the changes 
                    context.SaveChanges();
                    // Add data to the particular table 
                }
                string message = "Created the record successfully";

                // To display the message on the screen 
                // after the record is created successfully 
                ViewBag.Message = message;

                return RedirectToAction("Index");
            }
            catch
            {
                using (var context = new BDNOMINA2020Entities())
                {

                    // Return the list of data from the database 
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
                    return View();
                }
            }
        }

        // GET: Empleados/Edit/5
        public ActionResult Edit(int id)
        {
            using (var context = new BDNOMINA2020Entities())
            {
                var empleado = context.empleadoes.Where(x => x.numEmpleado == id).SingleOrDefault();
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
                return View(empleado);
            }
        }

        // POST: Empleados/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, empleado objEmpleado)
        {
            try
            {
                // To open a connection to the database 
                using (var context = new BDNOMINA2020Entities())
                {
                    // particular record from a database 
                    var data = context.empleadoes.FirstOrDefault(x => x.numEmpleado == id);
                    if (data != null)
                    {
                        // save the changes 
                        data.nombres = objEmpleado.nombres;
                        data.apellidos = objEmpleado.apellidos;
                        data.direccion = objEmpleado.direccion;
                        data.telefono = objEmpleado.telefono;
                        data.departamentoId = objEmpleado.departamentoId;
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
                using (var context = new BDNOMINA2020Entities())
                {

                    // Return the list of data from the database 
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
                    return View();
                }
            }
        }

        // GET: Empleados/Delete/5
        public ActionResult Delete(int id)
        {
            using (var context = new BDNOMINA2020Entities())
            {
                var empleado = context.empleadoes.Where(x => x.numEmpleado == id).SingleOrDefault();
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
                return View(empleado);
            }
        }

        // POST: Empleados/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (var context = new BDNOMINA2020Entities())
                {
                    var empleado = context.empleadoes.Where(x => x.numEmpleado == id).SingleOrDefault();
                    if (empleado != null)
                    {
                        var tabulador = context.tabuladors.Where(x => x.numEmpleado == id).SingleOrDefault();
                        if (tabulador != null)
                        {
                            context.tabuladors.Remove(tabulador);
                            context.SaveChanges();
                        }
                        context.empleadoes.Remove(empleado);
                        context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
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
                        return View(empleado);
                    }
                }
            }
            catch
            {
                using (var context = new BDNOMINA2020Entities())
                {
                    var empleado = context.empleadoes.Where(x => x.numEmpleado == id).SingleOrDefault();
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
                    return View(empleado);
                }
            }
        }
        // GET: Empleados/Tabulador
        public ActionResult Tabulador(int id)
        {
            using (var context = new BDNOMINA2020Entities())
            {
                var tabulador = context.tabuladors.Where(x => x.numEmpleado == id).SingleOrDefault();
                var empleados = context.empleadoes.Where(x => x.numEmpleado == id).SingleOrDefault();
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
                ViewData["numEmpleado"] = empleados.numEmpleado;
                ViewData["nombres"] = empleados.nombres;
                ViewData["apellidos"] = empleados.apellidos;
                ViewData["direccion"] = empleados.direccion;
                ViewData["telefono"] = empleados.telefono;
                ViewData["departamento"] = empleados.departamento.nombre;
                return View(tabulador);
            }
        }

        // POST: Empleados/Tabulador/5
        [HttpPost]
        public ActionResult Tabulador(int id, tabulador objTabulador)
        {
            try
            {
                // To open a connection to the database 
                using (var context = new BDNOMINA2020Entities())
                {
                    // particular record from a database 
                    var data = context.tabuladors.FirstOrDefault(x => x.numEmpleado == id);
                    if (data != null)
                    {
                        // save the changes 
                        data.sueldo = objTabulador.sueldo;
                        data.primaVacacional = objTabulador.primaVacacional;
                        data.aguinaldo = objTabulador.aguinaldo;
                        data.vales = objTabulador.vales;
                        data.vacaciones = objTabulador.vacaciones;
                        context.SaveChanges();
                    } else
                    {
                        context.tabuladors.Add(objTabulador);
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
                using (var context = new BDNOMINA2020Entities())
                {

                    var tabulador = context.tabuladors.Where(x => x.numEmpleado == id).SingleOrDefault();
                    var empleados = context.empleadoes.Where(x => x.numEmpleado == id).SingleOrDefault();
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
                    ViewData["numEmpleado"] = empleados.numEmpleado;
                    ViewData["nombres"] = empleados.nombres;
                    ViewData["apellidos"] = empleados.apellidos;
                    ViewData["direccion"] = empleados.direccion;
                    ViewData["telefono"] = empleados.telefono;
                    ViewData["departamento"] = empleados.departamento.nombre;
                    return View(tabulador);
                }
            }
        }
    }
}
