//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IMASD
{
    using System;
    using System.Collections.Generic;
    
    public partial class nominaEmpleado
    {
        public int nominaEmpleadoId { get; set; }
        public int nominaId { get; set; }
        public int numEmpleado { get; set; }
        public string status { get; set; }
        public int diasTrabajado { get; set; }
        public decimal sueldo { get; set; }
        public decimal vales { get; set; }
        public System.DateTime create_at { get; set; }
    
        public virtual empleado empleado { get; set; }
        public virtual nomina nomina { get; set; }
    }
}