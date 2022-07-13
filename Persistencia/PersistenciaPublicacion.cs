using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCOmpartidas;
using System.Data;
using System.Data.SqlClient;

namespace Persistencia
{
    class PersistenciaPublicacion
    {
        //Hay un solo eliminar en la bd, por lo cual generar una operacion en cada persistencia
        // de tipo publicacion, solo seria una duplicacion del codigo
        public static void Eliminar(Publicacion pPublicacion)
        {
            //Comando a ejecutar
            SqlConnection oConexion = new SqlConnection(Conexion.STR);
            SqlCommand oComando = new SqlCommand("EliminarPublicacion", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            //Parametros

            SqlParameter oIsbn = new SqlParameter("@Isbn", pPublicacion.ISBN);

            SqlParameter oRetorno = new SqlParameter();
            oRetorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(oIsbn);
            oComando.Parameters.Add(oRetorno);
            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                //Determino devolución del SP

                if ((int)oRetorno.Value == -1)
                    throw new Exception("Hay Prestamos Asociados - No se Elimina");
                else if ((int)oRetorno.Value == 0)
                    throw new Exception("No existe - No se Elimina");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConexion.Close();
            }
        }


    }

}
