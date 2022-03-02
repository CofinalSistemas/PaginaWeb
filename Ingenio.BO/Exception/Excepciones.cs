using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class Excepciones:Exception
    {


        public string Codigo { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }

        public Excepciones()
        {
        }


        public Excepciones(string message, Exception inner) //para retornar excepciones producidas por otras excepciones
            : base(message, inner)
        {
        }

        public Excepciones(string codigo, string titulo, string descripcion)
            : base()
        {
            Codigo = codigo;
            Titulo = titulo;
            Descripcion = descripcion;
        }
        public Excepciones(string codigo, string message)
            : base()
        {
            Codigo = codigo;
            Descripcion = message;
        }
    }

