using System.Collections.Generic;

namespace NovaVision.BaseClass.DataBase
{
    public class TableFieldInfo
    {
        public string FieldName;

        public DBDataType DbDataType;

        public int DataLength;

        public List<string> enumInfo;

        public bool IsNull;

        public string Comment;

        public string DefaultValue;
    }
}
