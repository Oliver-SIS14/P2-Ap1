using P2_Ap1.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2_Ap1.BLL
{
    public class RepositorioGuardar :  RepositorioBase<Factura>
    {
        public override bool Guardar(Factura factura)
        {
            bool paso = false;
            try
            {
                if (contexto.Factura.Add(factura) != null)
                    paso = contexto.SaveChanges() > 0;

                factura.productos.Count();
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
