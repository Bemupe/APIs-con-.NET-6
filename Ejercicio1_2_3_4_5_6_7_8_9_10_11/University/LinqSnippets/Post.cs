using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqSnippets
{
    public class Post
    {
        public int Id { get; set; }
        public string Titulo { get; set; }=string.Empty;
        public string Contenido { get; set; } = string.Empty;

        public DateTime Creacion { get; set;}
        public List <Comment>? Comentarios { get; set; }

    }
}
