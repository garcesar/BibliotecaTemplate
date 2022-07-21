using System;
using System.Collections.Generic;
using System.Text;

namespace EntidadesCOmpartidas
{
    public class Prestamo
    {
        //atributos
        private int numero;
        private DateTime fecha;
        private int dias;
        private string nombreUsuario;
        private bool devuelto;

        private Publicacion pub;


        //propiedades
        public int Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public int Dias
        {
            get { return dias; }
            set {
                if (value > 0)
                    dias = value;
                else
                    throw new Exception("Error - Dias");
            }
        }

        public string NombreUsuario
        {
            get { return nombreUsuario; }
            set { nombreUsuario = value; }
        }

        public bool Devuelto
        {
            get { return devuelto; }
            set { devuelto = value; }
        }

        public Publicacion Pub
        {
            get { return pub; }
            set
            {
                if (value == null)
                    throw new Exception("Se necesita publicacion para prestar");
                else
                    pub = value;
            }
        }

        public string Desplegar
        {
            get { return this.ToString(); }
        }

        //constructores
        public Prestamo(int pNumero, DateTime pFecha, int pDias, string pNombreUsuario, bool pDevuelto, Publicacion pPub)
        {
            Numero = pNumero;
            Fecha = pFecha;
            Dias = pDias;
            NombreUsuario = pNombreUsuario;
            Devuelto = pDevuelto;
            Pub = pPub;
        }


        //operaciones
        public override string ToString()
        {
            return "PRESATMO: N�mero: " + numero + " Fecha: " + fecha.ToShortDateString() + " Dias: " + dias + " Usuario: " + nombreUsuario + " Devuelta?: " + devuelto + " Publicaci�n: " + pub.Titulo;
        }
    }

}
