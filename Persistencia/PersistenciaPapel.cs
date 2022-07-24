using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCOmpartidas;
using System.Data;
using System.Data.SqlClient;
using System.Configuration; // -> Para archivos de configuración como el archivo Web.Config

namespace Persistencia
{
    public class PersistenciaPapel
    {
        public static void Agregar(Papel pPapel)
        {
            //Comando a ejecutar
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("AgregarPublicacionPapel", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            //Parametros
            oComando.Parameters.AddWithValue("@Isbn", pPapel.ISBN);
            oComando.Parameters.AddWithValue("@Titulo", pPapel.Titulo);
            oComando.Parameters.AddWithValue("@Peso", pPapel.Peso);
            

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
            finally { oComando.Clone(); }
        }

        public static void Modificar(Papel pPapel)
        {
            //Haciendo al ConfigurationManager
            SqlConnection oConexion = new SqlConnection(ConfigurationManager.ConnectionStrings["BiblioCnnStr"].ConnectionString);
            SqlCommand oComando = new SqlCommand("ModificarPublicacionPapel", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            //Parametros
            oComando.Parameters.AddWithValue("@Isbn", pPapel.ISBN);
            oComando.Parameters.AddWithValue("@Titulo", pPapel.Titulo);
            oComando.Parameters.AddWithValue("@Peso", pPapel.Peso);

            SqlParameter response = new SqlParameter();
            response.Direction = ParameterDirection.ReturnValue;
            oComando.Parameters.Add(response);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                if (Convert.ToInt32(response.Value) == 0)
                    throw new Exception("No existe - No se modifica");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { oConexion.Close(); }
        }

        public static Papel Buscar(int pIsbn)
        {
            int oIsbn, oPeso;
            string oTitulo;

            Papel p = null;
            SqlDataReader oReader;

            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("Exec BuscarPublicacionPapel " + pIsbn, oConexion);

            try
            {   
                oConexion.Open();
                oReader = oComando.ExecuteReader();

                if(oReader.Read())
                {
                    oIsbn = (int)oReader["Isbn"];
                    oPeso = (int)oReader["Peso"];
                    oTitulo = (string)oReader["Titulo"];
                    p = new Papel(oIsbn, oTitulo, oPeso);
                }
                oReader.Close();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problemas con la base de datos:" + ex.Message);
            }
            finally { oConexion.Close();}
            return p;
        }

        public static List<Publicacion> ListaPublicacionesPapel()
        {
            int oIsbn, oPeso;
            string oTitulo;

            Papel p;
            List<Publicacion> oListaPublicaciones = new List<Publicacion>();
            SqlDataReader oReader;

            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("Exec ListarPublicacionPapel ", oConexion);

            try
            {   
                oConexion.Open();
                oReader = oComando.ExecuteReader();

                if(oReader.Read())
                {
                    oIsbn = (int)oReader["Isbn"];
                    oTitulo = (string)oReader["Titulo"];
                    p = new Publicacion(oIsbn, oTitulo);
                    oListaPublicaciones.Add(p);
                }
                oReader.Close();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problemas con la base de datos:" + ex.Message);
            }
            finally { oConexion.Close();}
            return p;
        }
    }
}
