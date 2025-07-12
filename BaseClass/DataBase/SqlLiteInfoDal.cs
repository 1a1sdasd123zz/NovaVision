using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;
using System.Windows.Forms;

namespace NovaVision.BaseClass.DataBase
{
    public abstract class SqliteInfoDal
    {
        public static bool Create(string filepath, string name, List<TableFieldInfo> listfield)
        {
            string filename = filepath + "\\" + name + ".db";
            SQLiteConnection.CreateFile(filename);
            string SQLitestr = "data source=" + filepath + "\\" + name + ".db";
            SQLiteConnection sQLiteCon = new SQLiteConnection(SQLitestr);
            try
            {
                sQLiteCon.Open();
                StringBuilder createSql = new StringBuilder("CREATE TABLE IF NOT EXISTS ");
                createSql.AppendFormat("{0} (", name);
                createSql.Append(" ID INTEGER PRIMARY KEY");
                foreach (TableFieldInfo field in listfield)
                {
                    createSql.Append(",");
                    createSql.AppendFormat(" {0}", field.FieldName);
                    string strDataType = EnumHelper.GetDescription(field.DbDataType);
                    createSql.AppendFormat(" {0}", strDataType);
                    switch (field.DbDataType)
                    {
                        case DBDataType.CHAR:
                        case DBDataType.VARCHAR:
                            createSql.AppendFormat("({0})", field.DataLength);
                            break;
                        case DBDataType.ENUM:
                        case DBDataType.SET:
                            createSql.Append("(");
                            foreach (string info in field.enumInfo)
                            {
                                createSql.AppendFormat("'{0}',", info);
                            }
                            createSql.Remove(createSql.Length - 1, 1);
                            createSql.Append(")");
                            break;
                    }
                    if (field.IsNull)
                    {
                        createSql.Append(" NULL");
                    }
                    else
                    {
                        createSql.Append(" NOT NULL");
                    }
                }
                createSql.Append(")");
                SQLiteCommand sQLiteCom = new SQLiteCommand(sQLiteCon);
                string strr = createSql.ToString();
                sQLiteCom.CommandText = createSql.ToString();
                sQLiteCom.ExecuteNonQuery();
                sQLiteCon.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                sQLiteCon.Close();
                return false;
            }
        }

