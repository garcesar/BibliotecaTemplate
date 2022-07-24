using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCOmpartidas;
using Persistencia;

namespace Logica
{
    public class LogicaPrestamo
    {
        public static int Agregar(Prestamo pPrestamo)
        {
            return PersistenciaPrestamo.Agregar(pPrestamo);
        }

        public static Prestamo Buscar(int pNumero)
        {
            return PersistenciaPrestamo.Buscar(pNumero);
        }

        public static void Devolver(Prestamo pPrestamo)
        {
            PersistenciaPrestamo.Devolver(pPrestamo);
        }

        public static List<Prestamo> ListarPrestamosVencidos()
        {
            return PersistenciaPrestamo.ListarPrestamosVencidos(DateTime.Now);
        }

        public static List<Prestamo> ListarPrestamos()
        {
            return PersistenciaPrestamo.ListarPrestamos();
        }

        public static List<Prestamo> ListarPrestamosNoDevueltos()
        {
            return PersistenciaPrestamo.ListarPrestamoNoDevueltos();
        }
    }
}
