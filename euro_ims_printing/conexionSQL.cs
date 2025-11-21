using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;

namespace euro_ims_printing
{
    
    public class conexionSQL
    {
        public SqlConnection connSqlRemota;

        public string cadenaDeConexionSQLRemota;

        public SqlCommand comandoSqlRemota;

        public SqlDataAdapter adaptadorSqlRemota;

        public SqlDataReader readerSqlRemota;

        public conexionSQL()
        {
            try
            {

                connSqlRemota = new SqlConnection();
                cadenaDeConexionSQLRemota = ConfigurationManager.ConnectionStrings["ConexionFLX"].ConnectionString;
                comandoSqlRemota = new SqlCommand();
                adaptadorSqlRemota = new SqlDataAdapter();
            }
            catch (SqlException ex)
            {

                Console.WriteLine($"SQL Error during NonQuery execution [conexionSQL]: {ex.Message}");

                throw new ApplicationException("Database operation failed. [conexionSQL]", ex);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"General Error during NonQuery execution: {ex.Message}");
                throw new ApplicationException("An unexpected error occurred during database operation.", ex);
            }

        }

        public void conectar()
        {
            try
            {
                if (connSqlRemota.State == ConnectionState.Open)
                {
                    connSqlRemota.Close();
                    connSqlRemota.ConnectionString = cadenaDeConexionSQLRemota;
                    connSqlRemota.Open();
                }
                else if (connSqlRemota.State == ConnectionState.Closed)
                {
                    connSqlRemota.ConnectionString = cadenaDeConexionSQLRemota;
                    connSqlRemota.Open();
                }
            }
            catch (SqlException ex)
            {

                Console.WriteLine($"SQL Error during NonQuery execution [conectar]: {ex.Message}");

                throw new ApplicationException("Database operation failed. [conectar]", ex);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"General Error during NonQuery execution: {ex.Message}");
                throw new ApplicationException("An unexpected error occurred during database operation.", ex);
            }
        }

        public void desconectar()
        {
            try
            {
                connSqlRemota.Close();
            }
            catch (SqlException ex)
            {

                Console.WriteLine($"SQL Error during NonQuery execution: [desconectar] {ex.Message}");
                
                throw new ApplicationException("Database operation failed. [desconectar]", ex);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"General Error during NonQuery execution: {ex.Message}");
                throw new ApplicationException("An unexpected error occurred during database operation.", ex);
            }
        }

        public DataTable select(string maquina) {
            DataTable dt = new DataTable();
            try {
                if (connSqlRemota.State == ConnectionState.Open)
                {
                    string sql = "select * from EUR2_TAB_ArticulosNumImpresiones where Impreso=0 and NombreMaquina='"+maquina+"'";

                    adaptadorSqlRemota = new SqlDataAdapter(sql,connSqlRemota);
                    adaptadorSqlRemota.Fill(dt);                    

                }
                
                } catch (SqlException ex) {
                Console.WriteLine($"SQL Error during NonQuery execution: [select] {ex.Message}");

                throw new ApplicationException("Database operation failed. [select]", ex);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"General Error during NonQuery execution: {ex.Message}");
                throw new ApplicationException("An unexpected error occurred during database operation.", ex);
            }
            return dt;
        }

        public void update(string consecutivo) {

            try
            {
                if (connSqlRemota.State == ConnectionState.Open)
                {
                    string sql = "update EUR2_TAB_ArticulosNumImpresiones set Impreso=1 where Consecutivo="+consecutivo+" ";

                    comandoSqlRemota = new SqlCommand(sql, connSqlRemota);

                    int rowsAffected = comandoSqlRemota.ExecuteNonQuery();
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error during NonQuery execution: [update] {ex.Message}");

                throw new ApplicationException("Database operation failed. [update]", ex);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"General Error during NonQuery execution: {ex.Message}");
                throw new ApplicationException("An unexpected error occurred during database operation.", ex);
            }
            
        }
    }
    
}
