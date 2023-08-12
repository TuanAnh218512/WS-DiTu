using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace DiTuWebService
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/", Name = "DiTu")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        DBAccess objDBAccess = new DBAccess();
        DataTable dtPlanning = new DataTable();



        [WebMethod]
        public DataTable View(string table, string pk)
        {

            string query = "select * from " + table;
            objDBAccess.ReadDatathroughAdapter(query, dtPlanning);
            dtPlanning.TableName = table;
            dtPlanning.PrimaryKey = new DataColumn[] { dtPlanning.Columns[pk] };
            return dtPlanning;

        }


        [WebMethod]
        public bool Create(DataTable dtPlanning, object[] data)
        {
            try
            {
                string query = "insert into " + dtPlanning.TableName + " values (?)";
                string temp = "";
                for (int i = 0; i < dtPlanning.Columns.Count; i++)
                    temp += "@" + i + ",";
                query = query.Replace("?", temp.TrimEnd(','));
                objDBAccess.executeSQL(query, data);

                return true;
            }
            catch
            {
                return false;
            }
        }
        [WebMethod]
        public bool Edit(DataTable dtPlanning, object[] data)
        {
            string query = "update " + dtPlanning.TableName + " set ? where " + dtPlanning.PrimaryKey[0].ColumnName + "=@0";
            string temp = "";
            for (int i = 1; i < dtPlanning.Columns.Count; i++)
            {
                temp += dtPlanning.Columns[i].ColumnName + "=@" + i + ",";
            }
            query = query.Replace("?", temp.TrimEnd(','));
            objDBAccess.executeSQL(query, data);
            return true;
        }
        [WebMethod]
        public DataTable Search(string table, string pk, string keyword)
        {

            string query = "SELECT * FROM " + table + " WHERE " + pk + " LIKE '%"+keyword+"%'";
            objDBAccess.ReadDatathroughAdapter(query, dtPlanning);
            dtPlanning.TableName = table;
            dtPlanning.PrimaryKey = new DataColumn[] { dtPlanning.Columns[pk] };
            return dtPlanning;

        }
        [WebMethod]
        public bool Delete(DataTable dtPlanning, object[] data)
        {
            try
            {
                objDBAccess.executeSQL("delete from " + dtPlanning.TableName + " where " + dtPlanning.PrimaryKey[0].ColumnName + "=@0", new object[] { data[0] });
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
