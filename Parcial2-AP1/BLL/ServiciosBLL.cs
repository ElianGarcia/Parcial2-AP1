using Parcial2_AP1.DAL;
using Parcial2_AP1.Entidades;
using Parcial2_AP1.BLL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_AP1.BLL
{
    public class ServiciosBLL : GenericaBLL<Servicios>
    {
        public new bool Guardar(Servicios servicio)
        {
            bool realizado = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Servicio.Add(servicio) != null)
                    realizado = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }

            finally
            {
                db.Dispose();
            }

            return realizado;
        }

        public new bool Modificar(Servicios servicio)
        {
            bool realizado = false;
            Contexto db = new Contexto();
            GenericaBLL<Servicios> genericaBLL = new GenericaBLL<Servicios>();

            try
            {
                var Anterior = genericaBLL.Buscar(servicio.ServiciosID);
                foreach (var item in Anterior.Venta)
                {
                    if (!servicio.Venta.Exists(d => d.ServiciosDetalleID == item.ServiciosDetalleID))
                        db.Entry(item).State = EntityState.Deleted;
                }
                db.Entry(servicio).State = EntityState.Modified;
                realizado = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }

            finally
            {
                db.Dispose();
            }

            return realizado;
        }
    }
}
