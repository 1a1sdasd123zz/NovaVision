using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace NovaVision.BaseClass.DataBase
{
    public static class DataBaseHelper
    {
        public static List<T> DataTableToList<T>(DataTable dt)
        {
            List<T> list = new List<T>();
            Type t = typeof(T);
            List<PropertyInfo> plist = new List<PropertyInfo>(typeof(T).GetProperties());
            foreach (DataRow item in dt.Rows)
            {
                T s = Activator.CreateInstance<T>();
                int i;
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    PropertyInfo info = plist.Find((PropertyInfo p) => p.Name == dt.Columns[i].ColumnName);
                    if (info != null && !Convert.IsDBNull(item[i]))
                    {
                        info.SetValue(s, item[i], null);
                    }
                }
                list.Add(s);
            }
            return list;
        }
    }
}