        public static SQLiteConnection SQLConnect(string DBName)
        {
            try
            {
                string M_str_sqlcon = "Data Source=" + Application.StartupPath + "\\Project\\DataBase\\" + DBName + ".db";
                return new SQLiteConnection(M_str_sqlcon);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static int GetMaxID(string FieldName, string TableName)
        {
            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
            object obj = GetSingle(strsql, TableName);
            if (obj == null)
            {
                return 1;
            }
            return int.Parse(obj.ToString());
        }

        public static bool Exists(string SQLString, string TableName)
        {
            object obj = GetSingle(SQLString, TableName);
            if (object.Equals(obj, null) || object.Equals(obj, DBNull.Value) || int.Parse(obj.ToString()) == 0)
            {
                return false;
            }
            return true;
        }

        public static bool Exists(string SQLString, string TableName, params SQLiteParameter[] cmdParms)
        {
            object obj = GetSingle(SQLString, TableName, cmdParms);
            if (object.Equals(obj, null) || object.Equals(obj, DBNull.Value) || int.Parse(obj.ToString()) == 0)
            {
                return false;
            }
            return true;
        }

        public static int ExecuteSql(string SQLString, string TableName)
        {
            using SQLiteConnection conn = SQLConnect(TableName);
            using SQLiteCommand cmd = new SQLiteCommand(SQLString, conn);
            try
            {
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (SQLiteException E)
            {
                throw new Exception(E.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void ExecuteSql(List<string> SQLStrings, string TableName)
        {
            using SQLiteConnection conn = SQLConnect(TableName);
            conn.Open();
            try
            {
                foreach (string str in SQLStrings)
                {
                    using SQLiteCommand cmd = new SQLiteCommand(str, conn);
                    int rows = cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException E)
            {
                throw new Exception(E.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public static int ExecuteSql(string SQLString, string TableName, params SQLiteParameter[] cmdParms)
        {
            using SQLiteConnection conn = SQLConnect(TableName);
            using SQLiteCommand cmd = new SQLiteCommand();
            try
            {
                PrepareCommand(cmd, conn, null, SQLString, cmdParms);
                int rows = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return rows;
            }
            catch (SQLiteException E)
            {
                throw new Exception(E.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public static int ExecuteSql(string SQLString, string content, string TableName)
        {
            using SQLiteConnection conn = SQLConnect(TableName);
            SQLiteCommand cmd = new SQLiteCommand(SQLString, conn);
            SQLiteParameter myParameter = new SQLiteParameter("@content", DbType.String);
            myParameter.Value = content;
            cmd.Parameters.Add(myParameter);
            try
            {
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (SQLiteException E)
            {
                throw new Exception(E.Message);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }

        public static object GetSingle(string SQLString, string TableName)
        {
            using SQLiteConnection conn = SQLConnect(TableName);
            using SQLiteCommand cmd = new SQLiteCommand(SQLString, conn);
            try
            {
                conn.Open();
                object obj = cmd.ExecuteScalar();
                if (object.Equals(obj, null) || object.Equals(obj, DBNull.Value))
                {
                    return null;
                }
                return obj;
            }
            catch (SQLiteException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public static object GetSingle(string SQLString, string TableName, params SQLiteParameter[] cmdParms)
        {
            using SQLiteConnection conn = SQLConnect(TableName);
            using SQLiteCommand cmd = new SQLiteCommand();
            try
            {
                PrepareCommand(cmd, conn, null, SQLString, cmdParms);
                object obj = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                if (object.Equals(obj, null) || object.Equals(obj, DBNull.Value))
                {
                    return null;
                }
                return obj;
            }
            catch (SQLiteException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public static SQLiteDataReader ExecuteReader(string SQLString, string TableName, out SQLiteConnection conn)
        {
            conn = SQLConnect(TableName);
            SQLiteCommand cmd = new SQLiteCommand(SQLString, conn);
            try
            {
                conn.Open();
                return cmd.ExecuteReader();
            }
            catch (SQLiteException e)
            {
                throw new Exception(e.Message);
            }
        }

        public static SQLiteDataReader ExecuteReader(string SQLString, string TableName, params SQLiteParameter[] cmdParms)
        {
            using SQLiteConnection conn = SQLConnect(TableName);
            using SQLiteCommand cmd = new SQLiteCommand();
            try
            {
                PrepareCommand(cmd, conn, null, SQLString, cmdParms);
                SQLiteDataReader myReader = cmd.ExecuteReader();
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (SQLiteException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataSet Query(string SQLString, string TableName)
        {
            using SQLiteConnection conn = SQLConnect(TableName);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                SQLiteDataAdapter command = new SQLiteDataAdapter(SQLString, conn);
                command.Fill(ds, "ds");
            }
            catch (SQLiteException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public static DataSet Query(string SQLString, string TableName, params SQLiteParameter[] cmdParms)
        {
            using SQLiteConnection conn = SQLConnect(TableName);
            SQLiteCommand cmd = new SQLiteCommand();
            PrepareCommand(cmd, conn, null, SQLString, cmdParms);
            using SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds, "ds");
                cmd.Parameters.Clear();
            }
            catch (SQLiteException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public static void ExecuteSqlTran(Hashtable SQLStringList, string TableName)
        {
            using SQLiteConnection conn = SQLConnect(TableName);
            conn.Open();
            using SQLiteTransaction trans = conn.BeginTransaction();
            SQLiteCommand cmd = new SQLiteCommand();
            try
            {
                foreach (DictionaryEntry myDE in SQLStringList)
                {
                    string cmdText = myDE.Key.ToString();
                    SQLiteParameter[] cmdParms = (SQLiteParameter[])myDE.Value;
                    PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                    int val = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    trans.Commit();
                }
            }
            catch
            {
                trans.Rollback();
                throw;
            }
        }

        public static void ExecuteSqlTran(ArrayList SQLStringList, string TableName)
        {
            using SQLiteConnection conn = SQLConnect(TableName);
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = conn;
            SQLiteTransaction tx = (cmd.Transaction = conn.BeginTransaction());
            try
            {
                for (int i = 0; i < SQLStringList.Count; i++)
                {
                    string strsql = SQLStringList[i].ToString();
                    if (strsql.Trim().Length > 1)
                    {
                        cmd.CommandText = strsql;
                        cmd.ExecuteNonQuery();
                    }
                }
                tx.Commit();
            }
            catch (SQLiteException E)
            {
                tx.Rollback();
                throw new Exception(E.Message);
            }
        }

        public static int ExecuteSqlInsertImg(string strSQL, byte[] fs, string TableName)
        {
            using SQLiteConnection conn = SQLConnect(TableName);
            SQLiteCommand cmd = new SQLiteCommand(strSQL, conn);
            SQLiteParameter myParameter = new SQLiteParameter("@fs", DbType.Binary);
            myParameter.Value = fs;
            cmd.Parameters.Add(myParameter);
            try
            {
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (SQLiteException E)
            {
                throw new Exception(E.Message);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }

        private static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, SQLiteTransaction trans, string cmdText, SQLiteParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = CommandType.Text;
            if (cmdParms != null)
            {
                foreach (SQLiteParameter parm in cmdParms)
                {
                    cmd.Parameters.Add(parm);
                }
            }
        }
    }
}
