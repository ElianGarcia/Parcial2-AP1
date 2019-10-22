using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Parcial2_AP1.Entidades
{
    public class Servicios
    {
        [Key]
        public int ServiciosID { get; set; }
        public DateTime Fecha { get; set; }
        public string Estudiante { get; set; }
        public int CategoriaID { get; set; }
        public virtual List<ServiciosDetalle> Venta { get; set; }
        public decimal Total { get; set; }

        public Servicios()
        {

        }

        public Servicios(int serviciosID, DateTime fecha, string estudiante, int categoriaID)
        {
            ServiciosID = serviciosID;
            Fecha = fecha;
            Estudiante = estudiante ?? throw new ArgumentNullException(nameof(estudiante));
            CategoriaID = categoriaID;
        }
    }
}
