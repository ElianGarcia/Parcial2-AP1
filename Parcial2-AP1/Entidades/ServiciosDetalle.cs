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
        public double Precio { get; set; }
        public double Importe { get; set; }
        public double Total { get; set; }

        public ServiciosDetalle(int serviciosDetalleID, int categoriaID, int cantidad, double precio, double importe, double total)
        {
            ServiciosDetalleID = serviciosDetalleID;
            CategoriaID = categoriaID;
            Cantidad = cantidad;
            Precio = precio;
            Importe = importe;
            Total = total;
        }

        public ServiciosDetalle()
        {

        }

        public ServiciosDetalle(int serviciosDetalleID, string nombre, int cantidad, double precio, double importe, double total)
        {
            ServiciosDetalleID = serviciosDetalleID;
            Cantidad = cantidad;
            Precio = precio;
            Importe = importe;
            Total = total;
        }
    }
}
