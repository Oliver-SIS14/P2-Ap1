using P2_Ap1.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2_Ap1.BLL
{
    public class RepositorioModificar : RepositorioBase<Factura>
    {
        public override bool Modificar(Factura factura)
        {
            bool paso = false;

            try
            {
                 var Anterior = contexto.Factura.Find(factura.Facturaid);

                foreach (var item in Anterior.productos)
                {
                    if (!factura.productos.Exists(d => d.id == item.id))
                        contexto.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                }
                contexto.Entry(factura).State = System.Data.Entity.EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
           

            return paso;
        }
    }
}
