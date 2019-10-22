using Parcial2_AP1.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_AP1.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Categorias> Categoria { get; set; }
        public DbSet<Servicios> Servicio { get; set; }
        public DbSet<ServiciosDetalle> ServiciosDetalle { get; set; }

        public Contexto() : base("ConStr")
        {

        }
    }
}
