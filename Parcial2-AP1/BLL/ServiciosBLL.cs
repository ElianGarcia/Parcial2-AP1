using Parcial2_AP1.DAL;
using Parcial2_AP1.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_AP1.BLL
{
    public class ServiciosBLL
    {
        public static bool Guardar(Servicios servicio)
        {
            bool realizado = false;
            Contexto db = new Contexto();

            try
            {
                foreach (var item in servicio.ServiciosDetalle)
                {
                    var categoria = db.Categoria.Find(item.CategoriaID);
                }

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

        public static bool Modificar(Servicios servicio)
        {
            bool realizado = false;
            Contexto db = new Contexto();

            try
            {
                var Anterior = ServiciosBLL.Buscar(servicio.ServiciosID);
                foreach (var item in Anterior.ServiciosDetalle)
                {
                    if (!servicio.ServiciosDetalle.Exists(d => d.ServiciosDetalleID == item.ServiciosDetalleID))
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

        public static bool Eliminar(int id)
        {
            bool realizado = false;
            Contexto db = new Contexto();

            try
            {
                var eliminar = db.Servicio.Find(id);
                db.Entry(eliminar).State = EntityState.Deleted;
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

        public static Servicios Buscar(int id)
        {
            Contexto db = new Contexto();
            Servicios servicio = new Servicios();

            try
            {
                servicio = db.Servicio.Find(id);
                if (servicio != null)
                    servicio.Estudiante.Count();
            }
            catch (Exception)
            {
                throw;
            }

            finally
            {
                db.Dispose();
            }

            return servicio;
        }

        public static List<Servicios> GetList(Expression<Func<Servicios, bool>> asistencia)
        {
            List<Servicios> Lista = new List<Servicios>();
            Contexto db = new Contexto();

            try
            {
                Lista = db.Servicio.Where(asistencia).ToList();
            }
            catch (Exception)
            {
                throw;
            }

            finally
            {
                db.Dispose();
            }

            return Lista;
        }
    }
}
