using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2_Ap1.Entidades
{
    public class Factura
    {
        [key]
        public int Facturaid { get; set; }
        public DateTime Fecha { get; set; }
        public string Estudiante { get; set; }
        public decimal Total { get; set; }

        public virtual List<ProductosDetalle> productos { get; set; }

        public Factura()
        {
        }
    }
}
