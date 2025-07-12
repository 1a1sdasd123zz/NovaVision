using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace NovaVision.BaseClass
{
    public class ObjToTreeView
    {
        private static int leve = 1;

        private static string LeveStr = "[L1]";

        private static int flag = 0;

        private static Dictionary<string, Info> mInfo = new Dictionary<string, Info>();
        public static int Leve
        {
            get
            {
                return ObjToTreeView.leve;
            }
            set
            {
                bool flag = value != ObjToTreeView.leve;
                if (flag)
                {
                    ObjToTreeView.leve = value;
                    ObjToTreeView.LeveStr = "[L" + ObjToTreeView.leve.ToString() + "]";
                }
            }
        }

        public static void TransObjToTreeView(object obj, TreeNode fNode, Type t)
        {
            PropertyInfo[] propertyInfos = obj.GetType().GetProperties();
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                object value = propertyInfos[i].GetValue(obj);
                Type type = value.GetType();
                bool flag = typeof(IEnumerable).IsAssignableFrom(type);
                if (flag)
                {
                    IEnumerable ie = value as IEnumerable;
                    bool flag2 = ie != null;
                    if (flag2)
                    {
                        int count = 0;
                        IEnumerator list = ie.GetEnumerator();
                        while (list.MoveNext())
                        {
                            bool flag3 = list.Current.GetType().IsValueType || list.Current.GetType() == typeof(string);
                            if (flag3)
                            {
                                TreeNode node = new TreeNode();
                                node.Text = list.Current.ToString();
                                fNode.Nodes.Add(node);
                                count++;
                                ObjToTreeView.flag = 1;
                            }
                            else
                            {
                                bool flag4 = list.Current.GetType() == t;
                                if (flag4)
                                {
                                    ObjToTreeView.mInfo.Remove(ObjToTreeView.LeveStr);
                                    ObjToTreeView.Leve--;
                                    ObjToTreeView.mInfo[ObjToTreeView.LeveStr].num++;
                                    bool flag5 = ObjToTreeView.mInfo[ObjToTreeView.LeveStr].num >= ObjToTreeView.mInfo[ObjToTreeView.LeveStr].Count;
                                    if (flag5)
                                    {
                                        ObjToTreeView.mInfo.Remove(ObjToTreeView.LeveStr);
                                    }
                                    ObjToTreeView.flag = 2;
                                    return;
                                }
                                TreeNode node2 = fNode.Nodes[ObjToTreeView.mInfo[ObjToTreeView.LeveStr].num];
                                ObjToTreeView.Leve++;
                                ObjToTreeView.flag = 3;
                                ObjToTreeView.TransObjToTreeView(list.Current, node2, t);
                            }
                        }
                        bool flag6 = ObjToTreeView.flag == 2;
                        if (flag6)
                        {
                            ObjToTreeView.Leve--;
                            bool flag7 = ObjToTreeView.Leve > 0;
                            if (!flag7)
                            {
                                break;
                            }
                            ObjToTreeView.mInfo[ObjToTreeView.LeveStr].num++;
                            bool flag8 = ObjToTreeView.mInfo[ObjToTreeView.LeveStr].num >= ObjToTreeView.mInfo[ObjToTreeView.LeveStr].Count;
                            if (flag8)
                            {
                                ObjToTreeView.mInfo.Remove(ObjToTreeView.LeveStr);
                            }
                        }
                        else
                        {
                            ObjToTreeView.mInfo.Add(ObjToTreeView.LeveStr, new Info
                            {
                                Count = count,
                                num = 0
                            });
                        }
                    }
                }
            }
        }
    }
}
