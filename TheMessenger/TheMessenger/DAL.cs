using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TheMessenger
{
    /// <summary>
    /// This is DAL.   DAL = Data Layer
    /// This object will handle all the calls to the database
    /// this is a static class.  It does not need to be initialized using "new"
    /// </summary>
    public static class DAL
    {
        public static DataTable ExecStoredProcedure(string spName, List<SqlParameter> sqlParams = null)
        {
            string strConnect = ConfigurationManager.AppSettings["MessengerConn"].ToString(); // this is the connection string

            SqlConnection conn = new SqlConnection(); // initilizing an empty sql connection object

            DataTable dt = new DataTable(); // declaring a new datatable this datatable is in the scope of the ENTIRE method

            /// This is a try and catch section.
            /// If something fails during the try - we will have the error(exception) details in the "catch" section
            try
            {
                conn = new SqlConnection(strConnect); // we initilize the SqlConnection object with the connection string
                conn.Open(); // opening a connection

                // initilizing a SQlCommand object with the stored procedure name, and the connection string.
                SqlCommand command = new SqlCommand(spName, conn);

                // we need to make sure this is a stored procedure.  There are other types of SQL Commands:
                // such as table direct and text.  Both are significantly less secure.
                command.CommandType = CommandType.StoredProcedure;

                // adding the parameters that we sent to the command.
                if (sqlParams != null)
                {

                    command.Parameters.AddRange(sqlParams.ToArray());
                }

                // executing the command
                SqlDataReader dr = command.ExecuteReader();

                // filling our datatable (td) with the data.
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // this block of code executes no matter what
                conn.Close();
            }

            // returns the filled datatable
            return dt;
        }
    }
}