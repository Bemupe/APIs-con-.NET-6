using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqSnippets
{
    internal class Empresa
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Empleado[]? Empleados { get; set; }//Lista de empleado vacia o no (si fuera vacía seria : public Empleado[] Empleados { get; set; }= new Empleado[0];)
    }

}
