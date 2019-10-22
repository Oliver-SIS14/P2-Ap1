using P2_Ap1.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2_Ap1.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Factura> Factura { get; set; }
        public DbSet<Categorias> Categorias { get; set; }

        public Contexto() : base("ConStr") { }
    }
}
