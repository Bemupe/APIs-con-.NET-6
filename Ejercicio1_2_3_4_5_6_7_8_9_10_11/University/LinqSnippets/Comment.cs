using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqSnippets
{
    public class Comment
    {
        public int Id { get; set; }
        public string Contenido { get; set; }= string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public DateTime Creacion { get; set; }
    }
}
