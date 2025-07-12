using System;

namespace NovaVision.BaseClass.Authority
{
    [Serializable]
    public class UserInfo
    {
        private int _id;

        private string _username;

        private string _pwd;

        private string _authority;

        private string _createtime;

        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public string UserName
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
            }
        }

        public string Pwd
        {
            get
            {
                return _pwd;
            }
            set
            {
                _pwd = value;
            }
        }

        public string Authority
        {
            get
            {
                return _authority;
            }
            set
            {
                _authority = value;
            }
        }

        public string CreateTime
        {
            get
            {
                return _createtime;
            }
            set
            {
                _createtime = value;
            }
        }
    }
}
