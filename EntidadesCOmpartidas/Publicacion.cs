using System;
using System.Collections.Generic;
using System.Text;

namespace EntidadesCOmpartidas
{
    public abstract class Publicacion
    {
        //atributos
        private int isbn;
        private string titulo;


        //propiedades
        public int ISBN
        {
            get { return isbn; }
            set { isbn = value; }
        }

        public string Titulo
        {
            get { return titulo; }
            set { titulo = value; }
        }


        //constructores
         public Publicacion(int pISBN, string pTitulo)
        {
            ISBN = pISBN;
            Titulo = pTitulo;
        }


        //operaciones
        public override string ToString()
        {
            return "Título: " + titulo + " ISBN: " + isbn;
        }

    }
}
