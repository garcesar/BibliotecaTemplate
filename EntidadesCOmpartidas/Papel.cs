using System;
using System.Collections.Generic;
using System.Text;

namespace EntidadesCOmpartidas
{
    public class Papel : Publicacion
    {
        //atributos
        private int peso;


        //propiedades
        public int Peso
        {
            get { return peso; }
            set {
                if (value > 0)
                    peso = value;
                else
                    throw new Exception("Error - Peso");
            }
        }


        //constructores
        public Papel(int pISBN, string pTitulo, int pPeso)
            : base(pISBN, pTitulo)
        {
            Peso = pPeso;
        }


        //operaciones
        public override string ToString()
        {
            return "PAPEL: " + base.ToString() + " Peso: " + peso;

        }

    }

}
