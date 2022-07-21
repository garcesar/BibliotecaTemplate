using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCOmpartidas;
using System.Data;
using System.Data.SqlClient;

namespace Persistencia
{
    public class PersistenciaPrestamo
    {
        public static int Agregar(Prestamo pPrestamo)
        {
            //Comando a ejecutar
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("AgregarPrestamo", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            //Parametros
            oComando.Parameters.AddWithValue("@Fecha", pPrestamo.Fecha);
            oComando.Parameters.AddWithValue("@Dias", pPrestamo.Dias);
            oComando.Parameters.AddWithValue("@Nombre", pPrestamo.NombreUsuario);
            oComando.Parameters.AddWithValue("@Isbn", pPrestamo.Pub.ISBN);

            SqlParameter response = new SqlParameter("@Retorno", SqlDbType.int);
            response.Direction = ParameterDirection.ReturnValue;
            oComando.Parameters.Add(response);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                if (Convert.ToInt32(response.value) == -1 )
                    throw new Exception("La publicación no existe - No se presto");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { oComando.Clone(); }
            return Convert.ToInt32(response.value);
        }

        public static Prestamo Buscar(int pNumero)
        {
            //Se obtiene el código de la publicación, para luego buscar el prestamo
            int oIsbn = 0, oDias = 0;
            DateTime oFecha = DateTime.Now;
            string oNombre = "";
            bool oDevuelvo = true;
            
            Publicacion oPublicacion = null;
            Prestamo p = null;
            SqlDataReader oReader;

            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("Exec BuscarPrestamo " + pNumero, oConexion);

            try
            {   
                oConexion.Open();
                oReader = oComando.ExecuteReader();

                //Viene un solo registro
                if(oReader.read())
                {
                    oFecha = (DateTime)oReader["Fecha"];
                    oDias = (int)oReader["Dias"];
                    oDevuelto = (bool)oReader["Devuelto"];
                    oNombre = (string)oReader["Nombre"];
                    oISBN = (int)oReader["Isbn"];
                    
                    oPublicacion = PersistenciaPapel.Buscar(oISBN);
                    if (oPublicacion == null)
                        oPublicacion = PersistenciaDigital.Buscar(oISBN);
                    
                    p = new Prestamo(pNumero, oFecha, oDias, oNombre, oDevuelto, oPublicacion);;
                }
                oReader.Close();
            }
            catch (Exception ex)
            {
                throw ex.Message;
            }
            finally { oConexion.Close();}
            return p;
        }

        public static void Devolver(Prestamo pPrestamo) //Devolución del prestamo
        {
            //Comando a ejecutar
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("DevolverPrestamo", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter oNumero = new SqlParameter("@Numero", pPrestamo.Numero);
            
            SqlParameter response = new SqlParameter();
            response.Direction = ParameterDirection.ReturnValue;
            
            oComando.Parameters.Add(oNumero);
            oComando.Parameters.Add(response);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                if (Convert.ToInt32(response.value) == -1 )
                    throw new Exception("No existe - No se devuelve");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { oComando.Clone(); }
        }


        public static List<Prestamo> ListarPrestamoNoDevueltos()
        {
            int oNumero, oDias, oISBN;
            Boolean oDevuelto;
            string oNombre;

            Publicacion oPublicacion;
            Prestamo p;
            List<Prestamo> oListaPrestamo = new List<Prestamo>();
            SqlDataReader oReader;

            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("Exec ListarPrestamosNoDevueltos", oConexion);

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();

                while(oReader.Read())
                {
                    oNumero = (int)oReader["Numero"];
                    oFecha = (DateTime)oReader["Fecha"];
                    oDias = (int)oReader["Dias"];
                    oDevuelto = (bool)oReader["Devuelto"];
                    oNombre = (string)oReader("Nombre");
                    oIsbn = (int)oReader["Isbn"];

                    oPublicacion = PersistenciaPapel.Buscar(oISBN);
                    if (oPublicacion == null)
                        oPublicacion = PersistenciaDigital.Buscar(oISBN);
                    
                    p = new Prestamo(oNumero, oFecha, oDias, oNombre, oDevuelto, oPublicacion);
                    oListaPublicaciones.Add(p);
                }
                oReader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { oConexion.Close(); }
            return oListaPrestamo;
        }

        public static List<Prestamo> ListarPrestamosVencidos (DateTime pFechaVencimiento)
        {
            int oNumero, oDias, oISBN;
            DateTime oFecha;
            Boolean oDevuelto;
            string oNombre;

            Publicacion oPublicacion;
            Prestamo p;
            List<Prestamo> oListaPrestamo = new List<Prestamo>();
            SqlDataReader oReader;

            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("Exec ListarPrestamosVencidos " + pFechaVencimiento.ToString("yyyyMMdd"), oConexion);

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();

                while(oReader.Read())
                {
                    oNumero = (int)oReader["Numero"];
                    oFecha = (DateTime)oReader["Fecha"];
                    oDias = (int)oReader["Dias"];
                    oDevuelto = (bool)oReader["Devuelto"];
                    oNombre = (string)oReader("Nombre");
                    oIsbn = (int)oReader["Isbn"];

                    oPublicacion = PersistenciaPapel.Buscar(oISBN);
                    if (oPublicacion == null)
                        oPublicacion = PersistenciaDigital.Buscar(oISBN);
                    
                    p = new Prestamo(oNumero, oFecha, oDias, oNombre, oDevuelto, oPublicacion);
                    oListaPublicaciones.Add(p);
                }
                oReader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { oConexion.Close(); }
            return oListaPrestamo;
        }

        public static List<Prestamo> ListarPrestamos()
        {
            int oNumero, oDias, oISBN;
            DateTime oFecha;
            Boolean oDevuelto;
            string oNombre;

            Publicacion oPublicacion;
            Prestamo p;
            List<Prestamo> oListaPrestamo = new List<Prestamo>();
            SqlDataReader oReader;

            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("Exec ListarPrestamos", oConexion);

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();

                while(oReader.Read())
                {
                    oNumero = (int)oReader["Numero"];
                    oFecha = (DateTime)oReader["Fecha"];
                    oDias = (int)oReader["Dias"];
                    oDevuelto = (bool)oReader["Devuelto"];
                    oNombre = (string)oReader("Nombre");
                    oIsbn = (int)oReader["Isbn"];

                    oPublicacion = PersistenciaPapel.Buscar(oISBN);
                    if (oPublicacion == null)
                        oPublicacion = PersistenciaDigital.Buscar(oISBN);
                    
                    p = new Prestamo(oNumero, oFecha, oDias, oNombre, oDevuelto, oPublicacion);
                    oListaPublicaciones.Add(p);
                }
                oReader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { oConexion.Close(); }
            return oListaPrestamo;
        }
    }

    
}
