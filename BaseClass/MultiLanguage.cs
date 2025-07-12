using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using NovaVision.UserControlLibrary;

namespace NovaVision.BaseClass
{
    public static class MultiLanguage
    {
        public static string DefaultLanguage = "Chinese";

        private static XDocument xDocument = new XDocument();

        private static List<string> ListMenu = new List<string>();

        private static Dictionary<string, ToolStripMenuItem> DicMenu = new Dictionary<string, ToolStripMenuItem>();

        private static string defaultPath = Application.StartupPath + "\\Languages\\Lang.xml";

        public static string GetDefaultLanguage()
        {
            DefaultLanguage = ConfigurationManager.AppSettings["Language"];
            LoadXML();
            return DefaultLanguage;
        }

        private static void LoadXML()
        {
            try
            {
                Hashtable hashResult = new Hashtable();
                XmlReader reader = null;
                if (!new FileInfo(defaultPath).Exists)
                {
                    MessageBox.Show(@"语言配置文件不存在", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                reader = new XmlTextReader(defaultPath);
                xDocument = XDocument.Load(defaultPath);
                reader.Close();
            }
            catch (Exception ex)
            {
                LogUtil.LogError("读取语言配置文件出错，详细信息：" + ex.Message);
                MessageBox.Show("读取语言配置文件出错，详细信息：" + ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private static void EnumerateMenu(ToolStripMenuItem item)
        {
            try
            {
                foreach (ToolStripMenuItem subItem in item.DropDownItems)
                {
                    ListMenu.Add(subItem.Name);
                    DicMenu.Add(subItem.Name, subItem);
                    EnumerateMenu(subItem);
                }
            }
            catch
            {
            }
        }

        public static bool LoadLanguage(Form form, string language)
        {
            bool flag = form == null || form.IsDisposed;
            bool result2;
            if (flag)
            {
                result2 = false;
            }
            else
            {
                bool flag2 = string.IsNullOrEmpty(language);
                if (flag2)
                {
                    result2 = false;
                }
                else
                {
                    Hashtable hashText = MultiLanguage.ReadXMLText(form.Name, MultiLanguage.GetDefaultLanguage());
                    Hashtable hashHeaderText = MultiLanguage.ReadXMLHeaderText(form.Name, MultiLanguage.GetDefaultLanguage());
                    Hashtable hashTabPage = MultiLanguage.ReadXMLTabPageText(form.Name, MultiLanguage.GetDefaultLanguage());
                    IEnumerable<XElement> lstXElement = MultiLanguage.ReadXMLComboBoxItems(form.Name, MultiLanguage.GetDefaultLanguage());
                    bool flag3 = hashText == null;
                    if (flag3)
                    {
                        result2 = false;
                    }
                    else
                    {
                        Control.ControlCollection sonControls = form.Controls;
                        try
                        {
                            MultiLanguage.DicMenu.Clear();
                            MultiLanguage.ListMenu.Clear();
                            MenuStrip menu = form.MainMenuStrip;
                            bool flag4 = menu != null;
                            if (flag4)
                            {
                                foreach (object obj in menu.Items)
                                {
                                    ToolStripMenuItem item = (ToolStripMenuItem)obj;
                                    MultiLanguage.ListMenu.Add(item.Name);
                                    MultiLanguage.DicMenu.Add(item.Name, item);
                                    MultiLanguage.EnumerateMenu(item);
                                }
                            }
                            IOrderedEnumerable<KeyValuePair<string, ToolStripMenuItem>> result = MultiLanguage.DicMenu.OrderBy(delegate (KeyValuePair<string, ToolStripMenuItem> pair)
                            {
                                KeyValuePair<string, ToolStripMenuItem> keyValuePair = pair;
                                return keyValuePair.Key;
                            });
                            foreach (KeyValuePair<string, ToolStripMenuItem> pair2 in result)
                            {
                                bool flag5 = hashText.Contains(pair2.Key);
                                if (flag5)
                                {
                                    pair2.Value.Text = (string)hashText[pair2.Key];
                                }
                            }
                            foreach (object obj2 in sonControls)
                            {
                                Control control = (Control)obj2;
                                bool flag6 = control.GetType() == typeof(Panel);
                                if (flag6)
                                {
                                    MultiLanguage.GetSetSubControls(control.Controls, hashText, hashHeaderText, hashTabPage, lstXElement);
                                }
                                else
                                {
                                    bool flag7 = control.GetType() == typeof(GroupBox);
                                    if (flag7)
                                    {
                                        MultiLanguage.GetSetSubControls(control.Controls, hashText, hashHeaderText, hashTabPage, lstXElement);
                                    }
                                    else
                                    {
                                        bool flag8 = control.GetType() == typeof(TabControl);
                                        if (flag8)
                                        {
                                            MultiLanguage.GetSetSubControls(control.Controls, hashText, hashHeaderText, hashTabPage, lstXElement);
                                        }
                                        else
                                        {
                                            bool flag9 = control.GetType() == typeof(TabPage);
                                            if (flag9)
                                            {
                                                MultiLanguage.GetSetSubControls(control.Controls, hashText, hashHeaderText, hashTabPage, lstXElement);
                                            }
                                            else
                                            {
                                                bool flag10 = control.GetType() == typeof(TableLayoutPanel);
                                                if (flag10)
                                                {
                                                    MultiLanguage.GetSetSubControls(control.Controls, hashText, hashHeaderText, hashTabPage, lstXElement);
                                                }
                                                else
                                                {
                                                    bool flag11 = control.GetType() == typeof(DataGridView);
                                                    if (flag11)
                                                    {
                                                        MultiLanguage.GetSetHeaderCell((DataGridView)control, hashHeaderText);
                                                    }
                                                    else
                                                    {
                                                        bool flag12 = control.GetType() == typeof(Button);
                                                        if (flag12)
                                                        {
                                                            MultiLanguage.GetSetSubControls(control.Controls, hashText, hashHeaderText, hashTabPage, lstXElement);
                                                        }
                                                        else
                                                        {
                                                            bool flag13 = control.GetType() == typeof(ToolStrip);
                                                            if (flag13)
                                                            {
                                                                ToolStrip toolStrip = control as ToolStrip;
                                                                foreach (object item2 in toolStrip.Items)
                                                                {
                                                                    bool flag14 = item2 is ToolStripButton;
                                                                    if (flag14)
                                                                    {
                                                                        ToolStripButton toolStripButton = item2 as ToolStripButton;
                                                                        toolStripButton.ToolTipText = (string)hashText[toolStripButton.Name];
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                bool flag15 = control.GetType() == typeof(StatusStrip);
                                                                if (flag15)
                                                                {
                                                                    StatusStrip toolStrip2 = control as StatusStrip;
                                                                    foreach (object item3 in toolStrip2.Items)
                                                                    {
                                                                        bool flag16 = item3 is ToolStripLabel;
                                                                        if (flag16)
                                                                        {
                                                                            ToolStripLabel toolStripLabel = item3 as ToolStripLabel;
                                                                            string txt = (string)hashText[toolStripLabel.Name];
                                                                            bool flag17 = !string.IsNullOrWhiteSpace(txt);
                                                                            if (flag17)
                                                                            {
                                                                                toolStripLabel.Text = txt;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            bool flag18 = item3 is ToolStripDropDownButton;
                                                                            if (flag18)
                                                                            {
                                                                                ToolStripDropDownButton toolStripDropDownButton = item3 as ToolStripDropDownButton;
                                                                                toolStripDropDownButton.Text = (string)hashText[toolStripDropDownButton.Name];
                                                                                bool flag19 = toolStripDropDownButton.DropDownItems != null && toolStripDropDownButton.DropDownItems.Count > 0;
                                                                                if (flag19)
                                                                                {
                                                                                    foreach (object dropItems in toolStripDropDownButton.DropDownItems)
                                                                                    {
                                                                                        ToolStripItem toolStripItem = dropItems as ToolStripItem;
                                                                                        toolStripItem.Text = (string)hashText[toolStripItem.Name];
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    bool flag20 = control.GetType() == typeof(PathCtrl);
                                                                    if (flag20)
                                                                    {
                                                                        PathCtrl pathCtrl = control as PathCtrl;
                                                                        pathCtrl.Label_Text = (string)hashText[pathCtrl.Name];
                                                                    }
                                                                    else
                                                                    {
                                                                        bool flag21 = control.GetType() == typeof(SplitContainer);
                                                                        if (flag21)
                                                                        {
                                                                            MultiLanguage.GetSetSubControls(control.Controls, hashText, hashHeaderText, hashTabPage, lstXElement);
                                                                        }
                                                                        else
                                                                        {
                                                                            bool flag22 = control.GetType() == typeof(TableLayoutPanelEx);
                                                                            if (flag22)
                                                                            {
                                                                                MultiLanguage.GetSetSubControls(control.Controls, hashText, hashHeaderText, hashTabPage, lstXElement);
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                bool flag23 = hashText.Contains(control.Name);
                                if (flag23)
                                {
                                    control.Text = (string)hashText[control.Name];
                                }
                            }
                            bool flag24 = hashText.Contains(form.Name);
                            if (flag24)
                            {
                                form.Text = (string)hashText[form.Name];
                            }
                        }
                        catch (Exception ex)
                        {
                            string s = ex.ToString();
                            LogUtil.LogError("根据当前语言，替换窗体控件的值出错，详细信息：" + ex.Message + ",窗体名：" + ((form != null) ? form.ToString() : null));
                            return false;
                        }
                        result2 = true;
                    }
                }
            }
            return result2;
        }


        private static void GetSetSubControls(Control.ControlCollection controls, Hashtable hashText, Hashtable hashHeaderText, Hashtable hashTabPage, IEnumerable<XElement> lstXElement)
        {
            try
            {
                foreach (Control control in controls)
                {
                    if (control.GetType() == typeof(Panel))
                    {
                        GetSetSubControls(control.Controls, hashText, hashHeaderText, hashTabPage, lstXElement);
                    }
                    else if (control.GetType() == typeof(GroupBox))
                    {
                        GetSetSubControls(control.Controls, hashText, hashHeaderText, hashTabPage, lstXElement);
                    }
                    else if (control.GetType() == typeof(TabControl))
                    {
                        GetSetTabPageValue((TabControl)control, hashTabPage);
                        GetSetSubControls(control.Controls, hashText, hashHeaderText, hashTabPage, lstXElement);
                    }
                    else if (control.GetType() == typeof(TabPage))
                    {
                        GetSetSubControls(control.Controls, hashText, hashHeaderText, hashTabPage, lstXElement);
                    }
                    else if (control.GetType() == typeof(TableLayoutPanel))
                    {
                        GetSetSubControls(control.Controls, hashText, hashHeaderText, hashTabPage, lstXElement);
                    }
                    else if (control.GetType() == typeof(DataGridView))
                    {
                        GetSetHeaderCell((DataGridView)control, hashHeaderText);
                    }
                    else if (control.GetType() == typeof(Button))
                    {
                        GetSetSubControls(control.Controls, hashText, hashHeaderText, hashTabPage, lstXElement);
                    }
                    else if (control.GetType() == typeof(ToolStrip))
                    {
                        ToolStrip toolStrip = control as ToolStrip;
                        foreach (object item in toolStrip.Items)
                        {
                            if (item is ToolStripButton)
                            {
                                ToolStripButton toolStripButton = item as ToolStripButton;
                                toolStripButton.ToolTipText = (string)hashText[toolStripButton.Name];
                            }
                        }
                    }
                    else if (control.GetType() == typeof(SplitterPanel))
                    {
                        GetSetSubControls(control.Controls, hashText, hashHeaderText, hashTabPage, lstXElement);
                    }
                    else if (control.GetType() == typeof(ComboBox))
                    {
                        GetSetComboBoxItems((ComboBox)control, lstXElement);
                    }
                    else if (control.GetType() == typeof(SplitContainer))
                    {
                        GetSetSubControls(control.Controls, hashText, hashHeaderText, hashTabPage, lstXElement);
                    }
                    else if (control.GetType() == typeof(TableLayoutPanelEx))
                    {
                        GetSetSubControls(control.Controls, hashText, hashHeaderText, hashTabPage, lstXElement);
                    }
                    if (hashText.Contains(control.Name))
                    {
                        control.Text = (string)hashText[control.Name];
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.LogError(" 获取并设置控件中的子控件出错，详细信息：" + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        private static Hashtable ReadXMLText(string frmName, string language)
        {
            try
            {
                Hashtable hashResult = new Hashtable();
                List<XElement> classData = (from n in xDocument.Descendants("Form")
                                            where n.Attribute("Name").Value == frmName
                                            select n).ToList();
                foreach (XElement item in classData.Elements("Controls"))
                {
                    XElement xe = item;
                    XAttribute xName = xe.Attribute("name");
                    XAttribute xText = xe.Attribute(language + "text");
                    string name = xName.Value;
                    string text = xText.Value;
                    if (name != "" && text != "")
                    {
                        hashResult.Add(name, text);
                    }
                }
                return hashResult;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                LogUtil.LogError("获取一般控件Controls出错，详细信息：" + ex.Message + ",窗体名：" + frmName);
                return null;
            }
        }

        private static Hashtable ReadXMLHeaderText(string frmName, string language)
        {
            try
            {
                Hashtable hashResult = new Hashtable();
                List<XElement> classData = (from n in xDocument.Descendants("Form")
                                            where n.Attribute("Name").Value == frmName
                                            select n).ToList();
                foreach (XElement item in classData.Elements("Datagridview"))
                {
                    XElement xe = item;
                    XAttribute xName = xe.Attribute("name");
                    XAttribute xText = xe.Attribute(language + "text");
                    string name = xName.Value;
                    string text = xText.Value;
                    if (name != "" && text != "")
                    {
                        hashResult.Add(name, text);
                    }
                }
                return hashResult;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                LogUtil.LogError("获取一般datagridview控件出错，详细信息：" + ex.Message + ",窗体名：" + frmName);
                return null;
            }
        }

        private static Hashtable ReadXMLTabPageText(string frmName, string language)
        {
            try
            {
                Hashtable hashResult = new Hashtable();
                List<XElement> classData = (from n in xDocument.Descendants("Form")
                                            where n.Attribute("Name").Value == frmName
                                            select n).ToList();
                foreach (XElement item in classData.Elements("TabPage"))
                {
                    XElement xe = item;
                    XAttribute xName = xe.Attribute("name");
                    XAttribute xText = xe.Attribute(language + "text");
                    string name = xName.Value;
                    string text = xText.Value;
                    if (name != "" && text != "")
                    {
                        hashResult.Add(name, text);
                    }
                }
                return hashResult;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                LogUtil.LogError("获取tabcontrol控件出错，详细信息：" + ex.Message + ",窗体名：" + frmName);
                return null;
            }
        }

        private static IEnumerable<XElement> ReadXMLComboBoxItems(string algType, string language)
        {
            try
            {
                return (from n in xDocument.Descendants("Form")
                        where n.Attribute("Name").Value == algType
                        select n).ToList();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                LogUtil.LogError("获取combobox控件出错，详细信息：" + ex.Message);
                return null;
            }
        }

        private static Hashtable ReadXMLParameterText(string frmName, string language, ParameterMode parameterMode)
        {
            try
            {
                Hashtable hashResult = new Hashtable();
                List<XElement> classData = (from n in xDocument.Descendants("Form")
                                            where n.Attribute("Name").Value == frmName
                                            select n).ToList();
                foreach (XElement item in classData.Elements("OutputParams"))
                {
                    XElement xe = item;
                    XAttribute xName = xe.Attribute("name");
                    XAttribute xText = xe.Attribute(language + "text");
                    string name = xName.Value;
                    string text = xText.Value;
                    if (name != "" && text != "")
                    {
                        hashResult.Add(name, text);
                    }
                }
                return hashResult;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                LogUtil.LogError("获取OutputParams出错，详细信息：" + ex.Message + ",窗体名：" + frmName);
                return null;
            }
        }

        private static void GetSetHeaderCell(DataGridView dataGridView, Hashtable hashHeaderText)
        {
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                if (hashHeaderText.Contains(column.Name))
                {
                    column.HeaderText = (string)hashHeaderText[column.Name];
                }
            }
        }

        private static void GetSetComboBoxItems(ComboBox comboBox, IEnumerable<XElement> xElements)
        {
            Hashtable hashtable = new Hashtable();
            List<string> lstItems = new List<string>();
            foreach (XElement item in xElements.Elements("ComboBox"))
            {
                XElement xe = item;
                XAttribute xName = xe.Attribute("name");
                if (!(xName.Value == comboBox.Name))
                {
                    continue;
                }
                IEnumerable<XElement> nodeSecond = xe.Elements();
                foreach (XElement node in nodeSecond)
                {
                    XAttribute subName = node.Attribute("name");
                    XAttribute subText = node.Attribute(DefaultLanguage + "text");
                    string name = subName.Value;
                    string text = subText.Value;
                    if (name != "" && text != "")
                    {
                        hashtable.Add(name, text);
                    }
                }
                foreach (object cmbItems in comboBox.Items)
                {
                    lstItems.Add(cmbItems.ToString());
                }
                int index = comboBox.SelectedIndex;
                foreach (string lstsubItems in lstItems)
                {
                    if (hashtable.Contains(lstsubItems))
                    {
                        comboBox.Items.Remove(lstsubItems);
                        comboBox.Items.Add((string)hashtable[lstsubItems]);
                    }
                }
                comboBox.SelectedIndex = index;
                break;
            }
        }

        private static void GetSetTabPageValue(TabControl tabControl, Hashtable hashtable)
        {
            foreach (TabPage item in tabControl.TabPages)
            {
                if (hashtable.Contains(item.Name))
                {
                    item.Text = (string)hashtable[item.Name];
                }
            }
        }
    }
}
