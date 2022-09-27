using RecetasSLN.dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.datos
{
    class Helper
    {
        private static Helper instancia;
        private SqlConnection cnn;

        public Helper()
        {
            cnn = new SqlConnection(Properties.Resources.cnnString);    
        }
        public static Helper ObtenerInstancia() // singleton: metodo para instanciar por unica vez
        {
            if (instancia == null)
                instancia = new Helper();
            return instancia;
        }

        public int ObtenerProximo(string sp, string nombrePOut)
        {
            SqlCommand cmdProximo = new SqlCommand();
            cnn.Open();
            cmdProximo.Connection = cnn;
            cmdProximo.CommandText = sp;
            cmdProximo.CommandType = CommandType.StoredProcedure;
            SqlParameter pOut = new SqlParameter();
            pOut.ParameterName = nombrePOut;
            pOut.DbType = DbType.Int32;
            pOut.Direction = ParameterDirection.Output;
            cmdProximo.Parameters.Add(pOut);
            cmdProximo.ExecuteNonQuery();
            cnn.Close();
            return (int)pOut.Value;
        }

        public DataTable CargarCombo(string sp, List<Parametro> values)
        {
            DataTable table = new DataTable();
            SqlCommand cmdCombo = new SqlCommand();
            cnn.Open();
            cmdCombo.Connection = cnn;
            cmdCombo.CommandText = sp;
            cmdCombo.CommandType = CommandType.StoredProcedure;

            if (values != null)
            {
                foreach(Parametro oPar in values)
                {
                    cmdCombo.Parameters.AddWithValue(oPar.Clave, oPar.Valor);
                }
            }

            table.Load(cmdCombo.ExecuteReader());
            cnn.Close();
            return table;
        }

        public bool ConfirmarReceta(Receta oReceta)
        {
            bool ok = true;
            SqlTransaction trs = null;
            try
            {
                SqlCommand cmdMaestro = new SqlCommand();
                cnn.Open();
                trs = cnn.BeginTransaction();
                cmdMaestro.Connection = cnn;
                cmdMaestro.Transaction = trs;
                cmdMaestro.CommandText = "SP_INSERTAR_RECETA";
                cmdMaestro.CommandType = CommandType.StoredProcedure;
                cmdMaestro.Parameters.AddWithValue("@nombre", oReceta.Nombre);
                cmdMaestro.Parameters.AddWithValue("@cheff", oReceta.Cheff);
                cmdMaestro.Parameters.AddWithValue("@tipo_receta", oReceta.TipoReceta.IdTipo);


                SqlParameter pOut = new SqlParameter();
                pOut.ParameterName = "@id_receta";
                pOut.DbType = DbType.Int32;
                pOut.Direction = ParameterDirection.Output;
                cmdMaestro.Parameters.Add(pOut);
                cmdMaestro.ExecuteNonQuery();

                int RecetaNro = (int)pOut.Value;
                SqlCommand cmdDetalle;
                foreach (DetalleReceta item in oReceta.Detalles)
                {
                    cmdDetalle = new SqlCommand();
                    cmdDetalle.Connection = cnn;
                    cmdDetalle.Transaction = trs;
                    cmdDetalle.CommandText = "SP_INSERTAR_DETALLES";
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@id_receta", RecetaNro);
                    cmdDetalle.Parameters.AddWithValue("@id_ingrediente", item.Ingrediente.IngredienteId);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", item.Cantidad);
                    cmdDetalle.ExecuteNonQuery();
                }
                trs.Commit();
            }
            catch (Exception)
            {
                if (trs != null)
                    trs.Rollback();
                ok = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return ok;

        }

    }
}
