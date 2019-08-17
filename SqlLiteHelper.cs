using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace QMDJ
{
    static class SqlLiteHelper
    {
        
        public static SQLiteDataReader ExecuteDataReader(string sql,string db)
        {
            string datasource = db;// Application.StartupPath + "/nongli.db";
            //System.Data.SQLite.SQLiteConnection.CreateFile(datasource);
            //连接数据库 
            System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection();
            System.Data.SQLite.SQLiteConnectionStringBuilder connstr = new System.Data.SQLite.SQLiteConnectionStringBuilder();
            connstr.DataSource = datasource;
            connstr.Password = "chenli118";
            conn.ConnectionString = connstr.ToString();
            conn.Open();
            //取出数据 
            System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            System.Data.SQLite.SQLiteDataReader reader = cmd.ExecuteReader();
            return reader;
        }
        public static void SqlServerToSqlLite(string db,string sql)
        {
            //string sql = "select  [gzDay],[gzMonth],[gzYear],DateValue=rtrim([DateValue])+' '+ substring(JieQi,4,5),[weekDay],[constellation],JieQi,[nlMonth],[nlDay]  from [ChineseTenThousandCalendar] where left(ltrim(JieQi),2) in (" + JieQiHelper.GetInJieQis() + ")";            
            System.Data.SqlClient.SqlConnection sqlCon = new System.Data.SqlClient.SqlConnection();
            sqlCon.ConnectionString = "server=(local);user id=sa;password=***;initial catalog=HanZiMisc;TimeOut=10000;Packet Size=4096;Pooling=true;Max Pool Size=100;Min Pool Size=1";
            System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand(sql);
            sqlCmd.Connection = sqlCon;
            sqlCon.Open();
            System.Data.SqlClient.SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            if (sqlReader != null)
            {
                string datasource = db;// Application.StartupPath + "/JieQi.db";
                System.Data.SQLite.SQLiteConnection.CreateFile(datasource);
                //连接数据库 
                System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection();
                System.Data.SQLite.SQLiteConnectionStringBuilder connstr = new System.Data.SQLite.SQLiteConnectionStringBuilder();
                connstr.DataSource = datasource;
                connstr.Password = "chenli118";//可以设密码                
                conn.ConnectionString = connstr.ToString();
                conn.Open();
                //conn.ChangePassword("nguchen");//可以改已打开CON的密码
                //创建表 
                System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand();
                string sqlc = "CREATE TABLE ChinaJiQiTable(ID Integer PRIMARY KEY,DayGZ TEXT(4) NULL,MonthGZ TEXT(4) NULL,YearGZ TEXT(4) NULL,	DateValue datetime NULL,Week TEXT(6) NULL,Star TEXT(6) NULL,	JieQi TEXT(30) NULL,	NongLiMonth TEXT(6) NULL,NongLiDay TEXT(4) NULL)";
                cmd.CommandText = sqlc;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                //插入数据 
                SQLiteParameter[] sqlparams = new SQLiteParameter[]
                {
                     new SQLiteParameter("@ID",DbType.Int64,10),
                    new SQLiteParameter("@dG",DbType.String,4),
                    new SQLiteParameter("@mG",DbType.String,4),
                    new SQLiteParameter("@yG",DbType.String,4),
                    new SQLiteParameter("@start",DbType.String,6),
                    new SQLiteParameter("@wk",DbType.String,6),
                    new SQLiteParameter("@date",DbType.DateTime),
                    new SQLiteParameter("@jieqi",DbType.String,30),
                    new SQLiteParameter("@nM",DbType.String,6),
                    new SQLiteParameter("@nD",DbType.String,6),
                 };
                cmd.Parameters.AddRange(sqlparams);
                while (sqlReader.Read())
                {
                    InsertSQLiteGZTable(sqlReader["DateValue"].ToString().Trim(), sqlReader["weekDay"].ToString().Trim(), sqlReader["constellation"].ToString().Trim(),
                        sqlReader["gzYear"].ToString().Trim(), sqlReader["gzMonth"].ToString().Trim(), sqlReader["gzDay"].ToString().Trim(), sqlReader["JieQi"].ToString().Trim(),
                        sqlReader["nlMonth"].ToString().Trim(), sqlReader["nlDay"].ToString().Trim(), conn, cmd);
                }
                sqlReader.Close();
                conn.Close();
                cmd.Dispose();

            }
            sqlCon.Close();
            sqlCmd = null;
            sqlCon.Dispose();

        }
        private static void InsertSQLiteGZTable(string date, string wk, string start, string yG, string mG, string dG, string jieqi, string nm, string nd, SQLiteConnection con, SQLiteCommand cmd)
        {
            //cmd.Parameters["@ID"].Value = id;
            cmd.Parameters["@dG"].Value = dG;
            cmd.Parameters["@mG"].Value = mG;
            cmd.Parameters["@yG"].Value = yG;
            cmd.Parameters["@start"].Value = start;
            cmd.Parameters["@wk"].Value = wk;
            cmd.Parameters["@date"].Value = date;
            cmd.Parameters["@jieqi"].Value = jieqi;
            cmd.Parameters["@nM"].Value = nm;
            cmd.Parameters["@nD"].Value = nd;
            string insertSQL = "INSERT INTO [ChinaJiQiTable] ([DayGZ],[MonthGZ],[YearGZ],[DateValue],[Week],[Star],[JieQi],[NongLiMonth],[NongLiDay])VALUES(@dG,@mG,@yG,@date,@wk,@start,@jieqi,@nM,@nD)";
            cmd.CommandText = insertSQL;
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
        }       

    }
}
