using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using NovaVision.BaseClass.Authority;

namespace NovaVision.BaseClass.DataBase
{
    public class SQLiteInfoBll
    {
        private readonly SQLiteInfoDal dal = new SQLiteInfoDal();

        public bool Create(string filepath, string name, List<TableFieldInfo> listfield)
        {
            return dal.Create(filepath, name, listfield);
        }

        public int GetMaxId(string tablename)
        {
            return dal.GetMaxId(tablename);
        }

        public bool RenameTable(string oldTableName, string newTableName, string dbName)
        {
            return dal.RenameTable(oldTableName, newTableName, dbName);
        }

        public bool CreateTable(string tableName, List<TableFieldInfo> listfield)
        {
            return dal.CreateTable(tableName, listfield);
        }

        public bool CreateTableAndCopy(string tableName, string oldTableName, List<TableFieldInfo> listfield)
        {
            return dal.CreateTableAndCopy(tableName, oldTableName, listfield);
        }

        public bool DeleteTable(string DBName, string tableName, string DBpath = null)
        {
            return dal.DeleteTable(DBName, tableName, DBpath);
        }

        public bool Exists(int ID, string tablename)
        {
            return dal.Exists(ID, tablename);
        }

        public bool Exists(string field, string tablename)
        {
            return dal.Exists(field, tablename);
        }

        public bool Exists(string tableName)
        {
            return dal.Exists(tableName);
        }

        public bool Exists(string field, string data, string tablename)
        {
            return dal.Exists(field, data, tablename);
        }

        public bool Add(string field, string tablename)
        {
            return dal.Add(field, tablename);
        }

        public bool AddRange(List<string> fields, string tablename)
        {
            return dal.AddRange(fields, tablename);
        }

        public bool Add(UserInfo model, string tablename)
        {
            return dal.Add(model, tablename);
        }

        public bool Add(List<string> listfield, List<string> listdata, string tablename)
        {
            return dal.Add(listfield, listdata, tablename);
        }

        public bool Update(UserInfo model, string tablename)
        {
            return dal.Update(model, tablename);
        }

        public bool Delete(int ID, string tablename, string DBpath = null)
        {
            return dal.Delete(ID, tablename, DBpath);
        }

        public bool DeleteList(string field, string usename, string tablename)
        {
            return dal.DeleteList(field, usename, tablename);
        }

        public DataSet GetList(string tablename)
        {
            return dal.GetList(tablename);
        }

        public DataSet GetList(string tablename, string field = null, string strWhere = null, string startime = null, string endtime = null, string rangCol = null, double rangMin = 0.0, double rangMax = 0.0)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM " + tablename);
            strSql.Append(" where 1=1");
            if (!string.IsNullOrEmpty(startime) && DateTime.Parse(startime) != DateTime.MaxValue && DateTime.Parse(startime) != DateTime.MinValue && !string.IsNullOrEmpty(endtime) && DateTime.Parse(endtime) != DateTime.MaxValue && DateTime.Parse(endtime) != DateTime.MinValue)
            {
                strSql.Append(" and 时间 between '");
                strSql.Append(startime + "' and  '" + endtime + "'");
            }
            if (!string.IsNullOrWhiteSpace(field) && !string.IsNullOrWhiteSpace(strWhere))
            {
                strSql.Append(" and " + field + "='" + strWhere + "'");
            }
            if (!string.IsNullOrWhiteSpace(rangCol) && rangMin > -1.0 && rangMax > -1.0)
            {
                strSql.Append(" and " + rangCol + ">=" + rangMin + " and " + rangCol + "<=" + rangMax);
            }
            return SqliteInfoDal.Query(strSql.ToString(), tablename);
        }

