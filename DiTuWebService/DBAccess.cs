using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DiTuWebService
{
    public class DBAccess
    {
        private static SqlConnection _conn = new SqlConnection();
        private static SqlCommand command = new SqlCommand();
        private SqlDataAdapter adapter = new SqlDataAdapter();
        public SqlTransaction trans;

        private static string strConnString = @"Data Source=DESKTOP-LNR2UJ4\MSSQLSERVER01;Initial Catalog=DiTu;Integrated Security=True";

        public bool OpenConn()
        {
            try
            {
                _conn = new SqlConnection(strConnString);
                _conn.Open();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public void CloseConn()
        {
            _conn.Close();
            _conn.Dispose();
        }

        public void ReadDatathroughAdapter(string query, DataTable tblName)
        {
            OpenConn();

            try
            {
                command.Connection = _conn;
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                adapter = new SqlDataAdapter(command);
                adapter.Fill(tblName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            CloseConn();

        }

        public void executeSQL(string query, object[] data)
        {
            OpenConn();
            command = new SqlCommand(query, _conn);
            for (int i = 0; i < data.Length; i++)
                command.Parameters.Add(new SqlParameter("@" + i, data[i]));
            command.ExecuteNonQuery();
            CloseConn();
        }
    }
}