using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMASD.Models
{
    public class TabuladoEmpleado
    {
        public class TabEmpleado
        {
            public int numEmpleado { get; set; }
            public string nombres { get; set; }
            public string apellidos { get; set; }
            public int departamentoId { get; set; }
            public string departamento { get; set; }
            public decimal sueldo { get; set; }
            public decimal? vales { get; set; }
            public int? nominaEmpleadoId { get; set; }
            public int? diasTrabajado { get; set; }
            public string status { get; set; }
            public decimal? sueldo_n { get; set; }
            public decimal? vales_n { get; set; }
        }

        public List<TabEmpleado> GetTabEmpleados(int id)
        {
            List<TabEmpleado> empleados = new List<TabEmpleado>();

            using (var context = new BDNOMINA2020Entities())
            {

                empleados = (
                    from tab  in context.tabuladors
                    join emp in context.empleadoes on tab.numEmpleado equals emp.numEmpleado
                    join dep in context.departamentoes on emp.departamentoId equals dep.departamentoId
                    join nom in context.nominaEmpleadoes 
                    on tab.numEmpleado equals nom.numEmpleado into nE
                    from nomE in nE.Where(x => x.nominaId == id).DefaultIfEmpty()
                    select new TabEmpleado
                    {
                        numEmpleado = emp.numEmpleado,
                        nombres = emp.nombres,
                        apellidos = emp.apellidos,
                        departamento = dep.nombre,
                        departamentoId = emp.departamentoId,
                        sueldo = tab.sueldo,
                        vales = tab.vales,
                        nominaEmpleadoId = nomE.nominaEmpleadoId,
                        diasTrabajado = nomE.diasTrabajado,
                        status = nomE.status,
                        sueldo_n = nomE.sueldo,
                        vales_n = nomE.vales
                    }
                    ).ToList();
            }

            return empleados;
        }
    }
}