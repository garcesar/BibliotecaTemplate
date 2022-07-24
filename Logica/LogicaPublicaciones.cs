using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCOmpartidas;
using Persistencia;

namespace Logica
{
    public class LogicaPublicaciones
    {
        public static void Agregar(Publicacion pPublicacion)
        {
            if (pPublicacion is Digital)
                PersistenciaDigital.Agregar((Digital)pPublicacion);
            else
                PersistenciaPapel.Agregar((Papel)pPublicacion);
        }

        public static Publicacion Buscar(int pIsbn)
        {
            Publicacion p = null;

            p = (Publicacion)PersistenciaDigital.Buscar(pIsbn);

            if(p==null)
                p = (Publicacion)PersistenciaPapel.Buscar(pIsbn);

            return p;
        }

        public static List<Publicacion> ListarPublicaciones()
        {
            List<Publicacion> oAux = PersistenciaDigital.ListarPublicacionDigital();
            oAux.AddRange(PersistenciaPapel.ListaPublicacionesPapel());

            return oAux;
        }

        public static void Modificar(Publicacion pPublicacion)
        {
            if (pPublicacion is Digital)
                PersistenciaDigital.Modificar((Digital)pPublicacion);
            else
                PersistenciaPapel.Modificar((Papel)pPublicacion);
        }

        public static void Eliminar(Publicacion pPublicacion)
        {
            PersistenciaPublicacion.Eliminar(pPublicacion);
        }
    }
}
