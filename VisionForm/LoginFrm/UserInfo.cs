using System;

namespace NovaVision.VisionForm.LoginFrm
{
    public class UserInfo
    {
        public long ID { get; set; }

        public string UserName { get; set; }

        public string Pwd { get; set; }

        public string Authority { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
