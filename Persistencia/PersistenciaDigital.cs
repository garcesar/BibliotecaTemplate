using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCOmpartidas;
using System.Data;
using System.Data.SqlClient;

namespace Persistencia
{
    public class PersistenciaDigital
    {
        public static void Agregar(Digital pDigital)
        {
            //Comando a ejecutar
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("AgregarPublicacionDigital", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            //Parametros
            oComando.Parameters.AddWithValue("@Isbn", pDigital.ISBN);
            oComando.Parameters.AddWithValue("@Titulo", pDigital.Titulo);
            oComando.Parameters.AddWithValue("@Formato", pDigital.Formato);
            oComando.Parameters.AddWithValue("@Protegida", pDigital.Protegida);

            SqlParameter response = new SqlParameter();
            response.Direction = ParameterDirection.ReturnValue;
            oComando.Parameters.Add(response);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                //if ((int)response.Value == 1)
                //    throw new Exception("Ya Existe el ISBN");
                //else if ((int)response.Value == 0)
                //    throw new Exception("Error");

                throw new Exception((int)response.Value == 1 ? "Ya Existe el ISBN" : "Error");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { oConexion.Close(); }
        }

        public static void Modificar(Digital pDigital)
        {
            //Comando a ejecutar
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("ModificarPublicacionDigital", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            //Parametros
            oComando.Parameters.AddWithValue("@Isbn", pDigital.ISBN);
            oComando.Parameters.AddWithValue("Titulo", pDigital.Titulo);
            oComando.Parameters.AddWithValue("@Formato", pDigital.Formato);
            oComando.Parameters.AddWithValue("@Protegida", pDigital.Protegida);

            SqlParameter response = new SqlParameter("@Retorno", SqlDbType.Int);
            response.Direction = ParameterDirection.ReturnValue;
            oComando.Parameters.Add(response);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                if ((int)response.Value == 0)
                    throw new Exception("No existe - No se modifica");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { oConexion.Close(); }
        }

        public static Digital Buscar(int pIsbn)
        {
            int oIsbn;
            string oTitulo, oFormato;
            bool oProtegida;

            Digital p = null;
            SqlDataReader oReader;

            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("Exec BuscarPublicacionDigital " + pIsbn, oConexion);

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();

                if(oReader.Read())
                {
                    oIsbn = (int)oReader["Isbn"];
                    oTitulo = (string)oReader["Titulo"];
                    oFormato = (string)oReader["Formato"];
                    oProtegida = (bool)oReader["Protegida"];
                    p = new Digital(oIsbn, oTitulo, oFormato, oProtegida);
                }
                oReader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { oConexion.Close(); }
            return p;
        }

        public static List <Publicacion> ListarPublicacionDigital()
        {
            int oIsbn;
            Boolean oProtegida;
            string oTitulo, oFormato;

            List<Publicacion> oListaPublicaciones = new List<Publicacion>();
            SqlDataReader oReader;

            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("Exec ListarPublicacionesDigital", oConexion);

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();

                while(oReader.Read())
                {
                    oIsbn = (int)oReader["Isbn"];
                    oTitulo = (string)oReader["Titulo"];
                    oFormato = (string)oReader["Formato"];
                    oProtegida = (bool)oReader["Protegida"];

                    Digital p = new Digital(oIsbn, oTitulo, oFormato, oProtegida);
                    oListaPublicaciones.Add(p);
                }
                oReader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { oConexion.Close(); }
            return oListaPublicaciones;
        }
    }
}
