using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa4_Persistencia.SqlServer.ModuloBase
{
    public class ConectionSqlServer
    {
        private SqlConnection connection;
        private SqlTransaction transaction;

        public void OpenConnection()
        {
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = "Data Source = (local); Initial Catalog = cencepperuBD; Integrated Security = true";
                connection.Open();
            } 
            catch (Exception error)
            {
                throw new Exception("Error: The Database didn't connect.", error);
            }
        }

        public void CloseConnection()
        {
            try
            {
                connection.Close();
            }
            catch (Exception error)
            {
                throw new Exception("Error: The DataBase didn't close", error);
            }
        }

        public void StartTransaction()
        {
            try
            {
                OpenConnection();
                transaction = connection.BeginTransaction();

            }
            catch (Exception error)
            {
                throw new Exception("Error: The transaction with the Database did not start.", error);
            }
        }

        public void FinishTransaction()
        {
            try
            {
                transaction.Commit();
                connection.Close();
            }
            catch (Exception error)
            {
                throw new Exception("Error: The transaction with the Database did not finish.", error);
            }
        }

        public void CancelTransaction()
        {
            try
            {
                transaction.Rollback();
                connection.Close();
            }
            catch (Exception error)
            {
                throw new Exception("Error: The transaction with the Database did not cancel.", error);
            }
        }

        public SqlDataReader ExecuteQuery(string sentenciaSQL)
        {
            try
            {
                SqlCommand comando = connection.CreateCommand();
                if (transaction != null)
                    comando.Transaction = transaction;
                comando.CommandText = sentenciaSQL;
                comando.CommandType = CommandType.Text;
                return comando.ExecuteReader();
            } catch (Exception error)
            {
                throw new Exception("Error: The query did not execute", error);
            }
        }

        public SqlCommand GetCommand(string sentenciaSQL)
        {
            try
            {
                SqlCommand comando = connection.CreateCommand();
                if (transaction != null)
                    comando.Transaction = transaction;
                comando.CommandText = sentenciaSQL;
                comando.CommandType = CommandType.Text;
                return comando;
            } catch (Exception error)
            {
                throw new Exception("Error: The command was not obtained", error);
            }
        }

        public SqlCommand GetProcedureCommand(string procedimientoAlmacenado)
        {
            try
            {
                SqlCommand comando = connection.CreateCommand();
                if (transaction != null)
                    comando.Transaction = transaction;
                comando.CommandText = procedimientoAlmacenado;
                comando.CommandType = CommandType.StoredProcedure;
                return comando;
            } catch (Exception error)
            {
                throw new Exception("Error: The procedure command was not obtained", error);
            }
        }
    }
}
