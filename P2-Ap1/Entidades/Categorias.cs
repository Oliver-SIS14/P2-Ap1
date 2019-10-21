using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2_Ap1.Entidades
{
    public class Categorias
    {
        public int Categoriaid { get; set; }
        public string Descripcion { get; set; }

        public Categorias()
        {
            this.Categoriaid = 0;
            this.Descripcion = string.Empty;
        }
    }
}
