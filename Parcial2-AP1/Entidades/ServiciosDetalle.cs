using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_AP1.Entidades
{
    public class ServiciosDetalle
    {
        [Key]
        public int ServiciosDetalleID { get; set; }
        public int CategoriaID { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Importe { get; set; }

        public ServiciosDetalle(int serviciosDetalleID, int categoriaID, int cantidad, decimal precio, decimal importe)
        {
            ServiciosDetalleID = serviciosDetalleID;
            CategoriaID = categoriaID;
            Cantidad = cantidad;
            Precio = precio;
            Importe = importe;
        }

        public ServiciosDetalle()
        {

        }

        public ServiciosDetalle(int serviciosDetalleID, int categoriaID, string nombre, int cantidad, decimal precio, decimal importe)
        {
            ServiciosDetalleID = serviciosDetalleID;
            CategoriaID = categoriaID;
            Cantidad = cantidad;
            Precio = precio;
            Importe = importe;
        }
    }
}
