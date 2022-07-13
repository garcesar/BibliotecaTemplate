using System;
using System.Collections.Generic;
using System.Text;

namespace EntidadesCOmpartidas
{
    public class Digital : Publicacion
    {
        //atributos
        private string formato;
        private bool protegida;


        //propiedades
        public string Formato
        {
            get { return formato; }
            set { formato = value; }
        }
        public bool Protegida
        {
            get { return protegida; }
            set { protegida = value; }
        }


        //constructores
        public Digital(int pISBN, string pTitulo, string pFormato, bool pProtegida)
            : base(pISBN, pTitulo)
        {
            Formato = pFormato;
            Protegida = pProtegida;
        }


        //operaciones
        public override string ToString()
        {
            return "DIGITAL: " + base.ToString() + " Formato: " + formato + " Protegida?: " + protegida;
        }


    }

}
