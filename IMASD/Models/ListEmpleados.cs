using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMASD.Models
{
    public class ListEmpleados
    {
        public int numEmpleado { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public System.DateTime inicio { get; set; }
        public string status { get; set; }
        public int departamentoId { get; set; }
        public string departamento { get; set; }

        public List<ListEmpleados> getListEmpleados()
        {
            List<ListEmpleados> empleados = new List<ListEmpleados>();

            using (var context = new BDNOMINA2020Entities())
            {

                empleados = (
                    from emp in context.empleadoes
                    join dep in context.departamentoes on emp.departamentoId equals dep.departamentoId
                    select new ListEmpleados
                    {
                        numEmpleado = emp.numEmpleado,
                        nombres = emp.nombres,
                        apellidos = emp.apellidos,
                        direccion = emp.direccion,
                        telefono = emp.telefono,
                        inicio = emp.inicio,
                        status = emp.status,
                        departamentoId = dep.departamentoId,
                        departamento = dep.nombre
                    }
                    ).ToList();
            }

            return empleados;
        }
    }
}