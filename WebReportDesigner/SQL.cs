using System;
using System.Collections.Generic;
using System.Web;

namespace WebReportDesigner
{
    public class SQL
    {
        public static System.Data.DataTable GetDataTable(string strSQL)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            Npgsql.NpgsqlConnectionStringBuilder csb = new Npgsql.NpgsqlConnectionStringBuilder();
            csb.Host = "127.0.0.1";

            if (StringComparer.OrdinalIgnoreCase.Equals(Environment.MachineName, "COR-W81-101"))
                csb.Database = "testdb"; //"blogz";
            else
                csb.Database = "testdb";

            csb.IntegratedSecurity = false;
            if (!csb.IntegratedSecurity)
            {
                csb.UserName = "pgwebservices";
                csb.Password = "foobar2000";
            } // End if(!csb.IntegratedSecurity)

            csb.ApplicationName = "Web Report Designer";

            using (Npgsql.NpgsqlDataAdapter da = new Npgsql.NpgsqlDataAdapter(strSQL, csb.ConnectionString))
            {
                da.Fill(dt);
            } // End Using da

            return dt;
        } // End Function GetDataTable


    }


}
