using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqSnippets
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string? Nombre { get; set; } = string.Empty;
        public int NotaMedia { get; set; } = 0;
        public bool Certificacion { get; set; }

    }
}