        public DataSet GetListByPage(string tablename, PageInfo pageInfo, string field = null, string strWhere = null, string startime = null, string endtime = null, string rangCol = null, double rangMin = 0.0, double rangMax = 0.0)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM " + tablename);
            strSql.Append(" where 1=1");
            if (!string.IsNullOrEmpty(startime) && DateTime.Parse(startime) != DateTime.MaxValue && DateTime.Parse(startime) != DateTime.MinValue && !string.IsNullOrEmpty(endtime) && DateTime.Parse(endtime) != DateTime.MaxValue && DateTime.Parse(endtime) != DateTime.MinValue)
            {
                strSql.Append(" and 时间 between '");
                strSql.Append(startime + "' and  '" + endtime + "'");
            }
            if (!string.IsNullOrWhiteSpace(field) && !string.IsNullOrWhiteSpace(strWhere))
            {
                strSql.Append(" and " + field + "='" + strWhere + "'");
            }
            if (!string.IsNullOrWhiteSpace(rangCol) && rangMin > -1.0 && rangMax > -1.0)
            {
                strSql.Append(" and " + rangCol + ">=" + rangMin + " and " + rangCol + "<=" + rangMax);
            }
            strSql.Append(" order by id limit " + (pageInfo.CurrentPage - 1) * 40 + "," + pageInfo.PageSize);
            return SqliteInfoDal.Query(strSql.ToString(), tablename);
        }

        public int GetRecordCount(string tablename, PageInfo pageInfo, string field = null, string strWhere = null, string startime = null, string endtime = null, string rangCol = null, double rangMin = 0.0, double rangMax = 0.0)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*)");
            strSql.Append(" FROM " + tablename);
            strSql.Append(" where 1=1");
            if (!string.IsNullOrEmpty(startime) && DateTime.Parse(startime) != DateTime.MaxValue && DateTime.Parse(startime) != DateTime.MinValue && !string.IsNullOrEmpty(endtime) && DateTime.Parse(endtime) != DateTime.MaxValue && DateTime.Parse(endtime) != DateTime.MinValue)
            {
                strSql.Append(" and 时间 between '");
                strSql.Append(startime + "' and  '" + endtime + "'");
            }
            if (!string.IsNullOrWhiteSpace(field) && !string.IsNullOrWhiteSpace(strWhere))
            {
                strSql.Append(" and " + field + "='" + strWhere + "'");
            }
            if (!string.IsNullOrWhiteSpace(rangCol) && rangMin > -1.0 && rangMax > -1.0)
            {
                strSql.Append(" and " + rangCol + ">=" + rangMin + " and " + rangCol + "<=" + rangMax);
            }
            object obj = SqliteInfoDal.GetSingle(strSql.ToString(), tablename);
            if (obj == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj);
        }

        public List<string> GetFieldList(string tablename)
        {
            return dal.GetFieldList(tablename);
        }

        public List<string> GetPartFieldList(string tablename, List<string> notContainCol = null, List<string> containCol = null)
        {
            return dal.GetPartFieldList(tablename, notContainCol, containCol);
        }

        public DataSet GetList(string field, string tablename)
        {
            return dal.GetList(field, tablename);
        }

        public DataSet GetList(string field, string strWhere, string tablename)
        {
            return dal.GetList(field, strWhere, tablename);
        }

        public DataSet GetList(string field, string stratime, string endtime, string tablename)
        {
            return dal.GetList(field, stratime, endtime, tablename);
        }

        public int GetRecordCount(string strWhere, string tablename)
        {
            return dal.GetRecordCount(strWhere, tablename);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, string tablename)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex, tablename);
        }

        public DataSet GetListByPage(int page, string tablename)
        {
            return dal.GetListByPage(page, tablename);
        }

        public DataSet GetListByPage(DataSet dst, int page, int pagesize, int pagecount)
        {
            return dal.GetListByPage(dst, page, pagesize, pagecount);
        }

        public string GetlastedData(string mzm, string tablename)
        {
            return dal.GetlastedData(mzm, tablename);
        }

        public DataRow GetDataByCode(string mzm, string tablename)
        {
            return dal.GetDataByCode(mzm, tablename);
        }
    }
}
