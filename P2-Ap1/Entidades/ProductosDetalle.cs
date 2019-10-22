using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2_Ap1.Entidades
{
    public class ProductosDetalle
    {
        [key]
        public int id { get; set; }
        public int Facturaid { get; set; }
        public string Categoria { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Importe { get; set; }

        public ProductosDetalle()
        {

        }

        public ProductosDetalle(int id, int Facturaid, string Categoria, int Cantidad, decimal Precio, decimal Importe)
        {
            this.id = id;
            this.Facturaid = Facturaid;
            this.Categoria = Categoria;
            this.Cantidad = Cantidad;
            this.Precio = Precio;
            this.Importe = Importe;
        }
    }
}
