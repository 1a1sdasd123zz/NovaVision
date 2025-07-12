using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using NovaVision.BaseClass.Authority;

namespace NovaVision.BaseClass.DataBase
{
    public class SQLiteInfoDal
    {
        public bool Create(string filepath, string name, List<TableFieldInfo> listfield)
        {
            return SqliteInfoDal.Create(filepath, name, listfield);
        }

        public int GetMaxId(string tablename)
        {
            return SqliteInfoDal.GetMaxID("ID", tablename);
        }

        public bool Exists(int ID, string tablename)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + tablename);
            strSql.Append(" where ID=@ID ");
            SQLiteParameter[] parameters = new SQLiteParameter[1]
            {
            new SQLiteParameter("@ID", DbType.Int32, 8)
            };
            parameters[0].Value = ID;
            return SqliteInfoDal.Exists(strSql.ToString(), tablename, parameters);
        }

        public bool Exists(string field, string tablename)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from sqlite_master where  name ='" + tablename + "'");
            strSql.Append(" and sql like '%" + field + "%'");
            return SqliteInfoDal.Exists(strSql.ToString(), tablename);
        }

        public bool Exists(string field, string data, string tablename)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + tablename);
            strSql.Append(" where " + field + "= @" + field);
            SQLiteParameter[] parameters = new SQLiteParameter[1]
            {
            new SQLiteParameter("@" + field, DbType.String, 25)
            };
            parameters[0].Value = data;
            return SqliteInfoDal.Exists(strSql.ToString(), tablename, parameters);
        }

        public bool Exists(string tableName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from sqlite_master where type='table' and name = '" + tableName + "'");
            return SqliteInfoDal.Exists(strSql.ToString(), tableName);
        }

        public bool RenameTable(string oldTableName, string newTableName, string dbName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("alter table " + oldTableName);
            strSql.Append(" rename to " + newTableName);
            if (SqliteInfoDal.ExecuteSql(strSql.ToString(), dbName) == 0)
            {
                return true;
            }
            return false;
        }

        public bool CreateTable(string tableName, List<TableFieldInfo> listfield)
        {
            StringBuilder createSql = new StringBuilder("CREATE TABLE IF NOT EXISTS ");
            createSql.AppendFormat("{0} (", tableName);
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
            string strr = createSql.ToString();
            if (SqliteInfoDal.ExecuteSql(strr, tableName) == 0)
            {
                return true;
            }
            return false;
        }

        public bool CreateTableAndCopy(string tableName, string oldTableName, List<TableFieldInfo> listfield)
        {
            StringBuilder createSql = new StringBuilder("CREATE TABLE IF NOT EXISTS ");
            createSql.AppendFormat("{0} (", tableName);
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
            string strr = createSql.ToString();
            if (SqliteInfoDal.ExecuteSql(strr, tableName) == 0)
            {
                createSql.Clear();
                createSql.Append("INSERT INTO " + tableName + " ( ");
                for (int j = 0; j < listfield.Count - 1; j++)
                {
                    createSql.Append(listfield[j].FieldName + ", ");
                }
                createSql.Append($"{listfield[listfield.Count - 1]} ) SELECT ");
                for (int i = 0; i < listfield.Count - 1; i++)
                {
                    createSql.Append(listfield[i].FieldName + ", ");
                }
                createSql.Append($"{listfield[listfield.Count - 1]} FROM {oldTableName}");
                if (SqliteInfoDal.ExecuteSql(strr, tableName) == 0)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool DeleteTable(string DBName, string tableName, string DBpath = null)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select * from " + tableName);
            if (!string.IsNullOrWhiteSpace(DBpath))
            {
                DataSet exportDS = SqliteInfoDal.Query(str.ToString(), DBName);
                if (exportDS != null && exportDS.Tables.Count > 0)
                {
                    if (!Directory.Exists(DBpath + "\\DBbak\\"))
                    {
                        Directory.CreateDirectory(DBpath + "\\DBbak\\");
                    }
                    FileOperator.SaveCSV(exportDS.Tables[0], DBpath + "\\DBbak\\" + tableName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv", FileMode.Append);
                }
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DROP TABLE " + tableName);
            if (SqliteInfoDal.ExecuteSql(strSql.ToString(), DBName) == 0)
            {
                return true;
            }
            return false;
        }

        public bool Add(string field, string tablename)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("alter table " + tablename);
            if (field.Contains("判断结果"))
            {
                strSql.Append(" add column " + field + " VARCHAR(5) ");
            }
            else
            {
                strSql.Append(" add column " + field + " TEXT ");
            }
            if (SqliteInfoDal.ExecuteSql(strSql.ToString(), tablename) == 0)
            {
                return true;
            }
            return false;
        }

        public bool AddRange(List<string> fields, string tablename)
        {
            List<string> strSqls = new List<string>();
            foreach (string field in fields)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("alter table " + tablename);
                strSql.Append(" add column " + field + " TEXT ");
                strSqls.Add(strSql.ToString());
            }
            try
            {
                SqliteInfoDal.ExecuteSql(strSqls, tablename);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Add(UserInfo model, string tablename)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + tablename + "(");
            strSql.Append("UserName,Pwd,Authority,CreateTime)");
            strSql.Append(" values (");
            strSql.Append("@UserName,@Pwd,@Authority,@CreateTime)");
            SQLiteParameter[] parameters = new SQLiteParameter[4]
            {
            new SQLiteParameter("@UserName", DbType.String, 50),
            new SQLiteParameter("@Pwd", DbType.String, 25),
            new SQLiteParameter("@Authority", DbType.String, 25),
            new SQLiteParameter("@CreateTime", DbType.String, 50)
            };
            parameters[0].Value = model.UserName;
            parameters[1].Value = model.Pwd;
            parameters[2].Value = model.Authority;
            parameters[3].Value = model.CreateTime;
            int rows = SqliteInfoDal.ExecuteSql(strSql.ToString(), tablename, parameters);
            if (rows > 0)
            {
                return true;
            }
            return false;
        }

        public bool Add(List<string> listfield, List<string> listdata, string tablename)
        {
            string keys_string = "(" + listfield[0];
            string value_string = "('" + listdata[0] + "'";
            for (int j = 1; j < listfield.Count; j++)
            {
                keys_string = keys_string + "," + listfield[j];
            }
            for (int i = 1; i < listdata.Count; i++)
            {
                value_string = value_string + ",'" + listdata[i] + "'";
            }
            keys_string += ")";
            value_string += ")";
            string strSql = string.Format("INSERT INTO " + tablename + " {0} VALUES {1}", keys_string, value_string);
            int rows = SqliteInfoDal.ExecuteSql(strSql.ToString(), tablename);
            if (rows > 0)
            {
                return true;
            }
            return false;
        }

        public bool Update(UserInfo model, string tablename)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + tablename + " set ");
            strSql.Append("UserName=@UserName,");
            strSql.Append("Pwd=@Pwd,");
            strSql.Append("Authority=@Authority");
            strSql.Append("CreateTime=@CreateTime");
            strSql.Append(" where ID=@ID ");
            SQLiteParameter[] parameters = new SQLiteParameter[5]
            {
            new SQLiteParameter("@UserName", DbType.String, 50),
            new SQLiteParameter("@Pwd", DbType.String, 25),
            new SQLiteParameter("@Authority", DbType.String, 25),
            new SQLiteParameter("@CreateTime", DbType.String, 50),
            new SQLiteParameter("@ID", DbType.Int32, 8)
            };
            parameters[0].Value = model.UserName;
            parameters[1].Value = model.Pwd;
            parameters[2].Value = model.Authority;
            parameters[3].Value = model.CreateTime;
            parameters[4].Value = model.ID;
            int rows = SqliteInfoDal.ExecuteSql(strSql.ToString(), tablename, parameters);
            if (rows > 0)
            {
                return true;
            }
            return false;
        }

        public bool Delete(int ID, string tablename, string DBpath = null)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select * from " + tablename);
            str.Append(" where ID= " + ID);
            if (!string.IsNullOrWhiteSpace(DBpath))
            {
                DataSet exportDS = SqliteInfoDal.Query(str.ToString(), tablename);
                if (exportDS != null && exportDS.Tables.Count > 0)
                {
                    if (!Directory.Exists(DBpath + "\\DBbak\\"))
                    {
                        Directory.CreateDirectory(DBpath + "\\DBbak\\");
                    }
                    FileOperator.SaveCSV(exportDS.Tables[0], DBpath + "\\DBbak\\" + tablename + "_" + DateTime.Now.ToString("yyyyMMdd") + ".csv", FileMode.Append);
                }
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + tablename);
            strSql.Append(" where ID=@ID ");
            SQLiteParameter[] parameters = new SQLiteParameter[1]
            {
            new SQLiteParameter("@ID", DbType.Int32, 8)
            };
            parameters[0].Value = ID;
            int rows = SqliteInfoDal.ExecuteSql(strSql.ToString(), tablename, parameters);
            if (rows > 0)
            {
                return true;
            }
            return false;
        }

        public bool DeleteList(string field, string usename, string tablename)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + tablename);
            strSql.Append(" where " + field + " = '" + usename + "'");
            int rows = SqliteInfoDal.ExecuteSql(strSql.ToString(), tablename);
            if (rows > 0)
            {
                return true;
            }
            return false;
        }

        public bool DeleteList(int ID, string IDlist, string tablename)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + tablename);
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = SqliteInfoDal.ExecuteSql(strSql.ToString(), tablename);
            if (rows > 0)
            {
                return true;
            }
            return false;
        }

        public DataSet GetList(string tablename)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM " + tablename);
            return SqliteInfoDal.Query(strSql.ToString(), tablename);
        }

        public List<string> GetFieldList(string tablename)
        {
            List<string> fieldlist = new List<string>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("PRAGMA table_info( " + tablename + " )");
            SQLiteConnection conn;
            SQLiteDataReader sQLiteDataReader = SqliteInfoDal.ExecuteReader(strSql.ToString(), tablename, out conn);
            while (sQLiteDataReader.Read())
            {
                fieldlist.Add(sQLiteDataReader["Name"].ToString());
            }
            sQLiteDataReader.Close();
            conn.Close();
            conn.Dispose();
            return fieldlist;
        }

        public List<string> GetPartFieldList(string tablename, List<string> notContainCol = null, List<string> containCol = null)
        {
            List<string> fieldlist = new List<string>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("PRAGMA table_info( " + tablename + " )");
            SQLiteConnection conn;
            SQLiteDataReader sQLiteDataReader = SqliteInfoDal.ExecuteReader(strSql.ToString(), tablename, out conn);
            while (sQLiteDataReader.Read())
            {
                object sQLiteType = sQLiteDataReader["Type"];
                if (notContainCol != null && notContainCol.Count > 0 && !notContainCol.Contains(sQLiteType))
                {
                    fieldlist.Add(sQLiteDataReader["Name"].ToString());
                }
                if (containCol != null && containCol.Count > 0 && containCol.Contains(sQLiteType))
                {
                    fieldlist.Add(sQLiteDataReader["Name"].ToString());
                }
                if ((notContainCol == null || notContainCol.Count < 1) && (containCol == null || containCol.Count < 1))
                {
                    fieldlist.Add(sQLiteDataReader["Name"].ToString());
                }
            }
            sQLiteDataReader.Close();
            conn.Close();
            conn.Dispose();
            return fieldlist;
        }

        public DataSet GetList(string field, string strWhere, string tablename)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM " + tablename);
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + field + "='" + strWhere + "'");
            }
            return SqliteInfoDal.Query(strSql.ToString(), tablename);
        }

        public DataSet GetList(string field, string tablename)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select " + field);
            strSql.Append(" FROM " + tablename);
            return SqliteInfoDal.Query(strSql.ToString(), tablename);
        }

        public DataSet GetList(string field, string startime, string endtime, string tablename)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * ");
            strSql.Append(" FROM " + tablename);
            strSql.Append(" Where " + field + " between  '");
            strSql.Append(startime + " 00:00' and  '" + endtime + " 23:59'");
            return SqliteInfoDal.Query(strSql.ToString(), tablename);
        }

        public int GetRecordCount(string strWhere, string tablename)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM " + tablename);
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = SqliteInfoDal.GetSingle(strSql.ToString(), tablename);
            if (obj == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, string tablename)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from " + tablename + " T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return SqliteInfoDal.Query(strSql.ToString(), tablename);
        }

        public DataSet GetListByPage(int page, string tablename)
        {
            PageInfo pageInfo = new PageInfo();
            int startIndex = (page - 1) * pageInfo.PageSize;
            int endIndex = page * pageInfo.PageSize;
            string txtCommand1 = "select * from " + tablename + " order by ID limit " + pageInfo.PageSize + " offset " + startIndex;
            return SqliteInfoDal.Query(txtCommand1, tablename);
        }

        public DataSet GetListByPage(DataSet dst, int page, int pagesize, int pagecount)
        {
            DataSet vds = new DataSet();
            vds = dst.Clone();
            if (page > pagecount)
            {
                page = pagecount;
            }
            if (page < 1)
            {
                page = 1;
            }
            int startIndex = pagesize * (page - 1);
            int endIndex = pagesize * page;
            for (int i = startIndex; i < endIndex && i < dst.Tables[0].Rows.Count; i++)
            {
                vds.Tables[0].ImportRow(dst.Tables[0].Rows[i]);
            }
            dst.Dispose();
            return vds;
        }

        public string GetlastedData(string mzm, string tablename)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select 总结果 from {0} where 模组码='{1}' order by 时间 desc limit 0,1", tablename, mzm);
            DataSet rowData = SqliteInfoDal.Query(strSql.ToString(), tablename);
            DataTable dt = rowData.Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return rowData.Tables[0].Rows[0][0].ToString();
            }
            return "";
        }

        public DataRow GetDataByCode(string mzm, string tablename)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select * from {0} where 模组码='{1}' order by 时间 desc limit 0,1", tablename, mzm);
            DataSet rowData = SqliteInfoDal.Query(strSql.ToString(), tablename);
            DataTable dt = rowData.Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return rowData.Tables[0].Rows[0];
            }
            return null;
        }
    }
}
